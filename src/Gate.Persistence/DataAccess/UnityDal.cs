using Microsoft.Extensions.Configuration;
using Gate.Domain.Models;
using Gate.backend.Utils.Settings;
using MySqlConnector;
using Gate.Persistence.DataAccess.Interfaces;

namespace Gate.Persistence.DataAccess
{
  public class UnityDal : IUnityDal
  {
    string cmdSqlGetNextId =  "SELECT IF(UNI_IDENTI, MAX(UNI_IDENTI + 1), 1) as ID FROM UNITIES;"; 
    public string cmdSqlGetAll = "SELECT * FROM UNITIES;";
    public string cmdSqlGetById = "SELECT * FROM UNITIES WHERE UNI_IDENTI = @searchedId;";
    public string cmdSqlGetByTaxId = "SELECT * FROM UNITIES WHERE UNI_CNPJ = @taxId;";
    public string cmdSqlPost = "INSERT INTO UNITIES VALUES(@id, @taxId, (IF(@active, 1, 0)), @companyId, (SELECT NOW() FROM DUAL));";
    public string cmdSqlUpdate = "UPDATE UNITIES SET UNI_CNPJ = @taxId, UNI_ACTIVE = (IF(@active, 1, 0)) WHERE UNI_IDENTI = @searchedId;";
    public string cmdSqlDelete = "DELETE FROM UNITIES WHERE UNI_IDENTI = @searchedId";
    private IConfigurationRoot _connectionString; 

    public UnityDal() 
    {
      _connectionString = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
    }

    public int GetNextId(MySqlTransaction trans) {
      MySqlCommand command = new MySqlCommand(cmdSqlGetNextId, trans.Connection, trans);
      var id = 0;

      using(MySqlDataReader dr = command.ExecuteReader()) 
        while (dr.Read()) 
          id = dr.GetInt32(0);

      return id;
    }

    public List<UnityInfo> GetAll()
    {
      var unityList = new List<UnityInfo>();
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();
        MySqlCommand command = new MySqlCommand(cmdSqlGetAll, con);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            var unity = new UnityInfo()
            {
              Id = (int)dr["UNI_IDENTI"],
              TaxId = (string)dr["UNI_CNPJ"],
              CompanyId = (int)dr["UNI_COM_IDENTI"], 
              Active = (int)dr["UNI_ACTIVE"] == 1 ? true : false,
              CreationDate = (DateTime)dr["UNI_DATCRI"]
            };
            unityList.Add(unity);
          }
        }
        con.Close();
      }

      return unityList;
    }

    public UnityInfo GetById(int id)
    {
      var unity = new UnityInfo();
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();

        MySqlCommand command = new MySqlCommand(cmdSqlGetById, con);
        command.Parameters.AddWithValue("@id", id);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            unity = new UnityInfo()
            {
              Id = (int)dr["UNI_IDENTI"],
              TaxId = (string)dr["UNI_CNPJ"],
              CompanyId = (int)dr["UNI_COM_IDENTI"], 
              Active = (int)dr["UNI_ACTIVE"] == 1 ? true : false,
              CreationDate = (DateTime)dr["UNI_DATCRI"]
            };
          }
        }
        con.Close();
      }
      return unity;
    }

    public UnityInfo GetByTaxId(string taxId)
    {
      var unity = new UnityInfo();
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();

        MySqlCommand command = new MySqlCommand(cmdSqlGetByTaxId, con);
        command.Parameters.AddWithValue("@taxId", taxId);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            unity = new UnityInfo()
            {
              Id = (int)dr["UNI_IDENTI"],
              TaxId = (string)dr["UNI_CNPJ"],
              CompanyId = (int)dr["UNI_COM_IDENTI"], 
              Active = (int)dr["UNI_ACTIVE"] == 1 ? true : false,
              CreationDate = (DateTime)dr["UNI_DATCRI"]
            };
          }
        }
        con.Close();
      }
      return unity;
    }

    public UnityInfo Post(UnityInfo unity)
    {
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();
        MySqlCommand command = new MySqlCommand(cmdSqlPost, con);
        command.Parameters.AddWithValue("@id", unity.Id);
        command.Parameters.AddWithValue("@taxId", unity.TaxId);
        command.Parameters.AddWithValue("@companyId", unity.CompanyId);
        command.Parameters.AddWithValue("@active", unity.Active);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            unity = new UnityInfo()
            {
              Id = (int)dr["UNI_IDENTI"],
              TaxId = (string)dr["UNI_CNPJ"], 
              CompanyId = (int)dr["UNI_COM_IDENTI"], 
              Active = (int)dr["UNI_ACTIVE"] == 1 ? true : false,
              CreationDate = (DateTime)dr["UNI_DATCRI"]
            };
          }
        }
        con.Close();
      }
      return unity;
    }

    public UnityInfo Post(UnityInfo unity, MySqlTransaction trans)
    {
      MySqlCommand command = new MySqlCommand(cmdSqlPost, trans.Connection, trans);
      var id = GetNextId(trans);
      command.Parameters.AddWithValue("@id", id);
      command.Parameters.AddWithValue("@taxId", unity.TaxId);
      command.Parameters.AddWithValue("@companyId", unity.CompanyId);
      command.Parameters.AddWithValue("@active", unity.Active);
      command.ExecuteNonQuery();

      command = new MySqlCommand(cmdSqlGetById, trans.Connection, trans);
      command.Parameters.AddWithValue("@searchedId", id);
      using(MySqlDataReader dr = command.ExecuteReader()) 
      {
        while (dr.Read()) 
        {
          unity = new UnityInfo()
          {
            Id = (int)dr["UNI_IDENTI"],
            TaxId = (string)dr["UNI_CNPJ"], 
            CompanyId = (int)dr["UNI_COM_IDENTI"], 
            Active = (int)dr["UNI_ACTIVE"] == 1 ? true : false,
            CreationDate = (DateTime)dr["UNI_DATCRI"]
          };
        }
      }

      return unity;
    }

    public UnityInfo Update(UnityInfo unity)
    {
       using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();
        MySqlCommand command = new MySqlCommand(cmdSqlUpdate, con);
        var updatedUnity = GetById(unity.Id);

        command.Parameters.AddWithValue("@taxId", unity.TaxId);
        command.Parameters.AddWithValue("@active", unity.Active);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            unity = new UnityInfo()
            {
              Id = (int)dr["UNI_IDENTI"],
              TaxId = (string)dr["UNI_CNPJ"],
              CompanyId = (int)dr["UNI_COM_IDENTI"], 
              Active = (int)dr["UNI_ACTIVE"] == 1 ? true : false,
              CreationDate = (DateTime)dr["UNI_DATCRI"]
            };
          }
        }
        con.Close();
      }
      return unity;
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