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

        private string SQL_GetAllEmployersByMatchmakingId = "Select e.Employer_Id, e.Employer_Name, e.Number_Of_Teams, e.Summary From Employer e join Employer_Team et on et.Employer_Id = e.Employer_Id where et.Matchmaking_Id = @matchmakingId;";

        private string SQL_AddNewEmployer = "Insert into employer (Employer_Name, Summary, Number_Of_Teams) VALUES (@employerName, @summary, @numberOfTeams);";

        private string SQL_GetAllEmployersAndTeamsByMatchmakingId = "Select t.event_Id, t.Matchmaking_Id, e.Employer_Id, e.Employer_Name, t.Assigned_Room, t.Team_Id, l.Language, t.Start_Time, t.End_Time from Employer_Team t join Employer e ON e.Employer_Id = t.Employer_Id join Language l ON t.Language_Id = l.Language_Id where t.Matchmaking_Id = @matchmakingId;";

        private string SQL_GetAllEmployerTeams = "Select t.event_Id, t.Matchmaking_Id, e.Employer_Id, e.Employer_Name, t.Assigned_Room, t.Team_Id, l.Language, t.Start_Time, t.End_Time from Employer_Team t join Employer e ON e.Employer_Id = t.Employer_Id join Language l ON t.Language_Id = l.Language_Id;";

        private string SQL_GetParticipatingEmployersAndTeams = "Select t.event_Id, t.Matchmaking_Id, e.Employer_Id, e.Employer_Name, t.Assigned_Room, t.Team_Id, l.Language, t.Start_Time, t.End_Time from Employer_Team t join Employer e ON e.Employer_Id = t.Employer_Id join Language l ON t.Language_Id = l.Language_Id where t.Matchmaking_Id = @matchmakingId  and t.Event_Id = @eventId;";

        private string SQL_UpdateRoom = "Update Employer_Team SET assigned_room = @assignedRoom where matchmaking_id = @matchmakingId and employer_id = @employerId and team_id = @teamId";

        public List<Employer> GetAllEmployers(int matchmakingId)
        {
            List<Employer> results = new List<Employer>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllEmployersByMatchmakingId, conn);
                    cmd.Parameters.AddWithValue("@matchmakingId", matchmakingId);

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

        public List<EmployerTeam> GetAllEmployersAndTeams(int matchmakingId)
        {
            List<EmployerTeam> results = new List<EmployerTeam>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllEmployersAndTeamsByMatchmakingId, conn);
                    cmd.Parameters.AddWithValue("@matchmakingId", matchmakingId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        EmployerTeam e = new EmployerTeam();
                        e.MatchmakingId = Convert.ToInt32(reader["Matchmaking_Id"]);
                        e.EventId = Convert.ToInt32(reader["Event_Id"]);
                        e.EmployerId = Convert.ToInt32(reader["Employer_Id"]);
                        e.EmployerName = Convert.ToString(reader["Employer_Name"]);
                        e.TeamId = Convert.ToInt32(reader["Team_Id"]);
                        e.StartTime = Convert.ToDateTime(reader["Start_Time"]).ToShortTimeString();
                        e.EndTime = Convert.ToDateTime(reader["End_Time"]).ToShortTimeString();
                        e.AssignedRoom = Convert.ToString(reader["Assigned_Room"]);
                        e.EventDate = Convert.ToDateTime(reader["Start_Time"]).ToShortDateString();
                        e.Language = Convert.ToString(reader["Language"]);
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

        public List<EmployerTeam> GetAllEmployersAndTeams()
        {
            List<EmployerTeam> results = new List<EmployerTeam>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetAllEmployerTeams, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        EmployerTeam e = new EmployerTeam();
                        e.MatchmakingId = Convert.ToInt32(reader["Matchmaking_Id"]);
                        e.EventId = Convert.ToInt32(reader["Event_Id"]);
                        e.EmployerId = Convert.ToInt32(reader["Employer_Id"]);
                        e.EmployerName = Convert.ToString(reader["Employer_Name"]);
                        e.TeamId = Convert.ToInt32(reader["Team_Id"]);
                        e.StartTime = Convert.ToDateTime(reader["Start_Time"]).ToShortTimeString();
                        e.EndTime = Convert.ToDateTime(reader["End_Time"]).ToShortTimeString();
                        e.AssignedRoom = Convert.ToString(reader["Assigned_Room"]);
                        e.EventDate = Convert.ToDateTime(reader["Start_Time"]).ToShortDateString();
                        e.Language = Convert.ToString(reader["Language"]);
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

        public List<EmployerTeam> GetParticipatingEmployersAndTeams(int matchmakingId, int eventId)
        {
            List<EmployerTeam> results = new List<EmployerTeam>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetParticipatingEmployersAndTeams, conn);
                    cmd.Parameters.AddWithValue("@matchmakingId", matchmakingId);
                    cmd.Parameters.AddWithValue("@eventId", eventId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        EmployerTeam e = new EmployerTeam();
                        e.MatchmakingId = Convert.ToInt32(reader["Matchmaking_Id"]);
                        e.EventId = Convert.ToInt32(reader["Event_Id"]);
                        e.EmployerId = Convert.ToInt32(reader["Employer_Id"]);
                        e.EmployerName = Convert.ToString(reader["Employer_Name"]);
                        e.TeamId = Convert.ToInt32(reader["Team_Id"]);
                        e.StartTime = Convert.ToDateTime(reader["Start_Time"]).ToShortTimeString();
                        e.EndTime = Convert.ToDateTime(reader["End_Time"]).ToShortTimeString();
                        e.AssignedRoom = Convert.ToString(reader["Assigned_Room"]);
                        e.EventDate = Convert.ToDateTime(reader["Start_Time"]).ToShortDateString();
                        e.Language = Convert.ToString(reader["Language"]);
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

        public bool UpdateAssignedRoom(List<EmployerTeam> team)
        {
            try
            {
                int rowsUpdated = 0;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    foreach (EmployerTeam e in team)
                    {
                        //update assigned room
                        SqlCommand cmd = new SqlCommand(SQL_UpdateRoom, conn);
                        cmd.Parameters.AddWithValue("@employerId", e.EmployerId);
                        cmd.Parameters.AddWithValue("@teamId", e.TeamId);
                        cmd.Parameters.AddWithValue("@matchmakingId", e.MatchmakingId);
                        cmd.Parameters.AddWithValue("@assignedRoom", e.AssignedRoom);

                        rowsUpdated += cmd.ExecuteNonQuery();
                    }
                }

                return (rowsUpdated == team.Count());
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }
        }
    }

}
