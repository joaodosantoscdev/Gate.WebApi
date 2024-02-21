using Gate.Application.BusinessLogic.Interfaces;
using Gate.Persistence.DataAccess.Interfaces;
using Gate.Domain.Models;
using MySqlConnector;

namespace Gate.Application.BusinessLogic
{
  public class Company : ICompany
  {
    private readonly ICompanyDal _companyDal;
    public Company(ICompanyDal companyDal)
    {
      _companyDal = companyDal;
    }

    public CompanyInfo GetById(int id) => _companyDal.GetById(id);

    public CompanyInfo Post(CompanyInfo company) => _companyDal.Post(company);
    public CompanyInfo Post(CompanyInfo company, MySqlTransaction trans) 
    {
      try
      {
        return _companyDal.Post(company, trans);
      }
      catch (Exception ex)
      {
        throw new ArgumentException($"Falha ao realizar cadastro da Compania: {ex.Message}. StackTrace: {ex.StackTrace}");
      }
    } 
  }
}