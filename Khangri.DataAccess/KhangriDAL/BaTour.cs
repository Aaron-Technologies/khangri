using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaTour : IBaTour
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaTour(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetTour(int tourId, int tourTypeId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[2];
                para[vCount] = new SqlParameter("@TourId", SqlDbType.Int);
                para[vCount++].Value = tourId;
                para[vCount] = new SqlParameter("@TourTypeId", SqlDbType.Int);
                para[vCount++].Value = tourTypeId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[TOUR].[usp_getTour]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveTour(EntityTour oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[9];
                para[vCount] = new SqlParameter("@TourId", SqlDbType.Int);
                para[vCount++].Value = oEntity.TourId;
                para[vCount] = new SqlParameter("@TourTypeId", SqlDbType.Int);
                para[vCount++].Value = oEntity.TourTypeId;
                para[vCount] = new SqlParameter("@TourName", SqlDbType.NVarChar, 500);
                para[vCount++].Value = oEntity.TourName;
                para[vCount] = new SqlParameter("@TourInroduction", SqlDbType.NVarChar,4000);
                para[vCount++].Value = oEntity.TourInroduction;
                para[vCount] = new SqlParameter("@TourHighlight", SqlDbType.NVarChar);
                para[vCount++].Value = oEntity.TourHighlight;
                para[vCount] = new SqlParameter("@TourOverview", SqlDbType.NVarChar);
                para[vCount++].Value = oEntity.TourOverview;
                para[vCount] = new SqlParameter("@NoOfDays", SqlDbType.Int);
                para[vCount++].Value = oEntity.NoOfDays;
                para[vCount] = new SqlParameter("@Price", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.Price;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[vCount++].Value = oEntity.IsActive;

                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[TOUR].[usp_setTour]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
    }
}
