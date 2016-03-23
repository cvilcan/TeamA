using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamA.Models;

namespace TeamA.Controllers
{
    public class TeacherController : Controller
    {
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
            //homeworkService.CreateHomework(vm.);

            return RedirectToAction("Index");
        }

    }
}
