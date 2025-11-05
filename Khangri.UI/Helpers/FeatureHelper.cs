using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Helpers
{
    public interface IFeatureHelper
    {
        List<EntityFeature> GetAllFeature(int featureId, ref string msg);
    }

    public class FeatureHelper : IFeatureHelper
    {
        private readonly IBaFeature _baFeature;
        private readonly IMapper _mapper;

        public FeatureHelper(IMapper mapper, IBaFeature baFeature)
        {
            _mapper = mapper;
            _baFeature = baFeature;
        }

        public List<EntityFeature> GetAllFeature(int featureId, ref string msg)
        {
            DataSet data = null;

            var AllFeature = new List<EntityFeature>();
            try
            {
                data = _baFeature.GetFeature(featureId, ref msg, "0", 0);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                AllFeature = _mapper.Map<List<EntityFeature>>(data.Tables[1].Rows);
            }

            return AllFeature;
        }

    }
}
