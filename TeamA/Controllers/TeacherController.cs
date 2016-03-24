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
        private AdminService _adminService = new AdminService();

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

        public ActionResult ListStudents()
        {
             List<StudentVM> L = new List<StudentVM>();

            var a = userService.GetAllStudents();
            foreach (var item in a)
                L.Add(new StudentVM()
                {
                    StudentName = item.Username,
                    StudentID = item.ID,
                    StudentEmail = item.Email
                });
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View(L);
        }

        [HttpPost]
        public ActionResult SendMailWithPassword(string username)
        {
            _adminService.ResetPasswordSendMail(username);
            return new EmptyResult();
        }
    }
}
