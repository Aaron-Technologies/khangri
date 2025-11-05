using Khangri.DataAccess.KhangriDAL;
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

    public class TourTermsConditionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;
        private readonly IBaTourTermsCondition _baTourTermsCondition;
        private readonly ITourTypeHelper _tourTypeHelper;
        public TourTermsConditionController(IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, IBaTourTermsCondition baTourTermsCondition, ITourTypeHelper tourTypeHelper)
        {
            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
            _baTourTermsCondition = baTourTermsCondition;
            _tourTypeHelper = tourTypeHelper;
        }

        #region List
        [Route("[controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var msg = String.Empty;
            var tourTermsCondition=new TourTermsConditionViewModel();
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
            return View(tourTermsCondition);
        }
        [Route("[controller]/List")]
        [HttpPost]
        public IActionResult List(int tourTypeId)
        {
            var msg = "";
            var allTourTermsCondition = new TourTermsConditionViewModel();
            var data = _baTourTermsCondition.GetTourTermsCondition(0, tourTypeId, ref msg,"0",0);
            var allTourType = _tourTypeHelper.GetAllTourType(tourTypeId, ref msg);

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
     
                var entity = _mapper.Map<List<TourTermsConditionViewModel>>(data.Tables[1].Rows);
                allTourTermsCondition=entity.FirstOrDefault();
            }
            var tourTypeItems = allTourType.Select(s => new SelectListItem(s.TourType, s.TourTypeId.ToString())).ToList();
            tourTypeItems.Insert(0, new SelectListItem("Please Select", "0"));
            ViewBag.AllTourType = tourTypeItems;
            return View(allTourTermsCondition);
        }
        #endregion

        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new TourTermsConditionViewModel();
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
        public IActionResult Add(TourTermsConditionViewModel viewModel)
        {
            var msg = String.Empty;
            var allTourType = _tourTypeHelper.GetAllTourType(0, ref msg);
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }

            msg = String.Empty;
            try
            {
                var entity = _mapper.Map<EntityTourTermsCondition>(viewModel);
                var ds = _baTourTermsCondition.SaveTourTermsCondition(entity, ref msg, "0", 0);
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

            return View(viewModel);
        }
        #endregion

        //#region Update
        [Route("[controller]/Edit")]
        [HttpPost]
        public IActionResult Edit(TourTermsConditionViewModel viewModel)
        {
            var msg = String.Empty;
            var allTourType = _tourTypeHelper.GetAllTourType(0, ref msg);
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }

            msg = String.Empty;
            try
            {
                var entity = _mapper.Map<EntityTourTermsCondition>(viewModel);
                var ds = _baTourTermsCondition.SaveTourTermsCondition(entity, ref msg, "0", 0);
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
            var emptyViewModel=new TourTermsConditionViewModel();
            return View("list",emptyViewModel);
        }

        //[Route("[controller]/Edit")]
        //[HttpPost]
        //public IActionResult Edit(EntityTourItinerary viewModel)
        //{
        //    var msg = String.Empty;
        //    viewModel.IsActive = true;
        //    var allTour = _tourHelper.GetAllTour(0, 0, ref msg);
        //    var allImages = _imageHelper.GetAllImages(0, ref msg);

        //    if (!ModelState.IsValid)
        //    {
        //        return View(viewModel);

        //    }

        //    msg = String.Empty;
        //    try
        //    {

        //        var ds = _baTourItinerary.SaveTourItinerary(viewModel, ref msg, "0", 0);
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
        //    var tourItems = allTour.Where(s => (onlyActive ? s.IsActive : true)).Select(s => new SelectListItem(s.TourName, s.TourId.ToString())).ToList();
        //    tourItems.Insert(0, new SelectListItem("Please Select", "0"));
        //    ViewBag.AllTour = tourItems;
        //    onlyActive = false;
        //    var imageItems = allImages.Where(s => (onlyActive ? s.IsActive : true)).Select(s => new SelectListItem(s.ImageName, s.ImageId.ToString())).ToList();
        //    imageItems.Insert(0, new SelectListItem("Please Select", "0"));
        //    ViewBag.AllImages = imageItems;


        //    return View();
        //}
        //#endregion


    }
}
