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
using System.Text.Json;

namespace Khangri.UI.Controllers
{
    [Area("Admin")]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class ImageController : Controller
    {
        private readonly IBaImage _baImage;
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;

        public ImageController(IBaImage baImage, IMapper mapper, IOptions<MiscSettings> options, IUtility utilities)
        {
            _baImage = baImage;
            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
        }

        #region List
        [Route("[Controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var msg = "";
            var allImages = new List<ImageViewModel>();
            DataSet data = _baImage.GetImages(0,ref msg,"0",0);
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
                allImages = _mapper.Map<List<ImageViewModel>>(data.Tables[1].Rows);
            }
            return View(allImages);
        }
        #endregion

        #region Add
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new ImageViewModel();
            viewModel.IsActive = true;

            return View(viewModel);
        }

        [Route("[Controller]/Add")]
        [HttpPost]
        public IActionResult Add(ImageViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }
            var files = Request.Form.Files;
            if (files != null && files.Any())
            {
                var file = files.First();
                var fileName = file.FileName;

                if (!Directory.Exists(_miscSettings.ImagesPath))
                {
                    Directory.CreateDirectory(_miscSettings.ImagesPath);
                }
                var destFilePath = Path.Combine(_miscSettings.ImagesPath, fileName);
                using (var istream = file.OpenReadStream())
                {
                    using (var ostream = System.IO.File.OpenWrite(destFilePath))
                    {
                        istream.CopyTo(ostream);
                    }
                }
                viewModel.ImageName = Path.GetFileName(destFilePath);
            }
            else
            {
                viewModel.ImageName = String.Empty;
            }
            var msg = String.Empty;
            try
            {
                var data=_mapper.Map<EntityImage>(viewModel);
                var ds = _baImage.SaveImages(data, ref msg, "0", 0);
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
        public IActionResult Edit(int imageId)
        {

            var msg = String.Empty;
            var images = _baImage.GetImages(imageId, ref msg,"0",0);
            var allImages = _mapper.Map<List<ImageViewModel>>(images.Tables[1].Rows);
            if (!allImages.Any())
            {
                ViewBag.NoLocationFound = true;
            }
            else
            {
                ViewBag.NoLocationFound = false;
            }
            var viewModel = _mapper.Map<ImageViewModel>(allImages.FirstOrDefault());

            return View(viewModel);
        }

        [Route("[Controller]/Edit")]
        [HttpPost]
        public IActionResult Edit(ImageViewModel viewModel)
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
                var fileName = file.FileName;

                if (!Directory.Exists(_miscSettings.ImagesPath))
                {
                    Directory.CreateDirectory(_miscSettings.ImagesPath);
                }
                var destFilePath = Path.Combine(_miscSettings.ImagesPath, fileName);
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
                var data = _mapper.Map<EntityImage>(viewModel);
                var ds = _baImage.SaveImages(data, ref msg, "0", 0);
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
            msg = String.Empty;
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
            return View(viewModel);
        }
        #endregion

        #region Image
        [AllowAnonymous]
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
