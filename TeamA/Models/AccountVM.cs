using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamA.Models
{
    public class AccountVM
    {
        [Required]
        public string UserName {get;set;}
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public int? TeacherId { get; set; }
    }
}