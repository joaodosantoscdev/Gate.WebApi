using Microsoft.Extensions.Configuration;
using Gate.Persistence.DataAccess.Interfaces;
using Gate.Domain.Models;
using Gate.backend.Utils.Settings;
using MySqlConnector;

namespace Gate.Persistence.DataAccess
{
  public class EmployeeDal : IEmployeeDal
  {
    string cmdSqlGetNextId =  "SELECT IF(EMP_IDENTI, MAX(EMP_IDENTI + 1), 1) as ID FROM EMPLOYEES;"; 
    private string cmdSqlGetAll = "SELECT * FROM EMPLOYEES;";
    private string cmdSqlGetById = "SELECT * FROM EMPLOYEES WHERE EMP_IDENTI = @searchedId;";
    private string cmdSqlGetByRegister = "SELECT * FROM EMPLOYEES WHERE EMP_REGIS = @register;";
    private string cmdSqlPost = "INSERT INTO EMPLOYEES VALUES(@id, @register, @userId, @unityId, @documentNumber, @phone, @birthdate, (SELECT NOW() FROM DUAL));";
    private string cmdSqlUpdate = "UPDATE EMPLOYEES SET EMP_REGIS = @register, EMP_NUMDOC = @documentNumber, EMP_PHONE = @phone, EMP_BDATE = @birthdate WHERE EMP_IDENTI = @searchedId;";
    private string cmdSqlDelete = "DELETE FROM EMPLOYEES WHERE EMP_IDENTI = @searchedId";
    private IConfigurationRoot _connectionString; 

    public EmployeeDal() 
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

    public List<EmployeeInfo> GetAll()
    {
      var employeeList = new List<EmployeeInfo>();
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();
        MySqlCommand command = new MySqlCommand(cmdSqlGetAll, con);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            var employee = new EmployeeInfo()
            {
              Id = (int)dr["EMP_IDENTI"],
              Register = (int)dr["EMP_REGIS"],
              Cpf = (string)dr["EMP_NUMDOC"],
              Birthdate = (DateTime)dr["EMP_BDATE"],
              UnityId = (int)dr["EMP_UNI_IDENTI"],
              Phone = (string)dr["EMP_PHONE"],
              UserId = (int)dr["EMP_USU_IDENTI"],
              CreationDate = (DateTime)dr["EMP_DATCRI"]
            };
            employeeList.Add(employee);
          }
        }
        con.Close();
      }
      return employeeList;
    }

    public EmployeeInfo GetById(int id)
    {
      var employee = new EmployeeInfo();
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();

        MySqlCommand command = new MySqlCommand(cmdSqlGetById, con);
        command.Parameters.AddWithValue("@id", id);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            employee = new EmployeeInfo()
            {
              Id = (int)dr["EMP_IDENTI"],
              Register = (int)dr["EMP_REGIS"],
              Cpf = (string)dr["EMP_NUMDOC"],
              Birthdate = (DateTime)dr["EMP_BDATE"],
              UnityId = (int)dr["EMP_UNI_IDENTI"],
              Phone = (string)dr["EMP_PHONE"],
              UserId = (int)dr["EMP_USU_IDENTI"],
              CreationDate = (DateTime)dr["EMP_DATCRI"]
            };
          }
        }
        con.Close();
      }
      return employee;
    }

    public EmployeeInfo GetByRegister(int register)
    {
      var employee = new EmployeeInfo();
      using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();

        MySqlCommand command = new MySqlCommand(cmdSqlGetByRegister, con);
        command.Parameters.AddWithValue("@register", register);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            employee = new EmployeeInfo()
            {
              Id = (int)dr["EMP_IDENTI"],
              Register = (int)dr["EMP_REGIS"],
              Cpf = (string)dr["EMP_NUMDOC"],
              Birthdate = (DateTime)dr["EMP_BDATE"],
              UnityId = (int)dr["EMP_UNI_IDENTI"],
              Phone = (string)dr["EMP_PHONE"],
              UserId = (int)dr["EMP_USU_IDENTI"],
              CreationDate = (DateTime)dr["EMP_DATCRI"]
            };
          }
        }
        con.Close();
      }
      return employee;
    }

    // public EmployeeInfo Post(EmployeeInfo employee)
    // {
    //   using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
    //   {
    //     con.Open();
    //     MySqlCommand command = new MySqlCommand(cmdSqlPost, con);
    //     command.Parameters.AddWithValue("@id", employee.Id);
    //     command.Parameters.AddWithValue("@register", employee.Register);
    //     command.Parameters.AddWithValue("@userId", employee.UserId);
    //     command.Parameters.AddWithValue("@unityId", employee.UnityId);
    //     command.Parameters.AddWithValue("@documentNumber", employee.Cpf);
    //     command.Parameters.AddWithValue("@phone", employee.Phone);
    //     command.Parameters.AddWithValue("@birthdate", employee.Birthdate);
        
    //     using(MySqlDataReader dr = command.ExecuteReader()) 
    //     {
    //       while (dr.Read()) 
    //       {
    //         employee = new EmployeeInfo()
    //         {
    //           Id = (int)dr["EMP_IDENTI"],
    //           Register = (int)dr["EMP_REGIS"],
    //           Cpf = (string)dr["EMP_NUMDOC"],
    //           Birthdate = (DateTime)dr["EMP_BDATE"],
    //           UnityId = (int)dr["EMP_UNI_IDENTI"],
    //           Phone = (string)dr["EMP_PHONE"],
    //           UserId = (int)dr["EMP_USU_IDENTI"],
    //           CreationDate = (DateTime)dr["EMP_DATCRI"]
    //         };
    //       }
    //     }
    //     con.Close();
    //   }
    //   return employee;
    // }
    public EmployeeInfo Post(EmployeeInfo employee, MySqlTransaction trans)
    {
      MySqlCommand command = new MySqlCommand(cmdSqlPost, trans.Connection, trans);
      var id = GetNextId(trans);
      command.Parameters.AddWithValue("@id", id);
      command.Parameters.AddWithValue("@register", employee.Register);
      command.Parameters.AddWithValue("@userId", employee.UserId);
      command.Parameters.AddWithValue("@unityId", employee.UnityId);
      command.Parameters.AddWithValue("@documentNumber", employee.Cpf);
      command.Parameters.AddWithValue("@phone", employee.Phone);
      command.Parameters.AddWithValue("@birthdate", employee.Birthdate);
      command.ExecuteNonQuery();

      command = new MySqlCommand(cmdSqlGetById, trans.Connection, trans);
      command.Parameters.AddWithValue("@searchedId", id);  
      using(MySqlDataReader dr = command.ExecuteReader()) 
      {
        while (dr.Read()) 
        {
          employee = new EmployeeInfo()
          {
            Id = (int)dr["EMP_IDENTI"],
            Register = (int)dr["EMP_REGIS"],
            Cpf = (string)dr["EMP_NUMDOC"],
            Birthdate = (DateTime)dr["EMP_BDATE"],
            UnityId = (int)dr["EMP_UNI_IDENTI"],
            Phone = (string)dr["EMP_PHONE"],
            UserId = (int)dr["EMP_USU_IDENTI"],
            CreationDate = (DateTime)dr["EMP_DATCRI"]
          };
        }
      }

      return employee;
    }

    public EmployeeInfo Update(EmployeeInfo employee)
    {
       using(MySqlConnection con = new MySqlConnection(AppSettingsAcessor.GetConnectionString())) 
      {
        con.Open();
        MySqlCommand command = new MySqlCommand(cmdSqlUpdate, con);
        var updatedEmployee = GetById(employee.Id);

        command.Parameters.AddWithValue("@searchedId", employee.Id);
        command.Parameters.AddWithValue("@register", employee.Register);
        command.Parameters.AddWithValue("@documentNumber", employee.Cpf);
        command.Parameters.AddWithValue("@phone", employee.Phone);
        command.Parameters.AddWithValue("@birthdate", employee.Birthdate);
        
        using(MySqlDataReader dr = command.ExecuteReader()) 
        {
          while (dr.Read()) 
          {
            employee = new EmployeeInfo()
            {
              Id = (int)dr["EMP_IDENTI"],
              Register = (int)dr["EMP_REGIS"],
              Cpf = (string)dr["EMP_NUMDOC"],
              Birthdate = (DateTime)dr["EMP_BDATE"],
              UnityId = (int)dr["EMP_UNI_IDENTI"],
              Phone = (string)dr["EMP_PHONE"],
              UserId = (int)dr["EMP_USU_IDENTI"],
              CreationDate = (DateTime)dr["EMP_DATCRI"]
            };
          }
        }
        con.Close();
      }
      return employee;
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