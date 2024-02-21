using Gate.Persistence.DataAccess.Interfaces;
using Gate.backend.Utils.Settings;
using MySqlConnector;
using Gate.Domain.Models;

namespace Gate.Persistence.DataAccess
{
  public class UserDal : IUserDal
  {
    private MySqlConnection? _connection;
    private static readonly string cmdSqlGetNextId =  "SELECT IF(USU_IDENTI, MAX(USU_IDENTI + 1), 1) as ID FROM USERS;"; 
    private static readonly string cmdSqlGetAll = "SELECT * FROM USERS;";
    private static readonly string cmdSqlGetById = "SELECT * FROM USERS WHERE USU_IDENTI = @searchedId;";
    private static readonly string cmdSqlPost = "INSERT INTO USERS VALUES(@id, @description, @email, (IF(@active, 1, 0)), @password, (IF(@isAdmin, 1, 0)), (SELECT NOW() FROM DUAL), @companyId, @apiKey, @username);";
    private static readonly string cmdSqlUpdate = "UPDATE USERS SET USU_DESCRI = @description, USU_EMAIL = @email, USU_ACTIVE = (IF(@active, 1, 0)), USU_PASSWD = @password, USU_ISADM = (IF(@isAdmin, 1, 0)), USU_APIKEY = @apiKey WHERE USU_IDENTI = @searchedId;";
    private static readonly string cmdSqlDelete = "DELETE FROM USERS WHERE USU_IDENTI = @searchedId";

    public UserDal() { }

    private int GetNextId(MySqlTransaction trans) {
      MySqlCommand command = new MySqlCommand(cmdSqlGetNextId, trans.Connection, trans);
      var id = 0;

      using(MySqlDataReader dr = command.ExecuteReader()) 
        while (dr.Read()) 
          id = dr.GetInt32(0);

      return id;
    }

    public MySqlConnection OpenConnection()
    {
      _connection = new MySqlConnection(AppSettingsAcessor.GetConnectionString());
      _connection.Open();
      
      return _connection; 
    }

    public List<UserInfo> GetAll()
    {
      var userList = new List<UserInfo>();
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();
        MySqlCommand command = new MySqlCommand(cmdSqlGetAll, con);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            var user = new UserInfo()
            {
              Id = (int)dr["USU_IDENTI"],
              Description = (string)dr["USU_DESCRI"], 
              IsAdmin = (int)dr["USU_ISADM"] == 1 ? true : false,
              Active = (int)dr["USU_ACTIVE"] == 1 ? true : false,
              Email = (string)dr["USU_EMAIL"],
              Password = (string)dr["USU_PASSWD"],
              CreationDate = (DateTime)dr["USU_DATCRI"]
            };
            userList.Add(user);
          }
        }
        con.Close();
      }
      return userList;
    }

    public UserInfo GetById(int id)
    {
      var user = new UserInfo();
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();

        MySqlCommand command = new MySqlCommand(cmdSqlGetById, con);
        command.Parameters.AddWithValue("@id", id);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            user = new UserInfo()
            {
              Id = (int)dr["USU_IDENTI"],
              Description = (string)dr["USU_DESCRI"], 
              IsAdmin = (int)dr["USU_ISADM"] == 1 ? true : false,
              Active = (int)dr["USU_ACTIVE"] == 1 ? true : false,
              Email = (string)dr["USU_EMAIL"],
              CreationDate = (DateTime)dr["USU_DATCRI"]
            };
          }
        }
        con.Close();
      }
      return user;
    }
    
    public UserInfo Post(UserInfo user)
    {
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();
        MySqlCommand command = new MySqlCommand(cmdSqlPost, con);
        command.Parameters.AddWithValue("@id", user.Id);
        command.Parameters.AddWithValue("@description", user.Description);
        command.Parameters.AddWithValue("@email", user.Email);
        command.Parameters.AddWithValue("@active", user.Active);
        command.Parameters.AddWithValue("@password", user.Password);
        command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            user = new UserInfo()
            {
              Id = (int)dr["USU_IDENTI"],
              Description = (string)dr["USU_DESCRI"], 
              IsAdmin = (int)dr["USU_ISADM"] == 1 ? true : false,
              Active = (int)dr["USU_ACTIVE"] == 1 ? true : false,
              Email = (string)dr["USU_EMAIL"],
              Password = (string)dr["USU_PASSWD"],
              CreationDate = (DateTime)dr["USU_DATCRI"]
            };
          }
        }
        con.Close();
      }
      return user;
    }

    public UserInfo Post(UserInfo user, MySqlTransaction trans)
    {
      MySqlCommand command = new MySqlCommand(cmdSqlPost, trans.Connection, trans);
      var id = GetNextId(trans);
      command.Parameters.AddWithValue("@id", id);
      command.Parameters.AddWithValue("@description", user.Description);
      command.Parameters.AddWithValue("@username", user.Username);
      command.Parameters.AddWithValue("@email", user.Email);
      command.Parameters.AddWithValue("@active", user.Active);
      command.Parameters.AddWithValue("@password", user.Password);
      command.Parameters.AddWithValue("@isAdmin", user.IsAdmin);
      command.Parameters.AddWithValue("@companyId", user.CompanyId);
      command.ExecuteNonQuery();

      command = new MySqlCommand(cmdSqlGetById, trans.Connection, trans);
      command.Parameters.AddWithValue("@searchedId", id);
      using(MySqlDataReader dr = command.ExecuteReader()) 
      {
        while (dr.Read()) 
        {
          user.Id = (int)dr["USU_IDENTI"];
          user.Description = (string)dr["USU_DESCRI"];
          user.Username = (string)dr["USU_USERNAM"];
          user.IsAdmin = (int)dr["USU_ISADM"] == 1 ? true : false;
          user.Active = (int)dr["USU_ACTIVE"] == 1 ? true : false;
          user.Email = (string)dr["USU_EMAIL"];
          user.Password = (string)dr["USU_PASSWD"];
          user.CompanyId = (int)dr["USU_EMP_IDENTI"];
          user.CreationDate = (DateTime)dr["USU_DATCRI"];
        }
      }

      return user;
    }

    public UserInfo Update(UserInfo user)
    {
       using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();
        MySqlCommand command = new MySqlCommand(cmdSqlUpdate, con);
        var updatedUser = GetById(user.Id);

        command.Parameters.AddWithValue("@searchedId", user.Id);
        command.Parameters.AddWithValue("@description", user.Description);
        command.Parameters.AddWithValue("@email", user.Email);
        command.Parameters.AddWithValue("@active",  user.Active != null ? user.Active : updatedUser.Active);
        command.Parameters.AddWithValue("@password", user.Password);
        command.Parameters.AddWithValue("@isAdmin", user.IsAdmin != null ? user.IsAdmin : updatedUser.IsAdmin);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            user = new UserInfo()
            {
              Id = (int)dr["USU_IDENTI"],
              Description = (string)dr["USU_DESCRI"],
              IsAdmin = (int)dr["USU_ISADM"] == 1 ? true : false,
              Active = (int)dr["USU_ACTIVE"] == 1 ? true : false,
              Email = (string)dr["USU_EMAIL"],
              Password = (string)dr["USU_PASSWD"],
              CreationDate = (DateTime)dr["USU_DATCRI"],
            };
          }
        }
        con.Close();
      }
      return user;
    }

    public bool Delete(int id)
    {
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();
        MySqlCommand command = new MySqlCommand(cmdSqlDelete, con);

        command.Parameters.AddWithValue("@searchedId", id);
        var result = command.ExecuteNonQuery();
        con.Close();
         
        if (result == 1)
          return true;
      };
      return false;
    }
  }

}