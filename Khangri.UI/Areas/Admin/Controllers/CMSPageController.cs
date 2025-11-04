using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text.Json;

namespace Khangri.UI.Controllers
{
    [Area("Admin")]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class CMSPageController : Controller
    {
        private readonly IBaCMSPage _baCMSPage;
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;
        public CMSPageController(IBaCMSPage baCMSPage, IMapper mapper,IOptions<MiscSettings> options, IUtility utilities)
        {
            _baCMSPage = baCMSPage;
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
            var allCMSPages = new List<EntityCMSPage>();
            DataSet data = _baCMSPage.GetCMSPage(0, ref msg, "0", 0);
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
                allCMSPages = _mapper.Map<List<EntityCMSPage>>(data.Tables[1].Rows);
            }
            return View(allCMSPages);
        }
        #endregion

        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new EntityCMSPage();
            var msg = String.Empty;
            viewModel.IsActive = true;

            return View(viewModel);
        }

        [Route("[Controller]/Add")]
        [HttpPost]
        public IActionResult Add(EntityCMSPage viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }

            var msg = String.Empty;
            try
            {
                var ds = _baCMSPage.SaveCMSPage(viewModel, ref msg, "0", 0);
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
        public IActionResult Edit(int pageId)
        {

            var msg = String.Empty;
            // viewModel.IsActive = true;
            var cmsPages = _baCMSPage.GetCMSPage(pageId, ref msg, "0", 0);
            var allPages = _mapper.Map<List<EntityCMSPage>>(cmsPages.Tables[1].Rows);
            if (!allPages.Any())
            {
                ViewBag.NoLocationFound = true;
            }
            else
            {
                ViewBag.NoLocationFound = false;
            }
            var viewModel = allPages.FirstOrDefault();
            return View(viewModel);
        }

        [Route("[Controller]/Edit")]
        [HttpPost]
        public IActionResult Edit(EntityCMSPage viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }

            var msg = String.Empty;
            try
            {
                var ds = _baCMSPage.SaveCMSPage(viewModel, ref msg, "0", 0);
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
    }
}
