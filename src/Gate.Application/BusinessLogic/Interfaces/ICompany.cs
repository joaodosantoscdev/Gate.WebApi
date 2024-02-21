using Gate.Domain.Models;
using MySqlConnector;

namespace Gate.Application.BusinessLogic.Interfaces
{
    public interface ICompany
    {
        CompanyInfo GetById(int id);
        CompanyInfo Post(CompanyInfo company);
        CompanyInfo Post(CompanyInfo company, MySqlTransaction trans);

    }
}