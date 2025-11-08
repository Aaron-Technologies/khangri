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
    
    public class TrekDetailsController : Controller
    {
       
        private readonly IMapper _mapper;
        private readonly IBaWEBTourDetails _baWEBTourDetails;
        private readonly IContactHelper _contactHelper;
        private readonly IEmailService _emailService;
        private readonly ITourHelper _tourHelper;
		private readonly IBaWEBTourList _baWebTourList;

		public TrekDetailsController(IMapper mapper, IBaWEBTourDetails baWEBTourDetails, IContactHelper contactHelper, IEmailService emailService, ITourHelper tourHelper, IBaWEBTourList baWebTourList)
		{
			_mapper = mapper;
			_baWEBTourDetails = baWEBTourDetails;
			_contactHelper = contactHelper;
			_emailService = emailService;
			_tourHelper = tourHelper;
			_baWebTourList = baWebTourList;
		}

		[Route("[controller]/list")]
        [HttpGet]
        public IActionResult List(int tourTypeId)
        {
            var msg=String.Empty;
            var Trek = new List<TourViewModel>();
            var allTrek=_baWebTourList.GetTourList(tourTypeId,ref msg, "0",0);
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
			if (allTrek != null && allTrek.Tables != null && allTrek.Tables.Count > 0)
			{
				Trek = _mapper.Map<List<TourViewModel>>(allTrek.Tables[1].Rows);


			}
			return View(Trek);
        }


        [Route("[controller]/DetailsPage")]
        [HttpGet]
        public IActionResult DetailsPage(int tourId)
        {
            var tourDetailsViewModel = new TourdetailsViewModel();
            var msg = String.Empty;

            var data = _baWEBTourDetails.GetTourDetails(tourId, ref msg, "0", 0);
            
            if (data != null && data.Tables != null && data.Tables.Count>0)
            {
                var tour = _mapper.Map<List<EntityTour>>(data.Tables[1].Rows);
                tourDetailsViewModel.Tours = _mapper.Map<List<TourViewModel>>(tour).FirstOrDefault();
                var tourItinery = _mapper.Map<List<EntityTourItinerary>>(data.Tables[2].Rows);
                tourDetailsViewModel.TourItinerary = _mapper.Map<List<TourItineraryViewModel>>(tourItinery);
                var tourImage = _mapper.Map<List<EntityTourImage>>(data.Tables[3].Rows);
                tourDetailsViewModel.TourImages = _mapper.Map<List<TourImageViewModel>>(tourImage);
                var tourTermsCondition = _mapper.Map<List<TourTermsConditionViewModel>>(data.Tables[4].Rows);
                tourDetailsViewModel.TourTermsCondition = tourTermsCondition.FirstOrDefault();
            }
            var allcontacts = _contactHelper.GetAllContact(0, ref msg);
            tourDetailsViewModel.Contacts= _mapper.Map<List<ContactViewModel>>(allcontacts);
            return View(tourDetailsViewModel);
        }
    }
}
