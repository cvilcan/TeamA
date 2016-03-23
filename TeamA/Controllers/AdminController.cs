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


    }
}



    


