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
    public class InterviewDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["FinalCapstoneDatabase"].ConnectionString;

        private string SQL_GetMasterSchedule = @"select e.Employer_Name, i.Team_Id, s.Student_Name, i.Event_Date, t.Start_Time, i.Employer_id, i.student_id 
                                                from Interview_Schedule i join employer e on i.Employer_id = e.Employer_Id join student s on i.student_id = s.Student_Id  
                                                join Time_Slot_Rank t on i.Time_Slot_Rank = t.Time_Slot_Rank order by i.Employer_Id, t.Start_Time";

        private string SQL_GetStudentSchedule = @"select e.Employer_Name, i.Team_Id, s.Student_Name, i.Event_Date, t.Start_Time, i.Employer_id, i.student_id 
                                                from Interview_Schedule i join employer e on i.Employer_id = e.Employer_Id join student s on i.student_id = s.Student_Id  
                                                join Time_Slot_Rank t on i.Time_Slot_Rank = t.Time_Slot_Rank where s.student_id = @studentId order by t.Start_Time";

        private string SQL_GetEmployerSchedule = @"select e.Employer_Name, i.Team_Id, s.Student_Name, i.Event_Date, t.Start_Time, i.Employer_id, i.student_id 
                                                from Interview_Schedule i join employer e on i.Employer_id = e.Employer_Id join student s on i.student_id = s.Student_Id  
                                                join Time_Slot_Rank t on i.Time_Slot_Rank = t.Time_Slot_Rank where e.employer_id = @employerId order by t.Start_Time, i.Team_Id";

        private string SQL_GetNextAvailableTimeSlot = @"select max(Time_Slot_Rank) as maximum from Interview_Schedule where Employer_Id = @employerId";

        private string SQL_GetMaximumTimeSlotsAvailable = @"select max(Time_Slot_Rank) from Time_Slot_Rank";

        private string SQL_UpdateInterviewSchedule = @"insert into Interview_Schedule (Student_Id, Employer_Id, Team_Id, Time_Slot_Rank, Event_Date) values (@studentId, @employerId, @teamId, @timeSlotRank, @eventDate)";

        public List<Interview> GetMasterSchedule()
        {
            List<Interview> results = new List<Interview>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetMasterSchedule, conn);

                    results = PopulateSchedule(cmd);
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return results;
        }

        public List<Interview> GetStudentSchedule(int studentId)
        {
            List<Interview> results = new List<Interview>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetStudentSchedule, conn);
                    cmd.Parameters.AddWithValue("@studentId", studentId);

                    results = PopulateSchedule(cmd);

                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return results;
        }

        public List<Interview> GetEmployerSchedule(int employerId)
        {
            List<Interview> results = new List<Interview>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetEmployerSchedule, conn);
                    cmd.Parameters.AddWithValue("@employerId", employerId);

                    results = PopulateSchedule(cmd);

                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }

            return results;
        }

        public void UpdateEmployerScheduleWithStudent(StudentChoice choice)
        {
            int maxTimeSlotsAvailable = 0;
            int nextAvailableTimeSlot = 1;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_GetMaximumTimeSlotsAvailable, conn);
                    maxTimeSlotsAvailable = (int)cmd.ExecuteScalar();

                    cmd = new SqlCommand(SQL_GetNextAvailableTimeSlot, conn);
                    cmd.Parameters.AddWithValue("@employerId", choice.EmployerId);

                    //ask about null values cause the first time through for each employer will be null
                    // and I need it to use 1 instead of the null.
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.IsDBNull(reader.GetOrdinal("maximum")))
                    {
                        //nextAvailableTimeSlot = ((int)cmd.ExecuteScalar()) + 1;
                        nextAvailableTimeSlot = Convert.ToInt32(reader["maximum"]) + 1;
                    }

                    if (nextAvailableTimeSlot <= maxTimeSlotsAvailable)
                    {
                        cmd = new SqlCommand(SQL_UpdateInterviewSchedule, conn);
                        cmd.Parameters.AddWithValue("@studentId", choice.StudentId);
                        cmd.Parameters.AddWithValue("@employerId", choice.EmployerId);
                        cmd.Parameters.AddWithValue("@timeSlotRank", nextAvailableTimeSlot);
                        cmd.Parameters.AddWithValue("@eventDate", choice.EventDate);

                        //will need updated with team info when multiple teams
                        cmd.Parameters.AddWithValue("@teamId", 1);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw new NotImplementedException();
            }
        }

        public void RandomlyFillInRemainingEmployerSchedules()
        {
            //while there are more employer slots to fill
            //select a student and time who is not currently in the schedule
            //working on the joing sql
            //update the interviewschedule

        }

        public List<Interview> PopulateSchedule(SqlCommand cmd)
        {
            List<Interview> results = new List<Interview>();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Interview i = new Interview()
                {
                    EmployerId = Convert.ToInt32(reader["Employer_Id"]),
                    EmployerName = Convert.ToString(reader["Employer_Name"]),
                    TeamId = Convert.ToInt32(reader["Team_Id"]),
                    StudentId = Convert.ToInt32(reader["Student_Id"]),
                    StudentName = Convert.ToString(reader["Student_Name"]),
                    EventDate = Convert.ToDateTime(reader["Event_Date"]).ToShortDateString(),
                    StartTime = Convert.ToDateTime(reader["Start_Time"]).ToShortTimeString()
                };
                results.Add(i);
            }

            return results;
        }

    }

}
