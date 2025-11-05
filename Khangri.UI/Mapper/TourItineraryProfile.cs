using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class TourItineraryProfile : Profile
    {
        public TourItineraryProfile()
        {
            CreateMap<DataRow, EntityTourItinerary>()
               .ForMember(d => d.ItineraryId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "ItineraryId")))
               .ForMember(d => d.TourId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "T02_TourId")))
               .ForMember(d => d.TourName, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "TourName")))
               .ForMember(d => d.Day, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "Day")))
               .ForMember(d => d.Itinery, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Itinerary")))
               .ForMember(d => d.ItineryDetails, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "ItineraryDetails")))
               .ForMember(d => d.ImageId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "ImageId")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));
            
            CreateMap<TourItineraryViewModel, EntityTourItinerary>().ReverseMap();

        }
    }
}
