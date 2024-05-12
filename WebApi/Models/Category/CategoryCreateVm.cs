namespace WebApi.Models.Category;

public class CategoryCreateVm {
	public string Name { get; set; } = null!;
	public IFormFile Image { get; set; } = null!;
	public string Description { get; set; } = null!;
}
