using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Capstone.Web.DAL
{

    public class StudentChoiceDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["FinalCapstone"].ConnectionString;

        private string SQL_GetEmployersByRank = "Select * From Student_Choices where Employer_Rank = @EmployerRanking;";
        private string SQL_UpdateStudentChoice = "Insert INTO Student_Choices (Student_Id, Employer_Id, Employer_Rank, Event_Date) VALUES (@studentId, @employerId, @employerRank, @eventDate);";
        private string SQL_DeletePreviousChoices = "Delete From Student_Choices where Student_Id = @studentId AND Event_Date = @eventDate;";
        public List<StudentChoice> GetEmployersByRank(int ranking)
        {
            List<StudentChoice> results = new List<StudentChoice>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetEmployersByRank, conn);
                    cmd.Parameters.AddWithValue("@EmployerRanking", ranking);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        StudentChoice sc = new StudentChoice();
                        sc.StudentId = Convert.ToInt32(reader["Student_Id"]);
                        sc.EmployerId = Convert.ToInt32(reader["Employer_Id"]);
                        sc.EmployerRank = Convert.ToInt32(reader["Employer_Rank"]);
                        sc.EventDate = Convert.ToDateTime(reader["Event_Date"]).ToShortDateString();
                        results.Add(sc);
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
        
        public bool UpdateStudentChoice(List<StudentChoice> studentChoices)
        {      
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    int rowsUpdated = 0;
                 
                    foreach (StudentChoice choice in studentChoices)
                    {
                        SqlCommand cmd = new SqlCommand(SQL_UpdateStudentChoice, conn);
                        cmd.Parameters.AddWithValue("@employerRank", choice.EmployerRank);
                        cmd.Parameters.AddWithValue("@employerId", choice.EmployerId);
                        cmd.Parameters.AddWithValue("@studentId", choice.StudentId);
                        cmd.Parameters.AddWithValue("@eventDate", choice.EventDate);
                        rowsUpdated += cmd.ExecuteNonQuery();
                      
                    }
                    return (studentChoices.Count == rowsUpdated);  
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }
        }
        public void DeletePreviousChoices(int studentId,string eventDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_DeletePreviousChoices, conn);
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    cmd.Parameters.AddWithValue("@eventDate", eventDate);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }
        }
    }
}
