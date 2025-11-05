using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Helpers
{
    public interface IContactHelper
    {
        List<EntityContact> GetAllContact(int contactId, ref string msg);
    }

    public class ContactHelper : IContactHelper
    {
        private readonly IMapper _mapper;
        private readonly IBaContact _baContact;
        public ContactHelper(IMapper mapper, IBaContact baContact)
        {
            _mapper = mapper;
            _baContact = baContact;
        }

        public List<EntityContact> GetAllContact(int contactId, ref string msg)
        {
            DataSet data = null;

            var AllContact = new List<EntityContact>();
            try
            {
                data = _baContact.GetContact(contactId, ref msg, "0", 0);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                AllContact = _mapper.Map<List<EntityContact>>(data.Tables[1].Rows);
            }

            return AllContact;
        }

    }
}
