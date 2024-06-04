using Gate.Domain.Models;
using Gate.Persistence.Context;
using Gate.Persistence.Repositories.Interfaces;

namespace Gate.Persistence.Repositories
{
    public class PlaceRepository : BaseRepository<PlaceInfo>, IPlaceRepository
    {
        public PlaceRepository(AppDbContext dataContext) : base(dataContext) { }
    }
}