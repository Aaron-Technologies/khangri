using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaTourType : IBaTourType
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaTourType(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetTourType(int tourTypeId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[1];
                para[vCount] = new SqlParameter("@TourTypeId", SqlDbType.Int);
                para[vCount++].Value = tourTypeId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[TOUR].[usp_getTourType]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveTourType(EntityTourType oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[4];
                para[vCount] = new SqlParameter("@TourTypeId", SqlDbType.Int);
                para[vCount++].Value = oEntity.TourTypeId;
                para[vCount] = new SqlParameter("@TourType", SqlDbType.NVarChar, 100);
                para[vCount++].Value = oEntity.TourType;
                para[vCount] = new SqlParameter("@Description", SqlDbType.NVarChar, 4000);
                para[vCount++].Value = oEntity.Description;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[vCount++].Value = oEntity.IsActive;

                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[TOUR].[usp_setTourType]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
    }
}
