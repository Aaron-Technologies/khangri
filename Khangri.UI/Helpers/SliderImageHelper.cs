using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Helpers
{
    public interface ISliderImageHelper
    {
        List<SliderImageViewModel> GetAllSliderImages(int sliderImageId, ref string msg);
    }

    public class SliderImageHelper : ISliderImageHelper
    {
        private readonly IBaSliderImages _image;
        private readonly IMapper _mapper;

        public SliderImageHelper(IMapper mapper, IBaSliderImages image)
        {
            _mapper = mapper;
            _image = image;
        }

        public List<SliderImageViewModel> GetAllSliderImages(int imageId, ref string msg)
        {
            DataSet data = null;

            var allImages = new List<SliderImageViewModel>();
            try
            {
                data = _image.GetSliderImages(imageId, ref msg, "0", 0);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                var entity = _mapper.Map<List<EntitySliderImage>>(data.Tables[1].Rows);
                allImages = _mapper.Map<List<SliderImageViewModel>>(entity);
            }

            return allImages;
        }

    }
}
