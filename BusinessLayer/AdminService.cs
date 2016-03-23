using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessModels.Models;
using DAL.Repository;
using System.Data.SqlClient;
using BusinessLayer.Mail;
namespace BusinessLayer
{
    class AdminService
    {
        private AdminRepository adminRepository;
        public AdminService(AdminRepository adminRepository)
        {
            this.adminRepository=adminRepository;
        }
        public void addTeachersFromAdmin(UserProfile up) {

             try
            {
               
                MailHelper mp = new MailHelper();

                List<string> list = new List<string>() { up.Email };
                MailHelper.SendMail(list, "admin@admin.com", "Registration Details", up.Password);


                adminRepository.addTeachersFromAdmin(up);


            }
             catch (SqlException e)
             {
                 Console.WriteLine("admin service" + e);
             }
             


        }

        
        
    }
}
