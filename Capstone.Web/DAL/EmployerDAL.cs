using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Capstone.Web.DAL
{

    public class EmployerDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["FinalCapstone"].ConnectionString;

        private string SQL_GetAllEmployers = "Select Employer_Id, Employer_Name, Number_Of_Teams, Summary From Employer;";

        private string SQL_AddNewEmployer = "Insert into employer (Employer_Name, Summary, Number_Of_Teams) VALUES (@employerName, @summary, @numberOfTeams);";

        public List<Employer> GetAllEmployers()
        {
            List<Employer> results = new List<Employer>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllEmployers, conn);
                  
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Employer e = new Employer();
                        e.EmployerId = Convert.ToInt32(reader["Employer_Id"]);
                        e.EmployerName = Convert.ToString(reader["Employer_Name"]);
                        e.NumberOfTeams = Convert.ToInt32(reader["Number_Of_Teams"]);
                        e.Summary = Convert.ToString(reader["Summary"]);
                        results.Add(e);
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

        public bool AddNewEmployer(Employer employer)
        {
            try
            {
                int rowsUpdated = 0;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AddNewEmployer, conn);
                    cmd.Parameters.AddWithValue("@employerName", employer.EmployerName);
                    cmd.Parameters.AddWithValue("@summary", employer.Summary);
                    cmd.Parameters.AddWithValue("@numberOfTeams", employer.NumberOfTeams);

                    rowsUpdated += cmd.ExecuteNonQuery();
                }

                return (rowsUpdated == 1);
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }
        }
    }
}
