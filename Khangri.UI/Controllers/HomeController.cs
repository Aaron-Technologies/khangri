using AutoMapper;
using Khangri.DataAccess.Abstract;
using Khangri.UI.Helpers;
using Khangri.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Khangri.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISliderImageHelper _sliderImageHelper;
        private readonly ILocationHelper _locationHelper;
        private readonly IBaWEBTourList _baWebTourList;
        private readonly ICounterHelper _counterHelper;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, ISliderImageHelper sliderImageHelper, ILocationHelper locationHelper, ICounterHelper counterHelper, IBaWEBTourList baWebTourList, IMapper mapper)
        {
            _logger = logger;
            _sliderImageHelper = sliderImageHelper;
            _locationHelper = locationHelper;
            _counterHelper = counterHelper;
            _baWebTourList = baWebTourList;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            var msg = String.Empty;
            var allSliderImages = _sliderImageHelper.GetAllSliderImages(0, ref msg);
            var allLocations = _locationHelper.GetAllLocationViewModels(0, ref msg);
            var allTour = _baWebTourList.GetTourList(0, ref msg, "0", 0);
            var allCounter = _counterHelper.GetAllCounter(0, ref msg);
            if (allTour != null && allTour.Tables != null && allTour.Tables.Count > 0)
            {
                var entity = _mapper.Map<List<TourViewModel>>(allTour.Tables[1].Rows);
                homeViewModel.Tours = entity;

            }

            homeViewModel.SliderImages = allSliderImages;
            homeViewModel.Locations = allLocations;
            homeViewModel.Counters = allCounter;
            return View(homeViewModel);
        }

        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult LocationDetails()
        {
            return View();
        }

       
        public IActionResult TrekkingList()
        {
            return View();
        }
        public IActionResult TrekkingDetails()
        {
            return View();
        }
        
    }

}
