using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class FeatureProfile : Profile
    {
        public FeatureProfile()
        {
            CreateMap<DataRow, EntityFeature>()
               .ForMember(d => d.FeatureId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "FeatureId")))
               .ForMember(d => d.Title, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Title")))
               .ForMember(d => d.Description, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Description")))
               .ForMember(d => d.Icon, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Icon")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));
            CreateMap<FeatureViewModel, EntityFeature>().ReverseMap();
        }
    }
}
