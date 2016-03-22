using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

using TeamA.Models;




namespace TeamA.Controllers
{
    public class AdminController : Controller
    {
        UserService userService = new UserService();

        public ActionResult CreateTeacher()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult CreateTeacher(TeacherVM tcr)
        {
            //UserService.CreateTeacher();

            return View();
        }

        public ActionResult Index()
        {
            return View("CreateTeacher");
        }




    }
}



    


