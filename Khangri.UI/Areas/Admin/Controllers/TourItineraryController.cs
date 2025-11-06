using Khangri.DataAccess.KhangriDAL;
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

    public class TourItineraryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;
        private readonly ITourHelper _tourHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IBaTourItinerary _baTourItinerary;
        private readonly IBaImage _baImage;
        public TourItineraryController(IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, ITourHelper tourHelper, IBaTourItinerary baTourItinerary, IImageHelper imageHelper, IBaImage baImage)
        {
            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
            _tourHelper = tourHelper;
            _baTourItinerary = baTourItinerary;
            _imageHelper = imageHelper;
            _baImage = baImage;
        }

        #region List
        [Route("[controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var msg = "";
            var allTour = new List<EntityTourItinerary>();
            var data = _baTourItinerary.GetTourItinerary(0,0,ref msg,"0",0);
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
                allTour = _mapper.Map<List<EntityTourItinerary>>(data.Tables[1].Rows);
            }
            return View(allTour);
        }
        #endregion

        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new EntityTourItinerary();
            var msg = String.Empty;
            viewModel.IsActive = true;
            var allTour = _tourHelper.GetAllTour(0,0, ref msg);
            var allImages = _imageHelper.GetAllImages(0, ref msg);
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
            var onlyActive = false;
            var tourItems = allTour.Where(s => (onlyActive ? s.IsActive : true)).Select(s => new SelectListItem(s.TourName, s.TourId.ToString())).ToList();
            tourItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllTour = tourItems;
            var imageItems = allImages.Where(s => (onlyActive ? s.IsActive : true)).Select(s => new SelectListItem(s.ImageName, s.ImageId.ToString())).ToList();
            imageItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllImages = imageItems;

            return View(viewModel);
        }

        [Route("[controller]/Add")]
        [HttpPost]
        public IActionResult Add(EntityTourItinerary viewModel)
        {
            var msg = String.Empty;
            var allTour = _tourHelper.GetAllTour(0, 0, ref msg);
            var allImages = _imageHelper.GetAllImages(0, ref msg);
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }
           
             msg = String.Empty;
            try
            {

                var ds = _baTourItinerary.SaveTourItinerary(viewModel, ref msg, "0", 0);
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

            var onlyActive = false;
            var tourItems = allTour.Where(s => (onlyActive ? s.IsActive : true)).Select(s => new SelectListItem(s.TourName, s.TourId.ToString())).ToList();
            tourItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllTour = tourItems;
            var imageItems = allImages.Where(s => (onlyActive ? s.IsActive : true)).Select(s => new SelectListItem(s.ImageName, s.ImageId.ToString())).ToList();
            imageItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllImages = imageItems;

            return View();
        }
        #endregion

        #region Update
        [Route("[controller]/Edit")]
        [HttpGet]
        public IActionResult Edit(int itineraryId, int tourId)
        {
            var viewModel = new EntityTourItinerary();
            var msg = String.Empty;
            viewModel.IsActive = true;
            var allTour = _tourHelper.GetAllTour(0,0, ref msg);
            var allImages = _imageHelper.GetAllImages(0, ref msg);
            var data = _baTourItinerary.GetTourItinerary(itineraryId, tourId, ref msg, "0", 0);
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
            var entity = new List<EntityTourItinerary>();
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                entity = _mapper.Map<List<EntityTourItinerary>>(data.Tables[1].Rows);
                viewModel = entity.FirstOrDefault();
            }
            var onlyActive = false;
            var tourItems = allTour.Where(s => (onlyActive ? s.IsActive : true)).Select(s => new SelectListItem(s.TourName, s.TourId.ToString())).ToList();
            tourItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllTour = tourItems;
            var imageItems = allImages.Where(s => (onlyActive ? s.IsActive : true)).Select(s => new SelectListItem(s.ImageName, s.ImageId.ToString())).ToList();
            imageItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllImages = imageItems;

            return View(viewModel);
        }

        [Route("[controller]/Edit")]
        [HttpPost]
        public IActionResult Edit(EntityTourItinerary viewModel)
        {
            var msg = String.Empty;
            var allTour = _tourHelper.GetAllTour(0, 0, ref msg);
            var allImages = _imageHelper.GetAllImages(0, ref msg);

            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }

             msg = String.Empty;
            try
            {

                var ds = _baTourItinerary.SaveTourItinerary(viewModel, ref msg, "0", 0);
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

            var onlyActive = false;
            var tourItems = allTour.Where(s => (onlyActive ? s.IsActive : true)).Select(s => new SelectListItem(s.TourName, s.TourId.ToString())).ToList();
            tourItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllTour = tourItems;
            onlyActive = false;
            var imageItems = allImages.Where(s => (onlyActive ? s.IsActive : true)).Select(s => new SelectListItem(s.ImageName, s.ImageId.ToString())).ToList();
            imageItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllImages = imageItems;


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
            var data = _baImage.GetImages(imageId, ref msg, "0", 0);

            var images = _mapper.Map<List<ImageViewModel>>(data.Tables[1].Rows);

            if (!images.Any() || String.IsNullOrWhiteSpace(images.First().ImageName))
            {

                return DefaultShipTypeImage();
            }
            else
            {
                var imageFile = Path.Combine(_miscSettings.ImagesPath, images.First().ImageName);
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
