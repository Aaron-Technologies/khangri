using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class SightSeeingProfile : Profile
    {
        public SightSeeingProfile()
        {
            CreateMap<DataRow, EntitySightSeeing>()
               .ForMember(d => d.SightSeeingId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "SightSeeingId")))
               .ForMember(d => d.SightSeeing, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "SightSeeing")))
               .ForMember(d => d.EstimateTime, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "EstimateTime")))
               .ForMember(d => d.ImageFile, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "ImageFile")))
               .ForMember(d => d.Details, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Details")))
               .ForMember(d => d.Altitude, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Altitude")))
               .ForMember(d => d.GoogleMapUrl, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "GoogleMapUrl")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));

            CreateMap<SightSeeingViewModel, EntitySightSeeing>().ReverseMap();
        }
    }
}
