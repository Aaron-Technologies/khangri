using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Khangri.DataAccess.KhangriDAL
{
    public class BaSliderImages : IBaSliderImages
    {
        private readonly ISqlHelper sqlHelper;
        private Int16 vCount = 0;
        private DataSet ds = null;
        private SqlParameter[] para = null;
        public BaSliderImages(ISqlHelper sqlHelper)
        {
            this.sqlHelper = sqlHelper;
        }

        public DataSet GetSliderImages(int sliderImageId, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[1];
                para[vCount] = new SqlParameter("@SliderImageId", SqlDbType.Int);
                para[vCount++].Value = sliderImageId;
                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_getSliderImage]", CommandType.StoredProcedure, para, ref pMsg);

            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;

        }
        public DataSet SaveSliderImage(EntitySliderImage oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key)
        {
            try
            {
                vCount = 0;
                para = new SqlParameter[9];
                para[vCount] = new SqlParameter("@SliderImageId", SqlDbType.Int);
                para[vCount++].Value = oEntity.SliderImageId;
                para[vCount] = new SqlParameter("@PageTypeId", SqlDbType.Int);
                para[vCount++].Value = oEntity.PageTypeId;
                para[vCount] = new SqlParameter("@ImageName", SqlDbType.NVarChar, 50);
                para[vCount++].Value = oEntity.ImageName;
                para[vCount] = new SqlParameter("@Caption", SqlDbType.NVarChar, 200);
                para[vCount++].Value = oEntity.Caption;
                para[vCount] = new SqlParameter("@Details1", SqlDbType.NVarChar, 500);
                para[vCount++].Value = oEntity.Details1;
                para[vCount] = new SqlParameter("@Details2", SqlDbType.NVarChar, 500);
                para[vCount++].Value = oEntity.Details2;
                para[vCount] = new SqlParameter("@Link1", SqlDbType.NVarChar, 200);
                para[vCount++].Value = oEntity.Link1;
                para[vCount] = new SqlParameter("@Link2", SqlDbType.NVarChar, 200);
                para[vCount++].Value = oEntity.Link2;
                para[vCount] = new SqlParameter("@IsActive", SqlDbType.Bit);
                para[vCount++].Value = oEntity.IsActive;

                ds = sqlHelper.GetDataSet(pAccYr, pCompany_key, "[MTR].[usp_setSliderImage]", CommandType.StoredProcedure, para, ref pMsg);
            }
            catch (Exception ex)
            {
                pMsg = ex.Message;
            }
            return ds;
        }
    }
}
