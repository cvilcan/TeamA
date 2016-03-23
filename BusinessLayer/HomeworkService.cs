using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamA.Repository;
using AccessModels.Models;
using System.Data.SqlClient;
using System.IO;

namespace BusinessLayer
{
    public class HomeworkService
    {
        private HomeworkRepository hwRepository = new HomeworkRepository();
        private UserRepository userRepository = new UserRepository();
        private UserService userService = new UserService();


        public IEnumerable<HomeworkRepository> CreateHomework(int TeacherUserID, string name, string description, DateTime deadline)
        {
         var teachers=userService.GetAllTeachers();
            UserProfile teacher=teachers.Where(t => t.ID==TeacherUserID).FirstOrDefault();
            if (teacher != null)
            {
                Directory.CreateDirectory(@"~/Uploads/" + teacher.Username + '_' + Convert.ToString(teacher.ID)+'_'+name);
            }
            else{}
            
           
                //Uploads/user.Username+TeacherUserID.ToString()/name+homeworkid
        
        
    }
}
