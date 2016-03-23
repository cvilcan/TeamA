using System;
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

        public void CreateStudentUser(string username, string password, string email, int? teacherID)
        {
            userRepository.CreateStudentUser(username, email, password, teacherID);
            var guidstring = userRepository.GetGuid(username);

            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/") appUrl += "/";

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            string body = "Click the link to confirm the mail:\n";

            Mail.MailHelper.SendMail(new List<string>() { email }, "admin@admin.com", "Confirmation mail", body + baseUrl + "Account/ConfirmRegistration?GUID=" + guidstring);

        }

        public IEnumerable<UserProfile> GetAllTeachers()
        {
            var users = userRepository.GetAllUsers();
            var teachers = users.Where(x => x.RoleName == "Teacher");
            return teachers;
        }
        public int CheckGuid(string guid)
        {
            return userRepository.CheckGuid(guid);
        }

    }
}
