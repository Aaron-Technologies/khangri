using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class TourImageProfile:Profile
    {
        public TourImageProfile()
        {
            CreateMap<DataRow, EntityTourImage>()
               .ForMember(d => d.TourImageId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "TourImageId")))
               .ForMember(d => d.TourId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "T02_TourId")))
               .ForMember(d => d.ImageId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "M20_ImageId")))
               .ForMember(d => d.TourName, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "TourName")))
               .ForMember(d => d.Caption, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Caption")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));
            CreateMap<TourImageViewModel, EntityTourImage>().ReverseMap();

        }
    }
}
