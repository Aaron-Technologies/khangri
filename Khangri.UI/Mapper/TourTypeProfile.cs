using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class TourTypeProfile:Profile
    {
        public TourTypeProfile()
        {
            CreateMap<DataRow, EntityTourType>()
               .ForMember(d => d.TourTypeId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "TourTypeId")))
               .ForMember(d => d.TourType, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "TourType")))
               .ForMember(d => d.Description, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Description")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));

            // CreateMap<LocationsViewModel, EntityLocation>().ReverseMap();
        }
    }
}
