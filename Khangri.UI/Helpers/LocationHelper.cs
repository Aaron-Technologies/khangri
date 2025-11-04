using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using AutoMapper;
using System.Data;

namespace Khangri.UI.Helpers
{
    public interface ILocationHelper
    {
        List<LocationViewModel> GetAllLocationViewModels(long locationId,ref string msg);
    }

    public class LocationHelper : ILocationHelper
    {
        private readonly IBaLocation _location;
        private readonly IMapper _mapper;

        public LocationHelper(IBaLocation location, IMapper mapper)
        {
            _location = location;
            _mapper = mapper;
        }

        public List<LocationViewModel> GetAllLocationViewModels(long locationId,ref string msg)
        {
            DataSet data = null;

            var allLocations = new List<LocationViewModel>();
            try
            {
                data = _location.GetLocations(locationId, ref msg, "0", 0);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                allLocations = _mapper.Map<List<LocationViewModel>>(data.Tables[1].Rows);
            }

            return allLocations;
        }

    }
}
