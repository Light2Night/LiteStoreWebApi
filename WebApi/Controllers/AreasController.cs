using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Context;
using Data.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Areas;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AreasController(
	DataContext context,
	IMapper mapper,
	IValidator<CreateAreaVm> createValidator,
	IValidator<UpdateAreaVm> updateValidator
	) : ControllerBase {

	[HttpGet]
	[Authorize(Roles = "Admin,User")]
	public async Task<IActionResult> GetAll() {
		var areas = await context.Areas
			.ProjectTo<AreaItemVm>(mapper.ConfigurationProvider)
			.ToArrayAsync();

		return Ok(areas);
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Create([FromForm] CreateAreaVm model) {
		var validationResult = await createValidator.ValidateAsync(model);

		if (!validationResult.IsValid)
			return BadRequest(validationResult.Errors.First().ErrorMessage);

		var area = mapper.Map<Area>(model);
		await context.Areas.AddAsync(area);
		await context.SaveChangesAsync();

		return Ok();
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Update(UpdateAreaVm model) {
		var validationResult = await updateValidator.ValidateAsync(model);

		if (!validationResult.IsValid)
			return BadRequest(validationResult.Errors.First().ErrorMessage);

		var area = await context.Areas.FirstAsync(a => a.Id == model.Id);

		area.Name = model.Name;
		await context.SaveChangesAsync();

		return Ok();
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Delete(long id) {
		var area = await context.Areas
			.FirstOrDefaultAsync(a => a.Id == id);

		if (area is null)
			return Ok();

		context.Areas.Remove(area);
		await context.SaveChangesAsync();

		return Ok();
	}
}
