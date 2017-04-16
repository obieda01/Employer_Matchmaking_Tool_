-- *****************************************************************************
-- This script contains INSERT statements for populating tables with seed data
-- *****************************************************************************

--Student Ranking SQL Insert Statements below. Deleting all data to reset DB first
BEGIN transaction;

Delete from Employer_Team;
Delete from Interview_Schedule;
Delete from Student_Choices;
Delete from Time_Slot_Rank;
Delete from Wait_Listed;
Delete from Internal_Staff;
Delete from Employer;
Delete from Student;
Delete from Login;
Delete from Event;
Delete from Language;
Delete from Matchmaking_Arrangement;

SET IDENTITY_INSERT dbo.Matchmaking_Arrangement ON;
Insert into Matchmaking_Arrangement (Matchmaking_Id, Location, Season, Cohort_Number, Number_Of_Student_Choices, Schedule_Is_Generated) VALUES (1,'Columbus','Winter', 1,2,'Y');
SET IDENTITY_INSERT dbo.Matchmaking_Arrangement OFF;

Insert into Login (User_Name, Password, User_Role) VALUES ('Jacob','GWtlZ2gG14GC6DiY937w1L9RZCM=','staff');
Insert into Login (User_Name, Password, User_Role) VALUES ('Ahmad','wyG8WVYjxcSVHmQKpxaQSK4x+lU=','admin');
Insert into Login (User_Name, Password, User_Role) VALUES ('Letha','GWtlZ2gG14GC6DiY937w1L9RZCM=','student');
Insert into Login (User_Name, Password, User_Role) VALUES ('Jean','GWtlZ2gG14GC6DiY937w1L9RZCM=','student');
Insert into Login (User_Name, Password, User_Role) VALUES ('Mark','GWtlZ2gG14GC6DiY937w1L9RZCM=','student');
Insert into Login (User_Name, Password, User_Role) VALUES ('Ashley','GWtlZ2gG14GC6DiY937w1L9RZCM=','student');
Insert into Login (User_Name, Password, User_Role) VALUES ('Rob','GWtlZ2gG14GC6DiY937w1L9RZCM=','student');

SET IDENTITY_INSERT dbo.Employer ON;
Insert into Employer (Employer_Id, Employer_Name, Number_Of_Teams, Summary) VALUES (1,'Chase', 1,'Test Summary');
Insert into Employer (Employer_Id, Employer_Name, Number_Of_Teams, Summary) VALUES (2,'FastSwitch', 1,'Test Summary');
Insert into Employer (Employer_Id, Employer_Name, Number_Of_Teams, Summary) VALUES (3,'IGS', 1,'Test Summary');
SET IDENTITY_INSERT dbo.Employer OFF;

SET IDENTITY_INSERT dbo.Event ON;
Insert into Event (Event_Id, Matchmaking_Id, Start_Time, End_Time, Lunch_Start, Lunch_End, First_Break_Start, First_Break_End, Second_Break_Start, Second_Break_End, Interview_Length) VALUES (1, 1,'4/15/2017 1:00:00 PM', '4/15/2017 5:00:00 PM',null,null,'4/15/2017 3:15:00 PM','4/15/2017 3:30:00 PM',null,null,45);
SET IDENTITY_INSERT dbo.Event OFF;

Insert into Language (Language_Id, Language) VALUES (0,'Both');
Insert into Language (Language_Id, Language) VALUES (1,'Java');
Insert into Language (Language_Id, Language) VALUES (2,'.Net');

SET IDENTITY_INSERT dbo.Student ON;
Insert into Student (Student_Id,Student_Name, Language_Id, User_Name, Matchmaking_Id) VALUES (1,'Letha Lerman',2,'Letha', 1);
Insert into Student (Student_Id,Student_Name, Language_Id, User_Name, Matchmaking_Id) VALUES (2,'Jean Justus',1,'Jean', 1);
Insert into Student (Student_Id,Student_Name, Language_Id, User_Name, Matchmaking_Id) VALUES (3,'Mark Moore',2,'Mark', 1);
Insert into Student (Student_Id,Student_Name, Language_Id, User_Name, Matchmaking_Id) VALUES (4,'Ashley Anderson',1,'Ashley', 1);
Insert into Student (Student_Id,Student_Name, Language_Id, User_Name, Matchmaking_Id) VALUES (5,'Rob Redmond',1,'Rob', 1);
SET IDENTITY_INSERT dbo.Student OFF;

Insert into Employer_Team (Matchmaking_Id, Employer_Id, Team_Id, Event_Id, Language_Id, Start_Time, End_Time, Assigned_Room) VALUES (1,1,1, 1, 0,'4/15/2017 1:00:00 PM','4/15/2017 5:00:00 PM','Test Room A');
Insert into Employer_Team (Matchmaking_Id, Employer_Id, Team_Id, Event_Id, Language_Id, Start_Time, End_Time, Assigned_Room) VALUES (1,2,1, 1, 1,'4/15/2017 1:00:00 PM','4/15/2017 5:00:00 PM','Test Room B');
Insert into Employer_Team (Matchmaking_Id, Employer_Id, Team_Id, Event_Id, Language_Id, Start_Time, End_Time, Assigned_Room) VALUES (1,3,1, 1, 2,'4/15/2017 1:00:00 PM','4/15/2017 5:00:00 PM','Test Room C');

Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Matchmaking_Id) VALUES (1,2,1, 1);
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Matchmaking_Id) VALUES (1,3,2, 1);
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Matchmaking_Id) VALUES (2,1,1, 1);
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Matchmaking_Id) VALUES (2,2,2, 1);
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Matchmaking_Id) VALUES (3,3,1, 1);
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Matchmaking_Id) VALUES (3,1,2, 1);
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Matchmaking_Id) VALUES (4,1,1, 1);
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Matchmaking_Id) VALUES (4,3,2, 1);
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Matchmaking_Id) VALUES (5,2,1, 1);
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Matchmaking_Id) VALUES (5,3,2, 1);

Insert into Time_Slot_Rank (Matchmaking_Id, Start_Time, End_Time, Time_Slot_Rank) VALUES (1,'4/15/2017 1:00:00 PM','4/15/2017 1:45:00 PM',5);
Insert into Time_Slot_Rank (Matchmaking_Id, Start_Time, End_Time, Time_Slot_Rank) VALUES (1,'4/15/2017 1:45:00 PM','4/15/2017 2:30:00 PM',3);
Insert into Time_Slot_Rank (Matchmaking_Id, Start_Time, End_Time, Time_Slot_Rank) VALUES (1,'4/15/2017 2:30:00 PM','4/15/2017 3:15:00 PM',1);
Insert into Time_Slot_Rank (Matchmaking_Id, Start_Time, End_Time, Time_Slot_Rank) VALUES (1,'4/15/2017 3:30:00 PM','4/15/2017 4:15:00 PM',2);
Insert into Time_Slot_Rank (Matchmaking_Id, Start_Time, End_Time, Time_Slot_Rank) VALUES (1,'4/15/2017 4:15:00 PM','4/15/2017 5:00:00 PM',4);

SET IDENTITY_INSERT dbo.Internal_Staff ON;
Insert into Internal_Staff (Staff_Id, User_Name,Name, Admin_Flag) VALUES (1,'Jacob','Jacob Rutter','n');
Insert into Internal_Staff (Staff_Id, User_Name,Name, Admin_Flag) VALUES (2,'Ahmad','Ahmad Obiedat','y');
SET IDENTITY_INSERT dbo.Internal_Staff OFF;



COMMIT;
--End of Student Ranking SQL insert statements

