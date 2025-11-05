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

namespace Khangri.UI.Controllers
{
    [Area("Admin")]
    //[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class CounterController : Controller
    {
        private readonly IBaCounter _baCounter;
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;

        public CounterController(IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, IBaCounter baCounter)
        {

            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
            _baCounter = baCounter;
        }

        #region List
        [Route("[Controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var msg = "";
            var allCounter = new List<EntityCounter>();
            DataSet data = _baCounter.GetCounter(0,ref msg,"0",0);
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
                allCounter = _mapper.Map<List<EntityCounter>>(data.Tables[1].Rows);
            }
            return View(allCounter);
        }
        #endregion

        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new EntityCounter();
            var msg = String.Empty;
            viewModel.IsActive = true;
            return View(viewModel);
        }

        [Route("[controller]/Add")]
        [HttpPost]
        public IActionResult Add(EntityCounter viewModel)
        {
            var msg = String.Empty;
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }

            try
            { 
                var ds = _baCounter.SaveCounter(viewModel, ref msg, "0", 0);
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
        public IActionResult Edit(int counterId)
        {
            var msg = String.Empty;
            var counters = _baCounter.GetCounter(counterId, ref msg,"0",0);
            var allCounters = _mapper.Map<List<EntityCounter>>(counters.Tables[1].Rows);

            if (!allCounters.Any())
            {
                ViewBag.NoLocationFound = true;
            }
            else
            {
                ViewBag.NoLocationFound = false;
            }
            var viewModel = allCounters.FirstOrDefault();

            return View(viewModel);
        }

        [Route("[controller]/Edit")]
        [HttpPost]
        public IActionResult Edit(EntityCounter viewModel)
        {
            var msg = String.Empty;
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }

            try
            {
                var ds = _baCounter.SaveCounter(viewModel, ref msg, "0", 0);
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
