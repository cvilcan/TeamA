using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TeamA.Models
{
    public class AccountVM
    {
<<<<<<< HEAD
        [Required(ErrorMessage = "Name is Required")]
        
        public string UserName {get;set;}
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
=======
        [Required]
        public string UserName {get;set;}
        [Required]
        public string Password { get; set; }
        [Required]
>>>>>>> 74a98cb4588eb521e5e009a8168410f1013872e9
        public string Email { get; set; }
        public int? TeacherId { get; set; }
    }
}