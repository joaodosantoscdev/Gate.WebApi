using Gate.Application.Services.Interfaces;
using Gate.Domain.Models;
using Gate.Persistence.Repositories.Interfaces;

namespace Gate.Application.Services
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : Entity
    {
        private readonly IBaseRepository<TEntity> _repositoryBase;

        public BaseService(IBaseRepository<TEntity> repositoryBase) =>
            _repositoryBase = repositoryBase;

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await _repositoryBase.GetAllAsync();

        public virtual async Task<TEntity?> GetByIdAsync(int id) =>
            await _repositoryBase.GetByIdAsync(id);

        public virtual async Task<object> AddAsync(TEntity objeto) =>
            await _repositoryBase.AddAsync(objeto);

        public virtual async Task UpdateAsync(TEntity objeto) =>
            await _repositoryBase.UpdateAsync(objeto);
        
        public virtual async Task DeleteAsync(TEntity objeto) =>
            await _repositoryBase.DeleteAsync(objeto);

        public virtual async Task DeleteByIdAsync(int id) =>
            await _repositoryBase.DeleteByIdAsync(id);

        public void Dispose() => _repositoryBase.Dispose();
    }
}