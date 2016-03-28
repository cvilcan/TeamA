using AccessModels.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class StudentService
    {
        private StudentRepository _studentRepository = new StudentRepository();

        public void InsertStudentToHomework(string userName, int homeworkID, string fileName, string basePath)
        {
            _studentRepository.InsertStudentToHomework(userName, fileName, homeworkID);


             List<StudentUploadPath>  uploadParams = _studentRepository.GetStudentUploadParameters(userName, homeworkID);

           
             Directory.CreateDirectory(basePath + "/" + uploadParams[0].TeacherId + "_" + uploadParams[0].TeacherName + 
                 "/" + uploadParams[0].HomeWorkName + homeworkID + "/" + userName + "_" + uploadParams[0].StudentId + "/" + fileName + "_" + uploadParams[0].UploadID);
        }

        public IEnumerable<Tuple<string, string, string>> GetStudentsBelongingToTeacher(string teacherName)
        {
            var studentsToTeachers = _studentRepository.GetStudentsToTeachers();
            var studentsFromSameTeacher = studentsToTeachers.Where(x => x.Item3 == teacherName);
            return studentsFromSameTeacher.ToList();
        }

        public Tuple<string, string, string> GetTeacherBelongingToStudent(string studentName)
        {
            var studentsToTeachers = _studentRepository.GetStudentsToTeachers();
            var teachersOfStudent = studentsToTeachers.Where(x => x.Item1 == studentName).FirstOrDefault();
            return teachersOfStudent;
        }


        public IEnumerable<StudentHomeworkDetails> GetStudentPendingHomework(int studentID)
        {
                List<StudentHomeworkDetails> studentPendingHomeworkList = _studentRepository.GetStudentPendingHomework(studentID);

                return studentPendingHomeworkList;

        }

        public IEnumerable<StudentHomeworkDetails> GetStudentCompletedHomework(int studentID)
        {
            List<StudentHomeworkDetails> studentCompletedHomeworkList = _studentRepository.GetStudentCompletedHomework(studentID);

            return studentCompletedHomeworkList;
        }


         



    }



}
