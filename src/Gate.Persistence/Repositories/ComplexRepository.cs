using Gate.Domain.Models;
using Gate.Persistence.Context;
using Gate.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gate.Persistence.Repositories
{
    public class ComplexRepository : BaseRepository<ComplexInfo>, IComplexRepository
    {
        private readonly AppDbContext _dataContext;
        public ComplexRepository(AppDbContext dataContext) : base(dataContext) 
        {
            _dataContext = dataContext;
        }

        public async Task<List<ComplexInfo>> GetAllFullAsync()
        {
            IQueryable<ComplexInfo> query = _dataContext.Complexes.Include(c => c.Unities).OrderBy(c => c.Id);
            return query.ToList();
        }
    }
}