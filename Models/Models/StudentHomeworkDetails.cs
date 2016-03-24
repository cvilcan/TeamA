using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessModels.Models
{

   public class StudentHomeworkDetails
    {
        public int TeacherId  { get; set; }
        public string TeacherName { get; set; }
        public int StudentGrade { get; set; }
        public int UploadId { get; set; }
        public int HomeworkId { get; set; }
        public string HomeWorkName { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }



    }
}
