using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaTourItinerary : IBaTourItinerary
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaTourItinerary(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetTourItinerary(int itineraryId, int tourId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[2];
                para[vCount] = new SqlParameter("@ItineraryId", SqlDbType.Int);
                para[vCount++].Value = itineraryId;
                para[vCount] = new SqlParameter("@TourId", SqlDbType.Int);
                para[vCount++].Value = tourId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[TOUR].[usp_getTourItinery]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveTourItinerary(EntityTourItinerary oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[7];
                para[vCount] = new SqlParameter("@ItineraryId", SqlDbType.Int);
                para[vCount++].Value = oEntity.ItineraryId;
                para[vCount] = new SqlParameter("@TourId", SqlDbType.Int);
                para[vCount++].Value = oEntity.TourId;
                para[vCount] = new SqlParameter("@Day", SqlDbType.Int);
                para[vCount++].Value = oEntity.Day;
                para[vCount] = new SqlParameter("@Itinery", SqlDbType.NVarChar, 500);
                para[vCount++].Value = oEntity.Itinery;
                para[vCount] = new SqlParameter("@ItineryDetails", SqlDbType.NVarChar);
                para[vCount++].Value = oEntity.ItineryDetails;
                para[vCount] = new SqlParameter("@ImageId", SqlDbType.Int);
                para[vCount++].Value = oEntity.ImageId;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Int);
                para[vCount++].Value = oEntity.IsActive;

                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[TOUR].[usp_setTourItinery]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
    }
}
