using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaHotel
    {
		DataSet GetAmenities(int amenitiesId, ref string pMsg, string pAccYr, short? pCompany_key);
		DataSet GetHotel(int hotelId, ref string pMsg, string pAccYr, short? pCompany_key);
		DataSet GetHotelAmenities(int hotelAmenitiesId, int hotelId, ref string pMsg, string pAccYr, short? pCompany_key);
		DataSet GetHotelRoomType(int hotelRoomTypeId, int hotelId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet GetRoomType(int roomTypeId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveAmenities(EntityAmenities oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveHotel(EntityHotel oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveHotelAmenities(EntityHotelAmenities oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveHotelRoomType(EntityHotelRoomType oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveRoomType(EntityRoomType oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}