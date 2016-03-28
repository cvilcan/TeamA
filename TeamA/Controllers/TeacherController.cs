using BusinessLayer;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamA.Authorize;
using TeamA.Models;

namespace TeamA.Controllers
{
     [CustomAuthorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        private HomeworkService homeworkService = new HomeworkService();
        private UserService userService = new UserService();
        private FileSystemService fileSystemService = new FileSystemService();

        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Teacher")]
        public ActionResult CreateHomework()
        {
            return View(new HomeworkVM());
        }

        [CustomAuthorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult CreateHomework(HomeworkVM vm)
        {
            homeworkService.CreateHomework(22, vm.Name, vm.Description, vm.Deadline, Server.MapPath(ConfigurationManager.AppSettings["BasePath"]));
            return RedirectToAction("Index");
        }
       
        public ActionResult ListStudents()
        {
            List<StudentVM> L = new List<StudentVM>();
            var a = userService.GetAllStudents();
            foreach (var item in a)
                L.Add(new StudentVM()
                {
                    StudentName = item.Username,
                    StudentID = item.ID,
                    StudentEmail = item.Email
                });
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View(L);
        }


        [HttpPost]
        public ActionResult GeneratePDF()
        {
            List<StudentVM> L = new List<StudentVM>();
            var a = userService.GetAllStudents();
            foreach (var item in a)
                L.Add(new StudentVM()
                {
                    StudentName = item.Username,
                    StudentID = item.ID,
                    StudentEmail = item.Email
                });
            return new Rotativa.ViewAsPdf("Presenter", L);
        }

        public ActionResult ViewStudentUploads(string teacherFolder, string homeworkFolder, string studentFolder, string path)
        {
            string realPath;
            if ((Request.QueryString["teacherFolder"] != Session["SessionUser"] + "_" + Session["SessionID"]) || (Request.QueryString["teacherFolder"] == null))
                return View("Error", "You do not have the right to access this folder!");
            realPath = Server.MapPath(ConfigurationManager.AppSettings["BasePath"] + teacherFolder + "/");
            if (homeworkFolder != null)
                realPath += homeworkFolder + "/";
            if (studentFolder != null)
                realPath += studentFolder + "/";
            if (path != null)
                realPath += path;
            var ex = fileSystemService.GetExplorerModel(realPath, Request.Url);

            if (!ex.isFile)
                return View(ex);
            else
            {
                string fileText = "";
                try
                {
                    fileText = fileSystemService.GetFileText(realPath);
                }
                catch (Exception e)
                {

                }
                return View("ViewStudentHomework", (object)fileText);
            }
        }

        public ActionResult GetOneTeacherHomework(string username)
        {
           var teacherHomeworks= homeworkService.GetOneTeacherHomework(username);
           return View(teacherHomeworks);
        }

        [HttpPost]
        public ActionResult InsertCommentOrGradeOrStatus(int uploadId, int? grade = null, string comment = null)
        {
            try 
			{                 
                 if( grade <=10 && grade >=1)
                    {                
                        homeworkService.InsertCommentOrGradeOrStatus(uploadId, grade, comment);
                        ViewBag.Grade = "Valid Grade";
                    }
                else
                    {
                        ViewBag.Grade = "Please Enter a valid grade between 1 and 10";

            			homeworkService.InsertCommentOrGradeOrStatus(uploadId, grade, comment);
                    }
                return RedirectToAction("ViewStudentHomework");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        public ActionResult DownloadAsPDF(string path)
        {
            userService.SeeInPDF(path);
            EmptyResult result = new EmptyResult();
            return View(result);
        }
    }
}
