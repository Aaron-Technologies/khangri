using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaCMSPage : IBaCMSPage
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaCMSPage(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }
        public DataSet GetCMSPage(int CMSPageId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[1];
                para[vCount] = new SqlParameter("@CMSPageId", SqlDbType.BigInt);
                para[vCount++].Value = CMSPageId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_getCMSPage]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveCMSPage(EntityCMSPage oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[3];
                para[vCount] = new SqlParameter("@CMSPageId", SqlDbType.Int);
                para[vCount++].Value = oEntity.CMSPageId;
                para[vCount] = new SqlParameter("@Title", SqlDbType.NVarChar, 500);
                para[vCount++].Value = oEntity.Title;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[vCount++].Value = oEntity.IsActive;


                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_setCMSPage]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }

    }
}
