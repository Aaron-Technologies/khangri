using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaLocation
    {
        DataSet GetLocations(long LocationId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveLocation(EntityLocation oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}