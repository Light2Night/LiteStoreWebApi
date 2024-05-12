namespace WebApi.Models.Order;

public class FilteredCustomersOrdersVm {
	public ICollection<CustomerOrderItemVm> FilteredOrders { get; set; } = null!;
	public long AvailableQuantity { get; set; }
}