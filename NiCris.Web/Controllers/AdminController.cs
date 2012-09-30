using System;
using System.IO;
using System.Web.Mvc;
using NiCris.CoreServices.Interfaces;
using NiCris.CoreServices.Services;
using NLog;

namespace NiCris.Web.Controllers
{
    public class AdminController : Controller
    {
        // FIELDS
        static Logger _logger = LogManager.GetCurrentClassLogger();
        readonly IEmailService _emailService;


        // C~tors
        public AdminController() : this(new SmtpEmailService())
        {
        }
        public AdminController(IEmailService emailService)
        {
            _emailService = emailService;
        }


        // GET: /Admin/Settings
        public ActionResult Settings()
        {
            return View();
        }

        // GET: /Admin/Security
        public ActionResult Security()
        {
            return View();
        }

        // GET: /Admin/Log
        public ActionResult Log()
        {
            string logFilePath = @"~/_Logs";
            string logFileName = "logfile.txt";

            string fullLogFileName = Server.MapPath(Path.Combine(logFilePath, logFileName));
            if (!System.IO.File.Exists(fullLogFileName))
            {
                // return Content("A log file hasn't been created yet.", "text/plain");
                ViewBag.LogFileContent = "A log file hasn't been created yet.";
                return View();
            }

            string content = string.Empty;
            try
            {
                using (var stream = new StreamReader(fullLogFileName))
                {
                    content = stream.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorException("*** An Exception occurred while trying to fetch the Log File\n", ex);
                content = string.Format("An Exception occurred while trying to fetch the Log File\n{0}", ex.Message);
            }

            // Response.ContentEncoding = System.Text.Encoding.UTF8;
            string result = content.Replace(System.Environment.NewLine, "<br />");
            //return Content(result, "text/plain");

            ViewBag.LogFileContent = result;
            return View();
        }

    }
}
