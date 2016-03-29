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
using BusinessLayer.Mail;

namespace BusinessLayer
{
    public class HomeworkService
    {
        private HomeworkRepository hwRepository = new HomeworkRepository();
        private UserRepository userRepository = new UserRepository();
        private UserService userService = new UserService();
        private StudentService studentService = new StudentService();


        public int CreateHomework(int TeacherUserID, string name, string description, DateTime deadline, string basePath)
        {
            var teachers = userService.GetAllTeachers();
            UserProfile teacher = teachers.Where(t => t.ID == TeacherUserID).FirstOrDefault();
            int homeworkID = hwRepository.CreateHomework(TeacherUserID, name, description, deadline);

            string getStudEmail = userService.GetAllStudents().Select(x => x.Email).FirstOrDefault();
            

            if (teacher != null)
            {
                Directory.CreateDirectory(basePath + teacher.Username + '_' + Convert.ToString(teacher.ID) + '/' + name + '_' + Convert.ToString(homeworkID));
                if (getStudEmail != null)
                {
                    MailHelper.SendMail(new List<string> { getStudEmail }, "account@account.com", "New Homework" + deadline, description);
                }
            }
            else { }

            return homeworkID;
        }

        public IEnumerable<Homework> GetOneTeacherHomework(string username)
        {
            var teacherHomework = hwRepository.GetOneTeacherHomework(username);

            return teacherHomework;
        }


        public void InsertCommentOrGradeOrStatus(int uploadId, int? grade = null, string comment = null)
        {

            hwRepository.InsertCommentOrGradeOrStatus(uploadId, grade, comment);

        }

        public void CheckHomeworkDeadLine()
            
        {
            hwRepository.CheckHomeworkDeadLine();
        }
    }
}
