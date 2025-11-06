using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaFeature
    {
        DataSet GetFeature(int featureId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveFeature(EntityFeature oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}