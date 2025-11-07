using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Helpers;
using Khangri.UI.Models;
using Khangri.UI.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Khangri.UI.Controllers
{
    
    public class LocationDetailsController : Controller
    {
       
        private readonly IMapper _mapper;
		private readonly IBaLocationDetails _baLocationDetails;
		private readonly IContactHelper _contactHelper;
        private readonly ILocationHelper _locationHelper;

		public LocationDetailsController(IMapper mapper, IContactHelper contactHelper, ILocationHelper locationHelper, IBaLocationDetails baLocationDetails)
		{
			_mapper = mapper;
			_contactHelper = contactHelper;
			_locationHelper = locationHelper;
			_baLocationDetails = baLocationDetails;
		}


		[Route("[controller]/list")]
		[HttpGet]
		public IActionResult List()
		{
			var msg = String.Empty;
			var allLocations = _locationHelper.GetAllLocationViewModels(0, ref msg);
			if (!String.IsNullOrWhiteSpace(msg))
			{
				var status = new ActionStatus()
				{
					Status = Constants.NoDataFound,
					Title = "Error",
					Description = msg
				};
				ViewBag.Status = JsonSerializer.Serialize(status);
			}
			return View(allLocations);
		}

		[Route("[controller]/DetailsPage")]
        [HttpGet]
        public IActionResult DetailsPage(int locationId)
        {
			var msg = String.Empty;
			var viewModel = new LocationDetailsViewModel();
			var data = _baLocationDetails.GetLocationsDetails(locationId, ref msg, "0", 0);
			if (data != null && data.Tables != null && data.Tables.Count > 0)
			{
				var entity = _mapper.Map<List<LocationViewModel>>(data.Tables[1].Rows);
				viewModel.Location = _mapper.Map<LocationViewModel>(entity.FirstOrDefault());
				var entity2 = _mapper.Map<List<EntitySightSeeing>>(data.Tables[2].Rows);
				viewModel.SightSeeing = entity2;
				var entity3 = _mapper.Map<List<EntityLocationImage>>(data.Tables[3].Rows);
				viewModel.Image = entity3;
			}
			var allcontacts = _contactHelper.GetAllContact(0, ref msg);
			viewModel.Contacts = _mapper.Map<List<ContactViewModel>>(allcontacts);

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
    }
}
