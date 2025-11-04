using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Helpers
{
    public interface IImageHelper
    {
        List<ImageViewModel> GetAllImages(int imageId, ref string msg);
    }

    public class ImageHelper : IImageHelper
    {
        private readonly IBaImage _image;
        private readonly IMapper _mapper;

        public ImageHelper(IMapper mapper, IBaImage image)
        {
            _mapper = mapper;
            _image = image;
        }

        public List<ImageViewModel> GetAllImages(int imageId, ref string msg)
        {
            DataSet data = null;

            var allImages = new List<ImageViewModel>();
            try
            {
                data = _image.GetImages(imageId, ref msg, "0", 0);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                allImages = _mapper.Map<List<ImageViewModel>>(data.Tables[1].Rows);
            }

            return allImages;
        }

    }
}
