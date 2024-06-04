using AutoMapper;
using Gate.Application.DTOs.Response;
using Gate.Domain.Models;
using Gate.Persistence.Repositories.Interfaces;

namespace Gate.Application.Services
{
    public class PlaceService : BaseService<PlaceInfo>, IPlaceService
    {
        private readonly IMapper _mapper;
        public PlaceService(IPlaceRepository placeRepository, IMapper mapper) : base(placeRepository) 
        {
            _mapper = mapper; 
        }

        public async Task<List<PlaceResponse>> GetAllAsync()
        {
            var placeInfoList = await base.GetAllAsync();
            var response = _mapper.Map<List<PlaceResponse>>(placeInfoList);     
            return response;
        }
    }
}