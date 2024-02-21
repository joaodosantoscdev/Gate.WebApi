using Gate.Domain.Models;
using MySqlConnector;

namespace Gate.Persistence.DataAccess.Interfaces
{
    public interface IEmployeeDal
    {
        List<EmployeeInfo> GetAll();
        EmployeeInfo GetById(int id);
        EmployeeInfo GetByRegister(int register);
        // EmployeeInfo Post(EmployeeInfo employee);
        EmployeeInfo Post(EmployeeInfo employee, MySqlTransaction trans);
        EmployeeInfo Update(EmployeeInfo employee);
        bool Delete(int id);
    }
}