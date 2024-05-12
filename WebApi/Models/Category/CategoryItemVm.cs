namespace WebApi.Models.Category;

public class CategoryItemVm {
	public long Id { get; set; }
	public string Name { get; set; } = null!;
	public string Image { get; set; } = null!;
	public string Description { get; set; } = null!;
}
