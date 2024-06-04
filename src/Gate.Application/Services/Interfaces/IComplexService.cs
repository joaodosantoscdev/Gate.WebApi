using Gate.Application.Services.Interfaces;
using Gate.Domain.Models;

namespace Gate.Application.Services
{
    public interface IComplexService : IBaseService<ComplexInfo> 
    {
        Task<List<ComplexInfo>> GetAllFullAsync();
    }
}