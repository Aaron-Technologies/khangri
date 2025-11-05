using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaLocationDetails
    {
        DataSet GetLocationsDetails(long LocationId, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}