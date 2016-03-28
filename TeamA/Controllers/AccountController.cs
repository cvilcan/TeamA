using BusinessLayer;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamA.Attributes;
using TeamA.Authorize;
using TeamA.Models;

namespace TeamA.Controllers
{   [AllowAnonymous]
    [CookieFilter]
   
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
        public ActionResult Login(AccountVM vm,string ReturnUrl)
        {
            
                if (userService.Login(vm.UserName, vm.Password))
                {
                    Session["SessionUser"] = vm.UserName;
                    Session["SessionID"] = userService.GetUser(vm.UserName).Item1;

                    var cookie = new HttpCookie("Cookie");
                    cookie.Expires = DateTime.Now.AddDays(30);
                    cookie["username"] = vm.UserName;
                    cookie["password"] = vm.Password;
                    
                    
                    Response.AppendCookie(cookie);
                   string  role = userService.GetRole(vm.UserName);




                    return RedirectToAction("Index",role);
                }
            
            
                return View("Index");
            
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

        public ActionResult ConfirmRegistration(string GUID)
        {
            if (userService.CheckGuid(GUID) == 1)
                return View("RegistrationConfirmed");
            else return View("Error", "Invalid confirmation link!");
        }

        public AccountVM CheckCookie()
        {   AccountVM account=null;
               HttpCookie  cookie=null;
            string username = string.Empty; string password = string.Empty;
            if (HttpContext.Request.Cookies["Cookie"] != null) 
             cookie = Request.Cookies["Cookie"];
               username = cookie["username"];
               password = cookie["password"];
            

            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
                account = new AccountVM
                {
                    UserName = username,
                    Password = password
                };
            return account;
        }
    }

}
