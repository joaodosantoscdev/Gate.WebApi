using Gate.Domain.Models;
using MySqlConnector;

namespace Gate.Persistence.DataAccess.Interfaces
{
    public interface IUserDal
    {
        MySqlConnection OpenConnection();
        List<UserInfo> GetAll();
        UserInfo GetById(int id);
        UserInfo Post(UserInfo user);
        UserInfo Post(UserInfo user, MySqlTransaction trans);
        UserInfo Update(UserInfo user);
        bool Delete(int id);
    }
}