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

        public ActionResult GetStudentPendingHomework(int studentID)
        {
            var studentPendingHomework = _studentService.GetStudentPendingHomework(studentID);

            return View(studentPendingHomework);
        }

        public ActionResult GetStudentCompletedHomework(int studentID)
        {
            var studentCompletedHomework = _studentService.GetStudentCompletedHomework(studentID);
            return View(studentCompletedHomework);
        }
        public ActionResult InsertStudentToHomework(string userName,int homeworkID,string fileName,string basePath)
        {
            _studentService.InsertStudentToHomework(userName, homeworkID, fileName, basePath);


            return View();
        }


    }
}
