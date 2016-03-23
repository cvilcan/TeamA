using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAL.Repository
{
    public class StudentRepository
    {
        public string cs = ConfigurationManager.ConnectionStrings["TeamAConnection"].ConnectionString;

        public void AddStudentHomework(string username, string fileName, int homeworkID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    var uploadDate = DateTime.Now;

                    SqlCommand cmd = new SqlCommand("spCreateStudent", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@filename", fileName);
                    cmd.Parameters.AddWithValue("@homeworkID", homeworkID);
                    cmd.Parameters.AddWithValue("@uploadDate", uploadDate);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {


            }
         

            


        }
    }
}
