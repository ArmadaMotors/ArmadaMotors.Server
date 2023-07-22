using ArmadaMotors.Domain.Entities;
using ArmadaMotors.Service.DTOs.Products;
using ArmadaMotors.Service.DTOs.Users;
using AutoMapper;

namespace ArmadaMotors.Service.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserForCreationDto>().ReverseMap();
            CreateMap<UserForResultDto, User>().ReverseMap();
            
            CreateMap<Product, ProductForCreationDto>().ReverseMap();
            CreateMap<Product, ProductForUpdateDto>().ReverseMap();
            CreateMap<Category, CategoryForCreationDto>().ReverseMap();
            
            CreateMap<Feedback, FeedbackForCreationDto>().ReverseMap();
            CreateMap<Order, OrderForCreationDto>().ReverseMap();
        }
    }
}
