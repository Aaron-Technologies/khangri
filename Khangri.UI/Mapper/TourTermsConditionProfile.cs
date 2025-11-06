using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class TourTermsConditionProfile : Profile
    {
        public TourTermsConditionProfile()
        {
            CreateMap<DataRow, TourTermsConditionViewModel>()
               .ForMember(d => d.TourTermsConditionId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "TourTermsConditionId")))
               .ForMember(d => d.TourTypeId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "TourTypeId")))
               .ForMember(d => d.TourType, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "TourType")))
               .ForMember(d => d.Accommodation, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Accommodation")))
               .ForMember(d => d.Food, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Food")))
               .ForMember(d => d.TermsConditionsPermits, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "TermsConditionsPermits")))
               .ForMember(d => d.MiscellaneousExpenses, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "MiscellaneousExpenses")))
               .ForMember(d => d.TermsConditionsGeneral, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "TermsConditionsGeneral")))
               .ForMember(d => d.CancellationPolicy, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "CancellationPolicy")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));
            
            CreateMap<EntityTourTermsCondition, TourTermsConditionViewModel>().ReverseMap();

        }
    }
}
