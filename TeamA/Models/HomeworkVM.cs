using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamA.Models
{
    public class HomeworkVM
    {
        [Required]
        [DisplayName("Homework name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Homework description")]
        public string Description { get; set; }
        
        [Required]
        [DisplayName("Deadline")]
        public DateTime Deadline { get; set; }
        public int TeacherID { get; set; }
    }
}