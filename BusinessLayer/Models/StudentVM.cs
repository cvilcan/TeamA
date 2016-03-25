using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessLayer.Models
{
        public class StudentVM
        {
            public int StudentID { get; set; }
            public string StudentName { get; set; }
            public string StudentEmail { get; set; }
            public string TeacherName { get; set; }
            public int IsConfirmed { get; set; }
        }
    
}