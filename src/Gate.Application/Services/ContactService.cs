using Gate.Domain.Models;
using Gate.Persistence.Repositories.Interfaces;

namespace Gate.Application.Services
{
    public class ContactService : BaseService<ContactInfo>, IContactService
    {
        public ContactService(IContactRepository contactRepository) : base(contactRepository) { }
    }
}