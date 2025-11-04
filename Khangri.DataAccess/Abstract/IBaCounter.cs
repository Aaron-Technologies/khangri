using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaCounter
    {
        DataSet GetCounter(int counterId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveCounter(EntityCounter oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}