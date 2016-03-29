﻿using AccessModels.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Repository
{
    public class StudentRepository
    {

        public string cs = ConfigurationManager.ConnectionStrings["TeamAConnection"].ConnectionString;

        public void InsertStudentToHomework(string username, string fileName, int homeworkID)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                var uploadDate = DateTime.Now;

                SqlCommand cmd = new SqlCommand("spInsertStudentToHomework", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@filename", fileName);
                cmd.Parameters.AddWithValue("@homeworkID", homeworkID);
                cmd.Parameters.AddWithValue("@uploadDate", uploadDate);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Tuple<string, string, string>> GetStudentsToTeachers()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                List<Tuple<string, string, string>> userList = new List<Tuple<string, string, string>>();
                SqlCommand cmd = new SqlCommand("spGetStudentsToTeacher", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    UserProfile u = new UserProfile();
                    u.Username = rdr["Student"].ToString();
                    u.Email = rdr["StudentEmail"].ToString();
                    u.RoleName = rdr["Username"].ToString();
                    userList.Add(new Tuple<string, string, string>(u.Username, u.Email, u.RoleName));
                }
                return userList;
            }
        }

        public IEnumerable<Tuple<int, string, string>> GetStudentsBelongingToTeacher(int teacherID)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                List<Tuple<int, string, string>> studentList = new List<Tuple<int, string, string>>();
                SqlCommand cmd = new SqlCommand("spGetStudentsBelongingToTeacher", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@teacherID", teacherID);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    studentList.Add(new Tuple<int, string, string>(Convert.ToInt32(rdr["ID"]), rdr["Username"].ToString(), rdr["Email"].ToString()));
                }
                return studentList;
            }
        }

        //Needed to set the path for the student homework file 
        public List<StudentUploadPath> GetStudentUploadParameters(string username, int homeworkID)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                List<StudentUploadPath> studentPathList = new List<StudentUploadPath>();
                SqlCommand cmd = new SqlCommand("spGetStudentUploadPath", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@homeworkID", homeworkID);
                  
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();   
                while(rdr.Read())
                {
                    StudentUploadPath studentPath = new StudentUploadPath();

                    studentPath.TeacherId = Convert.ToInt32(rdr["TeacherId"]);
                    studentPath.TeacherName = rdr["TeacherName"].ToString();
                    studentPath.StudentId = Convert.ToInt32(rdr["StudentId"]);
                    studentPath.UploadID = Convert.ToInt32(rdr["UploadID"]);
                    studentPath.HomeWorkName = rdr["HomeWorkName"].ToString();

                    studentPathList.Add(studentPath);

                }

                return studentPathList;
            }    
        }

        public List<StudentHomeworkDetails> GetStudentPendingHomework(string userName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {

                    List<StudentHomeworkDetails> studentPendingHomeworkList = new List<StudentHomeworkDetails>();

                    SqlCommand cmd = new SqlCommand("spStudentHomeworkPending", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@username", userName);
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                   while(rdr.Read())
                   {
                       StudentHomeworkDetails studentPendingHomework = new StudentHomeworkDetails();

                       studentPendingHomework.TeacherId=Convert.ToInt32(rdr["TeacherUserId"]);           
                       studentPendingHomework.HomeworkId=Convert.ToInt32(rdr["HomeworkId"]);
                       studentPendingHomework.HomeWorkName=rdr["HomeWorkName"].ToString();
                       studentPendingHomework.Description=rdr["Description"].ToString();
                       studentPendingHomework.Deadline=Convert.ToDateTime(rdr["Deadline"]);
                       studentPendingHomework.Status = rdr["Status"].ToString();
                       studentPendingHomework.Comment = rdr["Comment"].ToString();
                       studentPendingHomework.UploadDate = Convert.ToDateTime(rdr["UploadDate"]);

                       studentPendingHomeworkList.Add(studentPendingHomework);
                   }
                   return studentPendingHomeworkList;
                }
            }
            catch (SqlException)
            {


                return null;

            }
         catch (Exception)
            {
                return null;
            }



        }

        public List<StudentHomeworkDetails> GetStudentCompletedHomework(string userName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {

                    List<StudentHomeworkDetails> studentCompletedHomeworkList = new List<StudentHomeworkDetails>();

                    SqlCommand cmd = new SqlCommand("spStudentHomeworkCompleted", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@username", userName);
                    con.Open();


                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        StudentHomeworkDetails studentCompletedHomework = new StudentHomeworkDetails();

                        studentCompletedHomework.TeacherId = Convert.ToInt32(rdr["TeacherUserId"]);                                   
                        studentCompletedHomework.HomeworkId = Convert.ToInt32(rdr["HomeworkId"]);
                        studentCompletedHomework.HomeWorkName = rdr["HomeWorkName"].ToString();
                        studentCompletedHomework.Description = rdr["Description"].ToString();
                        studentCompletedHomework.Deadline = Convert.ToDateTime(rdr["Deadline"]);
                        studentCompletedHomework.Comment = rdr["Comment"].ToString();
                        studentCompletedHomework.Status = rdr["Status"].ToString();
                        studentCompletedHomework.UploadDate = Convert.ToDateTime(rdr["UploadDate"]);
                        studentCompletedHomework.StudentGrade = Convert.ToInt32(rdr["UploadID"]);

                        studentCompletedHomeworkList.Add(studentCompletedHomework);
                    }

                    return studentCompletedHomeworkList;
                }
            }
            catch (SqlException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }



        }

       



        }



}

