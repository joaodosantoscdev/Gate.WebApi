using Gate.Domain.Models;

namespace Gate.Persistence.DataAccess.Interfaces
{
    public interface ISecurityDal
    {
        KeyInfo GetByUserGuid(Guid guid);
        KeyInfo Post(KeyInfo keyInfo);
    }
}