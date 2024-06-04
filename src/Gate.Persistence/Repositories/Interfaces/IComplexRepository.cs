using Gate.Domain.Models;

namespace Gate.Persistence.Repositories.Interfaces
{
    public interface IComplexRepository : IBaseRepository<ComplexInfo> {
        Task<List<ComplexInfo>> GetAllFullAsync();
    }
}