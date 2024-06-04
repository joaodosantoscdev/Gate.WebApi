using Gate.Domain.Models;
using Gate.Persistence.Repositories.Interfaces;

namespace Gate.Application.Services
{
    public class UnitService : BaseService<UnitInfo>, IUnitService
    {
        public UnitService(IUnitRepository unitRepository) : base(unitRepository) { }
    }
}