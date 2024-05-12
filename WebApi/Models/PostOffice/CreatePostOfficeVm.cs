namespace WebApi.Models.PostOffice;

public class CreatePostOfficeVm {
	public string Name { get; set; } = null!;

	public float Longitude { get; set; }

	public float Latitude { get; set; }

	public long SettlementId { get; set; }
}
