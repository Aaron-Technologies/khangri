using Khangri.UI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Khangri.Entities;
using Newtonsoft.Json;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Khangri.UI.Helpers;
using AutoMapper;
using Khangri.DataAccess.Abstract;
using System.Runtime.CompilerServices;
using Khangri.UI.Utils;
using Microsoft.AspNetCore.Http.Extensions;
using Khangri.UI.Services;

namespace Khangri.UI.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserTypeHelper _userTypeHelper;
        private readonly IUserHelper _userHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;
        private readonly IBaUser _baUser;
        private readonly IUtility _utility;
        private readonly IResetPasswordHelper _resetPasswordHelper;
        private readonly IEmailService _emailService;



        public AccountController(IHttpContextAccessor contextAccessor, IUserTypeHelper userTypeHelper, IMapper mapper, IUserHelper userHelper, ILogger<AccountController> logger, IBaUser baUser, IUtility utility, IResetPasswordHelper resetPasswordHelper, IEmailService emailService)
        {
            _contextAccessor = contextAccessor;
            _userTypeHelper = userTypeHelper;
            _mapper = mapper;
            _userHelper = userHelper;
            _logger = logger;
            _baUser = baUser;
            _utility = utility;
            _resetPasswordHelper = resetPasswordHelper;
            _emailService = emailService;
        }
        
        [Route("[controller]/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            _logger.LogError(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            var model = new LoginViewModel();
            return View(model);
        }
        
        [Route("[controller]/Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            var msg = String.Empty;
            if (!ModelState.IsValid)
            {
                return View();
            }
            var entities = _userHelper.Login(model.UserId, model.Password, ref msg);
            if (!String.IsNullOrWhiteSpace(msg))
            {
                ViewBag.ErrorMessage = msg;
                return View(model);
            }
            var user = entities.FirstOrDefault();
            if (user == null)
            {
                ViewBag.ErrorMessage = Khangri.Entities.Constants.LoginFailed;
                return View(model);
            }
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, model.UserId));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(ClaimTypes.MobilePhone, user.Mobile));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            //claims.Add(new Claim("UserTypeId", user.U00_UserTypeId.ToString()));
            claims.Add(new Claim("UserId",user.UserId.ToString()));
            claims.Add(new Claim("UserType", user.UserType));
            //claims.Add(new Claim("UserTypeId", viewModel.UserId.ToString()));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = false,//viewModel.RememberMe,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    IssuedUtc = DateTime.UtcNow,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };
                await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
                if (!String.IsNullOrWhiteSpace(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect(Url.Action("Dashboard", "Dashboard"));
                }

            }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception) { }

            return RedirectToAction("Login", "Account");
        }

        #region Reset Password
        [AllowAnonymous]
        [HttpGet]
        [Route("ResetPassword")]
        public IActionResult Forgot()
        {
            var viewModel = new ResetPasswordInitiativeRequestViewModel();
            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> Forgot(ResetPasswordInitiativeRequestViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var msg = String.Empty;
            var response = _resetPasswordHelper.InitiatePasswordReset(viewModel, ref msg);
            if (!String.IsNullOrWhiteSpace(msg))
            {
                ViewBag.ErrorMessage = msg;
                return View(viewModel);
            }
            var encUserId = _utility.EncryptString(response.Email.ToString());
            var encToken = _utility.EncryptString(response.Token.ToString());
            var link = Url.Action("FinalizeResetPassword", "account",
                new { userId = encUserId, token = encToken },
                new Uri(Request.GetDisplayUrl()).Scheme,
                Request.Host.ToString());

            //ViewBag.SuccessMessage = link;
            var fileText = String.Empty;
            using (var fs = System.IO.File.OpenRead("Resource\\ResetPasswordEmailTemplate.html"))
            {
                using (var reader = new StreamReader(fs))
                {
                    fileText = await reader.ReadToEndAsync();
                }
            }
            fileText = fileText.Replace("@@link", link);

            var status = await _emailService.SendEmailAsync(response.Email, Khangri.Entities.Constants.PasswordResetEmailSubject, fileText);
            if (!String.IsNullOrWhiteSpace(status))
            {
                ViewBag.ErrorMessage = $"Error sending email: {status}";
                return View(viewModel);
            }
            ViewBag.SuccessMessage = "A password reset link has been successfully sent your registered email address. " +
                "Please check you email for further instructions on how to reset your password.";

            return View(viewModel);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("FinalizeResetPassword")]
        public IActionResult FinalizeResetPassword([FromQuery] string userId, [FromQuery] string token)
        {
            if (String.IsNullOrWhiteSpace(userId) || String.IsNullOrWhiteSpace(token))
            {
                return RedirectToAction("login");
            }
            try
            {
                var decrypted_uid = _utility.DecryptString(userId.ToString());
                var decrypted_token = _utility.DecryptString(userId.ToString());
            }
            catch (Exception)
            {
                return RedirectToAction("login", "account");
            }

            var viewModel = new ResetPasswordViewModel()
            {
                UserId = userId,
                Token = token
            };
            return View(viewModel);

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("FinalizeResetPassword")]
        public IActionResult FinalizeResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var msg = String.Empty;
            try
            {
                viewModel.UserId = _utility.DecryptString(viewModel.UserId);
                viewModel.Token = _utility.DecryptString(viewModel.Token);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Invalid Link.";
                return View(viewModel);
            }
            _resetPasswordHelper.ResetPassword(viewModel, ref msg);
            if (!String.IsNullOrWhiteSpace(msg))
            {
                ViewBag.ErrorMessage = msg;
            }
            else
            {
                var successMsg = "Password successfully changed. Please ";
                successMsg += "<a href=\"" + Url.Action("login", "account") + "\">Login to your account</a>";
                ViewBag.SuccessMessage = successMsg;

            }

            return View(viewModel);

        }


        #endregion

        #region Profile
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("[controller]/Profile")]
        public IActionResult Profile(string userId)
        {
            var user = Convert.ToInt32(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
            if (!String.IsNullOrWhiteSpace(userId))
            {
                try
                {
                    user = Convert.ToInt32(userId?.Trim());
                }
                catch
                {
                    user = Convert.ToInt32(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
                }
            }

            var msg = "";
            _ = SetUserTypeDropdown(msg);
            var model = new UserViewModel { UserId = user, UserTypeId = 0 };
            try
            {
                var users = _userHelper.GetAllUser(user, 0, 0, 0, ref msg);
                if (!users.Any(u => u.UserId == user))
                {
                    //ViewBag.NoUserFound = true;
                    return NotFound();
                }
                else
                {
                    ViewBag.NoUserFound = false;
                }
                if (String.IsNullOrWhiteSpace(msg))
                {
                    model = _mapper.Map<UserViewModel>(users.FirstOrDefault(u => u.UserId == user));
                }
            }
            catch (Exception e)
            {
                msg = e.Message;
            }

            return View(model);
        }

        //[HttpPost]
        //[Route("[controller]/Profile")]
        //public IActionResult Profile(UserEditViewModel viewModel)
        //{
        //    var msg = String.Empty;
        //    _ = SetUserTypeDropdown(msg);
        //    _ = SetCompanyShipTypeDropdown(msg);
        //    if (!ModelState.IsValid)
        //    {
        //        return View(viewModel);
        //    }

        //    var entity = _mapper.Map<EntityUser>(viewModel);
        //    try
        //    {

        //        var res = _baUser.SaveUser(entity, ref msg, "0", 0);
        //        if (String.IsNullOrWhiteSpace(msg))
        //        {
        //            msg = _utilities.GetErrorMessageFromDataSet(res);
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
        //        ViewBag.Status = System.Text.Json.JsonSerializer.Serialize(status);

        //    }
        //    else
        //    {
        //        var status = new ActionStatus()
        //        {
        //            Status = Constants.StatusSuccess,
        //            Title = Constants.StatusSuccess,
        //            Description = Constants.UpdateSuccess
        //        };
        //        ViewBag.Status = System.Text.Json.JsonSerializer.Serialize(status);
        //        _utilities.InvalidateLoginCookie(viewModel.UserId.ToString());
        //    }

        //    return View(viewModel);
        //}

        //[HttpGet]
        //[Route("[Controller]/ProfilePic")]
        //public ActionResult ProfilePic(string userId)
        //{
        //    var msg = String.Empty;
        //    try
        //    {
        //        var user = _baUser.GetUser(Convert.ToInt64(userId), 0, 0, 0, 0, 0, ref msg, "0", 0);
        //        if (user == null) { return DefaultProfilePic(); }
        //        var userList = _mapper.Map<List<EntityUser>>(user.Tables[1].Rows);
        //        if (userList == null || !userList.Any()) { return DefaultProfilePic(); }
        //        var userObj = userList.FirstOrDefault();
        //        if (userObj == null) { return DefaultProfilePic(); }
        //        var imagePath = Path.Combine(_miscSettings.ProfileImagePath, userObj.ProfilePic);
        //        if (!System.IO.File.Exists(imagePath)) { return DefaultProfilePic(); }
        //        try
        //        {
        //            var fileContents = System.IO.File.ReadAllBytes(imagePath);
        //            return File(fileContents, Constants.GenericBinaryMimeType);
        //        }
        //        catch (Exception)
        //        {
        //            return NotFound();
        //        }
        //    }
        //    catch
        //    {
        //        return DefaultProfilePic();
        //    }
        //}

        //private ActionResult DefaultProfilePic()
        //{
        //    var imagePath = _miscSettings.ProfileImagePath;
        //    imagePath = Path.Combine(imagePath, "profilePic.png");
        //    try
        //    {
        //        var fileContents = System.IO.File.ReadAllBytes(imagePath);
        //        return File(fileContents, Constants.GenericBinaryMimeType);
        //    }
        //    catch (Exception)
        //    {
        //        return NotFound();
        //    }
        //}

        #endregion

        #region Change Password
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("[controller]/ChangePassword")]
        public IActionResult ChangePassword()
        {
            var viewModel = new EntityChangePassword();
            return View(viewModel);
        }

        [HttpPost]
        [Route("[controller]/ChangePassword")]
        public IActionResult ChangePassword(EntityChangePassword viewModel)
        {
            var msg = String.Empty;
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var ds = new DataSet();
            var currentUser = HttpContext.User;
            var userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
            viewModel.UserId = userId;
            var entity = _mapper.Map<EntityChangePassword>(viewModel);
            try
            {
                ds = _baUser.ChangePassword(entity, ref msg, "0", 0);
                if (String.IsNullOrWhiteSpace(msg))
                {
                    msg = _utility.GetErrorMessageFromDataSet(ds);
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
                ViewBag.Status = JsonConvert.SerializeObject(status);
            }
            else
            {
                var status = new ActionStatus()
                {
                    Status = Constants.StatusSuccess,
                    Title = Constants.StatusSuccess,
                    Description = Constants.UpdateSuccess
                };
                ViewBag.Status = JsonConvert.SerializeObject(status);
            }
            return View(viewModel);
        }

        #endregion

        [AllowAnonymous]
        [HttpGet]
        [Route("[controller]/Signup")]
        public IActionResult Signup()
        {
            return View();
        }
        #region dropdown
        [NonAction]
        private string SetUserTypeDropdown(string msg)
        {
            var allUserType = _userTypeHelper.GetAllUserType(0, ref msg);
            var userTypeItems = allUserType.Where(ut => ut.IsActive).Select(s => new SelectListItem(s.UserType.ToString(), s.UserTypeId.ToString())).ToList();
            userTypeItems.Insert(0, new SelectListItem("Please Select", ""));
            ViewBag.allUserTypes = userTypeItems;
            return msg;
        }
        #endregion
    }

}
