using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaCMSPage
    {
        DataSet GetCMSPage(int CMSPageId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveCMSPage(EntityCMSPage oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}