using Khangri.Entities;
using Khangri.UI.Models;
using static Khangri.UI.Mapper.ConverterHelpers;
using AutoMapper;
using System.Data;
namespace Khangri.UI.Mapper
{
    public class SliderImageProfile :Profile
    {
        public SliderImageProfile()
        {
            CreateMap<DataRow, EntitySliderImage>()
             .ForMember(d => d.SliderImageId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "SliderImageId")))
             .ForMember(d => d.PageTypeId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "PageTypeId")))
             .ForMember(d => d.ImageName, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "ImageName")))
             .ForMember(d => d.Caption, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Caption")))
             .ForMember(d => d.Details1, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Details1")))
             .ForMember(d => d.Details2, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Details2")))
             .ForMember(d => d.Link1, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Link1")))
             .ForMember(d => d.Link2, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Link2")))
             .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));

            CreateMap<EntitySliderImage, SliderImageViewModel>().ReverseMap();

        }
    }
}
