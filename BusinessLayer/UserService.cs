﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamA.Repository;
using AccessModels.Models;
using System.Data.SqlClient;
using System.Web;
using DAL.Repository;
namespace BusinessLayer
{
    public class UserService
    {
        private UserRepository _userRepository = new UserRepository();
        

        public IEnumerable<UserProfile> GetAllUsers()
        {
            try
            {
                IEnumerable<UserProfile> users = _userRepository.GetAllUsers();
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
            var users = _userRepository.GetAllUsers();
            var students = users.Where(x => x.RoleName == "Student");
            return students;
        }

        public IEnumerable<UserProfile> GetAllTeachers()
        {
            var users = _userRepository.GetAllUsers();
            var teachers = users.Where(x => x.RoleName == "Teacher");
            return teachers;
        }

<<<<<<< HEAD
        public Tuple<int, string, string> GetUser(string username)
        {
            var a = GetAllUsers();
            var user = a.Where(q => q.Username == username).SingleOrDefault();

            return new Tuple<int, string, string>(user.ID, user.Username, user.Email);
        }


=======
>>>>>>> 606b1f73e764a64dd1507aee141bb225e825aaa2
        public void CreateStudentUser(string username, string password, string email, int? teacherID)
        {
            _userRepository.CreateStudentUser(username, email, password, teacherID);
            var guidstring = _userRepository.GetGuid(username);

            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/") appUrl += "/";

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            string body = "Click the link to confirm the mail:\n";

            Mail.MailHelper.SendMail(new List<string>() { email }, "admin@admin.com", "Confirmation mail", body + baseUrl + "Account/ConfirmRegistration?GUID=" + guidstring);
        }

        public int CheckGuid(string guid)
        {
            return _userRepository.CheckGuid(guid);
        }

    }
}
