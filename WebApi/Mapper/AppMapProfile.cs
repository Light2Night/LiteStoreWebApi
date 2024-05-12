using AutoMapper;
using Data.Entities;
using Data.Entities.Identity;
using WebApi.Models.Account;
using WebApi.Models.Areas;
using WebApi.Models.Basket;
using WebApi.Models.Category;
using WebApi.Models.Order;
using WebApi.Models.PostOffice;
using WebApi.Models.Product;
using WebApi.Models.Settlement;
using WebApi.Seeders.PostOfficeHelpers.Response;
using WebApi.Seeders.PostOfficeHelpers.ViewModels;

namespace WebApi.Mapper;

public class AppMapProfile : Profile {
	public AppMapProfile() {
		CreateMap<AreaData, AreaVm>();
		CreateMap<SettlementData, SettlementVm>();
		CreateMap<PostOfficeData, PostOfficeVm>();



		CreateMap<Category, CategoryItemVm>();
		CreateMap<CategoryCreateVm, Category>();

		CreateMap<RegisterVm, User>();

		CreateMap<ProductCreateVm, Product>()
			.ForMember(p => p.Images, opt => opt.Ignore());
		CreateMap<ProductPutVm, Product>()
			.ForMember(p => p.Images, opt => opt.Ignore());
		CreateMap<Product, ProductItemVm>()
			.ForMember(
				dest => dest.Images,
				opt => opt.MapFrom(
					src => src.Images
						.Select(i => i.Name)
						.ToArray()
				)
			);

		CreateMap<BasketProduct, BasketItemVm>();
		CreateMap<BasketProduct, OrderedProduct>()
			.ForMember(
				dest => dest.UnitPrice,
				opt => opt.MapFrom(src => src.Product.Price)
			);

		CreateMap<OrderStatus, OrderStatusItemVm>();
		CreateMap<Order, OrderItemVm>();
		CreateMap<User, CustomerInfoVm>();
		CreateMap<Order, CustomerOrderItemVm>()
			.ForMember(
				dest => dest.Customer,
				opt => opt.MapFrom(
					src => src.User
				)
			);
		CreateMap<OrderedProduct, OrderedProductItemVm>();

		CreateMap<CreateAreaVm, Area>();
		CreateMap<Area, AreaItemVm>();

		CreateMap<CreateSettlementVm, Settlement>();
		CreateMap<Settlement, SettlementItemVm>();

		CreateMap<CreatePostOfficeVm, PostOffice>();
		CreateMap<PostOffice, PostOfficeItemVm>();
	}
}
