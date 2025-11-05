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

    public class CMSContentController : Controller
    {
        private readonly IBaCMSContent _baCMSContent;
        private readonly IBaCMSPage _baCMSPage;
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;
        public CMSContentController(IBaCMSContent baCMSContent, IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, IBaCMSPage baCMSPage)
        {
            _baCMSContent = baCMSContent;
            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
            _baCMSPage = baCMSPage;
        }

        #region List
        [Route("[Controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var allCMSContent=new List<EntityCMSContent>();
            PopulateDropDown();

            return View(allCMSContent);
        }
        #endregion

        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new EntityCMSContent();
            var msg = String.Empty;
            // viewModel.IsActive = true;
            var CMSPages = _baCMSPage.GetCMSPage(0, ref msg, "0", 0);
            var allCMSPages = _mapper.Map<List<EntityCMSPage>>(CMSPages.Tables[1].Rows);
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
            var items = allCMSPages.Where(s => (onlyActive ? s.IsActive : true)).Select(s => new SelectListItem(s.Title, s.CMSPageId.ToString())).ToList();
            items.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllCMSPages = items;

            return View(viewModel);
        }

        [Route("[Controller]/Add")]
        [HttpPost]
        public IActionResult Add(EntityCMSContent viewModel)
        {
            var msg = String.Empty;
            var CMSPages = _baCMSPage.GetCMSPage(0, ref msg, "0", 0);
            var allCMSPages = _mapper.Map<List<EntityCMSPage>>(CMSPages.Tables[1].Rows);

            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }
           
             msg = String.Empty;
            try
            {
                var ds = _baCMSContent.SaveCMSContent(viewModel, ref msg, "0", 0);
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
            var items = allCMSPages.Where(s => (onlyActive ? s.IsActive : true)).Select(s => new SelectListItem(s.Title, s.CMSPageId.ToString())).ToList();
            items.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllCMSPages = items;

            return View();
        }
        #endregion

        #region Update
        [Route("[controller]/PageData")]

        [HttpPost]
        public JsonResult PageData([FromBody] PagesRequest request)
        {
            var msg = String.Empty;
            var page = new List<EntityCMSContent>();
            DataSet data = _baCMSContent.GetCMSContent(request.PageId, ref msg, "0", 0);
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
                page = _mapper.Map<List<EntityCMSContent>>(data.Tables[1].Rows);
            }
            var result = new
            {
                Status = Constants.StatusSuccess,
                Title = "Success",
                Data = page.FirstOrDefault()
            };
            return Json(result);

        }

        [Route("[controller]/SaveData")]

        [HttpPost]
        public JsonResult SaveData([FromBody] EntityCMSContent request)
        {
            var msg = String.Empty;
            if(!ModelState.IsValid)
            {
                var status = new ActionStatus()
                {
                    Status = Constants.StatusFail,
                    Title = "Error",
                    Description = msg
                };
                return Json(status);
            }
            try
            {
                var ds = _baCMSContent.SaveCMSContent(request, ref msg, "0", 0);
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
                return Json(status);
            }
            else
            {
                 var status = new ActionStatus()
                {
                    Status = Constants.StatusSuccess,
                    Title = Constants.StatusSuccess,
                    Description = Constants.SaveSuccess
                };
                return Json(status);
            }



            

        }
        //[HttpGet]
        //public IActionResult Edit(int pageId)
        //{

        //    var msg = String.Empty;
        //    // viewModel.IsActive = true;
        //    var cmsContent = _baCMSContent.GetCMSContent(pageId, ref msg, "0", 0);
        //    var allContents = _mapper.Map<List<EntityCMSContent>>(cmsContent.Tables[1].Rows);
        //    var CMSPages = _baCMSPage.GetCMSPage(0, ref msg, "0", 0);
        //    var allCMSPages = _mapper.Map<List<EntityCMSPage>>(CMSPages.Tables[1].Rows);
        //    if (!allContents.Any())
        //    {
        //        ViewBag.NoLocationFound = true;
        //    }
        //    else
        //    {
        //        ViewBag.NoLocationFound = false;
        //    }
        //    var viewModel = allContents.FirstOrDefault();
        //    var onlyActive = false;
        //    var items = allCMSPages.Where(s => (onlyActive ? s.IsActive : true)).Select(s => new SelectListItem(s.Title, s.CMSPageId.ToString())).ToList();
        //    items.Insert(0, new SelectListItem("Please Select", "0"));
        //    ViewBag.AllCMSPages = items;
        //    return View(viewModel);
        //}

        //[Route("[Controller]/Edit")]
        //[HttpPost]
        //public IActionResult Edit(EntityCMSContent viewModel)
        //{
        //    var msg = String.Empty;

        //    var CMSPages = _baCMSPage.GetCMSPage(0, ref msg, "0", 0);
        //    var allCMSPages = _mapper.Map<List<EntityCMSPage>>(CMSPages.Tables[1].Rows);
        //    if (!ModelState.IsValid)
        //    {
        //        return View(viewModel);

        //    }

        //    msg = String.Empty;
        //    try
        //    {
        //        var ds = _baCMSContent.SaveCMSContent(viewModel, ref msg, "0", 0);
        //        if (String.IsNullOrWhiteSpace(msg))
        //        {
        //            msg = _utilities.GetErrorMessageFromDataSet(ds);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
        //    }
        //    if (!String.IsNullOrWhiteSpace(msg))
        //    {
        //        var status = new ActionStatus()
        //        {
        //            Status = Constants.StatusFail,
        //            Title = "Error",
        //            Description = msg

        //        };
        //        ViewBag.Status = JsonSerializer.Serialize(status);
        //    }
        //    else
        //    {
        //        var status = new ActionStatus()
        //        {
        //            Status = Constants.StatusSuccess,
        //            Title = Constants.StatusSuccess,
        //            Description = Constants.SaveSuccess
        //        };
        //        ViewBag.Status = JsonSerializer.Serialize(status);
        //    }
        //    var onlyActive = false;
        //    var items = allCMSPages.Where(s => (onlyActive ? s.IsActive : true)).Select(s => new SelectListItem(s.Title, s.CMSPageId.ToString())).ToList();
        //    items.Insert(0, new SelectListItem("Please Select", "0"));
        //    ViewBag.AllCMSPages = items;
        //    return View();
        //}
        #endregion

        #region Helper Methods
        [NonAction]
        private void PopulateDropDown()
        {
            var msg = String.Empty;
            var data = _baCMSPage.GetCMSPage(0, ref msg, "0", 0);
            var pageData = new List<EntityCMSPage>();
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                pageData = _mapper.Map<List<EntityCMSPage>>(data.Tables[1].Rows);
            }
            var items = new List<SelectListItem>();
            items.AddRange(pageData.Select(p => new SelectListItem { Text = p.Title, Value = p.CMSPageId.ToString() }));
            items.Insert(0, new SelectListItem { Text = "Please Select", Value = "0" });
            ViewBag.FilterItems = items;
        }
        #endregion
    }
}
