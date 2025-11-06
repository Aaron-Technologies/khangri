using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace AAA_Travels.UI.Mapper
{
    public class CMSPageProfile:Profile
    {
        public CMSPageProfile()
        {
            CreateMap<DataRow, EntityCMSPage>()
               .ForMember(d => d.CMSPageId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "CMSPageId")))
               .ForMember(d => d.Title, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Title")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));

             CreateMap<CMSPageViewModel, EntityCMSPage>().ReverseMap();
        }
    }
}
