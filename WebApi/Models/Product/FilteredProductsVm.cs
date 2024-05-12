namespace WebApi.Models.Product;

public class FilteredProductsVm {
	public ICollection<ProductItemVm> FilteredProducts { get; set; } = null!;
	public long AvailableQuantity { get; set; }
}
