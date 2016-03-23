﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamA.Repository;
using AccessModels.Models;
using System.Data.SqlClient;
using System.Web;
namespace BusinessLayer
{
    public class UserService
    {
        private UserRepository userRepository = new UserRepository();

        public IEnumerable<UserProfile> GetAllUsers()
        {

            try
            {
                IEnumerable<UserProfile> users = userRepository.GetAllUsers();
                return users;


            }
            catch (SqlException e)
            {
                Console.WriteLine("Service error " + e);
            }


            return null;


        }

        public IEnumerable<UserProfile> GetAllStudents()
        {
            var users = userRepository.GetAllUsers();
            var students = users.Where(x => x.RoleName == "Student");
            return students;
        }

        public IEnumerable<UserProfile> GetAllTeachers()
        {
            var users = userRepository.GetAllUsers();
            var teachers = users.Where(x => x.RoleName == "Teacher");
            return teachers;
        }



        public void CreateStudentUser(string username, string password, string email, int? teacherID)
        {


            userRepository.CreateStudentUser(username, email, password, teacherID);
            var guidstring = userRepository.GetGuid(username);
            
            Mail.MailHelper.SendMail(new List<string>() { "xulescu@yahoo.com" }, "admin@admin.com", "draga apas aici pt confirmare", GetBaseUrl() + guidstring);







        }
        public List<Tuple<UserProfile, string>> GetStudentTeacher()
        {
            throw new NotImplementedException();
            //var students = GetAllStudents();
            //var studentsToTeachers = GetAllStudentsToTeachers();
            //var teachers = GetAllTeachers();
            //List<Tuple<UserProfile, string>> pairList = new List<Tuple<UserProfile, string>>();
            //foreach (var student in students)
            //{
            //    pairList.Add(new Tuple<UserProfile,string>()
            //        {
            //            Item1 = student,
            //            Item2 = teachers.Where(t => t.Role == )
            //        })
            //}
        }

        public string GetBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/") appUrl += "/";

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }


        public void CheckGuid(string guid)
        {
            userRepository.CheckGuid(guid);
        }

    }
}
