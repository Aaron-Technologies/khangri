using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class WebTourListProfile:Profile
    {
        public WebTourListProfile()
        {
            CreateMap<DataRow, TourViewModel>()
               .ForMember(d => d.TourId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "TourId")))
               .ForMember(d => d.TourTypeId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "T01_TourTypeId")))
               .ForMember(d => d.TourName, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "TourName")))
               .ForMember(d => d.TourInroduction, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "TourInroduction")))
               .ForMember(d => d.TourHighlight, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "TourHighlight")))
               .ForMember(d => d.TourOverview, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "TourOverview")))
               .ForMember(d => d.NoOfDays, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "NoOfDays")))
               .ForMember(d => d.Price, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Price")))
               .ForMember(d => d.M20_ImageId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "M20_ImageId")))
               .ForMember(d => d.ImageName, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "ImageName")))
               .ForMember(d => d.Caption, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Caption")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));

            CreateMap<TourViewModel, EntityTour>().ReverseMap();
        }
    }
}
