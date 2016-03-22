using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessModels.Models;
using DAL.Repository;
using System.Data.SqlClient;
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
                adminRepository.addTeachersFromAdmin(up);


            }
             catch (SqlException e)
             {
                 Console.WriteLine("admin service" + e);
             }
             


        }


        
    }
}
