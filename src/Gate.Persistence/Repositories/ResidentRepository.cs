using Gate.Domain.Models;
using Gate.Persistence.Context;
using Gate.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gate.Persistence.Repositories
{
    public class ResidentRepository : BaseRepository<ResidentInfo>, IResidentRepository
    {
        private readonly AppDbContext _dataContext;
        public ResidentRepository(AppDbContext dataContext) : base(dataContext) 
        { 
            _dataContext = dataContext;
        }

        public async Task<List<ResidentInfo>> GetAllResidentsWithContact()
        {
            var residentInfoList = await _dataContext.Residents
                .Include(x => x.Contacts)
                .ToListAsync();

            return residentInfoList;
        }

        public async Task<ResidentInfo> GetFullById(int id)
        {
            var residentInfo = await _dataContext.Residents
                .Include(x => x.Contacts)
                .Include(x => x.Accesses.OrderByDescending(x => x.Id).Take(1))
                .Include(x => x.Places)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return residentInfo;
        }

        public async Task<ResidentInfo> GetResidentByDocumentNumber(string documentNumber)
        {
            var residentInfo = await _dataContext.Residents
                .Where(x => x.DocumentNumber == documentNumber)
                .FirstOrDefaultAsync();
                
            return residentInfo;
        }
    }
}