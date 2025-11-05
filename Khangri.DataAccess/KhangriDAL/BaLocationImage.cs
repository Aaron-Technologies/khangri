using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaLocationImage : IBaLocationImage
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaLocationImage(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetLocationImage(int locationImageId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[1];
                para[vCount] = new SqlParameter("@LocationImageId", SqlDbType.Int);
                para[vCount++].Value = locationImageId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_getLocationImage]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveLocationImage(EntityLocationImage oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[5];
                para[vCount] = new SqlParameter("@LocationImageId", SqlDbType.Int);
                para[vCount++].Value = oEntity.LocationImageId;
                para[vCount] = new SqlParameter("@ImageId", SqlDbType.Int);
                para[vCount++].Value = oEntity.ImageId;
                para[vCount] = new SqlParameter("@LocationId", SqlDbType.BigInt);
                para[vCount++].Value = oEntity.LocationId;
                para[vCount] = new SqlParameter("@Caption", SqlDbType.NVarChar, 500);
                para[vCount++].Value = oEntity.Caption;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[vCount++].Value = oEntity.IsActive;


                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_setLocationImage]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
    }
}
