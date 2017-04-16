using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;

namespace Capstone.Web.DAL
{

    public class EventDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["FinalCapstone"].ConnectionString;

        private string SQL_GetNumberOfStudentChoices = "select Number_Of_Student_Choices from Matchmaking_Arrangement;";

        public int GetNumberOfStudentChoices(int MatchmakingId)
        {
            List<StudentChoice> results = new List<StudentChoice>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetNumberOfStudentChoices, conn);

                    return (int)cmd.ExecuteScalar();
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