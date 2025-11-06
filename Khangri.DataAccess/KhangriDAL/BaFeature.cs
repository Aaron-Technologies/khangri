using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaFeature : IBaFeature
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaFeature(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetFeature(int featureId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[1];
                para[vCount] = new SqlParameter("@FeatureId", SqlDbType.Int);
                para[vCount++].Value = featureId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_getFeature]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveFeature(EntityFeature oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[5];
                para[vCount] = new SqlParameter("@FeatureId", SqlDbType.Int);
                para[vCount++].Value = oEntity.FeatureId;
                para[vCount] = new SqlParameter("@Title", SqlDbType.NVarChar, 200);
                para[vCount++].Value = oEntity.Title;
                para[vCount] = new SqlParameter("@Description", SqlDbType.NVarChar, 4000);
                para[vCount++].Value = oEntity.Description;
                para[vCount] = new SqlParameter("@Icon", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.Icon;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[vCount++].Value = oEntity.IsActive;

                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_setFeature]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
    }
}
