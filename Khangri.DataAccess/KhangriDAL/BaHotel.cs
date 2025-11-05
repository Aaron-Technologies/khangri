using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaHotel : IBaHotel
	{
		private readonly ISqlHelper sqlHelper;
		private Int16 vCount = 0;
		private DataSet ds = null;
		private SqlParameter[] para = null;
		public BaHotel(ISqlHelper sqlHelper)
		{
			this.sqlHelper = sqlHelper;
		}

		public DataSet GetAmenities(int amenitiesId, ref String pMsg, String pAccYr, Int16? pCompany_key)
		{
			try
			{
				vCount = 0;
				para = new SqlParameter[1];
				para[vCount] = new SqlParameter("@AmenitiesId", SqlDbType.Int);
				para[vCount++].Value = amenitiesId;
				ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "SELECT * FROM [HTL].[udf_getAmenities](" + amenitiesId+" )", CommandType.Text, para, ref pMsg);

			}
			catch (Exception ex)
			{
				pMsg = ex.Message;
			}
			return ds;

		}
		public DataSet SaveAmenities(EntityAmenities oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
		{
			try
			{
				vCount = 0;
				para = new SqlParameter[5];
				para[vCount] = new SqlParameter("@AmenitiesId", SqlDbType.Int);
				para[vCount++].Value = oEntity.AmenitiesId;
				para[vCount] = new SqlParameter("@Amenities", SqlDbType.NVarChar, 200);
				para[vCount++].Value = oEntity.Amenities;
				para[vCount] = new SqlParameter("@Description", SqlDbType.NVarChar);
				para[vCount++].Value = oEntity.Description;
				para[vCount] = new SqlParameter("@Icon", SqlDbType.NVarChar, 50);
				para[vCount++].Value = oEntity.Icon;
				para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
				para[vCount++].Value = oEntity.IsActive;

				ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[HTL].[usp_setAmenities]", CommandType.StoredProcedure, para, ref pMsg);
			}
			catch (Exception ex)
			{
				pMsg = ex.Message;
			}
			return ds;
		}

		public DataSet GetHotel(int hotelId, ref String pMsg, String pAccYr, Int16? pCompany_key)
		{
			try
			{
				vCount = 0;
				para = new SqlParameter[1];
				para[vCount] = new SqlParameter("@HotelId", SqlDbType.Int);
				para[vCount++].Value = hotelId;
				ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "SELECT * FROM [HTL].[udf_getHotel](" + hotelId+")", CommandType.StoredProcedure, para, ref pMsg);

			}
			catch (Exception ex)
			{
				pMsg = ex.Message;
			}
			return ds;

		}
		public DataSet SaveHotel(EntityHotel oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
		{
			try
			{
				vCount = 0;
				para = new SqlParameter[5];
				para[vCount] = new SqlParameter("@HotelId", SqlDbType.Int);
				para[vCount++].Value = oEntity.HotelId;
				para[vCount] = new SqlParameter("@HotelName", SqlDbType.NVarChar, 100);
				para[vCount++].Value = oEntity.HotelName;
				para[vCount] = new SqlParameter("@Description", SqlDbType.NVarChar);
				para[vCount++].Value = oEntity.Description;
				para[vCount] = new SqlParameter("@Address", SqlDbType.NVarChar, 2000);
				para[vCount++].Value = oEntity.Address;
				para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
				para[vCount++].Value = oEntity.IsActive;

				ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[HTL].[usp_setHotel]", CommandType.StoredProcedure, para, ref pMsg);
			}
			catch (Exception ex)
			{
				pMsg = ex.Message;
			}
			return ds;
		}
		public DataSet GetHotelAmenities(int hotelAmenitiesId,int hotelId, ref String pMsg, String pAccYr, Int16? pCompany_key)
		{
			try
			{
				vCount = 0;
				para = new SqlParameter[2];
				para[vCount] = new SqlParameter("@HotelAmenitiesId", SqlDbType.Int);
				para[vCount++].Value = hotelAmenitiesId;
				para[vCount] = new SqlParameter("@HotelId", SqlDbType.Int);
				para[vCount++].Value = hotelId;
				ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "SELECT * FROM [HTL].[udf_getHotelAmenities](" + hotelAmenitiesId+","+hotelId+" )", CommandType.StoredProcedure, para, ref pMsg);

			}
			catch (Exception ex)
			{
				pMsg = ex.Message;
			}
			return ds;

		}
		public DataSet SaveHotelAmenities(EntityHotelAmenities oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
		{
			try
			{
				vCount = 0;
				para = new SqlParameter[4];
				para[vCount] = new SqlParameter("@HotelAmenitiesId", SqlDbType.Int);
				para[vCount++].Value = oEntity.HotelAmenitiesId;
				para[vCount] = new SqlParameter("@HotelId", SqlDbType.Int);
				para[vCount++].Value = oEntity.HotelId;
				para[vCount] = new SqlParameter("@AmenitiesId", SqlDbType.Int);
				para[vCount++].Value = oEntity.AmenitiesId;
				para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
				para[vCount++].Value = oEntity.IsActive;

				ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[HTL].[usp_setHotelAmenities]", CommandType.StoredProcedure, para, ref pMsg);
			}
			catch (Exception ex)
			{
				pMsg = ex.Message;
			}
			return ds;
		}
		public DataSet GetHotelRoomType(int hotelRoomTypeId, int hotelId, ref String pMsg, String pAccYr, Int16? pCompany_key)
		{
			try
			{
				vCount = 0;
				para = new SqlParameter[2];
				para[vCount] = new SqlParameter("@HotelRoomTypeId", SqlDbType.Int);
				para[vCount++].Value = hotelRoomTypeId;
				para[vCount] = new SqlParameter("@HotelId", SqlDbType.Int);
				para[vCount++].Value = hotelId;
				ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "SELECT * FROM [HTL].[udf_getHotelRoomType](" + hotelRoomTypeId + "," + hotelId + " )", CommandType.StoredProcedure, para, ref pMsg);

			}
			catch (Exception ex)
			{
				pMsg = ex.Message;
			}
			return ds;

		}
        public DataSet GetRoomType(int roomTypeId,  ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[1];
                para[vCount] = new SqlParameter("@RoomTypeId", SqlDbType.Int);
                para[vCount++].Value = roomTypeId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "SELECT * FROM [HTL].[udf_getRoomType](" + roomTypeId + " )", CommandType.Text, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveRoomType(EntityRoomType oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[6];
                para[vCount] = new SqlParameter("@RoomTypeId", SqlDbType.Int);
                para[vCount++].Value = oEntity.RoomTypeId;
                para[vCount] = new SqlParameter("@RoomType", SqlDbType.NVarChar,200);
                para[vCount++].Value = oEntity.RoomType;
                para[vCount] = new SqlParameter("@Description", SqlDbType.NVarChar);
                para[vCount++].Value = oEntity.Description;
                para[vCount] = new SqlParameter("@Price", SqlDbType.Int);
                para[vCount++].Value = oEntity.Price;
                para[vCount] = new SqlParameter("@ImageFile", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.ImageFile;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[vCount++].Value = oEntity.IsActive;

                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[HTL].[usp_setRoomType]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
        public DataSet SaveHotelRoomType(EntityHotelRoomType oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
		{
			try
			{
				vCount = 0;
				para = new SqlParameter[6];
				para[vCount] = new SqlParameter("@HotelRoomTypeId", SqlDbType.Int);
				para[vCount++].Value = oEntity.HotelRoomTypeId;
				para[vCount] = new SqlParameter("@HotelId", SqlDbType.Int);
				para[vCount++].Value = oEntity.HotelId;
				para[vCount] = new SqlParameter("@RoomTypeId", SqlDbType.Int);
				para[vCount++].Value = oEntity.RoomTypeId;
				para[vCount] = new SqlParameter("@Price", SqlDbType.Int);
				para[vCount++].Value = oEntity.Price;
				para[vCount] = new SqlParameter("@ImageFile", SqlDbType.NVarChar, 50);
				para[vCount++].Value = oEntity.ImageFile;
				para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
				para[vCount++].Value = oEntity.IsActive;

				ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[HTL].[usp_setHotelAmenities]", CommandType.StoredProcedure, para, ref pMsg);
			}
			catch (Exception ex)
			{
				pMsg = ex.Message;
			}
			return ds;
		}
		

	}
}
