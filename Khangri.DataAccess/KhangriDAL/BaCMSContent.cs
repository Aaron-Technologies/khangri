using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaCMSContent : IBaCMSContent
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaCMSContent(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }
        public DataSet GetCMSContent(int CMSPageId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[1];
                para[vCount] = new SqlParameter("@CMSPageId", SqlDbType.BigInt);
                para[vCount++].Value = CMSPageId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_getCMSContent]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveCMSContent(EntityCMSContent oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[2];
                para[vCount] = new SqlParameter("@CMSPageId", SqlDbType.Int);
                para[vCount++].Value = oEntity.CMSPageId;
                para[vCount] = new SqlParameter("@Content", SqlDbType.NVarChar);
                para[vCount++].Value = oEntity.Content;


                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_setCMSContent]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }

    }
}
