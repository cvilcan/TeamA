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

        public void AddStudentHomework(string usernName, int homeworkID, string fileName)
        {
            _studentRepository.AddStudentHomework(usernName, fileName, homeworkID);
            Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings["UploadPath"] + @"/ProfName(pe baza homeworkid)_profID(idem)/StudentName(username)_StudentID(il iei)/Filename_uploadID(return de la sp)");
        }
    }
}
