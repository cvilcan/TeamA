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

        public ActionResult Index(string teacherName)
        {
            //var studList = _studentService.GetStudentTeacher(teacherName);
            return View();
        }

        public ActionResult GetStudentPendingHomeworkDetails(int studentID)
        {
             // var studentPendingHomework= _studentService.GetStudentPendingHomeworkDetails(studentID);

            return View();
        }


    }
}
