using WebApi.Models.PostOffice;

namespace WebApi.Models.Order;

public class OrderItemVm {
	public long Id { get; set; }

	public DateTime TimeOfCreation { get; set; }

	public OrderStatusItemVm Status { get; set; } = null!;

	public PostOfficeItemVm PostOffice { get; set; } = null!;

	public ICollection<OrderedProductItemVm> OrderedProducts { get; set; } = null!;
}