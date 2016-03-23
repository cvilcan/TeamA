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

       public void  CreateStudentUser(string userName, string email, string password,int? teacherID)
        {


        }


   


    
}}
