using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaSetUp
    {
        DataSet GetSetUp(int setUpId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveSetUp(EntitySetup oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}