using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NiCris.Web.ViewModels;
using NiCris.Web.Extensions;

namespace NiCris.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Display the Navigation partial view.
        /// </summary>
        public ActionResult Navigation()
        {
            return View("_Navigation");
        }

        /// <summary>
        /// Display the MainPanelItems partial view.
        /// </summary>
        public ActionResult MainPanelItems()
        {
            return View("_MainPanel");
        }


        // *** JSON & JSONP providers for the Main Panel Items & Panel Menu on the left
        public JsonResult PanelData()
        {
            var tasks = GetTasksEx().Select(x => new PanelData { text = x.Name, url = x.Url, imageUrl = Url.Content("~/Content/images/") + x.Icon });
            var samples = GetSamplesEx().Select(x => new PanelData{ text = x.Name, url = x.Url, imageUrl = Url.Content("~/Content/images/") + x.Icon});
            var admins = GetAdminEx().Select(x => new PanelData { text = x.Name, url = x.Url, imageUrl = Url.Content("~/Content/images/") + x.Icon }); 
            var feedback = GetFeedbackEx().Select(x => new PanelData
            {
                text = x.Name,
                url = x.Url,
                imageUrl = Url.Content("~/Content/images/") + x.Icon
            });

            var panelData = new List<PanelData>
                            {
                                new PanelData {text = "Tasks", imageUrl = Url.Content("~/Content/images/tasks_header.png"), items = tasks},
                                new PanelData {text = "Samples", imageUrl = Url.Content("~/Content/images/bestseller_header.png"), items = samples},
                                new PanelData {text = "Administration", imageUrl = Url.Content("~/Content/images/admin_header.png"), items = admins},
                                new PanelData {text = "Feedback", imageUrl = Url.Content("~/Content/images/feedback_header.png"), items = feedback}
                            };

            return Json(panelData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMainPanelItems()
        {
            // JSONP is needed here
            return new JsonpResult(GetMainPanelItemsEx());
        }


        // *** Helper Methods
        private IList<PanelItemEx> GetMainPanelItemsEx()
        {
            IList<PanelItemEx> mainPanelItems = new List<PanelItemEx>();
            mainPanelItems = new List<PanelItemEx>();
            mainPanelItems.Add(new PanelItemEx { Id = 1, Url = Url.Action("Index", "Task"), Title = "Tasks", Name = "Dashboard", Icon = "TasksMain.png" });

            mainPanelItems.Add(new PanelItemEx { Id = 2, Url = Url.Action("Settings", "Admin"), Title = "Settings", Name = "Administration", Icon = "SettingsMain.png" });
            mainPanelItems.Add(new PanelItemEx { Id = 3, Url = Url.Action("Security", "Admin"), Title = "Security", Name = "Administration", Icon = "SecurityMain.png" });
            mainPanelItems.Add(new PanelItemEx { Id = 4, Url = Url.Action("Log", "Admin"), Title = "Log", Name = "Log File Content", Icon = "LogMain.png" });

            mainPanelItems.Add(new PanelItemEx { Id = 5, Url = Url.Action("Index", "Feedback"), Title = "Feedback", Name = "Comments/Requests", Icon = "FeedbackMain.png" });
            return mainPanelItems;
        }

        private IList<PanelItem> GetTasksEx()
        {
            IList<PanelItem> tasks = new List<PanelItem>();
            tasks.Add(new PanelItem { Id = 1, Url = Url.Action("Index", "Task"), Name = "Dashboard", Icon = "task.png" });
            return tasks;
        }
        private IList<PanelItem> GetSamplesEx()
        {
            IList<PanelItem> samples = new List<PanelItem>();
            samples.Add(new PanelItem { Id = 1, Url = Url.Action("Notifications", "Samples"), Name = "Notifications", Icon = "notification.png" });
            samples.Add(new PanelItem { Id = 2, Url = Url.Action("Search", "Samples"), Name = "Search", Icon = "search.png" });
            return samples;
        }
        private IList<PanelItem> GetAdminEx()
        {
            IList<PanelItem> admins = new List<PanelItem>();
            admins.Add(new PanelItem { Id = 1, Url = Url.Action("Settings", "Admin"), Name = "Settings", Icon = "setting.png" });
            admins.Add(new PanelItem { Id = 2, Url = Url.Action("Security", "Admin"), Name = "Security", Icon = "security.png" });
            admins.Add(new PanelItem { Id = 3, Url = Url.Action("Log", "Admin"), Name = "Log", Icon = "log.png" });
            return admins;
        }
        private IList<PanelItem> GetFeedbackEx()
        {
            IList<PanelItem> feedback = new List<PanelItem>();
            feedback = new List<PanelItem>();
            feedback.Add(new PanelItem { Id = 1, Url = Url.Action("Index", "Feedback"), Name = "Comments & Requests", Icon = "feedback.png" });
            return feedback;
        }
    }

}

/* Icons */
// http://www.iconfinder.com/search/?q=iconset%3Afrankfurt
// http://www.iconfinder.com/search/1/?q=iconset%3Ade-munich-icon-pack

// http://www.iconfinder.com/search/?q=iconset%3Acc_mono_icon_set