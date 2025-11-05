using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class TeamMemberProfile:Profile
    {
        public TeamMemberProfile()
        {
            CreateMap<DataRow, EntityTeamMember>()
               .ForMember(d => d.TeamMemberId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "TeamMemberId")))
               .ForMember(d => d.Name, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Name")))
               .ForMember(d => d.ImageId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "ImageId")))
               .ForMember(d => d.Caption, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Caption")))
               .ForMember(d => d.Designation, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Designation")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));
        }
    }
}
