using Khangri.UI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;
using Khangri.UI.Helpers;

namespace Khangri.UI.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IUserHelper _userHelper;
        public AdminController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        [Route("[controller]/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("[controller]/Login")]
        [HttpPost]
		//public IActionResult Login(LoginViewModel model, string returnUrl)

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
            claims.Add(new Claim("UserId", user.UserId.ToString()));
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


















            //if (!ModelState.IsValid)
            //         {
            //             return View();
            //         }
            //         if (viewModel.UserId == "Khangritoursandtravels2008@gmail.com" && viewModel.Password == "gangtok@737102")
            //         {
            //         //    var claims = new List<Claim>();
            //         //    claims.Add(new Claim(ClaimTypes.Name, viewModel.UserId));
            //         //    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //         //    var authProperties = new AuthenticationProperties
            //         //    {
            //         //        AllowRefresh = true,
            //         //        // Refreshing the authentication session should be allowed.

            //         //        //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            //         //        // The time at which the authentication ticket expires. A 
            //         //        // value set here overrides the ExpireTimeSpan option of 
            //         //        // CookieAuthenticationOptions set with AddCookie.

            //         //        IsPersistent = false,//viewModel.RememberMe,
            //         //        // Whether the authentication session is persisted across 
            //         //        // multiple requests. When used with cookies, controls
            //         //        // whether the cookie's lifetime is absolute (matching the
            //         //        // lifetime of the authentication ticket) or session-based.

            //         //        IssuedUtc = DateTimeOffset.UtcNow,
            //         //        // The time at which the authentication ticket was issued.

            //         //        //RedirectUri = <string>
            //         //        // The full path or absolute URI to be used as an http 
            //         //        // redirect response value.
            //         //    };
            //         //    await HttpContext.SignInAsync(
            //         //CookieAuthenticationDefaults.AuthenticationScheme,
            //         //new ClaimsPrincipal(claimsIdentity),
            //         //authProperties);
            //             return Redirect(Url.Action("Dashboard", "Dashboard"));
            //         }
            //         else
            //         {
            //             return View();
            //         }

        }


        public IActionResult Signup()
        {
            return View();
        }

        // [Route("[controller]/Login")]
        // [HttpGet]
        //   public IActionResult Logout()
        // {
        //     return View();
        // }
    }
}
