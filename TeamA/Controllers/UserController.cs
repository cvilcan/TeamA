using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using TeamA.Models;

namespace TeamA.Controllers
{


    public class UserController : Controller
    {

        UserService userService = new UserService();

        public ActionResult CreateStudentUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStudentUser(AccountVM vm)
        {
            
            //userService.CreateStudentUser(vm.UserName, vm.Password,vm.Email,vm.TeacherId);
            
            return View();
        }



    }
}
