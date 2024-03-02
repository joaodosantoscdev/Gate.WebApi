using Gate.Application.DTOs.Request;
using Gate.Domain.Models;
using AutoMapper;

namespace Gate.Application.Profiles 
{
  public class MapperProfile : Profile
  {
    public MapperProfile()
    {
      CreateMap<RegisterUserRequest, UserInfo>().ReverseMap();
      CreateMap<RegisterUserRequest, EmployeeInfo>().ReverseMap();
    }
  }
}