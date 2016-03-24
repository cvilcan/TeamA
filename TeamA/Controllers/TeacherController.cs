using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamA.Models;

namespace TeamA.Controllers
{
    public class TeacherController : Controller
    {
        private HomeworkService homeworkService = new HomeworkService();
        //
        // GET: /Teacher/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateHomework()
        {
            return View(new HomeworkVM());
        }

        [HttpPost]
        public ActionResult CreateHomework(HomeworkVM vm)
        {
            homeworkService.CreateHomework(22, vm.Name, vm.Description, vm.Deadline, Server.MapPath(ConfigurationManager.AppSettings["BasePath"]));

            return RedirectToAction("Index");
        }

    }
}
