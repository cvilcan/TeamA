using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamA.Models;

namespace TeamA.Controllers
{
    public class AccountController : Controller
    {
        UserService userService = new UserService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AccountVM vm)
        {
            if (userService.Login(vm.UserName, vm.Password))
                return RedirectToAction("Register");
            else
                return View("Index");
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(AccountVM vm)
        {

            userService.CreateStudentUser(vm.UserName, vm.Password, vm.Email, vm.TeacherId);

            return View();
        }


    }
}
