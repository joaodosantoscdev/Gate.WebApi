using Gate.Domain.Models;
using Gate.Persistence.Context;
using Gate.Persistence.Repositories.Interfaces;

namespace Gate.Persistence.Repositories
{
    public class UnitRepository : BaseRepository<UnitInfo>, IUnitRepository
    {
        public UnitRepository(AppDbContext dataContext) : base(dataContext) { }
    }
}