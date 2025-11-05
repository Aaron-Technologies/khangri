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
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Khangri.UI.Controllers
{
    [Area("Admin")]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class SliderImageController : Controller
    {
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;
        private readonly IBaSliderImages _imageSliderImages;
        private readonly ISliderImageHelper _sliderImageHelper;

        public SliderImageController(IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, IBaSliderImages imageSliderImages, ISliderImageHelper sliderImageHelper)
        {
            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
            _imageSliderImages = imageSliderImages;
            _sliderImageHelper = sliderImageHelper;
        }

        #region List
        [Route("[controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var msg = "";
            var sliderImage=new List<SliderImageViewModel>();
            var data = _imageSliderImages.GetSliderImages(0, ref msg, "0", 0);
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
               var entity = _mapper.Map<List<EntitySliderImage>>(data.Tables[1].Rows);
                sliderImage = _mapper.Map<List<SliderImageViewModel>>(entity);
            }
            return View(sliderImage);
        }
        #endregion
        
        
        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new SliderImageViewModel();
            viewModel.IsActive = true;
            return View(viewModel);
        }

        [Route("[controller]/Add")]
        [HttpPost]
        public IActionResult Add(SliderImageViewModel viewModel)
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
                var fileName = String.Format(_miscSettings.SliderFilenameFormat,
                    now.ToString(_miscSettings.TimeStampFormat),
                    Path.GetExtension(file.FileName));

                if (!Directory.Exists(_miscSettings.SliderImagePath))
                {
                    Directory.CreateDirectory(_miscSettings.SliderImagePath);
                }
                var destFilePath = Path.Combine(_miscSettings.SliderImagePath, fileName);
                using (var istream = file.OpenReadStream())
                {
                    using (var ostream = System.IO.File.OpenWrite(destFilePath))
                    {
                        istream.CopyTo(ostream);
                    }
                }
                viewModel.ImageName = Path.GetFileName(destFilePath);
            }
           
            var msg = String.Empty;
            try
            {
                var data=_mapper.Map<EntitySliderImage>(viewModel);
                var ds = _imageSliderImages.SaveSliderImage(data, ref msg, "0", 0);
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
            return View(viewModel);
        }
        #endregion

        #region Update
        [Route("[controller]/Edit")]
        [HttpGet]
        public IActionResult Edit(int sliderImageId)
        {
           
            var msg = String.Empty;
            var viewModel=new SliderImageViewModel();
            var data = _imageSliderImages.GetSliderImages(sliderImageId,ref msg,"0",0);
            if(data !=null && data.Tables !=null && data.Tables.Count > 0)
            {
                var entity = _mapper.Map<List<EntitySliderImage>>(data.Tables[1].Rows);
                viewModel=_mapper.Map<SliderImageViewModel>(entity.FirstOrDefault());

            }
            return View(viewModel);
        }

        [Route("[controller]/Edit")]
        [HttpPost]
        public IActionResult Edit(SliderImageViewModel viewModel)
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
                var fileName = String.Format(_miscSettings.SliderFilenameFormat,
                    now.ToString(_miscSettings.TimeStampFormat),
                    Path.GetExtension(file.FileName));

                if (!Directory.Exists(_miscSettings.SliderImagePath))
                {
                    Directory.CreateDirectory(_miscSettings.SliderImagePath);
                }
                var destFilePath = Path.Combine(_miscSettings.SliderImagePath, fileName);
                using (var istream = file.OpenReadStream())
                {
                    using (var ostream = System.IO.File.OpenWrite(destFilePath))
                    {
                        istream.CopyTo(ostream);
                    }
                }
                viewModel.ImageName = Path.GetFileName(destFilePath);
            }
            
            var msg = String.Empty;
            try
            {
                var data = _mapper.Map<EntitySliderImage>(viewModel);
                var ds = _imageSliderImages.SaveSliderImage(data, ref msg, "0", 0);
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
            return View();
        }
        #endregion

        [AllowAnonymous]
        #region Image

        [Route("[controller]/image")]
        [HttpGet]
        public IActionResult Image(int sliderImageId)
        {
            var msg = "";
            var sliderImage = _sliderImageHelper.GetAllSliderImages(sliderImageId, ref msg);
            if (!sliderImage.Any() || String.IsNullOrWhiteSpace(sliderImage.First().ImageName))
            {

                return DefaultShipTypeImage();
            }
            else
            {
                var imageFile = Path.Combine(_miscSettings.SliderImagePath, sliderImage.First().ImageName);
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
