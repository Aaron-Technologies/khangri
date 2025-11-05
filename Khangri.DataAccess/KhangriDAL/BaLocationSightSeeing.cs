using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaLocationSightSeeing : IBaLocationSightSeeing
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaLocationSightSeeing(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetLocationSightSeeing(int locationSightSeeingId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[1];
                para[vCount] = new SqlParameter("@LocationSightSeeingId", SqlDbType.Int);
                para[vCount++].Value = locationSightSeeingId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_getLocationSightSeeing]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveLocationSightSeeing(EntityLocationSightSeeing oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[5];
                para[vCount] = new SqlParameter("@LocationSightSeeingId", SqlDbType.Int);
                para[vCount++].Value = oEntity.LocationSightSeeingId;
                para[vCount] = new SqlParameter("@LocationId", SqlDbType.Int);
                para[vCount++].Value = oEntity.LocationId;
                para[vCount] = new SqlParameter("@SightSeeingId", SqlDbType.BigInt);
                para[vCount++].Value = oEntity.SightSeeingId;
                para[vCount] = new SqlParameter("@DistanceFromLocation", SqlDbType.Int);
                para[vCount++].Value = oEntity.DistanceFromLocation;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[vCount++].Value = oEntity.IsActive;


                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_setLocationSightSeeing]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
    }
}
