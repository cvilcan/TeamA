using AccessModels.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
namespace DAL.Repository
{
    public class AdminRepository
    {
        

        public void addTeachersFromAdmin(UserProfile up)
        {

           
                string cs = ConfigurationManager.ConnectionStrings["TeamAConnection"].ConnectionString;

                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("spCreateTeacher",
                                con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    string encryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(up.Password, "SHA1");
                    cmd.Parameters.AddWithValue("@teacherName", up.Username);
                    cmd.Parameters.AddWithValue("@password", encryptedPassword);
                    cmd.Parameters.AddWithValue("@email", up.Email);
                   
                    con.Open();

                    cmd.ExecuteNonQuery();
                }
           
            //catch (SqlException ex)
            //{
            //    string str;
            //    str = "Source:" + ex.Source;
            //    str += "\n" + "Number:" + ex.Number.ToString();
            //    str += "\n" + "Message:" + ex.Message;
            //    str += "\n" + "Class:" + ex.Class.ToString();
            //    str += "\n" + "Procedure:" + ex.Procedure.ToString();
            //    str += "\n" + "Line Number:" + ex.LineNumber.ToString();
            //    str += "\n" + "Server:" + ex.Server.ToString();

                
            //}
        }

























    }

}