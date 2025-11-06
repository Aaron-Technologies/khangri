using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class RoomTypeProfile:Profile
    {
        public RoomTypeProfile()
        {
            CreateMap<DataRow, EntityRoomType>()
               .ForMember(d => d.RoomTypeId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "RoomTypeId")))
               .ForMember(d => d.RoomType, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "RoomType")))
               .ForMember(d => d.Description, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Description")))
               .ForMember(d => d.Price, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "Price")))
               .ForMember(d => d.ImageFile, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "ImageFile")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));
           // CreateMap<CounterViewModel, EntityCounter>().ReverseMap();
        }
    }
}
