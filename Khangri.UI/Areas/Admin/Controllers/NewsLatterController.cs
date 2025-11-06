using Khangri.DataAccess.KhangriDAL;
using Khangri.DataAccess.Abstract;
using Khangri.Entities;
using Khangri.UI.Models;
using Khangri.UI.Services;
using Khangri.UI.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;

namespace Khangri.UI.Controllers
{
    [Area("Admin")]
    public class NewsLatterController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IBaNewLatterContact _baNewLatterContact;
        private readonly IUtility _utilities;
        private readonly IMapper _mapper;
        public NewsLatterController(IEmailService emailService, IBaNewLatterContact baNewLatterContact, IUtility utilities, IMapper mapper)
        {
            _emailService = emailService;
            _baNewLatterContact = baNewLatterContact;
            _utilities = utilities;
            _mapper = mapper;
        }

        #region List
        [Route("[Controller]/List")]
        [HttpGet]
        public IActionResult List()
        {
            var msg = "";
            var allMails = new List<EntityNewsLetterContactEmail>();
            DataSet data = _baNewLatterContact.GetNewLatterContact(0, ref msg, "0", 0);
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
                allMails = _mapper.Map<List<EntityNewsLetterContactEmail>>(data.Tables[1].Rows);
            }
            return View(allMails);
        }
        #endregion


        [Route("[controller]/Mail")]
        [HttpPost]
        public async Task<IActionResult> Mail([FromBody] EmailRequest email)
        {
            var fileText = String.Empty;
            var msg=String.Empty;
            var model = new EntityNewsLetterContactEmail();
            model.NewsLetterContactEmail = email.Email;
            model.IsActive = true;
            using (var fs = System.IO.File.OpenRead("Resource\\email_subscribe_template.html"))
            {
                using (var reader = new StreamReader(fs))
                {
                    fileText = await reader.ReadToEndAsync();
                }
            }
            fileText = fileText.Replace("@@User", email.Email);
            fileText = fileText.Replace("@@Company", Constants.CompanyName);
            var status = await _emailService.SendEmailAsync(email.Email, Khangri.Entities.Constants.NewsLetterEmailSubject, fileText);
            if (String.IsNullOrWhiteSpace(status))
            {
                var response = new ActionStatus
                {
                    Status = Constants.StatusFail,
                    Title = "Error",
                    Description = $"Error sending email: {status}"
                };
                return Json(response);
            }
            //ViewBag.SuccessMessage = "A password reset link has been successfully sent your registered email address. " +
            //    "Please check you email for further instructions on how to reset your password.";
            try
            {
                var data = _baNewLatterContact.SaveNewLatterContact(model, ref msg, "0", 0);
                if (String.IsNullOrWhiteSpace(msg))
                {
                    msg = _utilities.GetErrorMessageFromDataSet(data);
                }
            }
            catch (Exception ex)
            {
                msg=ex.Message;
            }
            if (!String.IsNullOrWhiteSpace(msg))
            {
                 var response = new ActionStatus()
                {
                    Status = Constants.StatusFail,
                    Title = "Error",
                    Description = msg

                };
                return Json(response);
            }
            else
            {
                var statusSuccess = new ActionStatus
                {
                    Status = Constants.StatusSuccess,
                    Title = "Thank You",
                    Description = model.NewsLetterContactEmail
                };
                return Json(statusSuccess);
            }
            
        }
    }
}
