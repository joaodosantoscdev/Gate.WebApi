using Gate.Persistence.DataAccess.Interfaces;
using Gate.backend.Utils.Settings;
using MySqlConnector;

namespace Gate.Persistence.DataAccess
{
  public class BaseDal : IBaseDal
  {
    private  MySqlConnection connection;
    private MySqlTransaction trans;

    public BaseDal() {}

    public MySqlTransaction BeginTransaction()
    {
      connection = new MySqlConnection(AppSettingsAcessor.GetConnectionString());
      connection.Open();
      trans = connection.BeginTransaction();
      
      return trans; 
    }

    public void Commit() {
      trans.Commit();
      connection.Close();
    }
  }
}