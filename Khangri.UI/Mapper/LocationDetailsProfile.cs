using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class LocationDetailsProfile : Profile
    {
        public LocationDetailsProfile()
        {
            CreateMap<DataRow, LocationViewModel>()
               .ForMember(d => d.LocationId, opt => opt.MapFrom(row => GetNullValueIfNoRow<long>(row, "LocationId")))
               .ForMember(d => d.Location, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Location")))
               .ForMember(d => d.Caption, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Caption")))
               .ForMember(d => d.M01_LocationId, opt => opt.MapFrom(row => GetNullValueIfNoRow<long>(row, "M01_LocationId")))
               .ForMember(d => d.ParentLocation, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "ParentLocation")))
               .ForMember(d => d.Description, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Description")))
               .ForMember(d => d.Altitude, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Altitude")))
               .ForMember(d => d.ImageFiles, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "ImageFile")))
               .ForMember(d => d.WeatherUrl, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "WeatherUrl")))
               .ForMember(d => d.GoogleMapUrl, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "GoogleMapUrl")))
               .ForMember(d => d.TripAdvisorUrl, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "TripAdvisorUrl")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));
            
            
            CreateMap<DataRow, EntitySightSeeing>()
              .ForMember(d => d.SightSeeingId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "SightSeeingId")))
              .ForMember(d => d.SightSeeing, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "SightSeeing")))
              .ForMember(d => d.EstimateTime, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "EstimateTime")))
              .ForMember(d => d.ImageFile, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "ImageFile")))
              .ForMember(d => d.Details, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Details")))
              .ForMember(d => d.Altitude, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Altitude")))
              .ForMember(d => d.GoogleMapUrl, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "GoogleMapUrl")))
              .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));

        }
    }
}
