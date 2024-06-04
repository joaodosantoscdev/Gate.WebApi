using Gate.Application.DTOs.Request;
using Gate.Domain.Models;
using AutoMapper;
using Gate.Application.DTOs.Response;

namespace Gate.Application.Profiles
{
    public class MapperProfile : Profile
  {
    public MapperProfile()
    {
      //ACCESS
      CreateMap<AddAccessRequest, AccessInfo>().ReverseMap();
      CreateMap<UpdateAccessRequest, AccessInfo>().ReverseMap();
      CreateMap<AccessInfo, AccessResponse>()
        .ForPath(dest => dest.UnitDescription, src => src.MapFrom(src => src.Place.Unit.Description))
        .ForPath(dest => dest.ComplexDescription, src => src.MapFrom(src => src.Place.Unit.Complex.Description));

      //COMPLEX
      CreateMap<AddComplexRequest, ComplexInfo>().ReverseMap();
      
      //USER
      CreateMap<RegisterUserRequest, UserInfo>().ReverseMap();

      // RESIDENT
      CreateMap<AddResidentRequest, ResidentInfo>().ReverseMap();
      CreateMap<UpdateResidentRequest, ResidentInfo>().ReverseMap();
      CreateMap<ResidentResponse, ResidentInfo>().ReverseMap();

      // UNIT
      CreateMap<AddUnitRequest, UnitInfo>().ReverseMap();
      CreateMap<UpdateUnitRequest, UnitInfo>().ReverseMap();

      //PLACE
      CreateMap<AddPlaceRequest, PlaceInfo>().ReverseMap();
      CreateMap<UpdatePlaceRequest, PlaceInfo>().ReverseMap();
      CreateMap<PlaceResponse, PlaceInfo>().ReverseMap();


      //CONTACT
      CreateMap<AddContactRequest, ContactInfo>().ReverseMap();
      CreateMap<ContactResponse, ContactInfo>().ReverseMap();
    }
  }
}