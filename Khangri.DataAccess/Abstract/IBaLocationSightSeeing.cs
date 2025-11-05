using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaLocationSightSeeing
    {
        DataSet GetLocationSightSeeing(int locationSightSeeingId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveLocationSightSeeing(EntityLocationSightSeeing oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}