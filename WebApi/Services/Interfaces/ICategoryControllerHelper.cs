using WebApi.Models.Category;

namespace WebApi.Services.Interfaces {
	public interface ICategoryControllerHelper {
		Task AddCategoryAsync(CategoryCreateVm model);
		Task UpdateCategoryAsync(CategoryUpdateVm model);
	}
}