﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using AccessModels.Models;
using System.Web.Security;

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
                    SqlCommand cmd = new SqlCommand("spCreateStudent",con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@username", userName);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@teacherId", teacherID);
                    con.Open();
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
                    SqlCommand cmd = new SqlCommand("spLogin",con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    //password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    List<int> loginlist = new List<int>();
                    while (rdr.Read())
                    {
                        
                       int g;
                           g = (int) rdr["ReturnCode"];
                          loginlist.Add(g);
                    }


                    int number = loginlist[0];
                    

                    

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
                List<string> readguidlist = new List<string>();
                
                while(rdr.Read()){
                 string g  ;
                   g =rdr["HashConfirmationCode"].ToString();
                    readguidlist.Add(g);
                }
                guid=readguidlist[0];

                return guid;
            }
        }

        public int CheckGuid(string guid)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spCheckGuid", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@guid", guid);
                con.Open();

                return (int)cmd.ExecuteScalar();
            }
        }

        public string ResetPassword(string username)
        {
            Random rndm = new Random();
            string password = rndm.Next(100000, 999999).ToString();
            string hasedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spResetPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hasedPassword);
                con.Open();

                cmd.ExecuteNonQuery();

                return password;
            }
        }

        public string GetRole(string username){
            using (SqlConnection con = new SqlConnection(cs))
            {
                string role;
                SqlCommand cmd = new SqlCommand("spGetRole", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                List<string> readrolelist = new List<string>();

                while (rdr.Read())
                {
                    string r;
                    r = rdr["RoleName"].ToString();
                    readrolelist.Add(r);
                }
                role = readrolelist[0];

                return role;
            }


        }       
    

    }
}
