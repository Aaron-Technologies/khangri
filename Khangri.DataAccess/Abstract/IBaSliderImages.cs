using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaSliderImages
    {
        DataSet GetSliderImages(int sliderImageId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveSliderImage(EntitySliderImage oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}