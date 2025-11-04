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

    public class ContactController : Controller
    {
        private readonly IBaContact _baContact;
        private readonly IMapper _mapper;
        private readonly MiscSettings _miscSettings;
        private readonly IUtility _utilities;

        public ContactController(IMapper mapper, IOptions<MiscSettings> options, IUtility utilities, IBaContact baContact)
        {

            _mapper = mapper;
            _miscSettings = options.Value;
            _utilities = utilities;
            _baContact = baContact;
        }

        #region List
        [Route("[Controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var msg = "";
            var allContacts = new List<EntityContact>();
            DataSet data = _baContact.GetContact(0,ref msg,"0",0);
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
                allContacts = _mapper.Map<List<EntityContact>>(data.Tables[1].Rows);
            }
            return View(allContacts);
        }
        #endregion

        #region Insert
        [Route("[controller]/Add")]
        [HttpGet]
        public IActionResult Add()
        {
            var viewModel = new ContactViewModel();
            var msg = String.Empty;
            viewModel.IsActive = true;
            return View(viewModel);
        }

        [Route("[controller]/Add")]
        [HttpPost]
        public IActionResult Add(ContactViewModel viewModel)
        {
            var msg = String.Empty;
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }

            try
            {
                var data = _mapper.Map<EntityContact>(viewModel);
                var ds = _baContact.SaveContact(data, ref msg, "0", 0);
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
        public IActionResult Edit(int contactId)
        {
            var msg = String.Empty;
            var contacts = _baContact.GetContact(contactId, ref msg,"0",0);
            var allContacts = _mapper.Map<List<EntityContact>>(contacts.Tables[1].Rows);

            if (!allContacts.Any())
            {
                ViewBag.NoLocationFound = true;
            }
            else
            {
                ViewBag.NoLocationFound = false;
            }
            var viewModel = allContacts.FirstOrDefault();

            return View(viewModel);
        }

        [Route("[controller]/Edit")]
        [HttpPost]
        public IActionResult Edit(EntityContact viewModel)
        {
            var msg = String.Empty;
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }

            try
            {
                var ds = _baContact.SaveContact(viewModel, ref msg, "0", 0);
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
