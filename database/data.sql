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

Insert into Login (User_Name, Password, User_Role) VALUES ('Jacob','Password','staff');
Insert into Login (User_Name, Password, User_Role) VALUES ('Ahmad','Password','admin');
Insert into Login (User_Name, Password, User_Role) VALUES ('Letha','Password','student');
Insert into Login (User_Name, Password, User_Role) VALUES ('Jean','Password','student');
Insert into Login (User_Name, Password, User_Role) VALUES ('Mark','Password','student');
Insert into Login (User_Name, Password, User_Role) VALUES ('Ashley','Password','student');
Insert into Login (User_Name, Password, User_Role) VALUES ('Rob','Password','student');

SET IDENTITY_INSERT dbo.Employer ON;
Insert into Employer (Employer_Id, Employer_Name, Number_Of_Teams, Summary) VALUES (1,'Chase', 1,'Test Summary');
Insert into Employer (Employer_Id, Employer_Name, Number_Of_Teams, Summary) VALUES (2,'FastSwitch', 1,'Test Summary');
Insert into Employer (Employer_Id, Employer_Name, Number_Of_Teams, Summary) VALUES (3,'IGS', 1,'Test Summary');
SET IDENTITY_INSERT dbo.Employer OFF;

Insert into Event (Event_Date, Start_Time, End_Time, Lunch_Start, Lunch_End, First_Break_Start, First_Break_End, Second_Break_Start, Second_Break_End, Interview_Length) VALUES ('4/15/2017','4/15/2017 1:00:00 PM', '4/15/2017 5:00:00 PM',null,null,'4/15/2017 3:15:00 PM','4/15/2017 3:30:00 PM',null,null,45);
Insert into Language (Language_Id, Language) VALUES (0,'Both');
Insert into Language (Language_Id, Language) VALUES (1,'Java');
Insert into Language (Language_Id, Language) VALUES (2,'.Net');

SET IDENTITY_INSERT dbo.Student ON;
Insert into Student (Student_Id,Student_Name, Language_Id, User_Name) VALUES (1,'Letha Lerman',2,'Letha');
Insert into Student (Student_Id,Student_Name, Language_Id, User_Name) VALUES (2,'Jean Justus',1,'Jean');
Insert into Student (Student_Id,Student_Name, Language_Id, User_Name) VALUES (3,'Mark Moore',2,'Mark');
Insert into Student (Student_Id,Student_Name, Language_Id, User_Name) VALUES (4,'Ashley Anderson',1,'Ashley');
Insert into Student (Student_Id,Student_Name, Language_Id, User_Name) VALUES (5,'Rob Redmond',1,'Rob');
SET IDENTITY_INSERT dbo.Student OFF;

Insert into Employer_Team (Employer_Id, Team_Id, Event_Date, Language_Id, Start_Time, End_Time, Assigned_Room) VALUES (1,1,'4/15/2017',0,'1:00:00 PM','5:00:00 PM','Test Room A');
Insert into Employer_Team (Employer_Id, Team_Id, Event_Date, Language_Id, Start_Time, End_Time, Assigned_Room) VALUES (2,1,'4/15/2017',1,'1:00:00 PM','5:00:00 PM','Test Room B');
Insert into Employer_Team (Employer_Id, Team_Id, Event_Date, Language_Id, Start_Time, End_Time, Assigned_Room) VALUES (3,1,'4/15/2017',2,'1:00:00 PM','5:00:00 PM','Test Room C');

Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Event_Date) VALUES (1,2,1,'4/15/2017');
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Event_Date) VALUES (1,3,2,'4/15/2017');
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Event_Date) VALUES (2,1,1,'4/15/2017');
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Event_Date) VALUES (2,2,2,'4/15/2017');
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Event_Date) VALUES (3,3,1,'4/15/2017');
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Event_Date) VALUES (3,1,2,'4/15/2017');
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Event_Date) VALUES (4,1,1,'4/15/2017');
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Event_Date) VALUES (4,3,2,'4/15/2017');
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Event_Date) VALUES (5,2,1,'4/15/2017');
Insert into Student_Choices (Student_Id, Employer_Id, Employer_Rank, Event_Date) VALUES (5,3,2,'4/15/2017');

Insert into Time_Slot_Rank (Event_Date, Start_Time, End_Time, Time_Slot_Rank) VALUES ('4/15/2017','4/15/2017 1:00:00 PM','4/15/2017 1:45:00 PM',5);
Insert into Time_Slot_Rank (Event_Date, Start_Time, End_Time, Time_Slot_Rank) VALUES ('4/15/2017','4/15/2017 1:45:00 PM','4/15/2017 2:30:00 PM',3);
Insert into Time_Slot_Rank (Event_Date, Start_Time, End_Time, Time_Slot_Rank) VALUES ('4/15/2017','4/15/2017 2:30:00 PM','4/15/2017 3:15:00 PM',1);
Insert into Time_Slot_Rank (Event_Date, Start_Time, End_Time, Time_Slot_Rank) VALUES ('4/15/2017','4/15/2017 3:30:00 PM','4/15/2017 4:15:00 PM',2);
Insert into Time_Slot_Rank (Event_Date, Start_Time, End_Time, Time_Slot_Rank) VALUES ('4/15/2017','4/15/2017 4:15:00 PM','4/15/2017 5:00:00 PM',4);

SET IDENTITY_INSERT dbo.Internal_Staff ON;
Insert into Internal_Staff (Staff_Id, User_Name,Name, Admin_Flag) VALUES (1,'Jacob','Jacob Rutter','n');
Insert into Internal_Staff (Staff_Id, User_Name,Name, Admin_Flag) VALUES (2,'Ahmad','Ahmad Obiedat','y');
SET IDENTITY_INSERT dbo.Internal_Staff OFF;



COMMIT;
--End of Student Ranking SQL insert statements

