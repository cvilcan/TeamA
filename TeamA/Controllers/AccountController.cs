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
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            TeacherListVM vm = new TeacherListVM()
            {
                TeacherNameList = userService.GetAllTeachers().Select(x => x.Username).ToList()

            };

            return View(vm);
        }


        [HttpPost]
        public ActionResult Register(AccountVM vm)
        {
            if (ModelState.IsValid)
            {
                userService.CreateStudentUser(vm.UserName, vm.Password, vm.Email, vm.TeacherId);
            }
            TeacherListVM listVM = new TeacherListVM()
            {
                TeacherNameList = userService.GetAllTeachers().Select(x => x.Username).ToList()

            };
            return View(listVM);
        }


    }
}
