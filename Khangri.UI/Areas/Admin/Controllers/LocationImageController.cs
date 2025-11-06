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

    public class LocationImageController : Controller
    {
        private readonly IBaLocationImage _baLocationImage;
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;
        private readonly ILocationHelper _locationHelper;
        private readonly IImageHelper _imageHelper;

        public LocationImageController(IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, ILocationHelper locationHelper, IBaLocationImage baLocationImage, IImageHelper imageHelper)
        {
            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
            _locationHelper = locationHelper;
            _baLocationImage = baLocationImage;
            _imageHelper = imageHelper;
        }

        #region List
        [Route("[controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var msg = "";
            var locationsImage=new List<EntityLocationImage>();
            var data = _baLocationImage.GetLocationImage(0, ref msg, "0", 0);
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
                locationsImage = _mapper.Map<List<EntityLocationImage>>(data.Tables[1].Rows);
            }
            return View(locationsImage);
        }
        #endregion
        
        
        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new LocationImageViewModel();
            var msg=String.Empty;
            viewModel.IsActive = true;
            var allLocation = _locationHelper.GetAllLocationViewModels(0,ref msg);
            var images = _imageHelper.GetAllImages(0, ref msg);
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
            var imageItems = images.Select(s => new ImageSelectListItem(s.ImageName, s.ImageId.ToString()) { Id=s.ImageId, ImageUrl= Url.Action("image", "Image", new {imageId=s.ImageId }) }).ToList(); 
            imageItems.Insert(0, new ImageSelectListItem("Please Select", "0") {Id = 0, ImageUrl = String.Empty });
            ViewBag.AllImages = imageItems;
            ViewBag.ImageJson = Newtonsoft.Json.JsonConvert.SerializeObject(imageItems, Newtonsoft.Json.Formatting.None);    

            return View(viewModel);
        }

        [Route("[controller]/Add")]
        [HttpPost]
        public IActionResult Add(LocationImageViewModel viewModel)
        {
            var allLocation = new List<EntityLocation>();
            var images = new List<EntityImage>();
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }
            //var files = Request.Form.Files;
            //if (files != null && files.Any())
            //{
            //    var file = files.First();
            //    var now = DateTime.Now;
            //    var fileName = String.Format(_miscSettings.ImageFilenameFormat,
            //        now.ToString(_miscSettings.TimeStampFormat),
            //        Path.GetExtension(file.FileName));

            //    if (!Directory.Exists(_miscSettings.ImagesPath))
            //    {
            //        Directory.CreateDirectory(_miscSettings.ImagesPath);
            //    }
            //    var destFilePath = Path.Combine(_miscSettings.ImagesPath, fileName);
            //    using (var istream = file.OpenReadStream())
            //    {
            //        using (var ostream = System.IO.File.OpenWrite(destFilePath))
            //        {
            //            istream.CopyTo(ostream);
            //        }
            //    }
            //    viewModel.ImageFiles = Path.GetFileName(destFilePath);
            //}
            //else
            //{
            //    viewModel.ImageFiles = String.Empty;
            //}
            var msg = String.Empty;
            try
            {
                var data=_mapper.Map<EntityLocationImage>(viewModel);
                var ds = _baLocationImage.SaveLocationImage(data, ref msg, "0", 0);
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


            var locationItems = allLocation.Select(s => new SelectListItem(s.Location, s.LocationId.ToString())).ToList();
            locationItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllLocations = locationItems;
            var imageItems = images.Select(s => new ImageSelectListItem(s.ImageName, s.ImageId.ToString()) { Id = s.ImageId, ImageUrl = Url.Action("image", "Image", new { imageId = s.ImageId }) }).ToList();
            imageItems.Insert(0, new ImageSelectListItem("Please Select", "0") { Id = 0, ImageUrl = String.Empty });
            ViewBag.AllImages = imageItems;
            ViewBag.ImageJson = Newtonsoft.Json.JsonConvert.SerializeObject(imageItems, Newtonsoft.Json.Formatting.None);
            return View();
        }
        #endregion

        #region Update
        [Route("[controller]/Edit")]
        [HttpGet]
        public IActionResult Edit(int locationImageId)
        {
           
            var msg = String.Empty;
            var allLocation = _locationHelper.GetAllLocationViewModels(0, ref msg);
            var images = _imageHelper.GetAllImages(0, ref msg);
            var data = _baLocationImage.GetLocationImage(locationImageId,ref msg,"0",0);
            var allLocationImage = _mapper.Map<List<EntityLocationImage>>(data.Tables[1].Rows);
            if (!allLocationImage.Any())
            {
                ViewBag.NoLocationFound = true;
            }
            else
            {
                ViewBag.NoLocationFound = false;
            }
            var viewModel=_mapper.Map<LocationImageViewModel>(allLocationImage.FirstOrDefault());

            var locationItems = allLocation.Select(s => new SelectListItem(s.Location, s.LocationId.ToString())).ToList();
            locationItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllLocations = locationItems;
            var imageItems = images.Select(s => new ImageSelectListItem(s.ImageName, s.ImageId.ToString()) { Id = s.ImageId, ImageUrl = Url.Action("image", "Image", new { imageId = s.ImageId }) }).ToList();
            imageItems.Insert(0, new ImageSelectListItem("Please Select", "0") { Id = 0, ImageUrl = String.Empty });
            ViewBag.AllImages = imageItems;
            ViewBag.ImageJson = Newtonsoft.Json.JsonConvert.SerializeObject(imageItems, Newtonsoft.Json.Formatting.None);


            return View(viewModel);
        }

        [Route("[controller]/Edit")]
        [HttpPost]
        public IActionResult Edit(EntityLocationImage viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }
            //var files = Request.Form.Files;
            //if (files != null && files.Any())
            //{
            //    var file = files.First();
            //    var now = DateTime.Now;
            //    var fileName = String.Format(_miscSettings.ImageFilenameFormat,
            //        now.ToString(_miscSettings.TimeStampFormat),
            //        Path.GetExtension(file.FileName));

            //    if (!Directory.Exists(_miscSettings.ImagesPath))
            //    {
            //        Directory.CreateDirectory(_miscSettings.ImagesPath);
            //    }
            //    var destFilePath = Path.Combine(_miscSettings.ImagesPath, fileName);
            //    using (var istream = file.OpenReadStream())
            //    {
            //        using (var ostream = System.IO.File.OpenWrite(destFilePath))
            //        {
            //            istream.CopyTo(ostream);
            //        }
            //    }
            //    viewModel.ImageFiles = Path.GetFileName(destFilePath);
            //}
            //else
            //{
            //    viewModel.ImageFiles = String.Empty;
            //}
            var msg = String.Empty;
            try
            {

                var ds = _baLocationImage.SaveLocationImage(viewModel, ref msg, "0", 0);
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
            var images = new List<EntityImage>();
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
            var imageItems = images.Select(s => new ImageSelectListItem(s.ImageName, s.ImageId.ToString()) { Id = s.ImageId, ImageUrl = Url.Action("image", "Image", new { imageId = s.ImageId }) }).ToList();
            imageItems.Insert(0, new ImageSelectListItem("Please Select", "0") { Id = 0, ImageUrl = String.Empty });
            ViewBag.AllImages = imageItems;
            ViewBag.ImageJson = Newtonsoft.Json.JsonConvert.SerializeObject(imageItems, Newtonsoft.Json.Formatting.None);
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
            
            var locationImage = _imageHelper.GetAllImages(imageId, ref msg);
            if (!locationImage.Any() || String.IsNullOrWhiteSpace(locationImage.FirstOrDefault(i=>i.ImageId == imageId)?.ImageName))
            {

                return DefaultShipTypeImage();
            }
            else
            {
                var imageFile = Path.Combine(_miscSettings.ImagesPath, locationImage.FirstOrDefault(i=>i.ImageId == imageId)?.ImageName);
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
