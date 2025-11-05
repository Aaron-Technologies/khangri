using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Helpers;
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

    public class TourController : Controller
    {
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;
        private readonly ITourTypeHelper _tourTypeHelper;
        private readonly IBaTour _baTour;
        public TourController(IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, ITourTypeHelper tourTypeHelper, IBaTour baTour)
        {
            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
            _tourTypeHelper = tourTypeHelper;
            _baTour = baTour;
        }

        #region List
        [Route("[controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var msg = "";
            var allTour = new List<EntityTour>();
            var data = _baTour.GetTour(0,0,ref msg,"0",0);
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
                allTour = _mapper.Map<List<EntityTour>>(data.Tables[1].Rows);
            }
            return View(allTour);
        }
        #endregion

        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new EntityTour();
            var msg = String.Empty;
            viewModel.IsActive = true;
            var allTourType = _tourTypeHelper.GetAllTourType(0, ref msg);
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
            var tourTypeItems = allTourType.Select(s => new SelectListItem(s.TourType, s.TourTypeId.ToString())).ToList();
            tourTypeItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllTourType = tourTypeItems;

            return View(viewModel);
        }

        [Route("[controller]/Add")]
        [HttpPost]
        public IActionResult Add(EntityTour viewModel)
        {
            var allTourType = new List<EntityTourType>();

            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }
           
            var msg = String.Empty;
            try
            {

                var ds = _baTour.SaveTour(viewModel, ref msg, "0", 0);
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

            var tourTypeItems = allTourType.Select(s => new SelectListItem(s.TourType, s.TourTypeId.ToString())).ToList();
            tourTypeItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllTourType = tourTypeItems;


            return View();
        }
        #endregion

        #region Update
        [Route("[controller]/Edit")]
        [HttpGet]
        public IActionResult Edit(int tourId,int tourTypeId)
        {
            var msg = String.Empty;
            var allTourType = _tourTypeHelper.GetAllTourType(0, ref msg);
            var data = _baTour.GetTour(tourId, tourTypeId, ref msg, "0", 0);
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
            var entity = new List<EntityTour>();
            if (data != null && data.Tables != null && data.Tables.Count > 0)
            {
                entity = _mapper.Map<List<EntityTour>>(data.Tables[1].Rows);
            }
            var tourTypeItems = allTourType.Select(s => new SelectListItem(s.TourType, s.TourTypeId.ToString())).ToList();
            tourTypeItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllTourType = tourTypeItems;
            var viewModel = entity.FirstOrDefault();
            return View(viewModel);
        }

        [Route("[controller]/Edit")]
        [HttpPost]
        public IActionResult Edit(EntityTour viewModel)
        {
            var allTourType = new List<EntityTourType>();

            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }

            var msg = String.Empty;
            try
            {

                var ds = _baTour.SaveTour(viewModel, ref msg, "0", 0);
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

            var tourTypeItems = allTourType.Select(s => new SelectListItem(s.TourType, s.TourTypeId.ToString())).ToList();
            tourTypeItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllTourType = tourTypeItems;


            return View();
        }
        #endregion
    }
}
