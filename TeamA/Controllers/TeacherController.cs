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
        private UserService userService = new UserService();

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

        public List<StudentVM> L = new List<StudentVM>();

        public ActionResult ListStudents(StudentVM st)
        {
            L.Add(new StudentVM()
            {
                StudentID = 1,
                Username = "Gica Petrescu",
                Email = "gica@mailingator.com"
            });
            L.Add(new StudentVM()
            {
                StudentID = 2,
                Username = "Gheorghe Pop",
                Email = "gheorghe@mailingator.com"
            });
            var a = userService.GetAllStudents();
            foreach (var item in a)
                L.Add(new StudentVM()
                {
                    Username = item.Username,
                    StudentID = item.ID,
                    Email = item.Email
                });
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View(L);
        }

    }
}
