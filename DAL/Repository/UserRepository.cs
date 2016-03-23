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
            using (SqlConnection con = new SqlConnection(cs))
            {
                List<UserProfile> userList = new List<UserProfile>();
                SqlCommand cmd = new SqlCommand("spGetAllUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    UserProfile u = new UserProfile();
                    u.ID = Convert.ToInt32(rdr["ID"]);
                    u.Username = rdr["Username"].ToString();
                    u.Password = rdr["Password"].ToString();
                    u.Email = rdr["Email"].ToString();
                    u.RoleName = rdr["RoleName"].ToString();

                    userList.Add(u);
                }
                return userList;
            }
        }


        public void CreateStudentUser(string userName, string email, string password,int? teacherID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("spCreateStudent");

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@username", userName);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@teacherId", teacherID);
                    cmd.ExecuteNonQuery();

                }
            }

            catch (SqlException)
            {


            }



        }


        public bool Login(string username, string password)
        {
           try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("spLogin");

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);


                    int number = Convert.ToInt32( cmd.ExecuteReader());
                    if(number==1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    
                }
            }
            catch(SqlException )
           {
               return false;
           }
            catch(Exception)
           {
               return false;
           }

        }


        public string GetGuid(string username)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string guid;
                SqlCommand cmd = new SqlCommand("spGetGUID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username",username);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                
                
                  
                    guid=rdr["HashConfirmationCode"].ToString();
                                    

                    
                
                return guid;
            }
        }

        public void CheckGuid(string guid)
        {

        }
    
    
    }


    }

    
