using Gate.Domain.Models;

namespace Gate.Persistence.Repositories.Interfaces
{
    public interface IAccessRepository : IBaseRepository<AccessInfo> 
    {
        Task<List<AccessInfo>> GetAllFullAsync();
        Task<AccessInfo> GetFullByIdAsync(int id);
        Task<AccessInfo> GetLastAccessByDocumentNumber(string documentNumber);
    }
}