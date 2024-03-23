using Gate.Domain.Models;

namespace Gate.Persistence.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<object> AddAsync(TEntity objeto);
        Task UpdateAsync(TEntity objeto);
        Task DeleteAsync(TEntity objeto);
        Task DeleteByIdAsync(int id);
    }
}
