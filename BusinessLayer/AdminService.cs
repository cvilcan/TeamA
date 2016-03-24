﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessModels.Models;
using DAL.Repository;
using System.Data.SqlClient;
using System.IO;
using Helpers.Mail;
namespace BusinessLayer
{
    public class AdminService
    {
        private AdminRepository adminRepository = new AdminRepository();
        public AdminService(AdminRepository adminRepository)
        {
            this.adminRepository=adminRepository;
        }
        public AdminService()
        {

        }
        public void addTeachersFromAdmin(string username,string email, string basePath) {

             try
            {
                UserProfile up = new UserProfile(); 
                MailHelper mp = new MailHelper();
                 Random rndm = new Random();
                 string password = rndm.Next(100000, 999999).ToString();
                List<string> list = new List<string>() { email };
                MailHelper.SendMail(list, "admin@admin.com", "Registration Details", password);
                Directory.CreateDirectory(basePath + username + '_' + Convert.ToString(up.ID));

                up.Email = email;
                up.Username = username;
                up.Password = password;
                adminRepository.addTeachersFromAdmin(up);


            }
             catch (SqlException e)
             {
                 Console.WriteLine("admin service" + e);
             }
             


        }




       
    }
}
