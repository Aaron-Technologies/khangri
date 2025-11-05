using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Helpers
{
    public interface ITourImageHelper
    {
        List<TourImageViewModel> GetAllImages(int tourImageId, int tourId, ref string msg);
    }

    public class TourImageHelper : ITourImageHelper
    {
        private readonly IBaTourImage _baTourImage;
        private readonly IMapper _mapper;

        public TourImageHelper(IMapper mapper, IBaTourImage baTourImage)
        {
            _mapper = mapper;
            _baTourImage = baTourImage;
        }

        public List<TourImageViewModel> GetAllImages(int tourImageId, int tourId, ref string msg)
        {
            DataSet data = null;

            var allTourImages = new List<TourImageViewModel>();
            try
            {
                data = _baTourImage.GetTourImage(tourImageId, tourId, ref msg, "0", 0);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                var entity = _mapper.Map<List<EntityTourImage>>(data.Tables[1].Rows);
                allTourImages = _mapper.Map<List<TourImageViewModel>>(entity);
            }

            return allTourImages;
        }

    }
}
