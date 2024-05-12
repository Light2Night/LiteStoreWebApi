namespace WebApi.Models.Settlement;

public class CreateSettlementVm {
	public string Name { get; set; } = null!;
	public long AreaId { get; set; }
}
