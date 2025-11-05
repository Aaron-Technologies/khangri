using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class CounterProfile:Profile
    {
        public CounterProfile()
        {
            CreateMap<DataRow, EntityCounter>()
               .ForMember(d => d.CounterId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "CounterId")))
               .ForMember(d => d.Title, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Title")))
               .ForMember(d => d.Value, opt => opt.MapFrom(row => GetNullValueIfNoRow<long>(row, "Value")))
               .ForMember(d => d.Description, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Description")))
               .ForMember(d => d.Icon, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Icon")))
               .ForMember(d => d.Link, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Link")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));
            CreateMap<CounterViewModel, EntityCounter>().ReverseMap();
        }
    }
}
