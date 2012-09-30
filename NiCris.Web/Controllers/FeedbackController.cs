using System;
using System.IO;
using System.Net.Mail;
using System.Web.Mvc;
using NiCris.CoreServices.Interfaces;
using NiCris.CoreServices.Services;
using NiCris.Web.ViewModels;
using NLog;

namespace NiCris.Web.Controllers
{
    public class FeedbackController : Controller
    {
        // FIELDS
        static Logger _logger = LogManager.GetCurrentClassLogger();
        readonly IEmailService _emailService;


        // C~tors
        public FeedbackController() : this(new SmtpEmailService())
        {
        }
        public FeedbackController(IEmailService emailService)
        {
            _emailService = emailService;
        }


        // GET: /Feedback
        public ActionResult Index()
        {
            var model = new FeedbackModel();
            return View(model);
        }

        // POST: /Feedback
        [HttpPost]
        public ActionResult Index(FeedbackModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mail = new MailMessage();
                    
                    mail.To.Add("boris.momtchev@home.com");
                    // mail.From = new MailAddress(model.Email);
                    
                    mail.Subject = string.Format("NiCris Starter - message from: {0}, {1}", model.Name, model.Email);
                    mail.Body = string.Format("Subject: {0} <br/><br/>Body: <br/>{1}", model.Subject, model.Message);
                    mail.IsBodyHtml = true;

                    _emailService.SendEmailAsync(mail);
                }
                catch (Exception ex)
                {
                    // We couldn't send the email - report a general error for now
                    return Content("Error: " + ex.Message, "text/html");
                }

                // return RedirectToAction("Success");
                return Content("Thank you for your feedback!", "text/html");
            }

            // Invalid – redisplay with errors (kicks in only for an invalid Email, Email is validated on the Server)
            return View(model);
        }
    }
}
