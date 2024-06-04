using Gate.Domain.Models;
using Gate.Persistence.Repositories.Interfaces;

namespace Gate.Application.Services
{
    public class ComplexService : BaseService<ComplexInfo>, IComplexService
    {
        private readonly IComplexRepository _complexRepository;
        public ComplexService(IComplexRepository complexRepository) : base(complexRepository) 
        { 
            _complexRepository = complexRepository;
        }

        public async Task<List<ComplexInfo>> GetAllFullAsync()
        {
            try
            {
                var complexes = await _complexRepository.GetAllFullAsync();
                if (complexes == null) {
                    return null;
                }

                return complexes.ToList();
            }
            catch (Exception ex)
            {
                 throw new Exception(ex.Message);
            }
        }        
    }
}