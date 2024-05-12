namespace WebApi.Models.Order;

public class FilteredOrdersVm {
	public ICollection<OrderItemVm> FilteredOrders { get; set; } = null!;
	public long AvailableQuantity { get; set; }
}