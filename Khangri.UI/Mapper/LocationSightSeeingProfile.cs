using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class LocationSightSeeingProfile : Profile
    {
        public LocationSightSeeingProfile()
        {
            CreateMap<DataRow, EntityLocationSightSeeing>()
               .ForMember(d => d.LocationSightSeeingId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "LocationSightSeeingId")))
               .ForMember(d => d.LocationId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "LocationId")))
               .ForMember(d => d.Location, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Location")))
               .ForMember(d => d.SightSeeingId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "SightSeeingId")))
               .ForMember(d => d.SightSeen, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "SightSeeing")))
               .ForMember(d => d.DistanceFromLocation, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "DistanceFromLocation")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));

            CreateMap<LocationSightSeeingViewModel, EntityLocationSightSeeing>().ReverseMap();
        }
    }
}
