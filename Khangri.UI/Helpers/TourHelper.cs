using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Helpers
{
    public interface ITourHelper
    {
        List<TourViewModel> GetAllTour(int tourId, int tourTypeId, ref string msg);
    }

    public class TourHelper : ITourHelper
    {
        private readonly IBaTour _tour;
        private readonly IMapper _mapper;

        public TourHelper(IMapper mapper, IBaTour tour)
        {
            _mapper = mapper;
            _tour = tour;
        }

        public List<TourViewModel> GetAllTour(int tourId, int tourTypeId, ref string msg)
        {
            DataSet data = null;

            var allTour = new List<TourViewModel>();
            try
            {
                data = _tour.GetTour(tourId, tourTypeId, ref msg, "0", 0);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                var entity = _mapper.Map<List<EntityTour>>(data.Tables[1].Rows);
                allTour = _mapper.Map<List<TourViewModel>>(entity);
            }

            return allTour;
        }

    }
}
