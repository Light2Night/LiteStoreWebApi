﻿namespace WebApi.Models.PostOffice;

public class PostOfficeItemVm {
	public long Id { get; set; }

	public string Name { get; set; } = null!;

	public float Longitude { get; set; }

	public float Latitude { get; set; }
}