using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class StudentDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["FinalCapstone"].ConnectionString;

        private string SQL_GetStudents = "Select Matchmaking_Id, Student_Id, Student_Name, l.Language_Id, l.Language, User_Name from Student join Language l on l.Language_Id = student.Language_Id ";

        private string SQL_InsertStudentIntoLogin = "Insert into Login(User_Name, Password, User_Role) VALUES(@userName,'GWtlZ2gG14GC6DiY937w1L9RZCM=','student');";

        private string SQL_InsertStudentIntoStudent = "Insert into student(Student_Name, Language_Id, User_Name, Matchmaking_Id) values(@studentName, @languageId, @username, @matchmakingId);";

        private string SQL_GetParticipatingStudents = "Select s.Matchmaking_Id, s.Student_Id, s.Student_Name, l.Language_Id, l.Language, s.User_Name from Student s join Language l on l.Language_Id = s.Language_Id join Participating_Students p on s.student_Id = p.Student_Id  where event_id = @eventId;";

        public List<Student> GetAllStudents()
        {
            List<Student> results = new List<Student>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetStudents, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student s = new Student();
                        s.MatchmakingId = Convert.ToInt32(reader["Matchmaking_Id"]);
                        s.StudentId = Convert.ToInt32(reader["Student_Id"]);
                        s.StudentName = Convert.ToString(reader["Student_Name"]);
                        s.Language = Convert.ToString(reader["Language"]);
                        s.LanguageId = Convert.ToInt32(reader["Language_Id"]);
                        s.UserName = Convert.ToString(reader["User_Name"]);

                        //Note from KH: Need to consider whether it is necessary to populate the Student Schedule or EmployerRanking
                        results.Add(s);
                    }
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return results;
        }

        public List<Student> GetAllStudents(int matchmakingId)
        {
            List<Student> results = new List<Student>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string SQL_GetALLStudentById = SQL_GetStudents + " where Matchmaking_Id = @matchmakingId;";

                    SqlCommand cmd = new SqlCommand(SQL_GetALLStudentById, conn);
                    cmd.Parameters.AddWithValue("@matchmakingId", matchmakingId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student s = new Student();
                        s.MatchmakingId = Convert.ToInt32(reader["Matchmaking_Id"]);
                        s.StudentId = Convert.ToInt32(reader["Student_Id"]);
                        s.StudentName = Convert.ToString(reader["Student_Name"]);
                        s.Language = Convert.ToString(reader["Language"]);
                        s.LanguageId = Convert.ToInt32(reader["Language_Id"]);
                        s.UserName = Convert.ToString(reader["User_Name"]);

                        //Note from KH: Need to consider whether it is necessary to populate the Student Schedule or EmployerRanking
                        results.Add(s);
                    }
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return results;
        }

        public List<Student> GetParticipatingStudents(int eventId)
        {
            List<Student> results = new List<Student>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParticipatingStudents, conn);
                    cmd.Parameters.AddWithValue("@eventId", eventId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student s = new Student();
                        s.MatchmakingId = Convert.ToInt32(reader["Matchmaking_Id"]);
                        s.StudentId = Convert.ToInt32(reader["Student_Id"]);
                        s.StudentName = Convert.ToString(reader["Student_Name"]);
                        s.Language = Convert.ToString(reader["Language"]);
                        s.LanguageId = Convert.ToInt32(reader["Language_Id"]);
                        s.UserName = Convert.ToString(reader["User_Name"]);

                        //Note from KH: Need to consider whether it is necessary to populate the Student Schedule or EmployerRanking
                        results.Add(s);
                    }
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return results;
        }

        public bool AddNewStudent(Student student)
        {
            try
            {
                int rowsUpdated = 0;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_InsertStudentIntoLogin, conn);
                    cmd.Parameters.AddWithValue("@userName", student.UserName);
                    rowsUpdated += cmd.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand(SQL_InsertStudentIntoStudent, conn);
                    cmd2.Parameters.AddWithValue("@userName", student.UserName);
                    cmd2.Parameters.AddWithValue("@studentName", student.StudentName);
                    cmd2.Parameters.AddWithValue("@languageId", student.LanguageId);
                    cmd2.Parameters.AddWithValue("@matchmakingId", student.MatchmakingId);
                    rowsUpdated += cmd2.ExecuteNonQuery();
                }

                return (rowsUpdated == 2);
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }
        }

        public Student GetStudent(string userName)
        {
            Student result = new Student();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string SQL_GetAStudent = SQL_GetStudents + " where User_Name = @userName";

                    SqlCommand cmd = new SqlCommand(SQL_GetAStudent, conn);
                    cmd.Parameters.AddWithValue("@userName", userName);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result.MatchmakingId = Convert.ToInt32(reader["Matchmaking_Id"]);
                        result.StudentId = Convert.ToInt32(reader["Student_Id"]);
                        result.StudentName = Convert.ToString(reader["Student_Name"]);
                        result.Language = Convert.ToString(reader["Language"]);
                        result.LanguageId = Convert.ToInt32(reader["Language_Id"]);
                        result.UserName = Convert.ToString(reader["User_Name"]);

                        //Note from KH: Need to consider whether it is necessary to populate the Student Schedule or EmployerRanking

                    }
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return result;
        }
    }
}
