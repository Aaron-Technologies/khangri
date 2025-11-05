using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaContact
    {
        DataSet GetContact(int contactId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveContact(EntityContact oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}