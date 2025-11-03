using Microsoft.AspNetCore.Mvc;

namespace Khangri.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
