namespace WebApi.Models.Product;

public class ProductCreateVm {
	public string Name { get; set; } = null!;

	public ICollection<IFormFile> Images { get; set; } = null!;

	public string Description { get; set; } = null!;

	public double Price { get; set; }

	public long CategoryId { get; set; }
}