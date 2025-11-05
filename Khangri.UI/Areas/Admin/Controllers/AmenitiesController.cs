using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Khangri.DataAccess.Abstract;
using Khangri.DataAccess.KhangriDAL;
using Khangri.Entities;
using Khangri.UI.Utils;
using System.Data;
using System.Text.Json;

namespace Khangri.UI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
	public class AmenitiesController : Controller
	{
		private readonly IBaHotel _baHotel;
		private readonly IMapper _mapper;
		private readonly IUtility _utilities;
		public AmenitiesController(IBaHotel baHotel, IMapper mapper, IUtility utilities)
		{
			_baHotel = baHotel;
			_mapper = mapper;
			_utilities = utilities;
		}


		#region List
		[Route("[Controller]/List")]
		[HttpGet]
		public IActionResult List()
		{
			var msg = "";
			var allAmenities = new List<EntityAmenities>();
			DataSet data = _baHotel.GetAmenities(0, ref msg, "0", 0);
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
				allAmenities = _mapper.Map<List<EntityAmenities>>(data.Tables[0].Rows);
			}
			return View(allAmenities);
		}
		#endregion

		#region Insert
		[Route("[controller]/Add")]
		[HttpGet]
		public IActionResult Add()
		{
			var viewModel = new EntityAmenities();
			var msg = String.Empty;
			viewModel.IsActive = true;
			return View(viewModel);
		}

		[Route("[controller]/Add")]
		[HttpPost]
		public IActionResult Add(EntityAmenities viewModel)
		{
			var msg = String.Empty;
			if (!ModelState.IsValid)
			{
				return View(viewModel);

			}

			try
			{
				var ds = _baHotel.SaveAmenities(viewModel, ref msg, "0", 0);
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
		public IActionResult Edit(int amenitiesId)
		{
			var msg = String.Empty;
			var amenities = _baHotel.GetAmenities(amenitiesId, ref msg, "0", 0);
			var allAmenities = _mapper.Map<List<EntityAmenities>>(amenities.Tables[1].Rows);

			if (!allAmenities.Any())
			{
				ViewBag.NoLocationFound = true;
			}
			else
			{
				ViewBag.NoLocationFound = false;
			}
			var viewModel = allAmenities.FirstOrDefault();

			return View(viewModel);
		}

		[Route("[controller]/Edit")]
		[HttpPost]
		public IActionResult Edit(EntityAmenities viewModel)
		{
			var msg = String.Empty;
			if (!ModelState.IsValid)
			{
				return View(viewModel);

			}

			try
			{
				var ds = _baHotel.SaveAmenities(viewModel, ref msg, "0", 0);
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
