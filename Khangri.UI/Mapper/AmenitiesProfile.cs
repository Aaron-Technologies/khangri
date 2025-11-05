using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class AmenitiesProfile : Profile
    {
        public AmenitiesProfile()
        {
            CreateMap<DataRow, EntityAmenities>()
               .ForMember(d => d.AmenitiesId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "AmenitiesId")))
               .ForMember(d => d.Amenities, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Amenities")))
               .ForMember(d => d.Description, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Description")))
               .ForMember(d => d.Icon, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Icon")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));
            //CreateMap<CounterViewModel, EntityCounter>().ReverseMap();
        }
    }
}
