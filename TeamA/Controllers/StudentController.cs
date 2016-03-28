﻿using AccessModels.Models;
//using AccessModels.Models;
using BusinessLayer;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamA.Controllers
{   


    public class StudentController : Controller
    {
        private StudentService _studentService = new StudentService();
        private FileSystemService _fileSystemService = new FileSystemService();

        public ActionResult Index()
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

        [HttpPost]
        public ActionResult ViewHomeworkDetails(StudentHomeworkDetails model, string teacherFolder, string homeworkFolder, string studentFolder, string path)
        {
            if (ModelState.IsValid)
            {
                if (teacherFolder == null)
                    return View("Error", "You do not have the rights to access root folder!");
                else
                {
                    string teacherName = teacherFolder.Split('_')[0];
                    var teacherList = _studentService.GetTeachersBelongingToStudent((string)Session["SessionUser"]).ToList();
                    bool found = false;
                    for (int i = 0; i < teacherList.Count; i++)
                        if (teacherList[i].Item3 == teacherName)
                            found = true;
                    if ((Request.QueryString["studentFolder"] != (string)Session["SessionUser"] + "_" + Session["SessionID"]) || (studentFolder == null)
                        || (!found) || (homeworkFolder == null))
                        return View("Error", "You do not have the rights to access root folder!");
                    string realPath = Server.MapPath(ConfigurationManager.AppSettings["BasePath"] + teacherFolder + "/" + homeworkFolder + "/" + studentFolder + "/");
                    if (path != null)
                        realPath += path;
                    StudentHomeworkDetailsVM vm = new StudentHomeworkDetailsVM();
                    vm.Details = model;
                    //string realPath = Server.MapPath(ConfigurationManager.AppSettings["BasePath"] +
                    //    "/" + (string)Session["SessionUser"] + "_" + Session["SessionID"] + "/");
                    vm.FolderStructure = _fileSystemService.GetExplorerModel(realPath, Request.Url);
                    if (!vm.FolderStructure.isFile)
                        return View(vm);
                    else
                    {
                        string fileText = "";
                        try
                        {
                            fileText = _fileSystemService.GetFileText(realPath);
                        }
                        catch (Exception e)
                        {

                        }
                     	return View("ViewStudentHomework", (object)fileText);
                    }
                }
            }
            else return View("Error", "Invalid data input!");
        }

        public ActionResult ViewHomeworkDetails(string teacherFolder, string homeworkFolder, string studentFolder, string path)
        {
            if (teacherFolder == null)
                return View("Error", "You do not have the rights to access root folder!");
            else
            {
                string teacherName = teacherFolder.Split('_')[0];
                var teacherList = _studentService.GetTeachersBelongingToStudent((string)Session["SessionUser"]).ToList();
                bool found = false;
                for (int i = 0; i < teacherList.Count; i++)
                    if (teacherList[i].Item3 == teacherName)
                        found = true;
                if ((Request.QueryString["studentFolder"] != (string)Session["SessionUser"] + "_" + Session["SessionID"]) || (studentFolder == null)
                    || (!found) || (homeworkFolder == null))
                    return View("Error", "You do not have the rights to access root folder!");
                string realPath = Server.MapPath(ConfigurationManager.AppSettings["BasePath"] + teacherFolder + "/" + homeworkFolder + "/" + studentFolder + "/");
                if (path != null)
                    realPath += path;
                StudentHomeworkDetailsVM vm = new StudentHomeworkDetailsVM();
                vm.Details = new StudentHomeworkDetails();
                //string realPath = Server.MapPath(ConfigurationManager.AppSettings["BasePath"] +
                //    "/" + (string)Session["SessionUser"] + "_" + Session["SessionID"] + "/");
                vm.FolderStructure = _fileSystemService.GetExplorerModel(realPath, Request.Url);
                if (!vm.FolderStructure.isFile)
                    return View(vm);
                else
                {
                    string fileText = "";
                    try
                    {
                        fileText = _fileSystemService.GetFileText(realPath);
                    }
                    catch (Exception e)
                    {

                    }
                    return View("ViewStudentHomework", (object)fileText);
                }
            }
        }
    }
}
