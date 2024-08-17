using AutoMapper;
using Backend.src.DTO;
using Backend.src.Entity;

public class MapperProfile : Profile
{
    public MapperProfile()
    {

        //CreateMap<Category, CategoryReadDto>().ReserveMap();
        CreateMap<Category, CategoryReadDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>()
                   .ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));



        CreateMap<Product, ProductReadDto>();
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>()
                   .ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));


        CreateMap<User, UserReadDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>()
                   .ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));


        // OrderDetail mappings
        CreateMap<OrderDetail, OrderDetailReadDto>();
        CreateMap<OrderDetailCreateDto, OrderDetail>();

        // Order mappings
        CreateMap<Order, OrderReadDto>();
        CreateMap<OrderCreateDto, Order>()
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));

    }
}