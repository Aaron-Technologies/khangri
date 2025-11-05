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

    public class RoomTypeController : Controller
    {
    //    private readonly IMapper _mapper;
    //    private readonly MiscSettings _miscSettings;
    //    private readonly IUtility _utilities;
    //    private readonly IBaHotel _baHotel;

       // public RoomTypeController(IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, IBaHotel baHotel)
        //{

        //    _mapper = mapper;
        //    _miscSettings = options.Value;
        //    _utilities = utilities;
        //    _baHotel = baHotel;
        //}

        #region List
        //[Route("[Controller]/List")]
        //[HttpGet]
        //public IActionResult List()
        //{
        //    var msg = "";
        //    var allRoomType = new List<EntityRoomType>();
        //    DataSet data = _baHotel.GetRoomType(0, ref msg, "0", 0);
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
        //    if (TempData["DeleteStatus"] != null)
        //    {
        //        ViewBag.Status = TempData["DeleteStatus"];
        //    }
        //    if (data != null && data.Tables != null && data.Tables.Count > 0)
        //    {
        //        allRoomType = _mapper.Map<List<EntityRoomType>>(data.Tables[0].Rows);
        //    }
        //    return View(allRoomType);
        //}
        #endregion

        #region Insert
        //[Route("[controller]/Add")]
        //[HttpGet]
        //public IActionResult Add()
        //{
        //    var viewModel = new EntityRoomType();
        //    viewModel.IsActive = true;
           
        //    return View(viewModel);
        //}

        //[Route("[controller]/Add")]
        //[HttpPost]
        //public IActionResult Add(EntityRoomType viewModel)
        //{
        //    var msg = String.Empty;
        //    if (!ModelState.IsValid)
        //    {
        //        return View(viewModel);

        //    }

        //    try
        //    { 
        //        var ds = _baHotel.SaveRoomType(viewModel, ref msg, "0", 0);
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
           
        //    return View();
       // }
        #endregion

        #region Update
        //[Route("[controller]/Edit")]
        //[HttpGet]
        //public IActionResult Edit(int roomTypeId)
        //{
        //    var msg = String.Empty;
        //    var roomType = _baHotel.GetRoomType(roomTypeId, ref msg,"0",0);
        //    var allRooomType = _mapper.Map<List<EntityRoomType>>(roomType.Tables[1].Rows);

        //    if (!allRooomType.Any())
        //    {
        //        ViewBag.NoLocationFound = true;
        //    }
        //    else
        //    {
        //        ViewBag.NoLocationFound = false;
        //    }
        //    var viewModel = allRooomType.FirstOrDefault();

        //    return View(viewModel);
        //}

        //[Route("[controller]/Edit")]
        //[HttpPost]
        //public IActionResult Edit(EntityRoomType viewModel)
        //{
        //    var msg = String.Empty;
        //    if (!ModelState.IsValid)
        //    {
        //        return View(viewModel);

        //    }

        //    try
        //    {
        //        var ds = _baHotel.SaveRoomType(viewModel, ref msg, "0", 0);
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

        //    return View();
        //}
        #endregion
        #region Image
        //[AllowAnonymous]
        //[Route("[controller]/image")]
        //[HttpGet]
        //public IActionResult Image(int imageId)
        //{
        //    var msg = "";
        //    var data = _baHotel.GetRoomType(imageId, ref msg, "0", 0);

        //    var images = _mapper.Map<List<EntityRoomType>>(data.Tables[1].Rows);

        //    if (!images.Any() || String.IsNullOrWhiteSpace(images.First().ImageFile))
        //    {

        //        return DefaultShipTypeImage();
        //    }
        //    else
        //    {
        //        var imageFile = Path.Combine(_miscSettings.RoomTypeImagePath, images.First().ImageFile);
        //        try
        //        {
        //            var fileContents = System.IO.File.ReadAllBytes(imageFile);
        //            return File(fileContents, Constants.GenericBinaryMimeType);
        //        }
        //        catch (Exception)
        //        {
        //            return DefaultShipTypeImage();
        //        }
        //    }

        //}

        //[NonAction]
        //private IActionResult DefaultShipTypeImage()
        //{
        //    var defaultImageFile = "wwwroot/images/no-image.jpg";
        //    try
        //    {
        //        var fileContents = System.IO.File.ReadAllBytes(defaultImageFile);
        //        return File(fileContents, Constants.GenericBinaryMimeType);
        //    }
        //    catch (Exception)
        //    {
        //        return NotFound();
        //    }
        //}

        #endregion

    }
}
