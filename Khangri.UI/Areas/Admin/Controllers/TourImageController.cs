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

    public class TourImageController : Controller
    {
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;
        private readonly ITourHelper _tourHelper;
        private readonly IBaTourImage _baTourImage;
        private readonly IImageHelper _imageHelper;
        private readonly IBaImage  _baImage;
        public TourImageController(IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, ITourHelper tourHelper, IBaTourImage baTourImage, IImageHelper imageHelper, IBaImage baImage)
        {
            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
            _tourHelper = tourHelper;
            _baTourImage = baTourImage;
            _imageHelper = imageHelper;
            _baImage = baImage;
        }

        #region List
        [Route("[controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var msg = "";
            var allTour = new List<EntityTourImage>();
            var data = _baTourImage.GetTourImage(0, 0, ref msg, "0", 0);
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
                allTour = _mapper.Map<List<EntityTourImage>>(data.Tables[1].Rows);
            }
            return View(allTour);
        }
        #endregion

        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new EntityTourImage();
            var msg = String.Empty;
            viewModel.IsActive = true;
            var allTour = _tourHelper.GetAllTour(0, 0, ref msg);
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
        public IActionResult Add(EntityTourImage viewModel)
        {
            var allTour = new List<EntityTour>();
            var allImages = new List<ImageViewModel>();
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }

            var msg = String.Empty;
            try
            {

                var ds = _baTourImage.SaveTourImage(viewModel, ref msg, "0", 0);
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
        public IActionResult Edit( int tourImageId, int tourId)
        {
            var viewModel = new EntityTourImage();
            var msg = String.Empty;
            viewModel.IsActive = true;
            var allTour = _tourHelper.GetAllTour(0, 0, ref msg);
            var allImages = _imageHelper.GetAllImages(0, ref msg);
            var data = _baTourImage.GetTourImage(tourImageId, tourId, ref msg,"0",0);
            var allTourImage = _mapper.Map<List<EntityTourImage>>(data.Tables[1].Rows);
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
            viewModel=_mapper.Map<EntityTourImage>(allTourImage.FirstOrDefault());
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
        public IActionResult Edit(EntityTourImage viewModel)
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

                var ds = _baTourImage.SaveTourImage(viewModel, ref msg, "0", 0);
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



            return View(viewModel);
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
