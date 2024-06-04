using Gate.Domain.Models;
using Gate.Persistence.Context;
using Gate.Persistence.Repositories.Interfaces;

namespace Gate.Persistence.Repositories
{
    public class ContactRepository : BaseRepository<ContactInfo>, IContactRepository
    {
        public ContactRepository(AppDbContext dataContext) : base(dataContext) { }
    }
}