using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaTourImage
    {
        DataSet GetTourImage(int tourImageId, int tourId,  ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveTourImage(EntityTourImage oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}