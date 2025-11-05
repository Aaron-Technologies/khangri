using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<DataRow, EntityUser>()
               .ForMember(d => d.UserId, opt => opt.MapFrom(row => GetNullValueIfNoRow<long>(row, "UserId")))
               .ForMember(d => d.UserTypeId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "UserTypeId")))
               .ForMember(d => d.UserType, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "UserType")))
               .ForMember(d => d.FName, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "FName")))
               .ForMember(d => d.LName, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "LName")))
               .ForMember(d => d.Email, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Email")))
               .ForMember(d => d.Mobile, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Mobile")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));
            CreateMap<UserViewModel, EntityUser>().ReverseMap();
        }
    }
}
