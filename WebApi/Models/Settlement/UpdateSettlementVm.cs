namespace WebApi.Models.Settlement;

public class UpdateSettlementVm {
	public long Id { get; set; }
	public string Name { get; set; } = null!;
	public long AreaId { get; set; }
}