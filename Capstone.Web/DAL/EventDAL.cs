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

        private string SQL_GetNumberOfStudentChoices = @"select Number_Of_Student_Choices from Matchmaking_Arrangement where Matchmaking_Id = @matchmakingId;";

        private string SQL_GetAllArrangements = @"select Matchmaking_Id, Location, Season, Cohort_Number, Number_Of_Student_Choices from matchmaking_arrangement";

        private string SQL_AddNewArrangement = @"insert into matchmaking_arrangement (Location, Season, Cohort_Number, Number_Of_Student_Choices,Schedule_Is_Generated) values (@location, @season, @cohortNumber, @numberOfStudentChoices,'N')";

        private string SQL_AddNewEvent = @"Insert into Event (Matchmaking_Id, Start_Time, End_Time, Lunch_Start, Lunch_End, First_Break_Start, First_Break_End, Second_Break_Start, Second_Break_End, Interview_Length) VALUES (@matchmakingId,@startTime, @endTime,@lunchStart,@lunchEnd,@firstBreakStart,@firstBreakEnd,@secondBreakStart,@secondBreakEnd,@interviewLength);";
        private string SQL_GetAllEvents = @"Select Event_Id, Matchmaking_Id, Start_Time, End_Time, Lunch_Start, Lunch_End, First_Break_Start, First_Break_End, Second_Break_Start, Second_Break_End, Interview_Length from Event where matchmaking_Id = @matchmakingId;";
        public int GetNumberOfStudentChoices(int matchmakingId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetNumberOfStudentChoices, conn);
                    cmd.Parameters.AddWithValue("@matchmakingId", matchmakingId);

                    return (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }
        }

        public List<MatchmakingArrangement> GetAllArrangements()
        {
            List<MatchmakingArrangement> results = new List<MatchmakingArrangement>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllArrangements, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MatchmakingArrangement arrangement = new MatchmakingArrangement();
                        arrangement.MatchmakingId = Convert.ToInt32(reader["Matchmaking_Id"]);
                        arrangement.Location = Convert.ToString(reader["Location"]);
                        arrangement.Season = Convert.ToString(reader["Season"]);
                        arrangement.CohortNumber = Convert.ToInt32(reader["Cohort_Number"]);
                        arrangement.NumberOfStudentChoices = Convert.ToInt32(reader["Number_Of_Student_Choices"]);

                        results.Add(arrangement);
                    }


                }

                return results;
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }
        }

        public bool AddNewArrangement(MatchmakingArrangement arrangement)
        {
            try
            {
                int rowsUpdated = 0;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AddNewArrangement, conn);
                    cmd.Parameters.AddWithValue("@location", arrangement.Location);
                    cmd.Parameters.AddWithValue("@season", arrangement.Season);
                    cmd.Parameters.AddWithValue("@cohortNumber", arrangement.CohortNumber);
                    cmd.Parameters.AddWithValue("@numberOfStudentChoices", arrangement.NumberOfStudentChoices);

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

        public bool AddNewEvent(Event matchmakingEvent)
        {
            try
            {
                int rowsUpdated = 0;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_AddNewEvent, conn);
                    cmd.Parameters.AddWithValue("@matchmakingId", matchmakingEvent.MatchmakingId);
                    cmd.Parameters.AddWithValue("@startTime", matchmakingEvent.StartTime);
                    cmd.Parameters.AddWithValue("@endTime", matchmakingEvent.EndTime);

                    cmd.Parameters.AddWithValue("@lunchStart", matchmakingEvent.LunchStart ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@lunchEnd", matchmakingEvent.LunchEnd ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@firstBreakStart", matchmakingEvent.FirstBreakStart ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@firstBreakEnd", matchmakingEvent.FirstBreakEnd ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@secondBreakStart", matchmakingEvent.SecondBreakStart ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@secondBreakEnd", matchmakingEvent.SecondBreakEnd ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@interviewLength", matchmakingEvent.InterviewLength);

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
        public List<Event> GetAllEvents(int matchmakingId)
        {
            List<Event> result = new List<Event>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllEvents, conn);
                    cmd.Parameters.AddWithValue("@matchmakingId", matchmakingId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Event e = new Event();
                        e.MatchmakingId = Convert.ToInt32(reader["Matchmaking_Id"]);
                        e.EventDate = Convert.ToDateTime(reader["Start_Time"]).ToShortDateString();
                        e.StartTime = Convert.ToDateTime(reader["Start_Time"]).ToShortTimeString();
                        e.EndTime = Convert.ToDateTime(reader["End_Time"]).ToShortTimeString();
                        e.LunchStart = Convert.ToDateTime(reader["Lunch_Start"]).ToShortTimeString();
                        e.LunchStart = Convert.ToDateTime(reader["Lunch_End"]).ToShortTimeString();
                        e.FirstBreakStart = Convert.ToDateTime(reader["First_Break_Start"]).ToShortTimeString();
                        e.FirstBreakEnd = Convert.ToDateTime(reader["First_Break_End"]).ToShortTimeString();
                        e.SecondBreakStart = Convert.ToDateTime(reader["Second_Break_Start"]).ToShortTimeString();
                        e.SecondBreakEnd = Convert.ToDateTime(reader["Second_Break_End"]).ToShortTimeString();
                        e.InterviewLength = Convert.ToInt32(reader["Interview_Length"]);

                        result.Add(e);
                    }

                    return result;
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