using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Helpers
{
    public interface ITourTypeHelper
    {
        List<EntityTourType> GetAllTourType(int tourTypeId, ref string msg);
    }

    public class TourTypeHelper : ITourTypeHelper
    {
        private readonly IBaTourType _tourType;
        private readonly IMapper _mapper;

        public TourTypeHelper(IMapper mapper, IBaTourType tourType)
        {
            _mapper = mapper;
            _tourType = tourType;
        }

        public List<EntityTourType> GetAllTourType(int tourTypeId, ref string msg)
        {
            DataSet data = null;

            var allTourTypes = new List<EntityTourType>();
            try
            {
                data = _tourType.GetTourType(tourTypeId, ref msg, "0", 0);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                allTourTypes = _mapper.Map<List<EntityTourType>>(data.Tables[1].Rows);
            }

            return allTourTypes;
        }

    }
}
