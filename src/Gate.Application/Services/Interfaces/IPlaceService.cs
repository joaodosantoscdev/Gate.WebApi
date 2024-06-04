using Gate.Application.DTOs.Response;
using Gate.Application.Services.Interfaces;
using Gate.Domain.Models;

namespace Gate.Application.Services
{
    public interface IPlaceService : IBaseService<PlaceInfo> {
        Task<List<PlaceResponse>> GetAllAsync();
    }
}