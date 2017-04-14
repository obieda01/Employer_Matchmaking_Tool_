-- *************************************************************************************************
-- This script creates all of the database objects (tables, constraints, etc) for the database
-- *************************************************************************************************

USE [master]

DROP DATABASE [FinalCapstone];

CREATE DATABASE [FinalCapstone];

use [FinalCapstone];

BEGIN TRANSACTION;

CREATE TABLE [dbo].[Login](
	[User_Name] [varchar] (50) NOT NULL, --
	[Password] [varchar] (32) NOT NULL, ---
	[User_Role] [varchar](7) NOT NULL,
CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED ([User_Name] ASC));

CREATE TABLE [dbo].[Internal_Staff](
	[Staff_Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Name] [varchar] (50) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Admin_Flag] [char](1) NOT NULL,
 CONSTRAINT [PK_Internal_Staff] PRIMARY KEY CLUSTERED ([Staff_Id] ASC));

CREATE TABLE [dbo].[Employer](
	[Employer_Id] [int] IDENTITY(1,1) NOT NULL,
	[Employer_Name] [varchar](50) NOT NULL,
	[Number_Of_Teams] [int] NOT NULL,
	[Summary] [text] NULL,
 CONSTRAINT [PK_Employer] PRIMARY KEY CLUSTERED ([Employer_Id] ASC));

CREATE TABLE [dbo].[Interview_Schedule](
	[Student_Id] [int] NOT NULL,
	[Employer_Id] [int] NOT NULL,
	[Team_Id] [int] NOT NULL,
	[Time_Slot_Rank] [int] NOT NULL,
	[Event_Date] [date] NOT NULL,

 CONSTRAINT [PK_Interview_Schedule] PRIMARY KEY CLUSTERED 
 (
	[Student_Id] ASC,
	[Employer_Id] ASC,
	[Team_Id] ASC
));

CREATE TABLE [dbo].[Employer_Team](
	[Employer_Id] [int] NOT NULL,
	[Team_Id] [int] NOT NULL,
	[Event_Date] [date] NOT NULL,
	[Language_Id] [int] NOT NULL,
	[Start_Time] [datetime] NOT NULL,
	[End_Time] [datetime] NOT NULL,
	[Assigned_Room] [varchar](50) NULL,
 CONSTRAINT [PK_Employer_Team] PRIMARY KEY CLUSTERED 
(
	[Team_Id] ASC,
	[Employer_Id] ASC
));

CREATE TABLE [dbo].[Wait_Listed](
	[Student_Id] [int] NOT NULL,
	[Employer_Id] [int] NOT NULL,
	[Event_Date] [date] NOT NULL,
	[Resolved] [varchar](1) NOT NULL,
 CONSTRAINT [PK_Wait_Listed] PRIMARY KEY CLUSTERED 
(
	[Student_Id] ASC,
	[Employer_Id] ASC
));

CREATE TABLE [dbo].[Event](
	[Event_Date] [date] NOT NULL,
	[Start_Time] [datetime] NOT NULL,
	[End_Time] [datetime] NOT NULL,
	[Lunch_Start] [datetime] NULL,
	[Lunch_End] [datetime] NULL,
	[First_Break_Start] [datetime] NULL,
	[First_Break_End] [datetime] NULL,
	[Second_Break_Start] [datetime] NULL,
	[Second_Break_End] [datetime] NULL,
	[Interview_Length] [int] NOT NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Event_Date] ASC
));

CREATE TABLE [dbo].[Student_Choices](
	[Student_Id] [int] NOT NULL,
	[Employer_Id] [int] NOT NULL,
	[Employer_Rank] [int] NULL,
	[Event_Date] [date] NOT NULL,
 CONSTRAINT [PK_Student_Choices] PRIMARY KEY CLUSTERED 
(
	[Student_Id] ASC,
	[Employer_Id] ASC
));

CREATE TABLE [dbo].[Student](
	[Student_Id] [int] IDENTITY(1,1) NOT NULL,
	[Student_Name] [varchar](50) NOT NULL,
	[Language_Id] [int] NOT NULL,
	[User_Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Student_Id] ASC
));


CREATE TABLE [dbo].[Language](
	[Language_Id] [int] NOT NULL,
	[Language] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[Language_Id] ASC
));


CREATE TABLE [dbo].[Time_Slot_Rank](
	[Event_Date] [date] NOT NULL,
	[Start_Time] [datetime] NOT NULL,
	[End_Time] [datetime] NOT NULL,
	[Time_Slot_Rank] [int] NULL,
 CONSTRAINT [PK_Time_Slot_Rank] PRIMARY KEY CLUSTERED 
(
	[Event_Date] ASC,
	[Start_Time] ASC
));


IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interview_Schedule_Employer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interview_Schedule]'))
ALTER TABLE [dbo].[Interview_Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Interview_Schedule_Employer] FOREIGN KEY([Employer_Id])
REFERENCES [dbo].[Employer] ([Employer_Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interview_Schedule_Employer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interview_Schedule]'))
ALTER TABLE [dbo].[Interview_Schedule] CHECK CONSTRAINT [FK_Interview_Schedule_Employer]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interview_Schedule_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interview_Schedule]'))
ALTER TABLE [dbo].[Interview_Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Interview_Schedule_Event] FOREIGN KEY([Event_Date])
REFERENCES [dbo].[Event] ([Event_Date])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interview_Schedule_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interview_Schedule]'))
ALTER TABLE [dbo].[Interview_Schedule] CHECK CONSTRAINT [FK_Interview_Schedule_Event]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interview_Schedule_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interview_Schedule]'))
ALTER TABLE [dbo].[Interview_Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Interview_Schedule_Student] FOREIGN KEY([Student_Id])
REFERENCES [dbo].[Student] ([Student_Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Interview_Schedule_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Interview_Schedule]'))
ALTER TABLE [dbo].[Interview_Schedule] CHECK CONSTRAINT [FK_Interview_Schedule_Student]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employer_Team_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employer_Team]'))
ALTER TABLE [dbo].[Employer_Team]  WITH CHECK ADD  CONSTRAINT [FK_Employer_Team_Event] FOREIGN KEY([Event_Date])
REFERENCES [dbo].[Event] ([Event_Date])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employer_Team_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employer_Team]'))
ALTER TABLE [dbo].[Employer_Team] CHECK CONSTRAINT [FK_Employer_Team_Event]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employer_Team_Language]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employer_Team]'))
ALTER TABLE [dbo].[Employer_Team]  WITH CHECK ADD  CONSTRAINT [FK_Employer_Team_Language] FOREIGN KEY([Language_Id])
REFERENCES [dbo].[Language] ([Language_Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employer_Team_Language]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employer_Team]'))
ALTER TABLE [dbo].[Employer_Team] CHECK CONSTRAINT [FK_Employer_Team_Language]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Wait_Listed_Employer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Wait_Listed]'))
ALTER TABLE [dbo].[Wait_Listed]  WITH CHECK ADD  CONSTRAINT [FK_Wait_Listed_Employer] FOREIGN KEY([Employer_Id])
REFERENCES [dbo].[Employer] ([Employer_Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Wait_Listed_Employer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Wait_Listed]'))
ALTER TABLE [dbo].[Wait_Listed] CHECK CONSTRAINT [FK_Wait_Listed_Employer]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Wait_Listed_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Wait_Listed]'))
ALTER TABLE [dbo].[Wait_Listed]  WITH CHECK ADD  CONSTRAINT [FK_Wait_Listed_Event] FOREIGN KEY([Event_Date])
REFERENCES [dbo].[Event] ([Event_Date])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Wait_Listed_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Wait_Listed]'))
ALTER TABLE [dbo].[Wait_Listed] CHECK CONSTRAINT [FK_Wait_Listed_Event]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Wait_Listed_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Wait_Listed]'))
ALTER TABLE [dbo].[Wait_Listed]  WITH CHECK ADD  CONSTRAINT [FK_Wait_Listed_Student] FOREIGN KEY([Student_Id])
REFERENCES [dbo].[Student] ([Student_Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Wait_Listed_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Wait_Listed]'))
ALTER TABLE [dbo].[Wait_Listed] CHECK CONSTRAINT [FK_Wait_Listed_Student]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Choices_Employer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Choices]'))
ALTER TABLE [dbo].[Student_Choices]  WITH CHECK ADD  CONSTRAINT [FK_Student_Choices_Employer] FOREIGN KEY([Employer_Id])
REFERENCES [dbo].[Employer] ([Employer_Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Choices_Employer]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Choices]'))
ALTER TABLE [dbo].[Student_Choices] CHECK CONSTRAINT [FK_Student_Choices_Employer]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Choices_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Choices]'))
ALTER TABLE [dbo].[Student_Choices]  WITH CHECK ADD  CONSTRAINT [FK_Student_Choices_Event] FOREIGN KEY([Event_Date])
REFERENCES [dbo].[Event] ([Event_Date])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Choices_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Choices]'))
ALTER TABLE [dbo].[Student_Choices] CHECK CONSTRAINT [FK_Student_Choices_Event]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Choices_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Choices]'))
ALTER TABLE [dbo].[Student_Choices]  WITH CHECK ADD  CONSTRAINT [FK_Student_Choices_Student] FOREIGN KEY([Student_Id])
REFERENCES [dbo].[Student] ([Student_Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Choices_Student]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student_Choices]'))
ALTER TABLE [dbo].[Student_Choices] CHECK CONSTRAINT [FK_Student_Choices_Student]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Language]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student]'))
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Language] FOREIGN KEY([Language_Id])
REFERENCES [dbo].[Language] ([Language_Id])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Language]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student]'))
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Language]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Time_Slot_Rank_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Time_Slot_Rank]'))
ALTER TABLE [dbo].[Time_Slot_Rank]  WITH CHECK ADD  CONSTRAINT [FK_Time_Slot_Rank_Event] FOREIGN KEY([Event_Date])
REFERENCES [dbo].[Event] ([Event_Date])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Time_Slot_Rank_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Time_Slot_Rank]'))
ALTER TABLE [dbo].[Time_Slot_Rank] CHECK CONSTRAINT [FK_Time_Slot_Rank_Event]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Time_Slot_Rank_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Time_Slot_Rank]'))
ALTER TABLE [dbo].[Time_Slot_Rank]  WITH CHECK ADD  CONSTRAINT [FK_Time_Slot_Rank_Event] FOREIGN KEY([Event_Date])
REFERENCES [dbo].[Event] ([Event_Date])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Time_Slot_Rank_Event]') AND parent_object_id = OBJECT_ID(N'[dbo].[Time_Slot_Rank]'))
ALTER TABLE [dbo].[Time_Slot_Rank] CHECK CONSTRAINT [FK_Time_Slot_Rank_Event]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Internal_Staff_Login]') AND parent_object_id = OBJECT_ID(N'[dbo].[Internal_Staff]'))
ALTER TABLE [dbo].[Internal_Staff]  WITH CHECK ADD CONSTRAINT [FK_Internal_Staff_Login] FOREIGN KEY([User_Name])
REFERENCES [dbo].[Login] ([User_Name])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Internal_Staff_Login]') AND parent_object_id = OBJECT_ID(N'[dbo].[Internal_Staff]'))
ALTER TABLE [dbo].[Internal_Staff] CHECK CONSTRAINT [FK_Internal_Staff_Login]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Login]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student]'))
ALTER TABLE [dbo].[Student]  WITH CHECK ADD CONSTRAINT [FK_Student_Login] FOREIGN KEY([User_Name])
REFERENCES [dbo].[Login] ([User_Name])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Student_Login]') AND parent_object_id = OBJECT_ID(N'[dbo].[Student]'))
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Login]
GO

IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[Admin_Yes_or_No]') AND parent_object_id = OBJECT_ID(N'[dbo].[Internal_Staff]'))
ALTER TABLE [dbo].[Internal_Staff]  WITH CHECK ADD  CONSTRAINT [Admin_Yes_or_No] CHECK  (([Admin_Flag]='Y' OR [Admin_Flag]='N'))
GO

IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[Admin_Yes_or_No]') AND parent_object_id = OBJECT_ID(N'[dbo].[Internal_Staff]'))
ALTER TABLE [dbo].[Internal_Staff] CHECK CONSTRAINT [Admin_Yes_or_No]
GO

IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[Role_is_Appropriate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Login]'))
ALTER TABLE [dbo].[Login]  WITH CHECK ADD  CONSTRAINT [Role_is_Appropriate] CHECK  (([User_Role]='Student' OR [User_Role]='Admin' OR [User_Role]='Staff'))
GO

IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[Role_is_Appropriate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Login]'))
ALTER TABLE [dbo].[Login] CHECK CONSTRAINT [Role_is_Appropriate]
GO

Commit;