using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaTour
    {
        DataSet GetTour(int tourId, int tourTypeId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveTour(EntityTour oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}