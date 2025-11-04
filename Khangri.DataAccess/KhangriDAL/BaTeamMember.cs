using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaTeamMember : IBaTeamMember
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaTeamMember(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetTeamMember(int teamMemberId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[1];
                para[vCount] = new SqlParameter("@TeamMemberId", SqlDbType.Int);
                para[vCount++].Value = teamMemberId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_getTeamMember]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveTeamMember(EntityTeamMember oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[6];
                para[vCount] = new SqlParameter("@TeamMemberId", SqlDbType.Int);
                para[vCount++].Value = oEntity.TeamMemberId;
                para[vCount] = new SqlParameter("@Name", SqlDbType.NVarChar, 100);
                para[vCount++].Value = oEntity.Name;
                para[vCount] = new SqlParameter("@ImageId", SqlDbType.Int);
                para[vCount++].Value = oEntity.ImageId;
                para[vCount] = new SqlParameter("@Caption", SqlDbType.NVarChar, 500);
                para[vCount++].Value = oEntity.Caption;
                para[vCount] = new SqlParameter("@Designation", SqlDbType.NVarChar, 100);
                para[vCount++].Value = oEntity.Designation;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[vCount++].Value = oEntity.IsActive;


                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_setTeamMember]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
    }
}
