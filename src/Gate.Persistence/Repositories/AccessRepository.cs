using Gate.Domain.Models;
using Gate.Persistence.Context;
using Gate.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gate.Persistence.Repositories
{
    public class AccessRepository : BaseRepository<AccessInfo>, IAccessRepository
    {
        private readonly AppDbContext _dataContext;
        public AccessRepository(AppDbContext dataContext) : base(dataContext) 
        {
            _dataContext = dataContext;
        }

        public async Task<List<AccessInfo>> GetAllFullAsync()
        {
            var accessInfo = await _dataContext.Access
                .Include(a => a.Place)
                .Include(a => a.Place.Unit)
                .Include(a => a.Place.Unit.Complex)
                .Include(a => a.Resident)
                .OrderByDescending(a => a.Date)
                .ToListAsync();

            return accessInfo;
        }

        public async Task<AccessInfo> GetFullByIdAsync(int id)
        {
            var accessInfo = await _dataContext.Access
                .Include(a => a.Place)
                .Include(a => a.Place.Unit)
                .Include(a => a.Place.Unit.Complex)
                .Include(a => a.Resident)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();

            return accessInfo;
        }

        public async Task<AccessInfo> GetLastAccessByDocumentNumber(string documentNumber)
        {
            var accessInfo = await _dataContext.Access
                .Include(a => a.Place)
                .Include(a => a.Resident)
                .Include(a => a.Resident.Contacts)
                .Where(a => a.Resident.DocumentNumber == documentNumber)
                .OrderByDescending(a => a.Date)
                .FirstOrDefaultAsync();

            return accessInfo;
        }
    }
}