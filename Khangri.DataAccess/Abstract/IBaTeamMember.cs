using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaTeamMember
    {
        DataSet GetTeamMember(int teamMemberId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveTeamMember(EntityTeamMember oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}