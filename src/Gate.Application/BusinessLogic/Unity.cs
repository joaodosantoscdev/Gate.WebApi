using Gate.Application.BusinessLogic.Interfaces;
using Gate.Persistence.DataAccess.Interfaces;
using Gate.Domain.Models;
using MySqlConnector;

namespace Gate.Application.BusinessLogic
{
  public class Unity : IUnity
  {
    private readonly IUnityDal _unityDal;
    public Unity(IUnityDal unityDal)
    {
      _unityDal = unityDal;
    }

    public UnityInfo GetById(int id) => _unityDal.GetById(id);
    public UnityInfo GetByTaxId(string taxId) => _unityDal.GetByTaxId(taxId);
    public UnityInfo Post(UnityInfo unity) 
    {
      unity.Active = true;

      return _unityDal.Post(unity);
    }

    public UnityInfo Post(UnityInfo unity, MySqlTransaction trans) 
    {
      unity.Active = true;

      return _unityDal.Post(unity, trans);
    }
  }
}