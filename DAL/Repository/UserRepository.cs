using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using AccessModels.Models;

namespace TeamA.Repository
{
    public class UserRepository
    {
        string cs = ConfigurationManager.ConnectionStrings["TeamAConnection"].ConnectionString;

        public IEnumerable<UserProfile> GetAllUsers()
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                IEnumerable<UserProfile> userList = new List<UserProfile>();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    UserProfile u = new UserProfile();
                    u.ID = Convert.ToInt32(rdr["ID"]);
                    //...
                    //userList.Add(u);
                }
                return userList;
            }
        }
    }
}