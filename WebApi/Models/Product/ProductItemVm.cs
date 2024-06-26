﻿using WebApi.Models.Category;

namespace WebApi.Models.Product;

public class ProductItemVm {
	public long Id { get; set; }
	public DateTime DateCreated { get; set; }
	public string Name { get; set; } = null!;
	public string Description { get; set; } = null!;
	public double Price { get; set; }
	public CategoryItemVm Category { get; set; } = null!;
	public ICollection<string> Images { get; set; } = null!;
}
