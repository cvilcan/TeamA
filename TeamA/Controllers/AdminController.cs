using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using TeamA.Models;
using AccessModels.Models;
using System.Configuration;
using BusinessLayer.Models;
using TeamA.Authorize;

namespace TeamA.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private AdminService adminService = new AdminService();
        UserService userService = new UserService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateTeacher()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult CreateTeacher(TeacherVM tcr)
        {
            adminService.addTeachersFromAdmin(tcr.Username,tcr.Email, Server.MapPath(ConfigurationManager.AppSettings["BasePath"]));

            return View();
        }

        public ActionResult ViewAllStudents()
        {
            var lista = userService.GetAllStudents().ToList();
            List<AccountVM> VMList = new List<AccountVM>();
            foreach (var item in lista)
            {
                VMList.Add(new AccountVM()
                {
                    UserName=item.Username,
                    Email=item.Email,
                    //TeacherId=item.
                    
                });
            }
            return View(VMList);
        }
        public ActionResult ViewAllTeachers()
        {
            var lista = userService.GetAllTeachers().ToList();
            List<TeacherVM> VMList = new List<TeacherVM>();
            foreach (var item in lista)
            {
                VMList.Add(new TeacherVM()
                    {
                        Username = item.Username,
                        Email = item.Email
                    });
            }
            return View(VMList.ToList());
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMailWithPassword(string username)
        {
            adminService.ResetPasswordSendMail(username);
            return new EmptyResult();
        }
    }
}



    


