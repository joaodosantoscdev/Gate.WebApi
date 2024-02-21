using Gate.Domain.Models;
using MySqlConnector;

namespace Gate.Application.BusinessLogic.Interfaces
{
    public interface IEmployee
    {
        EmployeeInfo GetById(int id);
        EmployeeInfo GetByRegister(int register);
        // EmployeeInfo Post(EmployeeInfo employee);
        EmployeeInfo Post(EmployeeInfo employee, MySqlTransaction trans);
    }
}