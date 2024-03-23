using Gate.Domain.Models;

namespace Gate.Application.Services.Interfaces
{
    public interface IBaseService<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<object> AddAsync(TEntity objeto);
        Task UpdateAsync(TEntity objeto);
        Task DeleteAsync(TEntity objeto);
        Task DeleteByIdAsync(int id);
    }
}