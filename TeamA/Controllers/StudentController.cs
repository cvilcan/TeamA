using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamA.Controllers
{
    public class StudentController : Controller
    {
        private StudentService _studentService = new StudentService();

        public ActionResult Index()
        {
            //var studList = _studentService.GetStudentTeacher();
            return View();
        }

    }
}
