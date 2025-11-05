using AutoMapper;
using Khangri.Entities;
using static Khangri.UI.Mapper.ConverterHelpers;
using System.Data;
using Khangri.UI.Models;

namespace AAA_Travels.UI.Mapper
{
    public class ContactProfile:Profile
    {
        public ContactProfile()
        {
            CreateMap<DataRow, EntityContact>()
               .ForMember(d => d.ContactId, opt => opt.MapFrom(row => GetNullValueIfNoRow<int>(row, "ContactId")))
               .ForMember(d => d.Header, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Header")))
               .ForMember(d => d.Phone, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Phone")))
               .ForMember(d => d.Email, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Email")))
               .ForMember(d => d.Address, opt => opt.MapFrom(row => GetNullValueIfNoRow(row, "Address")))
               .ForMember(d => d.IsActive, opt => opt.MapFrom(row => GetNullValueIfNoRow<bool>(row, "IsActive")));
            
            CreateMap<ContactViewModel, EntityContact>().ReverseMap();
        }
    }
}
