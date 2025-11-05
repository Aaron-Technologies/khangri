using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaTourImage : IBaTourImage
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaTourImage(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetTourImage(int tourImageId, int tourId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[2];
                para[vCount] = new SqlParameter("@TourImageId", SqlDbType.Int);
                para[vCount++].Value = tourImageId;
                para[vCount] = new SqlParameter("@TourId", SqlDbType.Int);
                para[vCount++].Value = tourId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[TOUR].[usp_getTourImage]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveTourImage(EntityTourImage oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[5];
                para[vCount] = new SqlParameter("@TourImageId", SqlDbType.Int);
                para[vCount++].Value = oEntity.TourImageId;
                para[vCount] = new SqlParameter("@TourId", SqlDbType.Int);
                para[vCount++].Value = oEntity.TourId;
                para[vCount] = new SqlParameter("@ImageId", SqlDbType.Int);
                para[vCount++].Value = oEntity.ImageId;
                para[vCount] = new SqlParameter("@Caption", SqlDbType.NVarChar, 200);
                para[vCount++].Value = oEntity.Caption;

                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[vCount++].Value = oEntity.IsActive;

                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[TOUR].[usp_setTourImage]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
    }
}
