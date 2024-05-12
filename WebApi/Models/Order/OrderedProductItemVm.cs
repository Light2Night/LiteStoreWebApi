using WebApi.Models.Product;

namespace WebApi.Models.Order;

public class OrderedProductItemVm {
	public long Id { get; set; }

	public ProductItemVm Product { get; set; } = null!;

	public double UnitPrice { get; set; }

	public int Quantity { get; set; }
}
