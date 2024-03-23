using Gate.Domain.Models;
using Gate.Persistence.Context;
using Gate.Persistence.Repositories.Interfaces;

namespace Gate.Persistence.Repositories
{
    public class ComplexRepository : BaseRepository<ComplexInfo>, IComplexRepository
    {
        public ComplexRepository(AppDbContext dataContext) : base(dataContext) { }
    }
}