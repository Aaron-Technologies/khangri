using Khangri.Entities;
using Khangri.UI.Models;
using static Khangri.UI.Mapper.ConverterHelpers;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Mapper
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<DataRow, ImageViewModel>()
              .ForMember(d => d.ImageId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "ImageId")))
              .ForMember(d => d.ImageName, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "ImageName")))
              .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));

            CreateMap<ImageViewModel,EntityImage>().ReverseMap();
        }
    }
}
