using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaLocationImage
    {
        DataSet GetLocationImage(int locationImageId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveLocationImage(EntityLocationImage oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}