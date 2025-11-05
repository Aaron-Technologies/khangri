using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaContact : IBaContact
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaContact(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetContact(int contactId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[1];
                para[vCount] = new SqlParameter("@ContactId", SqlDbType.Int);
                para[vCount++].Value = contactId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_getContact]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveContact(EntityContact oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[6];
                para[vCount] = new SqlParameter("@ContactId", SqlDbType.Int);
                para[vCount++].Value = oEntity.ContactId;
                para[vCount] = new SqlParameter("@Header", SqlDbType.NVarChar, 200);
                para[vCount++].Value = oEntity.Header;
                para[vCount] = new SqlParameter("@Phone", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.Phone;
                para[vCount] = new SqlParameter("@Email", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.Email;
                para[vCount] = new SqlParameter("@Address", SqlDbType.NVarChar, 500);
                para[vCount++].Value = oEntity.Address;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[vCount++].Value = oEntity.IsActive;

                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_setContact]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
    }
}
