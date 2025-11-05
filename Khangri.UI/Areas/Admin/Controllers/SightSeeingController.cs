using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using Khangri.UI.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Data;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace Khangri.UI.Controllers
{
    [Area("Admin")]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class SightSeeingController : Controller
    {
        private readonly IBaSightSeeing _baSightSeeing;
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;

        public SightSeeingController(IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, IBaSightSeeing baSightSeeing)
        {
            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
            _baSightSeeing = baSightSeeing;
        }

        #region List
        [Route("[controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var msg = "";
            var allSights = new List<EntitySightSeeing>();
            DataSet data = _baSightSeeing.GetSeightSeen(0, ref msg, "0", 0);
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
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                allSights = _mapper.Map<List<EntitySightSeeing>>(data.Tables[1].Rows);
            }
            return View(allSights);
        }
        #endregion

        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new SightSeeingViewModel();
            viewModel.IsActive = true;

            return View(viewModel);
        }

        [Route("[controller]/Add")]
        [HttpPost]
        public IActionResult Add(SightSeeingViewModel viewModel)
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

                if (!Directory.Exists(_miscSettings.SightSeeingImagePath))
                {
                    Directory.CreateDirectory(_miscSettings.SightSeeingImagePath);
                }
                var destFilePath = Path.Combine(_miscSettings.SightSeeingImagePath, fileName);
                using (var istream = file.OpenReadStream())
                {
                    using (var ostream = System.IO.File.OpenWrite(destFilePath))
                    {
                        istream.CopyTo(ostream);
                    }
                }
                viewModel.ImageFile = Path.GetFileName(destFilePath);
            }
            else
            {
                viewModel.ImageFile = String.Empty;
            }
            var msg = String.Empty;
            try
            {
                var data = _mapper.Map<EntitySightSeeing>(viewModel);
                var ds = _baSightSeeing.SaveSeightSeen(data, ref msg, "0", 0);
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
            return View();
        }
        #endregion

        #region Update
        [Route("[controller]/Edit")]
        [HttpGet]
        public IActionResult Edit(int sightSeeingId)
        {   
            var msg=String.Empty;
            var sightSeeing = _baSightSeeing.GetSeightSeen(sightSeeingId, ref msg, "0", 0);
            var allSightSeeing = _mapper.Map<List<EntitySightSeeing>>(sightSeeing.Tables[1].Rows);

            if (!allSightSeeing.Any())
            {
                ViewBag.NoLocationFound = true;
            }
            else
            {
                ViewBag.NoLocationFound = false;
            }
             var viewModel =  allSightSeeing.FirstOrDefault();
            return View(viewModel);
        }

        [Route("[controller]/Edit")]
        [HttpPost]
        public IActionResult Edit(SightSeeingViewModel viewModel)
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

                if (!Directory.Exists(_miscSettings.SightSeeingImagePath))
                {
                    Directory.CreateDirectory(_miscSettings.SightSeeingImagePath);
                }
                var destFilePath = Path.Combine(_miscSettings.SightSeeingImagePath, fileName);
                using (var istream = file.OpenReadStream())
                {
                    using (var ostream = System.IO.File.OpenWrite(destFilePath))
                    {
                        istream.CopyTo(ostream);
                    }
                }
                viewModel.ImageFile = Path.GetFileName(destFilePath);
            }
            else
            {
                viewModel.ImageFile = String.Empty;
            }
            var msg = String.Empty;
            try
            {
                var data = _mapper.Map<EntitySightSeeing>(viewModel);
                var ds = _baSightSeeing.SaveSeightSeen(data, ref msg, "0", 0);
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
            return View();
        }
        #endregion

        [AllowAnonymous]
        #region Image

        [Route("[controller]/image")]
        [HttpGet]
        public IActionResult Image(int imageId)
        {
            var msg = "";
            var data = _baSightSeeing.GetSeightSeen(imageId, ref msg, "0", 0);

            var images = _mapper.Map<List<EntitySightSeeing>>(data.Tables[1].Rows);

            if (!images.Any() || String.IsNullOrWhiteSpace(images.First().ImageFile))
            {

                return DefaultShipTypeImage();
            }
            else
            {
                var imageFile = Path.Combine(_miscSettings.SightSeeingImagePath, images.First().ImageFile);
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
