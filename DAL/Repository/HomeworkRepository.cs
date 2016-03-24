﻿using System;
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
                SqlCommand cmd = new SqlCommand("spCreateHomework", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@teacherID", TeacherUserID);
                cmd.Parameters.AddWithValue("@homeWorkName", name);
                cmd.Parameters.AddWithValue("@homeWorkDescription", description);
                cmd.Parameters.AddWithValue("@deadlineInDays", deadline);
                con.Open();

                return (int)cmd.ExecuteScalar();
            }
        }

        public List<Homework> GetOneTeacherHomework(string username)
        {
            try {  
            using (SqlConnection con = new SqlConnection(cs))
            {

                List<Homework> teacherHomeworkList = new List<Homework>();

                SqlCommand cmd = new SqlCommand("spGetOneTeacherHomework", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@usernameTeacher", username);
              
                con.Open();

                    SqlDataReader rdr =cmd.ExecuteReader();
               
                    while(rdr.Read())
                    {
                        Homework teacherHomework = new Homework();

                        teacherHomework.HomeworkID = Convert.ToInt32(rdr["HomeworkId"]);
                        teacherHomework.TeacherUserID = Convert.ToInt32(rdr["TeacherId"]);
                        teacherHomework.HomeworkName = rdr["HomeWorkName"].ToString();
                        teacherHomework.Description = rdr["Description"].ToString();
                        teacherHomework.Deadline = Convert.ToDateTime(rdr["Deadline"]);

                        teacherHomeworkList.Add(teacherHomework);
                        
                    }

                    return teacherHomeworkList;
            }
            }
            catch (SqlException)
            {
                return null;
            }
            catch(Exception)
            {
                return null;
            }



        }


    }
}
