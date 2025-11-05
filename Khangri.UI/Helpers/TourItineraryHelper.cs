using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Helpers
{
    public interface ITourItineraryHelper
    {
        List<TourItineraryViewModel> GetAllTourItinerary(int tourItineraryId, int tourId, ref string msg);
    }

    public class TourItineraryHelper : ITourItineraryHelper
    {
        private readonly IMapper _mapper;
        private readonly IBaTourItinerary _treatItinerary;
        public TourItineraryHelper(IMapper mapper, IBaTourItinerary treatItinerary)
        {

            _mapper = mapper;
            _treatItinerary = treatItinerary;
        }

        public List<TourItineraryViewModel> GetAllTourItinerary(int tourItineraryId, int tourId, ref string msg)
        {
            DataSet data = null;

            var allTourItinerary = new List<TourItineraryViewModel>();
            try
            {
                data = _treatItinerary.GetTourItinerary(tourItineraryId, tourId, ref msg, "0", 0);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                var entity = _mapper.Map<List<EntityTourItinerary>>(data.Tables[1].Rows);
                allTourItinerary = _mapper.Map<List<TourItineraryViewModel>>(entity);
            }

            return allTourItinerary;
        }

    }
}
