using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaWEBTourList
    {
        DataSet GetTourList(int tourTypeId, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}