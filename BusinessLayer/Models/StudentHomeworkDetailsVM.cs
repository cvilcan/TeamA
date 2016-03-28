using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessModels.Models;

namespace BusinessLayer.Models
{
    public class StudentHomeworkDetailsVM
    {
        public StudentHomeworkDetails Details { get; set; }
        public ExplorerModelVM FolderStructure { get; set; }
    }
}
