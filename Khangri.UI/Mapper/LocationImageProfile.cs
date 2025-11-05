using Khangri.Entities;
using Khangri.UI.Models;
using static Khangri.UI.Mapper.ConverterHelpers;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Mapper
{
    public class LocationImageProfile : Profile
    {
        public LocationImageProfile()
        {
            CreateMap<DataRow, EntityLocationImage>()
              .ForMember(d => d.LocationImageId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "LocationImageId")))
              .ForMember(d => d.ImageId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "M20_ImageId")))
              .ForMember(d => d.LocationId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "M01_LocationId")))
              .ForMember(d => d.Caption, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Caption")))
              .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));

            CreateMap<LocationImageViewModel, EntityLocationImage>().ReverseMap();
        }
    }
}
