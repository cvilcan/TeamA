using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TeamA.Models
{
    public class HomeworkVM
    {
        [DisplayName("Problem Description")]
        public string Description { get; set; }
        
        public DateTime Deadline { get; set; }
        public int TeacherID { get; set; }
    }
}