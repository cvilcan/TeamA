using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using AccessModels.Models;
namespace DAL.Repository
{
    public class HomeworkRepository
    {
        string cs = ConfigurationManager.ConnectionStrings["TeamAConnection"].ConnectionString;
        
        public int CreateHomework(int TeacherUserID, string name, string description, DateTime deadline)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spCreateHomework");

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@teacherID", TeacherUserID);
                cmd.Parameters.AddWithValue("@homeWorkName", name);
                cmd.Parameters.AddWithValue("@homeWorkDescription", description);
                cmd.Parameters.AddWithValue("@deadlineInDays", deadline);
                

                return (int)cmd.ExecuteScalar();
            }
        }
    }
}
