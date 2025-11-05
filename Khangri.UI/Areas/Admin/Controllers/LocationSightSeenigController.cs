using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Helpers;
using Khangri.UI.Models;
using Khangri.UI.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Khangri.UI.Controllers
{
    [Area("Admin")]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class LocationSightSeenigController : Controller
    {
        private readonly IBaLocationSightSeeing _baLocationSightSeeing;
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;
        private readonly ILocationHelper _locationHelper;
        private readonly IBaSightSeeing _baSightSeeing;

        public LocationSightSeenigController(IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, ILocationHelper locationHelper, IBaLocationSightSeeing baLocationSightSeeing, IBaSightSeeing baSightSeeing)
        {
            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
            _locationHelper = locationHelper;
            _baLocationSightSeeing = baLocationSightSeeing;
            _baSightSeeing = baSightSeeing;
        }

        #region List
        [Route("[controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var msg = "";
            var locationSightSeeings=new List<EntityLocationSightSeeing>();
            var data = _baLocationSightSeeing.GetLocationSightSeeing(0, ref msg, "0", 0);
            if (!String.IsNullOrWhiteSpace(msg))
            {
                var status = new ActionStatus()
                {
                    Status = Constants.StatusFail,
                    Title = "Error",
                    Description = msg
                };
                ViewBag.Status = JsonSerializer.Serialize(status);
            }
            if (TempData["DeleteStatus"] != null)
            {
                ViewBag.Status = TempData["DeleteStatus"];
            }
            if(data != null && data.Tables!=null && data.Tables.Count>0) 
            {
                locationSightSeeings = _mapper.Map<List<EntityLocationSightSeeing>>(data.Tables[1].Rows);
            }
            return View(locationSightSeeings);
        }
        #endregion
        
        
        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new EntityLocationSightSeeing();
            var msg=String.Empty;
            viewModel.IsActive = true;
            var allLocation = _locationHelper.GetAllLocationViewModels(0,ref msg);
            var data = _baSightSeeing.GetSeightSeen(0, ref msg, "0", 0);
            var allSightSeen = _mapper.Map<List<EntitySightSeeing>>(data.Tables[1].Rows);
            if (!String.IsNullOrWhiteSpace(msg))
            {
                var status = new ActionStatus()
                {
                    Status = Constants.StatusFail,
                    Title = "Error",
                    Description = msg

                };
                ViewBag.Status = JsonSerializer.Serialize(status);
            }
            var locationItems = allLocation.Select(s => new SelectListItem(s.Location, s.LocationId.ToString())).ToList();
            locationItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllLocations = locationItems;
            var sightSeenItems = allSightSeen.Select(s => new SelectListItem(s.SightSeeing,s.SightSeeingId.ToString())).ToList();
            sightSeenItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllSightSeen = sightSeenItems;
            return View(viewModel);
        }

        [Route("[controller]/Add")]
        [HttpPost]
        public IActionResult Add(EntityLocationSightSeeing viewModel)
        {
            var msg = String.Empty;
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }
            try
            {

                var ds = _baLocationSightSeeing.SaveLocationSightSeeing(viewModel, ref msg, "0", 0);
                if (String.IsNullOrWhiteSpace(msg))
                {
                    msg = _utilities.GetErrorMessageFromDataSet(ds);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (!String.IsNullOrWhiteSpace(msg))
            {
                var status = new ActionStatus()
                {
                    Status = Constants.StatusFail,
                    Title = "Error",
                    Description = msg

                };
                ViewBag.Status = JsonSerializer.Serialize(status);
            }
            else
            {
                var status = new ActionStatus()
                {
                    Status = Constants.StatusSuccess,
                    Title = Constants.StatusSuccess,
                    Description = Constants.SaveSuccess
                };
                ViewBag.Status = JsonSerializer.Serialize(status);
            }
            var allLocation = new List<EntityLocation>();
            var allSightSeen=new List<EntitySightSeeing>();
            if (!String.IsNullOrWhiteSpace(msg))
            {
                var status = new ActionStatus()
                {
                    Status = Constants.StatusFail,
                    Title = "Error",
                    Description = msg
                };
                ViewBag.Status = JsonSerializer.Serialize(status);
            }
            var locationItems = allLocation.Select(s => new SelectListItem(s.Location, s.LocationId.ToString())).ToList();
            locationItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllLocations = locationItems;
            var sightSeenigItems = allSightSeen.Select(s => new SelectListItem(s.SightSeeing, s.SightSeeingId.ToString())).ToList();
            sightSeenigItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllSightSeen=sightSeenigItems;
            return View();
        }
        #endregion

        #region Update
        [Route("[controller]/Edit")]
        [HttpGet]
        public IActionResult Edit(int locationSightSeenigId)
        {

            var msg = String.Empty;
            var viewModel = new EntityLocationSightSeeing();
            var location = _locationHelper.GetAllLocationViewModels(0, ref msg);
            var data = _baSightSeeing.GetSeightSeen(0, ref msg, "0", 0);
            var allSightSeen = _mapper.Map<List<EntitySightSeeing>>(data.Tables[1].Rows);
           
            
            var locationSigthSeenig = _baLocationSightSeeing.GetLocationSightSeeing(locationSightSeenigId, ref msg, "0", 0);
            var allLocationSightSeenig = _mapper.Map<List<EntityLocationSightSeeing>>(locationSigthSeenig.Tables[1].Rows);
            if (!allLocationSightSeenig.Any())
            {
                ViewBag.NoLocationFound = true;
            }
            else
            {
                ViewBag.NoLocationFound = false;
            }
             viewModel = allLocationSightSeenig.FirstOrDefault();

            var locationItems = location.Select(s => new SelectListItem(s.Location, s.LocationId.ToString())).ToList();
            locationItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllLocations = locationItems;
            var sightSeenigItems = allSightSeen.Select(s => new SelectListItem(s.SightSeeing, s.SightSeeingId.ToString())).ToList();
            sightSeenigItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllSightSeen = sightSeenigItems;
            return View(viewModel);
        }

        [Route("[controller]/Edit")]
        [HttpPost]
        public IActionResult Edit(EntityLocationSightSeeing viewModel)
        {
            var msg = String.Empty;
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }
            try
            {

                var ds = _baLocationSightSeeing.SaveLocationSightSeeing(viewModel, ref msg, "0", 0);
                if (String.IsNullOrWhiteSpace(msg))
                {
                    msg = _utilities.GetErrorMessageFromDataSet(ds);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (!String.IsNullOrWhiteSpace(msg))
            {
                var status = new ActionStatus()
                {
                    Status = Constants.StatusFail,
                    Title = "Error",
                    Description = msg

                };
                ViewBag.Status = JsonSerializer.Serialize(status);
            }
            else
            {
                var status = new ActionStatus()
                {
                    Status = Constants.StatusSuccess,
                    Title = Constants.StatusSuccess,
                    Description = Constants.SaveSuccess
                };
                ViewBag.Status = JsonSerializer.Serialize(status);
            }
            var allLocation = new List<EntityLocation>();
            var allSightSeen = new List<EntitySightSeeing>();
            if (!String.IsNullOrWhiteSpace(msg))
            {
                var status = new ActionStatus()
                {
                    Status = Constants.StatusFail,
                    Title = "Error",
                    Description = msg
                };
                ViewBag.Status = JsonSerializer.Serialize(status);
            }
            var locationItems = allLocation.Select(s => new SelectListItem(s.Location, s.LocationId.ToString())).ToList();
            locationItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllLocations = locationItems;
            var sightSeenigItems = allSightSeen.Select(s => new SelectListItem(s.SightSeeing, s.SightSeeingId.ToString())).ToList();
            sightSeenigItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllSightSeen = sightSeenigItems;
            return View();
        }
        #endregion

     
    }
}
