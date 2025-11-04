using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Data;
using Khangri.DataAccess.KhangriDAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Khangri.UI.Controllers
{
    [Area("Admin")]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class SetUpController : Controller
    {
        private readonly IBaSetUp _baSetUp;
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;
        private readonly IBaCMSPage _baCMSPage;

        public SetUpController(IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, IBaSetUp baSetUp, IBaCMSPage baCMSPage)
        {

            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
            _baSetUp = baSetUp;
            _baCMSPage = baCMSPage;
        }

        #region List
        [Route("[Controller]/List")]
        [HttpGet]
        public IActionResult List(int setUpid=1)
        {
            //PopulateDropDown();
            var msg = String.Empty;
            var data = _baSetUp.GetSetUp(setUpid, ref msg, "0", 0);
            var pageData = new List<EntitySetup>();
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                pageData = _mapper.Map<List<EntitySetup>>(data.Tables[1].Rows);
            }


            return View(pageData);
        }
        #endregion

        //[NonAction]
        //private void PopulateDropDown()
        //{
        //    var msg=String.Empty;
        //    var data = _baSetUp.GetSetUp(0, ref msg, "0", 0);
        //    var pageData=new List<EntitySetup>();
        //    if(data != null && data.Tables != null &&data.Tables.Count > 0)
        //    {
        //        pageData = _mapper.Map<List<EntitySetup>>(data.Tables[1].Rows);   
        //    }
        //    var items=new List<SelectListItem>();
        //    items.AddRange(pageData.Select(p => new SelectListItem { Text = p.Title, Value = p.CMSPageId.ToString() }));
        //    items.Insert(0, new SelectListItem { Text = "Please Select", Value = "0" });
        //    ViewBag.FilterItems=items;
        //}

        [HttpPost]
        public JsonResult SetupData([FromBody] SetupRequest setup)
        {
            var msg = String.Empty;
            var setups=new List<EntitySetup>();
            DataSet data = _baSetUp.GetSetUp(setup.SetupId, ref msg, "0", 0);
            if (!String.IsNullOrWhiteSpace(msg))
            {
                var status = new ActionStatus()
                {
                    Status = Constants.StatusFail,
                    Title = "Error",
                    Description = msg
                };
                return Json(status);
            }
           
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                setups = _mapper.Map<List<EntitySetup>>(data.Tables[1].Rows);
            }
            var result = new
            {
                Status = Constants.StatusSuccess,
                Title = "Success",
                Data = setups.FirstOrDefault()
            };
            return Json(result);

        }

        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new EntitySetup();
            return View(viewModel);
        }

        [Route("[controller]/Add")]
        [HttpPost]
        public IActionResult Add(EntitySetup viewModel)
        {
            var msg = String.Empty;
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }

            try
            { 
                var ds = _baSetUp.SaveSetUp(viewModel, ref msg, "0", 0);
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

        #region Update
        [Route("[controller]/Edit")]
        [HttpGet]
        public IActionResult Edit(int setUpId)
        {
            var msg = String.Empty;
            var setups = _baSetUp.GetSetUp(setUpId, ref msg,"0",0);
            var allSetUps = _mapper.Map<List<EntitySetup>>(setups.Tables[1].Rows);

            if (!allSetUps.Any())
            {
                ViewBag.NoLocationFound = true;
            }
            else
            {
                ViewBag.NoLocationFound = false;
            }
            var viewModel = allSetUps.FirstOrDefault();

            return View(viewModel);
        }

        [Route("[controller]/Edit")]
        [HttpPost]
        public IActionResult Edit(EntitySetup viewModel)
        {
            var msg = String.Empty;
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
            //    viewModel.LogoPrimary = Path.GetFileName(destFilePath);
            //}
            //else
            //{
            //    viewModel.LogoPrimary = String.Empty;
            //}

            try
            {
                var ds = _baSetUp.SaveSetUp(viewModel, ref msg, "0", 0);
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

        #region Image

        [Route("[controller]/image")]
        [HttpGet]
        public IActionResult Image(int imageId)
        {
            var msg = "";
            var data = _baSetUp.GetSetUp(imageId, ref msg, "0", 0);

            var images = _mapper.Map<List<EntitySetup>>(data.Tables[1].Rows);

            if (!images.Any() || String.IsNullOrWhiteSpace(images.First().LogoPrimary))
            {

                return DefaultShipTypeImage();
            }
            else
            {
                var imageFile = Path.Combine(_miscSettings.ImagesPath, images.First().LogoPrimary);
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
