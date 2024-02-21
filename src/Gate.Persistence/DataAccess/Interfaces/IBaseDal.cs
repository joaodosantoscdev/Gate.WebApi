using System.Transactions;
using MySqlConnector;

namespace Gate.Persistence.DataAccess.Interfaces
{
    public interface IBaseDal
    {
        MySqlTransaction BeginTransaction();
        void Commit();
    }
}