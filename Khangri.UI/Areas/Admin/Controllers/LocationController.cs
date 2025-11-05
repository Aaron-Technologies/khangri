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

namespace Khangri.UI.Controllers
{
    [Area("Admin")]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class LocationController : Controller
    {
        private readonly IBaLocation _location;
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;
        private readonly ILocationHelper _locationHelper;

        public LocationController(IBaLocation location, IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, ILocationHelper locationHelper)
       {
            _location = location;
            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
            _locationHelper = locationHelper;
        }

        #region List
        [Route("[controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var msg = "";
            var locations = _locationHelper.GetAllLocationViewModels(0,ref msg);
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
            return View(locations);
        }
        #endregion
        
        
        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new LocationViewModel();
            var msg=String.Empty;
            viewModel.IsActive = true;
            var allLocation = _locationHelper.GetAllLocationViewModels(0,ref msg);
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

            return View(viewModel);
        }

        [Route("[controller]/Add")]
        [HttpPost]
        public IActionResult Add(LocationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }
            var files = Request.Form.Files;
            if (files != null && files.Any())
            {
                var file = files.First();
                var now = DateTime.Now;
                var fileName = String.Format(_miscSettings.ImageFilenameFormat,
                    now.ToString(_miscSettings.TimeStampFormat),
                    Path.GetExtension(file.FileName));

                if (!Directory.Exists(_miscSettings.LocationsImagePath))
                {
                    Directory.CreateDirectory(_miscSettings.LocationsImagePath);
                }
                var destFilePath = Path.Combine(_miscSettings.LocationsImagePath, fileName);
                using (var istream = file.OpenReadStream())
                {
                    using (var ostream = System.IO.File.OpenWrite(destFilePath))
                    {
                        istream.CopyTo(ostream);
                    }
                }
                viewModel.ImageFiles = Path.GetFileName(destFilePath);
            }
            else
            {
                viewModel.ImageFiles = String.Empty;
            }
           
            var msg = String.Empty;
            try
            {
                var data = _mapper.Map<EntityLocation>(viewModel);
                var ds = _location.SaveLocation(data, ref msg, "0", 0);
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
            var allLocation = new List<LocationViewModel>();
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

            return View();
        }
		#endregion

		#region Update
		[Route("[controller]/Edit")]
		[HttpGet]
		public IActionResult Edit(long locationId)
		{
			
			var msg = String.Empty;
            var location = _locationHelper.GetAllLocationViewModels(locationId,ref msg);
			if (!location.Any())
			{
				ViewBag.NoLocationFound = true;
			}
			else
			{
				ViewBag.NoLocationFound = false;
			}
            var viewModel = _mapper.Map<LocationViewModel>(location.FirstOrDefault());
		
			var locationItems = location.Select(s => new SelectListItem(s.Location, s.LocationId.ToString())).ToList();
			locationItems.Insert(0, new SelectListItem("Please Select", "0"));
			ViewBag.AllLocations = locationItems;

			return View(viewModel);
		}

		[Route("[controller]/Edit")]
		[HttpPost]
		public IActionResult Edit(LocationViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(viewModel);

			}
			var files = Request.Form.Files;
			if (files != null && files.Any())
			{
				var file = files.First();
				var now = DateTime.Now;
				var fileName = String.Format(_miscSettings.ImageFilenameFormat,
					now.ToString(_miscSettings.TimeStampFormat),
					Path.GetExtension(file.FileName));

				if (!Directory.Exists(_miscSettings.LocationsImagePath))
				{
					Directory.CreateDirectory(_miscSettings.LocationsImagePath);
				}
				var destFilePath = Path.Combine(_miscSettings.LocationsImagePath, fileName);
				using (var istream = file.OpenReadStream())
				{
					using (var ostream = System.IO.File.OpenWrite(destFilePath))
					{
						istream.CopyTo(ostream);
					}
				}
				viewModel.ImageFiles = Path.GetFileName(destFilePath);
			}
            
            var msg = String.Empty;
			try
			{
                var data = _mapper.Map<EntityLocation>(viewModel);
				var ds = _location.SaveLocation(data, ref msg, "0", 0);
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
					Description = Constants.UpdateSuccess
				};
				ViewBag.Status = JsonSerializer.Serialize(status);
			}
             msg= String.Empty;
			var allLocation = _locationHelper.GetAllLocationViewModels(0,ref msg);
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

			return View(viewModel);
		}
        #endregion

        [AllowAnonymous]
		#region Image

		[Route("[controller]/image")]
        [HttpGet]
        public IActionResult Image(int locationId)
        {
            var msg = "";
            var data = _location.GetLocations(locationId, ref msg, "0", 0);

            var locations = _mapper.Map<List<LocationViewModel>>(data.Tables[1].Rows);

            if (!locations.Any() || String.IsNullOrWhiteSpace(locations.First().ImageFiles))
            {

                return DefaultShipTypeImage();
            }
            else
            {
                var imageFile = String.Empty;
               
                imageFile = Path.Combine(_miscSettings.LocationsImagePath, locations.First().ImageFiles);
                
                try
                {
                    var fileContents = System.IO.File.ReadAllBytes(imageFile);
                    return File(fileContents, Constants.GenericBinaryMimeType);
                }
                catch (Exception)
                {
                    return DefaultShipTypeImage();
                }
            }

        }

        [NonAction]
        private IActionResult DefaultShipTypeImage()
        {
            var defaultImageFile = "wwwroot/images/no-image.jpg";
            try
            {
                var fileContents = System.IO.File.ReadAllBytes(defaultImageFile);
                return File(fileContents, Constants.GenericBinaryMimeType);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        #endregion
    }
}
