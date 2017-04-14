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
    }
}
