using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamA.Controllers
{
    public class AdminController : Controller
    {
        UserService userService;

        public ActionResult Index()
        {
            return View();
        }

    }
}
