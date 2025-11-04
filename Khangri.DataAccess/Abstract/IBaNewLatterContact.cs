using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaNewLatterContact
    {
        DataSet GetNewLatterContact(int newsLetterContactId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveNewLatterContact(EntityNewsLetterContactEmail oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}