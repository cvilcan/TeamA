using AccessModels.Models;
using BusinessLayer;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamA.Attributes;
using TeamA.Authorize;
using TeamA.Models;

namespace TeamA.Controllers
{
    [CookieFilter]
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
            try
            {
                homeworkService.CreateHomework((int)Session["SessionUserId"], vm.Name, vm.Description, vm.Deadline, ConfigurationManager.AppSettings["BasePath"]);                
                
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error",(object)"Invalid credentials");
            }
        }

        [CustomAuthorize(Roles = "Teacher")]
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


       // [CustomAuthorize(Roles = "Teacher")]
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

        public ActionResult GenerateHomeworkPDF()
        {
            List<HomeworkVM> L = new List<HomeworkVM>();
            var a = homeworkService.GetOneTeacherHomework((string)Session["SessionUser"]);
            foreach (var item in a)
                L.Add(new HomeworkVM()
                {
                    HomeworkID=item.HomeworkID,
                    Name = item.Name,
                    Description = item.Description,
                    Deadline = item.Deadline
                });
            return new Rotativa.ViewAsPdf("HomeworkPresenter", L);
        }

        public ActionResult ViewStudentUploads(string teacherFolder, string homeworkFolder, string studentFolder, string path)
        {
            string realPath;
            if ((Request.QueryString["teacherFolder"] != Session["SessionUser"] + "_" + Session["SessionUserId"]) || (Request.QueryString["teacherFolder"] == null))
                return View("Error", "You do not have the right to access this folder!");
            realPath = ConfigurationManager.AppSettings["BasePath"] + teacherFolder + "/";
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
                catch (Exception)
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


        //Insert Grade 
        [HttpPost]
        public ActionResult InsertGradeOrStatus(int uploadId, int? grade = null, string comment = null)
        {
            try 
			{                              

                if(grade != null)
                { 
                     homeworkService.InsertCommentOrGradeOrStatus(uploadId, grade, comment);
                     return Content("Success!");
                }
                else
                {
                    return Content("Please enter a grade!");
                }
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }

        // Insert Comment
        [HttpPost]
        public ActionResult InsertCommentOrStatus(int uploadId, int? grade = null, string comment = null)
        {
            try
            {
                if (comment != null) 
				{ 
                
                homeworkService.InsertCommentOrGradeOrStatus(uploadId, null, comment);
                }
                return Content("Success!");
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }
       
        //Insert Rejected Status
        [HttpPost]
        public ActionResult InsertStatus(int uploadId, int? grade = null, string comment = null)
        {
            try
            {
                homeworkService.InsertCommentOrGradeOrStatus(uploadId, null, null);
               
                return Content("Success!");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }        
        }

        public ActionResult DownloadAsPDF(string path)
        {
            userService.SeeInPDF(path);
            EmptyResult result = new EmptyResult();
            return View(result);
        }

        //De facut View si scos raportul cu top 10 studenti in functie de numele profesorului
        public PartialViewResult GetStudentsAvgGradeByTeacher()
        {

            List<StudentToHomework> studentAvgGradeByTeacher = homeworkService.GetStudentsAvgGradeByTeacher((string)Session["SessionUser"]);


            
            return PartialView("GetStudentsAvgGradeByTeacher", studentAvgGradeByTeacher);

        }
        //De facut View si scos raportul cu top 10 studenti in functie de numele profesorului si de tema 
        public PartialViewResult GetStudentsGradeByTeacherAndHomework(string userName, int homeworkID)
        {



            List<StudentToHomework> studentGradeByTeacherAndHomework = homeworkService.GetStudentsGradeByTeacherAndHomework(userName, homeworkID);
            HomeworkListVM homeworkVm = new HomeworkListVM()
            {

                HomeworkList = homeworkService.GetOneTeacherHomework((string)Session["SessionUser"]).Select(x => x.Name).ToList()              
            };
            //vm.TeacherNameList.Insert(0, null);

            //return View(vm);




            return PartialView(studentGradeByTeacherAndHomework);
        }

        public ActionResult Raports()
        {
            //HomeworkListVM homeworkVm = new HomeworkListVM()
            //{

            //    HomeworkList = homeworkService.GetOneTeacherHomework((string)Session["SessionUser"]).Select(x => x.Name).ToList()
            //};
            List<HomeworkVM> vmList = homeworkService.GetOneTeacherHomework((string)Session["SessionUser"]).ToList();

            return View("Raports", vmList);

            

        }

        public ActionResult ViewOneTeacherHomeworks()
        {
            var homeworks= homeworkService.GetOneTeacherHomework((string)Session["SessionUser"]);
            return View(homeworks);
        }
    }
}
