using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class SetUpProfile:Profile
    {
        public SetUpProfile()
        {
            CreateMap<DataRow, EntitySetup>()
               .ForMember(d => d.SetUpId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "SetUpId")))
               .ForMember(d => d.CompanyName, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "CompanyName")))
               .ForMember(d => d.CompanyIntroduction, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "CompanyIntroduction")))
               .ForMember(d => d.Caption, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Caption")))
               .ForMember(d => d.LogoPrimary, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "LogoPrimary")))
               .ForMember(d => d.LogoSecondary, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "LogoSecondary")))
               .ForMember(d => d.PhoneNoPrimary, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "PhoneNoPrimary")))
               .ForMember(d => d.WhatsAppPrimary, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "WhatsAppPrimary")))
               .ForMember(d => d.EmailPrimary, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "EmailPrimary")))
               .ForMember(d => d.GoogleReviewUrl, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "GoogleReviewUrl")))
               .ForMember(d => d.FBUrl, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "FBUrl")))
               .ForMember(d => d.InstagramUrl, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "InstagramUrl")))
               .ForMember(d => d.TwiterUrl, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "TwiterUrl")))
               .ForMember(d => d.YouTubeUrl, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "YouTubeUrl")))
               .ForMember(d => d.LinkedinUrl, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "LinkedinUrl")))
               .ForMember(d => d.Favicon, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Favicon")));
            CreateMap<SetupViewModel , EntitySetup>().ReverseMap();

        }
    }
}
