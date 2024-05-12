namespace WebApi.Models.Category;

public class FilteredCategoriesVm {
	public ICollection<CategoryItemVm> FilteredCategories { get; set; } = null!;
	public long AvailableCategories { get; set; }
}
