using Gate.Domain.Models;

namespace Gate.Persistence.Repositories.Interfaces
{
    public interface IResidentRepository : IBaseRepository<ResidentInfo> 
    {
        Task<ResidentInfo> GetResidentByDocumentNumber(string documentNumber);   
        Task<ResidentInfo> GetFullById(int id);   

        Task<List<ResidentInfo>> GetAllResidentsWithContact(); 
    }
}