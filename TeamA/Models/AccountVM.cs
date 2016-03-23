using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamA.Models
{
    public class AccountVM
    {
        public string UserName {get;set;}
        public string Password { get; set; }
        public string Email { get; set; }
        public int? TeacherId { get; set; }
    }
}