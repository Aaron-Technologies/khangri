using Khangri.Entities;
using Khangri.UI.Models;
using AutoMapper;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;

namespace Khangri.UI.Mapper
{
    public class ResetPasswordProfile : Profile
    {
        public ResetPasswordProfile()
        {
            CreateMap<EntityResetPasswordInitiativeRequest, ResetPasswordInitiativeRequestViewModel>().ReverseMap();
            CreateMap<DataRow, EntityResetPasswordInitiativeResponse>()
               .ForMember(d => d.UserId, opt => opt.MapFrom(row => GetNullValueIfNoRow<long>(row, "UserId")))
               .ForMember(d => d.Email, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Email")))
               .ForMember(d => d.Mobile, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Mobile")))
               .ForMember(d => d.Token, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Token")));

        }
    }
}
