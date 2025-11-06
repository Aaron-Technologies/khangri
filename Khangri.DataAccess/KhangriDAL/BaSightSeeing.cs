using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaSightSeeing : IBaSightSeeing
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaSightSeeing(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetSeightSeen(int sightSeeingId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[1];
                para[vCount] = new SqlParameter("@SightSeeingId", SqlDbType.BigInt);
                para[vCount++].Value = sightSeeingId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_getSightSeeing]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveSeightSeen(EntitySightSeeing oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[8];
                para[vCount] = new SqlParameter("@SightSeeingId", SqlDbType.Int);
                para[vCount++].Value = oEntity.SightSeeingId;
                para[vCount] = new SqlParameter("@SightSeeing", SqlDbType.NVarChar, 100);
                para[vCount++].Value = oEntity.SightSeeing;
                para[vCount] = new SqlParameter("@EstimateTime", SqlDbType.NVarChar,50);
                para[vCount++].Value = oEntity.EstimateTime;
                para[vCount] = new SqlParameter("@ImageFile", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.ImageFile;
                para[vCount] = new SqlParameter("@Details", SqlDbType.NVarChar);
                para[vCount++].Value = oEntity.Details;
                para[vCount] = new SqlParameter("@Altitude", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.Altitude;
                para[vCount] = new SqlParameter("@GoogleMapUrl", SqlDbType.NVarChar, 500);
                para[vCount++].Value = oEntity.GoogleMapUrl;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[vCount++].Value = oEntity.IsActive;

                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_setSightSeeing]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
    }
}
