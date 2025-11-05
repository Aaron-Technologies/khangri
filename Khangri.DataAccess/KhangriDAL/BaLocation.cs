using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaLocation : IBaLocation
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaLocation(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetLocations(long LocationId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[1];
                para[vCount] = new SqlParameter("@LocationId", SqlDbType.BigInt);
                para[vCount++].Value = LocationId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_getLocation]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveLocation(EntityLocation oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[11];
                para[vCount] = new SqlParameter("@LocationId", SqlDbType.Int);
                para[vCount++].Value = oEntity.LocationId;
                para[vCount] = new SqlParameter("@Location", SqlDbType.NVarChar, 100);
                para[vCount++].Value = oEntity.Location;
                para[vCount] = new SqlParameter("@Caption", SqlDbType.NVarChar, 200);
                para[vCount++].Value = oEntity.Caption;
                para[vCount] = new SqlParameter("@M01_LocationId", SqlDbType.Int);
                para[vCount++].Value = oEntity.M01_LocationId;
                para[vCount] = new SqlParameter("@Description", SqlDbType.NVarChar, 4000);
                para[vCount++].Value = oEntity.Description;
                para[vCount] = new SqlParameter("@Altitude", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.Altitude;
                para[vCount] = new SqlParameter("@ImageFiles", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.ImageFiles;
                para[vCount] = new SqlParameter("@WeatherUrl", SqlDbType.NVarChar, 500);
                para[vCount++].Value = oEntity.WeatherUrl;
                para[vCount] = new SqlParameter("@GoogleMapUrl", SqlDbType.NVarChar, 500);
                para[vCount++].Value = oEntity.GoogleMapUrl;
                para[vCount] = new SqlParameter("@TripAdvisorUrl", SqlDbType.NVarChar, 500);
                para[vCount++].Value = oEntity.TripAdvisorUrl;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[vCount++].Value = oEntity.IsActive;


                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_setLocation]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
    }
}
