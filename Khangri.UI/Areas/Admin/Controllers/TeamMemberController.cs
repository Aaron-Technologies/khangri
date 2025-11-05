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

    public class TeamMemberController : Controller
    {
        private readonly IBaTeamMember _baTeamMember;
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;
        private readonly IImageHelper _imageHelper;

        public TeamMemberController(IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, IBaTeamMember baTeamMember, IImageHelper imageHelper)
        {

            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
            _baTeamMember = baTeamMember;
            _imageHelper = imageHelper;
        }

        #region List
        [Route("[controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var msg = "";
            var data = _baTeamMember.GetTeamMember(0,ref msg,"0",0);
            var viewModel = _mapper.Map<List<EntityTeamMember>>(data.Tables[1].Rows);

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
            return View(viewModel);
        }
        #endregion
        
        
        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new EntityTeamMember();
            var msg=String.Empty;
            viewModel.IsActive = true;
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
            var imageItems=allImages.Select(s=>new SelectListItem(s.ImageName,s.ImageId.ToString())).ToList();
            imageItems.Insert(0,new SelectListItem("Please Select", "0"));
            ViewBag.AllImages=imageItems;
            return View(viewModel);
        }

        [Route("[controller]/Add")]
        [HttpPost]
        public IActionResult Add( EntityTeamMember viewModel)
        {
            
            var msg = String.Empty;
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
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }
            try
            {

                var ds = _baTeamMember.SaveTeamMember(viewModel, ref msg, "0", 0);
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
            var imageItems = allImages.Select(s => new SelectListItem(s.ImageName, s.ImageId.ToString())).ToList();
            imageItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllImages = imageItems;
            return View();
        }
        #endregion

        #region Update
        [Route("[controller]/Edit")]
        [HttpGet]
        public IActionResult Edit(int teamMemberId)
        {
            var msg = String.Empty;
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
            var data = _baTeamMember.GetTeamMember(teamMemberId, ref msg, "0", 0);
            var teamMember = _mapper.Map<List<EntityTeamMember>>(data.Tables[1].Rows);
            if (!teamMember.Any())
            {
                ViewBag.NoLocationFound = true;
            }
            else
            {
                ViewBag.NoLocationFound = false;
            }
            var viewModel=teamMember.FirstOrDefault();
            var imageItems = allImages.Select(s => new SelectListItem(s.ImageName, s.ImageId.ToString())).ToList();
            imageItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllImages = imageItems;
            return View(viewModel);
        }

        [Route("[controller]/Edit")]
        [HttpPost]
        public IActionResult Edit(EntityTeamMember viewModel)
        {
            var msg = String.Empty;
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
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }
            try
            {

                var ds = _baTeamMember.SaveTeamMember(viewModel, ref msg, "0", 0);
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
            var imageItems = allImages.Select(s => new SelectListItem(s.ImageName, s.ImageId.ToString())).ToList();
            imageItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllImages = imageItems;
            return View();
        }
        #endregion

       
    }
}
