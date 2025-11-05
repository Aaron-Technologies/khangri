using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaImage
    {
        DataSet GetImages(int imageId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveImages(EntityImage oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}