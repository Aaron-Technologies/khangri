using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaUser
    {
        DataSet GetUsers(long userId, int userTypeId, int pageNo, int pageSize, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveUser(EntityUser oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet ChangePassword(EntityChangePassword oEntity, ref String pMsg, String pAccYr, Int16? pCompany_key);
        DataSet Login(string LoginName, string Password, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet ResetPassword(EntityUserPasswordResetRequest oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet InitiateResetPassword(EntityResetPasswordInitiativeRequest oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}