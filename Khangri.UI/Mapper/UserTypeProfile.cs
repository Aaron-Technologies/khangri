using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class UserTypeProfile:Profile
    {
        public UserTypeProfile()
        {
            CreateMap<DataRow, EntityUserType>()
               .ForMember(d => d.UserTypeId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "UserTypeId")))
               .ForMember(d => d.UserType, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "UserType")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));
        }
    }
}
