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
        private StudentService studentService = new StudentService();


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


        public void InsertCommentOrGradeOrStatus(int uploadId, int? grade = null, string comment = null)
        {

            hwRepository.InsertCommentOrGradeOrStatus(uploadId, grade, comment);

        }

        public void CheckHomeworkDeadLine()
            
        {
            hwRepository.CheckHomeworkDeadLine();
        }

        public Models.StudentHomeworkDetailsVM GetStudentHomeworkDetails(int studentID, int homeworkID)
        {
            var model = hwRepository.GetStudentHomeworkDetails(studentID, homeworkID);
            return new Models.StudentHomeworkDetailsVM()
            {
                HomeworkId = model.HomeworkId,
                HomeWorkName = model.HomeWorkName,
                StudentGrade = model.StudentGrade,
                Description = model.Description,
                Status = model.Status,
                TeacherId = model.TeacherId
            };
        }
    }
}
