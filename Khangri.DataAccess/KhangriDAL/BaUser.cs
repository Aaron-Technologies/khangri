using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaUser : IBaUser
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaUser(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetUsers(long userId, int userTypeId, int pageNo, int pageSize, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[4];
                para[vCount] = new SqlParameter("@UserId", SqlDbType.Int);
                para[vCount++].Value = userId;
                para[vCount] = new SqlParameter("@UserTypeId", SqlDbType.Int);
                para[vCount++].Value = userTypeId;
                para[vCount] = new SqlParameter("@PageNumber", SqlDbType.Int);
                para[vCount++].Value = pageNo;
                para[vCount] = new SqlParameter("@PageSize", SqlDbType.Int);
                para[vCount++].Value = pageSize;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[USR].[usp_GetUser]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveUser(EntityUser oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[8];
                para[vCount] = new SqlParameter("@UserId", SqlDbType.BigInt);
                para[vCount++].Value = oEntity.UserId;
                para[vCount] = new SqlParameter("@UserTypeId", SqlDbType.Int);
                para[vCount++].Value = oEntity.UserTypeId;
                para[vCount] = new SqlParameter("@FName", SqlDbType.NVarChar, 100);
                para[vCount++].Value = oEntity.FName;
                para[vCount] = new SqlParameter("@LName", SqlDbType.NVarChar, 100);
                para[vCount++].Value = oEntity.LName;
                para[vCount] = new SqlParameter("@Email", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.Email;
                para[vCount] = new SqlParameter("@Mobile", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.Mobile;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[vCount++].Value = oEntity.IsActive;
                para[vCount] = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.Password;

                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[USR].[usp_SetUser]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
        public DataSet ChangePassword(EntityChangePassword oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[3];
                para[vCount] = new SqlParameter("@UserId", SqlDbType.Int);
                para[vCount++].Value = oEntity.UserId;
                para[vCount] = new SqlParameter("@OldPassword", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.OldPassword;
                para[vCount] = new SqlParameter("@NewPassword", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.NewPassword;


                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[USR].[usp_UserPasswordUpdate]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet Login(string LoginName, string Password, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[2];
                para[vCount] = new SqlParameter("@LoginName", SqlDbType.NVarChar, 50);
                para[vCount++].Value = LoginName;
                para[vCount] = new SqlParameter("@Password", SqlDbType.NVarChar, 50);
                para[vCount++].Value = Password;


                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[USR].[usp_UserLogin]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet ResetPassword(EntityUserPasswordResetRequest oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[3];
                para[vCount] = new SqlParameter("@UserId", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.UserId;
                para[vCount] = new SqlParameter("@Token", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.Token;
                para[vCount] = new SqlParameter("@NewPassword", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.NewPassword;


                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[USR].[usp_UserPasswordReset]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet InitiateResetPassword(EntityResetPasswordInitiativeRequest oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[1];
                para[vCount] = new SqlParameter("@LoginName", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.LoginName;


                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[USR].[usp_UserPasswordResetInitiative]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }

    }
}
