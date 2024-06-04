using Gate.Application.DTOs.Request.Access;
using Gate.Application.DTOs.Response;
using Gate.Application.Services.Interfaces;
using Gate.Domain.Models;

namespace Gate.Application.Services
{
    public interface IAccessService : IBaseService<AccessInfo> 
    {
        Task<List<AccessResponse>> GetAllFullAsync();
        Task<AccessResponse> GetFullByIdAsync(int id);
        Task<AccessResponse> GetLastAccessByDocumentNumber(string documentNumber);
        Task<object> InsertFastAccess(FastAccessRequest fastAccess);
    }
}