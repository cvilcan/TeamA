using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentUsername { get; set; }
        public string StudentEmail { get; set; }
        public int IsConfirmed { get; set; }
        public int TeacherID { get; set; }
        public string TeacherName { get; set; }
    }
}
