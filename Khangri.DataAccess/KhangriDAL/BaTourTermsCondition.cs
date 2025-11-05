using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaTourTermsCondition : IBaTourTermsCondition
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaTourTermsCondition(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetTourTermsCondition(int tourTermsConditionId, int tourTypeId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[2];
                para[vCount] = new SqlParameter("@TourTermsConditionId", SqlDbType.Int);
                para[vCount++].Value = tourTermsConditionId;
                para[vCount] = new SqlParameter("@TourTypeId", SqlDbType.Int);
                para[vCount++].Value = tourTypeId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[TOUR].[usp_getTourTermsCondition]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveTourTermsCondition(EntityTourTermsCondition oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[9];
                para[vCount] = new SqlParameter("@TourTermsConditionId", SqlDbType.Int);
                para[vCount++].Value = oEntity.TourTermsConditionId;
                para[vCount] = new SqlParameter("@TourTypeId", SqlDbType.Int);
                para[vCount++].Value = oEntity.TourTypeId;
                para[vCount] = new SqlParameter("@Accommodation", SqlDbType.NVarChar);
                para[vCount++].Value = oEntity.Accommodation;
                para[vCount] = new SqlParameter("@Food", SqlDbType.NVarChar);
                para[vCount++].Value = oEntity.Food;
                para[vCount] = new SqlParameter("@TermsConditionsPermits", SqlDbType.NVarChar);
                para[vCount++].Value = oEntity.TermsConditionsPermits;
                para[vCount] = new SqlParameter("@MiscellaneousExpenses", SqlDbType.NVarChar);
                para[vCount++].Value = oEntity.MiscellaneousExpenses;
                para[vCount] = new SqlParameter("@TermsConditionsGeneral", SqlDbType.NVarChar);
                para[vCount++].Value = oEntity.TermsConditionsGeneral;
                para[vCount] = new SqlParameter("@CancellationPolicy", SqlDbType.NVarChar);
                para[vCount++].Value = oEntity.CancellationPolicy;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Int);
                para[vCount++].Value = oEntity.IsActive;

                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[TOUR].[usp_setTourTermsCondition]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
    }
}
