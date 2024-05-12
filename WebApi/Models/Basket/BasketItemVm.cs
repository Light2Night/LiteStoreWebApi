using WebApi.Models.Product;

namespace WebApi.Models.Basket;

public class BasketItemVm {
	public long Id { get; set; }

	public ProductItemVm Product { get; set; } = null!;

	public int Quantity { get; set; }
}
