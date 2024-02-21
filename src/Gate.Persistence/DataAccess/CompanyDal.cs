using Gate.Persistence.DataAccess.Interfaces;
using Gate.backend.Utils.Settings;
using Gate.Domain.Models;
using MySqlConnector;

namespace Gate.Persistence.DataAccess
{
  public class CompanyDal : ICompanyDal
  {

    private static readonly string cmdSqlGetNextId =  "SELECT IF(COM_IDENTI, MAX(COM_IDENTI + 1), 1) as ID FROM COMPANIES;";
    private static readonly string cmdSqlGetAll = "SELECT * FROM COMPANIES;";
    private static readonly string cmdSqlGetById = "SELECT * FROM COMPANIES WHERE COM_IDENTI = @searchedId;";
    private static readonly string cmdSqlPost = "INSERT INTO COMPANIES VALUES(@id, @description, @primaryColor, @accentColor, @warnColor, (SELECT NOW() FROM DUAL));";
    private static readonly string cmdSqlUpdate = "UPDATE COMPANIES SET COM_DESCRI = @description, COM_PCOLOR = @primaryColor, COM_ACOLOR = @accentColor, COM_WCOLOR = @warnColor WHERE COM_IDENTI = @searchedId;";
    private static readonly string cmdSqlDelete = "DELETE FROM COMPANIES WHERE COM_IDENTI = @searchedId";

    public CompanyDal() {}

    private int GetNextId(MySqlTransaction trans) {
            if (trans is null)
            {
                throw new ArgumentNullException(nameof(trans));
            }

            MySqlCommand command = new MySqlCommand(cmdSqlGetNextId, trans.Connection, trans);
      var id = 0;

      using(MySqlDataReader dr = command.ExecuteReader()) 
        while (dr.Read()) 
          id = dr.GetInt32(0);

      return id;
    }

    public List<CompanyInfo> GetAll()
    {
      var companyList = new List<CompanyInfo>();
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();
        MySqlCommand command = new MySqlCommand(cmdSqlGetAll, con);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            var company = new CompanyInfo()
            {
              Id = (int)dr["COM_IDENTI"],
              Description = (string)dr["COM_DESCRI"],
              PrimaryColor = (string)dr["COM_PCOLOR"],
              AccentColor = (string)dr["COM_SCOLOR"],
              WarnColor = (string)dr["COM_WCOLOR"],
              CreationDate = (DateTime)dr["COM_DATCRI"]
            };
            companyList.Add(company);
          }
        }
        con.Close();
      }
      return companyList;
    }

    public CompanyInfo GetById(int id)
    {
      var company = new CompanyInfo();
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();

        MySqlCommand command = new MySqlCommand(cmdSqlGetById, con);
        command.Parameters.AddWithValue("@id", id);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            company = new CompanyInfo()
            {
              Id = (int)dr["COM_IDENTI"],
              Description = (string)dr["COM_DESCRI"],
              PrimaryColor = (string)dr["COM_PCOLOR"],
              AccentColor = (string)dr["COM_SCOLOR"],
              WarnColor = (string)dr["COM_WCOLOR"],
              CreationDate = (DateTime)dr["COM_DATCRI"]
            };
          }
        }
        con.Close();
      }
      return company;
    }

    public CompanyInfo Post(CompanyInfo company)
    {
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();
        MySqlCommand command = new MySqlCommand(cmdSqlPost, con);
        command.Parameters.AddWithValue("@id", company.Id);
        command.Parameters.AddWithValue("@description", company.Description);
        command.Parameters.AddWithValue("@primaryColor", company.PrimaryColor);
        command.Parameters.AddWithValue("@accentColor", company.AccentColor);
        command.Parameters.AddWithValue("@warnColor", company.WarnColor);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            company = new CompanyInfo()
            {
              Id = (int)dr["COM_IDENTI"],
              Description = (string)dr["COM_DESCRI"],
              PrimaryColor = (string)dr["COM_PCOLOR"],
              AccentColor = (string)dr["COM_SCOLOR"],
              WarnColor = (string)dr["COM_WCOLOR"],
              CreationDate = (DateTime)dr["COM_DATCRI"]
            };
          }
        }
        con.Close();
      }
      return company;
    }
    
    public CompanyInfo Post(CompanyInfo company, MySqlTransaction trans)
    {
      MySqlCommand command = new MySqlCommand(cmdSqlPost, trans.Connection, trans);
      var id = GetNextId(trans);
      command.Parameters.AddWithValue("@id", id);
      command.Parameters.AddWithValue("@description", company.Description);
      command.Parameters.AddWithValue("@primaryColor", company.PrimaryColor);
      command.Parameters.AddWithValue("@accentColor", company.AccentColor);
      command.Parameters.AddWithValue("@warnColor", company.WarnColor);
      command.ExecuteNonQuery();

      command = new MySqlCommand(cmdSqlGetById, trans.Connection, trans);
      command.Parameters.AddWithValue("@searchedId", id);
      using(MySqlDataReader dr = command.ExecuteReader()) 
      {
        while (dr.Read()) 
        {
          company = new CompanyInfo()
          {
            Id = (int)dr["COM_IDENTI"],
            Description = (string)dr["COM_DESCRI"],
            PrimaryColor = (string)dr["COM_PCOLOR"],
            AccentColor = (string)dr["COM_ACOLOR"],
            WarnColor = (string)dr["COM_WCOLOR"],
            CreationDate = (DateTime)dr["COM_DATCRI"]
          };
        }
      }
      return company;
    }
    
    public CompanyInfo Update(CompanyInfo company)
    {
       using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();
        MySqlCommand command = new MySqlCommand(cmdSqlUpdate, con);
        var updatedCompany = GetById(company.Id);

        command.Parameters.AddWithValue("@searchedId", company.Id);
        command.Parameters.AddWithValue("@description", company.Description);
        command.Parameters.AddWithValue("@primaryColor", company.PrimaryColor);
        command.Parameters.AddWithValue("@accentColor", company.AccentColor);
        command.Parameters.AddWithValue("@warnColor", company.WarnColor);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            company = new CompanyInfo()
            {
              Id = (int)dr["COM_IDENTI"],
              Description = (string)dr["COM_DESCRI"],
              PrimaryColor = (string)dr["COM_PCOLOR"],
              AccentColor = (string)dr["COM_SCOLOR"],
              WarnColor = (string)dr["COM_WCOLOR"],
              CreationDate = (DateTime)dr["COM_DATCRI"]
            };
          }
        }
        con.Close();
      }
      return company;
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