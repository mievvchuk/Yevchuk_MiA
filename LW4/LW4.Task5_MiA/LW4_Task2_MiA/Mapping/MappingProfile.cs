using AutoMapper;
using LW4_Task2_MiA.Models;
using LW4_Task4_MiA.DTO;

namespace LW4_Task4_MiA.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type.ToString())).ReverseMap();
            CreateMap<Recipe, RecipeDto>().ForMember(d => d.Difficulty, opt => opt.MapFrom(s => s.Difficulty.ToString())).ReverseMap();
            CreateMap<Rating, RatingDto>().ReverseMap();
            CreateMap<User, UserDto>().ForMember(d => d.Role, opt => opt.MapFrom(s => s.Role.ToString())).ReverseMap();
        }
    }
}
