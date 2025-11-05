using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaTourTermsCondition
    {
        DataSet GetTourTermsCondition(int tourTermsConditionId, int tourTypeId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveTourTermsCondition(EntityTourTermsCondition oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}