using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaSetUp : IBaSetUp
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaSetUp(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetSetUp(int setUpId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[1];
                para[vCount] = new SqlParameter("@SetUpId", SqlDbType.Int);
                para[vCount++].Value = setUpId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_getSetUp]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveSetUp(EntitySetup oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[16];
                para[vCount] = new SqlParameter("@SetUpId", SqlDbType.Int);
                para[vCount++].Value = oEntity.SetUpId;
                para[vCount] = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 500);
                para[vCount++].Value = oEntity.CompanyName;
                para[vCount] = new SqlParameter("@CompanyIntroduction", SqlDbType.NVarChar);
                para[vCount++].Value = oEntity.CompanyIntroduction;
                para[vCount] = new SqlParameter("@Caption", SqlDbType.NVarChar, 4000);
                para[vCount++].Value = oEntity.Caption;
                para[vCount] = new SqlParameter("@LogoPrimary", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.LogoPrimary;
                para[vCount] = new SqlParameter("@LogoSecondary", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.LogoSecondary;
                para[vCount] = new SqlParameter("@PhoneNoPrimary", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.PhoneNoPrimary;
                para[vCount] = new SqlParameter("@WhatsAppPrimary", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.WhatsAppPrimary;
                para[vCount] = new SqlParameter("@EmailPrimary", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.EmailPrimary;
                para[vCount] = new SqlParameter("@GoogleReviewUrl", SqlDbType.NVarChar, 500);
                para[vCount++].Value = oEntity.GoogleReviewUrl;
                para[vCount] = new SqlParameter("@FBUrl", SqlDbType.NVarChar, 200);
                para[vCount++].Value = oEntity.FBUrl;
                para[vCount] = new SqlParameter("@InstagramUrl", SqlDbType.NVarChar, 200);
                para[vCount++].Value = oEntity.InstagramUrl;
                para[vCount] = new SqlParameter("@TwiterUrl", SqlDbType.NVarChar, 200);
                para[vCount++].Value = oEntity.TwiterUrl;
                para[vCount] = new SqlParameter("@YouTubeUrl", SqlDbType.NVarChar, 200);
                para[vCount++].Value = oEntity.YouTubeUrl;
                para[vCount] = new SqlParameter("@LinkedinUrl", SqlDbType.NVarChar, 200);
                para[vCount++].Value = oEntity.LinkedinUrl;
                para[vCount] = new SqlParameter("@Favicon", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.Favicon;

                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_setSetUp]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
    }
}
