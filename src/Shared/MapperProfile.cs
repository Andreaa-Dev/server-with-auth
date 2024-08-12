using AutoMapper;
using Backend.src.DTO;
using Backend.src.Entity;

public class MapperProfile : Profile
{
    public MapperProfile()
    {

        CreateMap<Category, CategoryReadDto>();
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>()
                   .ForAllMembers(opts => opts.Condition((src, dest, srcProperty) => srcProperty != null));


    }
}