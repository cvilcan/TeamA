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

        public void AddHomework(int studentUserID, int homeworkID, string path)
        {
            _studentRepository.AddHomework(studentUserID, homeworkID);
            Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings["UploadPath"] + path);
        }
    }
}
