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
    }
}
