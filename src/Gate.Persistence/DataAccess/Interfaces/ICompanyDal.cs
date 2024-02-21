using Gate.Domain.Models;
using MySqlConnector;

namespace Gate.Persistence.DataAccess.Interfaces
{
    public interface ICompanyDal
    {
        List<CompanyInfo> GetAll();
        CompanyInfo GetById(int id);
        CompanyInfo Post(CompanyInfo company);
        CompanyInfo  Post(CompanyInfo company, MySqlTransaction trans);
        CompanyInfo Update(CompanyInfo company);
        bool Delete(int id);
    }
}