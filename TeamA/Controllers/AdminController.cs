using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

using TeamA.Models;
using AccessModels.Models;




namespace TeamA.Controllers
{
    public class AdminController : Controller
    {
        AdminService adminService = new AdminService();
        UserService userService = new UserService();
        public ActionResult CreateTeacher()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult CreateTeacher(TeacherVM tcr)
        {
            adminService.addTeachersFromAdmin(tcr.Username,tcr.Email);

            return View();
        }

        public ActionResult Index()
        {
            
            return View(userService.GetAllStudents());
        }

        public ActionResult ResetPassword()
        {
            return View();
        }


    }
}



    


