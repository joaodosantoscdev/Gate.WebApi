using Gate.Domain.Models;
using Gate.Persistence.Context;
using Gate.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gate.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity 
    {
        protected readonly AppDbContext Context;

        public BaseRepository(AppDbContext dataContext) =>
            Context = dataContext;

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await Context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();

        public virtual async Task<TEntity?> GetByIdAsync(int id) =>
            await Context.Set<TEntity>().FindAsync(id);

        public virtual async Task<object> AddAsync(TEntity obj)
        {
            Context.Add(obj);
            await Context.SaveChangesAsync();
            return obj.Id;
        }

        public virtual async Task UpdateAsync(TEntity obj)
        {
            Context.Entry(obj).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }
        
        public virtual async Task DeleteAsync(TEntity obj)
        {
            Context.Set<TEntity>().Remove(obj);
            await Context.SaveChangesAsync();
        }

        public virtual async Task DeleteByIdAsync(int id)
        {
            var obj = await GetByIdAsync(id);
            if (obj == null)
                throw new Exception("O registro não existe na base de dados.");
            await DeleteAsync(obj);
        }

        public void Dispose() => Context.Dispose();
    }
}