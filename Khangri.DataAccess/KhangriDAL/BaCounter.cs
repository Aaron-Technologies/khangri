using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaCounter : IBaCounter
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaCounter(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetCounter(int counterId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[1];
                para[vCount] = new SqlParameter("@CounterId", SqlDbType.Int);
                para[vCount++].Value = counterId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_getCounter]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveCounter(EntityCounter oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[7];
                para[vCount] = new SqlParameter("@CounterId", SqlDbType.Int);
                para[vCount++].Value = oEntity.CounterId;
                para[vCount] = new SqlParameter("@Title", SqlDbType.NVarChar, 200);
                para[vCount++].Value = oEntity.Title;
                para[vCount] = new SqlParameter("@Description", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.Description;
                para[vCount] = new SqlParameter("@Value", SqlDbType.BigInt);
                para[vCount++].Value = oEntity.Value;
                para[vCount] = new SqlParameter("@Icon", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.Icon;
                para[vCount] = new SqlParameter("@Link", SqlDbType.NVarChar, 200);
                para[vCount++].Value = oEntity.Link;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[vCount++].Value = oEntity.IsActive;

                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_setCounter]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
    }
}
