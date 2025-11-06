using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaUserType
    {
        DataSet GetUserType(int userTypeId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveUserType(EntityUserType oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}