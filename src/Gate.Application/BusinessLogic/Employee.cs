using Gate.Application.BusinessLogic.Interfaces;
using Gate.Persistence.DataAccess.Interfaces;
using Gate.Domain.Models;
using MySqlConnector;

namespace Gate.Application.BusinessLogic
{
    public class Employee : IEmployee
  {
    private readonly IEmployeeDal _employeeDal;
    public Employee(IEmployeeDal employeeDal)
    {
      _employeeDal = employeeDal;
    }

    public EmployeeInfo GetById(int id) => _employeeDal.GetById(id);
    public EmployeeInfo GetByRegister(int register) => _employeeDal.GetByRegister(register);
    // public EmployeeInfo Post(EmployeeInfo employee) => _employeeDal.Post(employee);
    public EmployeeInfo Post(EmployeeInfo employee, MySqlTransaction trans) => _employeeDal.Post(employee, trans);
  }
}