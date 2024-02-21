using Gate.Domain.Models;
using MySqlConnector;

namespace Gate.Persistence.DataAccess.Interfaces
{
    public interface IUnityDal
    {
        List<UnityInfo> GetAll();
        UnityInfo GetById(int id);
        public UnityInfo GetByTaxId(string taxId);
        UnityInfo Post(UnityInfo unity);
        UnityInfo Post(UnityInfo unity, MySqlTransaction trans);
        UnityInfo Update(UnityInfo unity);
        bool Delete(int id);
    }
}