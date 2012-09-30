using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using NiCris.BusinessObjects;
using NiCris.DataAccess.Dapper;
using NiCris.DataAccess.Interfaces;
using NiCris.Web.Hubs;
using SignalR;
using SignalR.Hubs;

namespace NiCris.Web.Controllers
{
    public class TaskController : Controller
    {
        // FIELDS
        private static readonly Random _rnd = new Random();
        ITaskRepository _taskRepository;
        IHubContext _context;

        // C~tors
        public TaskController()
        {
            _taskRepository = new TaskRepository();
            _context = GlobalHost.ConnectionManager.GetHubContext<TaskHub>();
        }

        // VIEWS
        public ActionResult Index()
        {
            ViewBag.ClientName = "user-" + _rnd.Next(10000, 99999);
            return View();
        }

        // JSON PROVIDERS
        [HttpPost]
        public JsonResult TaskList()
        {
            try
            {
                var taskRespo = new TaskRepository();
                List<TaskEx> tasks = taskRespo.GetAllTasks();
                return Json(new { Result = "OK", Records = tasks });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult CreateTask(TaskEx task)
        {
            try
            {
                // Validation
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                var taskRespo = new TaskRepository();
                TaskEx addedTask = taskRespo.AddTask(task);

                // Inform all connected clients
                var clientName = Request["clientName"];
                Task.Factory.StartNew(
                    () =>
                    {
                        // var clients = Hub.GetClients<TaskHub>();
                        _context.Clients.RecCreated(clientName, task);
                    });

                // Return result to current (caller) client
                return Json(new { Result = "OK", Record = addedTask });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult UpdateTask(TaskEx task)
        {
            try
            {
                // Validation
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                var taskRespo = new TaskRepository();
                taskRespo.UpdateTask(task);

                // Inform all connected clients
                var clientName = Request["clientName"];
                Task.Factory.StartNew(
                    () =>
                    {
                        // var clients = Hub.GetClients<TaskHub>();
                        _context.Clients.RecUpdated(clientName, task);
                    });

                // Return result to current (caller) client
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteTask(int id)
        {
            try
            {
                var taskRespo = new TaskRepository();
                taskRespo.DeleteTask(id);

                // Inform all connected clients
                var clientName = Request["clientName"];
                Task.Factory.StartNew(
                    () =>
                    {
                        // var clients = Hub.GetClients<TaskHub>();
                        _context.Clients.RecDeleted(clientName, id);
                    });

                // Return result to current (caller) client
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}
