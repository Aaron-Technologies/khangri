using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaSightSeeing
    {
        DataSet GetSeightSeen(int sightSeeingId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveSeightSeen(EntitySightSeeing oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}