using AutoMapper;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.Category;
using WebApi.Services.Interfaces;

namespace WebApi.Services;

public class CategoryControllerHelper(
	DataContext context,
	IMapper mapper
	) : ICategoryControllerHelper {

	public async Task AddCategoryAsync(CategoryCreateVm model) {
		var category = mapper.Map<Category>(model);
		category.Image = await ImageWorker.SaveImageAsync(model.Image);

		try {
			await context.Categories.AddAsync(category);
			await context.SaveChangesAsync();
		}
		catch (Exception) {
			ImageWorker.DeleteImageIfExists(category.Image);
			throw;
		}
	}

	public async Task UpdateCategoryAsync(CategoryUpdateVm model) {
		var category = await context.Categories
			.FirstAsync(c => c.Id == model.Id);

		string oldImagePath = category.Image;
		string? newImagePath = null;

		if (model.Image is not null) {
			category.Image = newImagePath = await ImageWorker.SaveImageAsync(model.Image);
		}

		try {
			category.Name = model.Name ?? category.Name;
			category.Description = model.Description ?? category.Description;

			context.Update(category);
			await context.SaveChangesAsync();

			if (newImagePath is not null)
				ImageWorker.DeleteImageIfExists(oldImagePath);
		}
		catch (Exception) {
			if (newImagePath is not null)
				ImageWorker.DeleteImageIfExists(newImagePath);

			throw;
		}
	}
}
