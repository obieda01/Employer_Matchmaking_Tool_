﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;

namespace Capstone.Web.DAL
{
    public class InterviewDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["FinalCapstone"].ConnectionString;

        private string SQL_GetSchedule = @"select e.Employer_Name, i.Team_Id, s.Student_Name, i.Matchmaking_Id, t.Start_Time, i.Employer_id, i.student_id, et.assigned_room
                                                from Interview_Schedule i join employer e on i.Employer_id = e.Employer_Id join student s on i.student_id = s.Student_Id  
                                                join Time_Slot_Rank t on i.Time_Slot_Rank = t.Time_Slot_Rank 
												join Employer_Team et on et.Employer_Id = e.Employer_Id and et.Team_Id = i.Team_Id and et.matchmaking_id = i.Matchmaking_Id ";

        private string SQL_GetNextAvailableTimeSlot = @"select max(Time_Slot_Rank) as maximum from Interview_Schedule where Employer_Id = @employerId";

        private string SQL_GetMaximumTimeSlotsAvailable = @"select max(Time_Slot_Rank) from Time_Slot_Rank Matchmaking_Id = @matchmakingId";

        private string SQL_UpdateInterviewSchedule = @"insert into Interview_Schedule (Student_Id, Employer_Id, Team_Id, Time_Slot_Rank, Matchmaking_Id) values (@studentId, @employerId, @teamId, @timeSlotRank, @matchmaking_Id)";

        private string SQL_GetTotalEmployerInterview = @"select employer_id, count(*) as totalInterviews from Interview_Schedule  group by Employer_id";

        private string SQL_GetEarliestAvailableTimeSlot = @"select min(time_slot_rank) from Time_Slot_rank where Matchmaking_Id = @matchmakingId and Time_Slot_Rank not in 
                                                    (select Time_Slot_Rank from  interview_schedule where Matchmaking_Id = @matchmakingId and employer_id = @employerId)";

        private string SQL_GetStudentWhoHasAvailabilityToInterview = @"select top 1 (Student_Id) from Student where Matchmaking_Id = @matchmakingId and Student_Id NOT IN(SELECT Student_Id FROM Interview_Schedule where Matchmaking_Id = @matchmakingId and Employer_Id = @employerId)
                                                    and (Student_Id not in (select Student_Id from Interview_Schedule where Matchmaking_Id = @matchmakingId and Time_Slot_Rank = @timeSlotRank))";

        public List<Interview> GetMasterSchedule(int matchmakingId)
        {
            List<Interview> results = new List<Interview>();

            string SQL_GetMasterSchedule = SQL_GetSchedule + "where i.Matchmaking_Id = @matchmakingId order by i.Employer_Id, t.Start_Time;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetMasterSchedule, conn);
                    cmd.Parameters.AddWithValue("@matchmakingId", matchmakingId);

                    results = populateSchedule(cmd);
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return results;
        }

        public List<Interview> GetStudentSchedule(int studentId, int matchmakingId)
        {
            List<Interview> results = new List<Interview>();

            string SQL_GetStudentSchedule = SQL_GetSchedule + "where i.Matchmaking_Id = @matchmakingId  and s.student_id = @studentId order by t.Start_Time;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetStudentSchedule, conn);
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    cmd.Parameters.AddWithValue("@matchmakingId", matchmakingId);

                    results = populateSchedule(cmd);

                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return results;
        }

        public List<Interview> GetEmployerSchedule(int employerId, int matchmakingId)
        {
            string SQL_GetEmployerSchedule = SQL_GetSchedule + "where e.employer_id = @employerId and i.Matchmaking_Id = @matchmakingId order by t.Start_Time, i.Team_Id;";
            List<Interview> results = new List<Interview>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetEmployerSchedule, conn);
                    cmd.Parameters.AddWithValue("@employerId", employerId);
                    cmd.Parameters.AddWithValue("@matchmakingId", matchmakingId);

                    results = populateSchedule(cmd);

                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return results;
        }

        public List<Interview> GetAllStudentsSchedules(int matchmakingId)
        {
            List<Interview> results = new List<Interview>();

            string SQL_GetAllStudentSchedules = SQL_GetSchedule + "where i.Matchmaking_Id = @matchmakingId  order by s.Student_Name, t.Start_Time;";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAllStudentSchedules, conn);
                    cmd.Parameters.AddWithValue("@matchmakingId", matchmakingId);
                    results = populateSchedule(cmd);

                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return results;
        }

        public void GenerateMatchesByStudentRanking(StudentChoice choice)
        {
            int maxTimeSlotsAvailable = 0;
            Interview interviewToBeAdded = new Interview();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetMaximumTimeSlotsAvailable, conn);
                    cmd.Parameters.AddWithValue("@matchmakingId", choice.MatchmakingId);
                    maxTimeSlotsAvailable = (int)cmd.ExecuteScalar();

                    cmd = new SqlCommand(SQL_GetNextAvailableTimeSlot, conn);
                    cmd.Parameters.AddWithValue("@employerId", choice.EmployerId);

                    var temp = cmd.ExecuteScalar();

                    int nextAvailableTimeSlot = (!DBNull.Value.Equals(temp)) ? (int)(temp) + 1 : 1;

                    if (nextAvailableTimeSlot <= maxTimeSlotsAvailable)
                    {
                        interviewToBeAdded.StudentId = choice.StudentId;
                        interviewToBeAdded.EmployerId = choice.EmployerId;
                        interviewToBeAdded.TimeSlotRank = nextAvailableTimeSlot;
                        interviewToBeAdded.Matchmaking_Id = choice.MatchmakingId;
                        //will need updated with team info when multiple teams - need to update TeamId accordingly
                        interviewToBeAdded.TeamId = 1;

                        UpdateInterviewSchedule(cmd, conn, interviewToBeAdded);
                    }
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }
        }

        public void RandomlyGenerateRemainingSchedule(int matchmakingId)
        {
            int maxTimeSlotsAvailable = 0;
            Dictionary<int, int> totalInterviewsGroupedByEmployers = new Dictionary<int, int>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetMaximumTimeSlotsAvailable, conn);
                    cmd.Parameters.AddWithValue("@matchmakingId", matchmakingId);

                    maxTimeSlotsAvailable = (int)cmd.ExecuteScalar();

                    cmd = new SqlCommand(SQL_GetTotalEmployerInterview, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        totalInterviewsGroupedByEmployers.Add((Convert.ToInt32(reader["employer_id"])), (Convert.ToInt32(reader["totalInterviews"])));
                    }
                    reader.Close();

                    foreach (var employerTotal in totalInterviewsGroupedByEmployers)
                    {
                        int totalInterviews = employerTotal.Value;

                        while (totalInterviews < maxTimeSlotsAvailable)
                        {
                            Interview interviewToBeInserted = new Interview();
                            interviewToBeInserted.EmployerId = employerTotal.Key;

                            SqlCommand cmd2 = new SqlCommand(SQL_GetEarliestAvailableTimeSlot, conn);
                            cmd2.Parameters.AddWithValue("@employerId", interviewToBeInserted.EmployerId);
                            cmd2.Parameters.AddWithValue("@matchmakingId", matchmakingId);

                            interviewToBeInserted.TimeSlotRank = (int)cmd2.ExecuteScalar();

                            cmd = new SqlCommand(SQL_GetStudentWhoHasAvailabilityToInterview, conn);
                            cmd.Parameters.AddWithValue("@employerId", interviewToBeInserted.EmployerId);
                            cmd.Parameters.AddWithValue("@timeSlotRank", interviewToBeInserted.TimeSlotRank);
                            cmd.Parameters.AddWithValue("@matchmakingId", matchmakingId);

                            interviewToBeInserted.StudentId = (int)cmd.ExecuteScalar();
                            interviewToBeInserted.Matchmaking_Id = matchmakingId;

                            //will need updated with team info when multiple teams - need to update TeamId accordingly
                            interviewToBeInserted.TeamId = 1;

                            UpdateInterviewSchedule(cmd, conn, interviewToBeInserted);

                            totalInterviews++;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }
        }

        public List<Interview> populateSchedule(SqlCommand cmd)
        {
            List<Interview> results = new List<Interview>();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Interview i = new Interview();
                i.EmployerId = Convert.ToInt32(reader["Employer_Id"]);
                i.EmployerName = Convert.ToString(reader["Employer_Name"]);
                i.TeamId = Convert.ToInt32(reader["Team_Id"]);
                i.StudentId = Convert.ToInt32(reader["Student_Id"]);
                i.StudentName = Convert.ToString(reader["Student_Name"]);
                i.Matchmaking_Id = Convert.ToInt32(reader["Matchmaking_Id"]);
                i.StartTime = Convert.ToDateTime(reader["Start_Time"]).ToShortTimeString();
                i.EventDate = Convert.ToDateTime(reader["Start_Time"]).ToShortDateString();
                i.AssignedRoom = Convert.ToString(reader["Assigned_room"]);

                results.Add(i);
            }

            return results;
        }

        public void UpdateInterviewSchedule(SqlCommand cmd, SqlConnection conn, Interview interviewToBeAdded)
        {
            cmd = new SqlCommand(SQL_UpdateInterviewSchedule, conn);
            cmd.Parameters.AddWithValue("@studentId", interviewToBeAdded.StudentId);
            cmd.Parameters.AddWithValue("@employerId", interviewToBeAdded.EmployerId);
            cmd.Parameters.AddWithValue("@timeSlotRank", interviewToBeAdded.TimeSlotRank);
            cmd.Parameters.AddWithValue("@matchmakingId", interviewToBeAdded.Matchmaking_Id);
            cmd.Parameters.AddWithValue("@teamId", interviewToBeAdded.TeamId);

            cmd.ExecuteNonQuery();
        }
    }

}