using Khangri.Entities;
using System.Data;

namespace Khangri.DataAccess.Abstract
{
    public interface IBaTourItinerary
    {
        DataSet GetTourItinerary(int itineraryId, int tourId, ref string pMsg, string pAccYr, short? pCompany_key);
        DataSet SaveTourItinerary(EntityTourItinerary oEntity, ref string pMsg, string pAccYr, short? pCompany_key);
    }
}