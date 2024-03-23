using Gate.Application.Services.Interfaces;
using Gate.Domain.Models;
using Gate.Persistence.Repositories.Interfaces;

namespace Gate.Application.Services
{
    public class ComplexService : BaseService<ComplexInfo>, IComplexService
    {
        public ComplexService(IComplexRepository complexRepository) : base(complexRepository) { }
    }
}