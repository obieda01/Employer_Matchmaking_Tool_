using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.Web.Models;
using System.Configuration;

namespace Capstone.Web.Tests.DAL
{
    [TestClass]
    public class InterviewDALTests
    {
        private TransactionScope tran;
        private string connectionString = ConfigurationManager.ConnectionStrings["FinalCapstone"].ConnectionString;
        // Set up the database before each test        
        [TestInitialize]
        public void Initialize()
        {

            // Initialize a new transaction scope. This automatically begins the transaction.
            tran = new TransactionScope();
           
            
            // Open a SqlConnection object using the active transaction
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;

                conn.Open();
                string masterScheduleSQL = @"Insert into Interview_Schedule (Student_Id, Employer_Id, Team_Id, Time_Slot_Rank, Matchmaking_Id) VALUES (1,2,1, 1, 2);
Insert into Interview_Schedule (Student_Id, Employer_Id, Team_Id, Time_Slot_Rank, Matchmaking_Id) VALUES (1,3,1, 2, 2);
Insert into Interview_Schedule (Student_Id, Employer_Id, Team_Id, Time_Slot_Rank, Matchmaking_Id) VALUES (2,1,1, 1, 2);
Insert into Interview_Schedule (Student_Id, Employer_Id, Team_Id, Time_Slot_Rank, Matchmaking_Id) VALUES (2,2,1, 3, 2);
Insert into Interview_Schedule (Student_Id, Employer_Id, Team_Id, Time_Slot_Rank, Matchmaking_Id) VALUES (3,1,1, 3, 2);
Insert into Interview_Schedule (Student_Id, Employer_Id, Team_Id, Time_Slot_Rank, Matchmaking_Id) VALUES (3,3,1, 1, 2);";
                //Insert a Dummy Record for Interview                
                //cmd = new SqlCommand(masterScheduleSQL, conn);
                //cmd.ExecuteNonQuery();

                
                //cmd = new SqlCommand("INSERT INTO City VALUES ('Test City', 'ABC', 'Test District', 1); SELECT CAST(SCOPE_IDENTITY() as int);", conn);
               // cityId = (int)cmd.ExecuteScalar();
            }
        }

        // Cleanup runs after every single test
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose(); //<-- disposing the transaction without committing it means it will get rolled back
        }


        [TestMethod()]
        public void GetMasterScheduleTest()
        {

            // Arrange 
            InterviewDAL iDal = new InterviewDAL();

            //Act
            List<Interview> schedules = iDal.GetMasterSchedule(1); 

            //Assert
            Assert.AreEqual(15, schedules.Count);              
            /*Assert.AreEqual(cityId, cities[0].CityId); */    
        }

        [TestMethod()]
        public void GetStudentScheduleTest()
        {

            // Arrange 
            InterviewDAL iDal = new InterviewDAL();

            //Act
            List<Interview> schedules = iDal.GetStudentSchedule(1,1);

            //Assert
            Assert.AreEqual(3, schedules.Count);
            /*Assert.AreEqual(cityId, cities[0].CityId); */
        }

        [TestMethod()]
        public void GetEmployerScheduleTest()
        {

            // Arrange 
            InterviewDAL iDal = new InterviewDAL();

            //Act
            List<Interview> schedules = iDal.GetEmployerSchedule(1, 1);

            //Assert
            Assert.AreEqual(5, schedules.Count);
            /*Assert.AreEqual(cityId, cities[0].CityId); */
        }
    }
}
