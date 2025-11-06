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
using Khangri.UI.Models;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Khangri.UI.Helpers;

namespace Khangri.UI.Controllers
{
    [Area("Admin")]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;
        private readonly IBaUser _baUser;
        private readonly IUserTypeHelper _userTypeHelper;


        public UserController(IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, IBaUser baUser, IUserTypeHelper userTypeHelper)
        {

            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
            _baUser = baUser;
            _userTypeHelper = userTypeHelper;
        }

        #region List
        [Route("[Controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var msg = "";
            var allUser = new List<UserViewModel>();
            var userData = _baUser.GetUsers(0,0,0,0, ref msg, "0", 0);
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
            
            if (userData != null && userData.Tables != null && userData.Tables.Count > 0)
            {
                var userEntity = _mapper.Map<List<EntityUser>>(userData.Tables[1].Rows);
                allUser=_mapper.Map<List<UserViewModel>>(userEntity);
            }
            return View(allUser);
        }
        #endregion

        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var msg =String.Empty;
            var viewModel = new EntityUser();
            viewModel.IsActive = true;
            var userTypes = _userTypeHelper.GetAllUserType(0, ref msg);
            if (!String.IsNullOrEmpty(msg))
            {
                var status = new ActionStatus
                {
                    Status= Constants.StatusFail,
                    Title = "Error",
                    Description = msg
                };
                return ViewBag.Status = JsonSerializer.Serialize(status);
            }
            var allUserType = userTypes.Select(s => new SelectListItem(s.UserType, s.UserTypeId.ToString())).ToList();
            allUserType.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllUserType = allUserType;
            return View(viewModel);
        }

        [Route("[controller]/Add")]
        [HttpPost]
        public IActionResult Add(EntityUser viewModel)
        {
            var msg = String.Empty;
            var userTypes = _userTypeHelper.GetAllUserType(0, ref msg);
            if (!String.IsNullOrEmpty(msg))
            {
                var status = new ActionStatus
                {
                    Status = Constants.StatusFail,
                    Title = "Error",
                    Description = msg
                };
                return ViewBag.Status = JsonSerializer.Serialize(status);
            }
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }

            try
            { 
                var ds = _baUser.SaveUser(viewModel, ref msg, "0", 0);
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
            var allUserType = userTypes.Select(s => new SelectListItem(s.UserType, s.UserTypeId.ToString())).ToList();
            allUserType.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllUserType = allUserType;
            return View(viewModel);
        }
        #endregion

        #region Update
        [Route("[controller]/Edit")]
        [HttpGet]
        public IActionResult Edit(long userId,int userTypeId)
        {
            var msg = String.Empty;
            var users = _baUser.GetUsers(userId,userTypeId,0,0, ref msg, "0", 0);
            var allUser = _mapper.Map<List<EntityUser>>(users.Tables[1].Rows);
            var userTypes = _userTypeHelper.GetAllUserType(0, ref msg);
            if (!String.IsNullOrEmpty(msg))
            {
                var status = new ActionStatus
                {
                    Status = Constants.StatusFail,
                    Title = "Error",
                    Description = msg
                };
                return ViewBag.Status = JsonSerializer.Serialize(status);
            }
            if (!allUser.Any())
            {
                ViewBag.NoLocationFound = true;
            }
            else
            {
                ViewBag.NoLocationFound = false;
            }
            var viewModel = allUser.FirstOrDefault();
            var allUserType = userTypes.Select(s => new SelectListItem(s.UserType, s.UserTypeId.ToString())).ToList();
            allUserType.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllUserType = allUserType;
            return View(viewModel);
        }

        [Route("[controller]/Edit")]
        [HttpPost]
        public IActionResult Edit(EntityUser viewModel)
        {
            var msg = String.Empty;
            var userTypes = _userTypeHelper.GetAllUserType(0, ref msg);
            if (!String.IsNullOrEmpty(msg))
            {
                var status = new ActionStatus
                {
                    Status = Constants.StatusFail,
                    Title = "Error",
                    Description = msg
                };
                return ViewBag.Status = JsonSerializer.Serialize(status);
            }
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }

            try
            {
                var ds = _baUser.SaveUser(viewModel, ref msg, "0", 0);
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
            var allUserType = userTypes.Select(s => new SelectListItem(s.UserType, s.UserTypeId.ToString())).ToList();
            allUserType.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllUserType = allUserType;
            return View();
        }
        #endregion


    }
}
