using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamA.Repository;
using AccessModels.Models;
using System.Data.SqlClient;
namespace BusinessLayer
{
    public class UserService
    {
        private UserRepository userRepository = new UserRepository();

        public IEnumerable<UserProfile> GetAllUsers() {

           try{
               IEnumerable<UserProfile> users = userRepository.GetAllUsers();
            return users;




        }catch(SqlException e){
            Console.WriteLine("Service error "+e);}
          
            
            return null;


    }
        public IEnumerable<UserProfile> GetAllStudents()
        {
            var users = userRepository.GetAllUsers();
            var students = users.Where(x => x.RoleName == "Student");
            return students;
        }


<<<<<<< HEAD
        
        public void CreateStudentUser(string username, string password, string email, int? teacherID)
        {
            var guidstring = userRepository.GetGuid(username);
            userRepository.CreateStudentUser(username, email, password, teacherID);
            Mail.MailHelper.SendMail(new List<string>() { "xulescu@yahoo.com" }, "admin@admin.com", "draga apas aici pt confirmare", "loclalhost/confirm?guid="+guidstring);
=======


        public void CreateStudentUser(string username, string password, string email, int? teacherID)
        {

            userRepository.CreateStudentUser(username, password,email , teacherID);
            Mail.MailHelper.SendMail(new List<string>() { "xulescu@yahoo.com" }, "admin@admin.com", "draga apas aici pt confirmare", "loclalhost/confirm?guid=....");
>>>>>>> 009c19117c8fab5e4dce06e4270a181788fd0c3a

        }
   


    
}}
