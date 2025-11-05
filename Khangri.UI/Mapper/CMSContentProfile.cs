using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace Khangri.UI.Mapper
{
    public class CMSContentProfile:Profile
    {
        public CMSContentProfile()
        {
            CreateMap<DataRow, EntityCMSContent>()
               .ForMember(d => d.CMSPageId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "CMSPageId")))
               .ForMember(d => d.Content, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Content")));

             CreateMap<CMSContentViewModel, EntityCMSContent>().ReverseMap();
        }
    }
}
