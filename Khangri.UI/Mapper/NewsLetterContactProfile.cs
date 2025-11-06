using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class NewsLetterContactProfile : Profile
    {
        public NewsLetterContactProfile()
        {
            CreateMap<DataRow, EntityNewsLetterContactEmail>()
               .ForMember(d => d.NewsLetterContactId, opt => opt.MapFrom(row => GetNullValueIfNoRow<long>(row, "NewsLetterContactId")))
               .ForMember(d => d.NewsLetterContactEmail, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "NewsLetterContactEmail")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));
            CreateMap<FeatureViewModel, EntityFeature>().ReverseMap();
        }
    }
}
