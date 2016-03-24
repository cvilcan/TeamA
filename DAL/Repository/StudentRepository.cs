using AccessModels.Models;
using Models.Models;
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
                    u.ID = Convert.ToInt32(rdr["ID"]);
                    u.Username = rdr["Student"].ToString();
                    u.Email = rdr["StudentEmail"].ToString();
                    u.RoleName = rdr["Username"].ToString();
                    userList.Add(new Tuple<string, string, string>(u.Username, u.Email, u.RoleName));
                }
                return userList;
            }
        }

        //Needed to set the path for the student homework file 
        public Tuple<int, string, int, string> GetStudentUploadParameters(string username, int homeworkID)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                   

                    SqlCommand cmd = new SqlCommand("spGetStudentUploadPath", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@homeworkID", homeworkID);
                  
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                  
                    var uploadParams = new Tuple<int,string,int,string>(Convert.ToInt32(rdr["TeacherId"]),rdr["TeacherName"].ToString(),Convert.ToInt32(rdr["StudentId"]),rdr["StudentName"].ToString());


                    return uploadParams;
                }
            }
            catch (SqlException)
            {
               
               
                return null ;

            }
         



        }

        public List<StudentPendingHomeworkDetails> GetStudentPendingHomeworkDetails(int studentID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {

                    List<StudentPendingHomeworkDetails> studentPendingHomeworkList = new List<StudentPendingHomeworkDetails>();

                    SqlCommand cmd = new SqlCommand("spStudentHomeworkPending", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@StudentId", studentID);
                    con.Open();


                    SqlDataReader rdr = cmd.ExecuteReader();

                   while(rdr.Read())
                   {
                       StudentPendingHomeworkDetails studentPendingHomework = new StudentPendingHomeworkDetails();

                       studentPendingHomework.TeacherId=Convert.ToInt32(rdr["TeacherUserId"]);
                       studentPendingHomework.TeacherName=rdr["TeacherUserId"].ToString();
                       studentPendingHomework.StudentGrade=Convert.ToInt32(rdr["StudentGrade"]);
                       studentPendingHomework.HomeworkId=Convert.ToInt32(rdr["HomeworkId"]);
                       studentPendingHomework.HomeWorkName=rdr["HomeWorkName"].ToString();
                       studentPendingHomework.Description=rdr["Description"].ToString();
                       studentPendingHomework.Deadline=Convert.ToDateTime(rdr["Deadline"]);

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



    }
}
