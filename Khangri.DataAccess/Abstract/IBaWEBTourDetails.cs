using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaWEBTourDetails
    {
        DataSet GetTourDetails(int tourId, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}