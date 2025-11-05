using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaTourType
    {
        DataSet GetTourType(int tourTypeId, ref String pMsg, String pAccYr, Int16? pCompany_key);
        DataSet SaveTourType(EntityTourType oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key);
    }
}