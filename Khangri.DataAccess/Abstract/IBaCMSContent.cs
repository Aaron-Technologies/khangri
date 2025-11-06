using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaCMSContent
    {
        DataSet GetCMSContent(int CMSPageId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveCMSContent(EntityCMSContent oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}