﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Context;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Category;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CategoriesController(
	DataContext context,
	IMapper mapper,
	IValidator<CategoryCreateVm> createValidator,
	IValidator<CategoryUpdateVm> updateValidator,
	ICategoryControllerHelper helper
	) : ControllerBase {

	[HttpGet]
	public async Task<IActionResult> GetAll() {
		var list = await context.Categories
			.Where(c => !c.IsDeleted)
			.ProjectTo<CategoryItemVm>(mapper.ConfigurationProvider)
			.ToListAsync();

		return Ok(list);
	}

	[HttpGet]
	public async Task<IActionResult> GetFiltered([FromQuery] CategoryFilterVm filter) {
		var categories = context.Categories
			.OrderBy(c => c.Id)
			.Where(c => !c.IsDeleted);

		if (filter.Name is not null) {
			categories = categories
				.Where(c => c.Name.ToLower().Contains(filter.Name.ToLower()));
		}

		long availableCategories = categories.Count();

		if (filter.Offset is not null) {
			categories = categories
				.Skip((int)filter.Offset);
		}

		if (filter.Limit is not null) {
			categories = categories
				.Take((int)filter.Limit);
		}

		var list = await categories
			.ProjectTo<CategoryItemVm>(mapper.ConfigurationProvider)
			.ToListAsync();

		return Ok(new FilteredCategoriesVm {
			FilteredCategories = list,
			AvailableCategories = availableCategories
		});
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Create([FromForm] CategoryCreateVm model) {
		var validationResult = await createValidator.ValidateAsync(model);

		if (!validationResult.IsValid)
			return BadRequest(validationResult.Errors.First().ErrorMessage);

		try {
			await helper.AddCategoryAsync(model);
		}
		catch (Exception) {
			return StatusCode(StatusCodes.Status500InternalServerError);
		}
		return Ok();
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Update([FromForm] CategoryUpdateVm model) {
		var validationResult = await updateValidator.ValidateAsync(model);

		if (!validationResult.IsValid)
			return BadRequest(validationResult.Errors.First().ErrorMessage);

		try {
			await helper.UpdateCategoryAsync(model);
		}
		catch (Exception) {
			return StatusCode(StatusCodes.Status500InternalServerError);
		}
		return Ok();
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Delete(int id) {
		var category = await context.Categories
			.Where(c => c.Id == id)
			.FirstOrDefaultAsync();

		if (category is not null) {
			category.IsDeleted = true;

			await context.SaveChangesAsync();
		}

		return Ok();
	}
}