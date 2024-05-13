using AutoMapper;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Constants;
using WebApi.Models.Account;
using WebApi.Services;
using WebApi.Services.Interfaces;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController(
	UserManager<User> userManager,
	IJwtTokenService jwtTokenService,
	IMapper mapper,
	IConfiguration configuration
	) : ControllerBase {

	[HttpPost]
	public async Task<IActionResult> Login([FromForm] LoginVm model) {
		User? user = await userManager.FindByEmailAsync(model.Email);

		if (user is null || !await userManager.CheckPasswordAsync(user, model.Password))
			return BadRequest("Wrong authentication data");

		return Ok(new { Token = await jwtTokenService.CreateTokenAsync(user) });
	}

	[HttpPost]
	public async Task<IActionResult> Registration([FromForm] RegisterVm model) {
		if (await userManager.FindByEmailAsync(model.Email) is not null)
			return BadRequest("The user with this email is already registered");

		User user = mapper.Map<RegisterVm, User>(model);

		try {
			user.Photo = await ImageWorker.SaveImageAsync(model.Image);
		}
		catch (Exception) {
			return BadRequest("Image error");
		}

		try {
			IdentityResult identityResult = await userManager.CreateAsync(user, model.Password);
			if (!identityResult.Succeeded)
				return BadRequest(identityResult.Errors.First().Description);

			identityResult = await userManager.AddToRoleAsync(user, Roles.User);

			if (!identityResult.Succeeded) {
				await userManager.DeleteAsync(user);
				ImageWorker.DeleteImageIfExists(user.Photo);
				return BadRequest("Role assignment error");
			}

			return Ok(new { Token = await jwtTokenService.CreateTokenAsync(user) });
		}
		catch {
			ImageWorker.DeleteImageIfExists(user.Photo);
			throw;
		}
	}

	[HttpPost]
	public async Task<IActionResult> GoogleSingIn([FromForm] GoogleSingInVm model) {
		Payload payload = await ValidateAsync(
			model.Credential,
			new ValidationSettings {
				Audience = [configuration["Authentication:Google:ClientId"]]
			}
		);

		var user = await userManager.FindByEmailAsync(payload.Email);

		if (user is null) {
			using var httpClient = new HttpClient();

			user = new User {
				FirstName = payload.GivenName,
				LastName = payload.FamilyName,
				Email = payload.Email,
				UserName = payload.Email,
				Photo = await ImageWorker.SaveImageAsync(await httpClient.GetByteArrayAsync(payload.Picture))
			};

			IdentityResult identityResult = await userManager.CreateAsync(user);
			if (!identityResult.Succeeded) {
				ImageWorker.DeleteImage(user.Photo);
				return BadRequest(identityResult.Errors);
			}

			await userManager.AddToRoleAsync(user, Roles.User);
		}

		return Ok(new { Token = await jwtTokenService.CreateTokenAsync(user) });
	}
}
