using Gate.Domain.Models;
using MySqlConnector;

namespace Gate.Application.BusinessLogic.Interfaces
{
    public interface IUnity
    {
        UnityInfo GetById(int id);
        UnityInfo GetByTaxId(string taxId);
        UnityInfo Post(UnityInfo unity);
        UnityInfo Post(UnityInfo unity, MySqlTransaction trans);
    }
}