using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class LoginUserProfile:Profile
    {
        public LoginUserProfile()
        {
            CreateMap<DataRow, EntityLoginUser>()
               .ForMember(d => d.UserId, opt => opt.MapFrom(row => GetNullValueIfNoRow<long>(row, "UserId")))
               .ForMember(d => d.U00_UserTypeId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "U00_UserTypeId")))
               .ForMember(d => d.UserType, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "UserType")))
               .ForMember(d => d.FirstName, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "FirstName")))
               .ForMember(d => d.LastName, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "LastName")))
               .ForMember(d => d.Email, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Email")))
               .ForMember(d => d.Mobile, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Mobile")))
               .ForMember(d => d.EntryDateTime, opt => opt.MapFrom(row => GetNullValueIfNoRow<DateTime>(row, "EntryDateTime")));
        }
    }
}
