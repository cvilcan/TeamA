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


        public int CreateHomework(int TeacherUserID, string name, string description, DateTime deadline, string basePath)
        {
            var teachers = userService.GetAllTeachers();
            UserProfile teacher = teachers.Where(t => t.ID == TeacherUserID).FirstOrDefault();
            int homeworkID = hwRepository.CreateHomework(TeacherUserID, name, description, deadline);

            if (teacher != null)
            {
                Directory.CreateDirectory(basePath + teacher.Username + '_' + Convert.ToString(teacher.ID) + '/' + name + '_' + Convert.ToString(homeworkID));
            }
            else { }

            return homeworkID;
        }

        public IEnumerable<Homework> GetOneTeacherHomework(string username)
        {
            var teacherHomework = hwRepository.GetOneTeacherHomework(username);

            return teacherHomework;
        }

        
    }
}
