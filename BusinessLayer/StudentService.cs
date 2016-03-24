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

        public void AddStudentHomework(string usernName, int homeworkID, string fileName,string basePath)
        {
            _studentRepository.AddStudentHomework(usernName, fileName, homeworkID);


            Tuple<int, string, int, string> uploadParams = _studentRepository.GetStudentUploadParameters(usernName, homeworkID);

            Directory.CreateDirectory(basePath + "/" + uploadParams.Item2 + "_" + uploadParams.Item1 + "/"+usernName+"_"+uploadParams.Item3+"/"+fileName+"_"+uploadParams.Item4);
        }

        public IEnumerable<Tuple<string, string, string>> GetStudentTeacher(string teacherName)
        {
            var studentsToTeachers = _studentRepository.GetStudentsToTeachers();
            var studentsFromSameTeacher = studentsToTeachers.Where(x => x.Item3 == teacherName);
            return studentsFromSameTeacher.ToList();
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
