using System.Buffers.Text;
using Gate.Persistence.DataAccess.Interfaces;
using Gate.backend.Utils.Settings;
using Gate.Domain.Models;
using MySqlConnector;

namespace Gate.Persistence.DataAccess
{
  public class SecurityDal : ISecurityDal
  {
    private static readonly string cmdSqlGetByUserGuid = "SELECT * FROM $USUKEY WHERE KEY_USER_GUID = @guid;";
    private static readonly string cmdSqlPost = "INSERT INTO $USUKEY VALUES(@guid, @key);";

    public SecurityDal() { }

    public KeyInfo GetByUserGuid(Guid guid)
    {
      var keyInfo = new KeyInfo();
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();

        MySqlCommand command = new MySqlCommand(cmdSqlGetByUserGuid, con);
        command.Parameters.AddWithValue("@guid", guid);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            keyInfo = new KeyInfo()
            {
              Guid = (string)dr["USU_KEY_GUID"],
              Key = (string)dr["KEY_CONTENT"]
            };
          }
        }
        con.Close();
      }
      return keyInfo;
    }
    
    public KeyInfo Post(KeyInfo keyInfo)
    {
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();
        MySqlCommand command = new MySqlCommand(cmdSqlPost, con);
        command.Parameters.AddWithValue("@guid", keyInfo.Guid);
        command.Parameters.AddWithValue("@key", keyInfo.Key);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            keyInfo = new KeyInfo()
            {
              Guid = (string)dr["USU_IDENTI"],
              Key = (string)dr["USU_DESCRI"], 
            };
          }
        }
        con.Close();
      }
      return keyInfo;
    }
  }

}