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

        private string SQL_GetAllStudents = "Select Student_Id, Student_Name, l.Language, User_Name from Student join Language l on l.Language_Id = student.Language_Id;";

        private string SQL_InsertStudentIntoLogin = "Insert into Login(User_Name, Password, User_Role) VALUES(@userName,'Password','student');";

        private string SQL_InsertStudentIntoStudent = "Insert into student(Student_Name, Language_Id, User_Name) values(@studentName, @languageId, @username);";


        public List<Student> GetAllStudents()
        {
            List<Student> results = new List<Student>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllStudents, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Student s = new Student();
                        s.StudentId = Convert.ToInt32(reader["Student_Id"]);
                        s.StudentName = Convert.ToString(reader["Student_Name"]);
                        s.Language = Convert.ToString(reader["Language"]);
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
    }
}
