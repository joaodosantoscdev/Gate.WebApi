using Gate.Application.DTOs.Response;
using Gate.Domain.Models;

namespace Gate.Application.Services.Interfaces
{
    public interface IResidentService : IBaseService<ResidentInfo> {
        Task<ResidentInfo> GetResidentByDocumentNumberAsync(string documentNumber);

        Task<ResidentResponse> GetFullById(int id);

        Task<List<ResidentInfo>> GetAllResidentsWithContact();
    }
}