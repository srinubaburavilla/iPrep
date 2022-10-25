Select * from IprepMapper
Select * from SubjectMaster
Select * from QuestionMaster
Select * from AnswerMaster

Select s.[Subject], q.Question, a.Answer
from dbo.IprepMapper m
INNER JOIN SubjectMaster s
ON m.SubjectId = s.Id
INNER JOIN [QuestionMaster] q
ON m.QuestionId = q.Id
INNER JOIN AnswerMaster a
ON m.AnswerId = a.Id
where m.Id =2

--select * from SubjectMaster


---- Reset Data
USE [IPrep]
GO


DELETE FROM IprepMapper
DELETE FROM AnswerMaster
DELETE FROM QuestionMaster
DELETE FROM SubjectMaster
DBCC CHECKIDENT ('IPrep.dbo.IprepMapper', RESEED, 0)
DBCC CHECKIDENT ('IPrep.dbo.AnswerMaster', RESEED, 0)
DBCC CHECKIDENT ('IPrep.dbo.QuestionMaster', RESEED, 0)
DBCC CHECKIDENT ('IPrep.dbo.SubjectMaster', RESEED, 0)

INSERT INTO [dbo].[SubjectMaster] ([Subject]) VALUES ('C#.NET')
INSERT INTO [dbo].[SubjectMaster] ([Subject]) VALUES ('JavaScript')
INSERT INTO [dbo].[SubjectMaster] ([Subject]) VALUES ('Entity Framework')
INSERT INTO [dbo].[SubjectMaster] ([Subject]) VALUES ('ASP.NET MVC')
INSERT INTO [dbo].[SubjectMaster] ([Subject]) VALUES ('SQL Server')
INSERT INTO [dbo].[SubjectMaster] ([Subject]) VALUES ('Web API')
INSERT INTO [dbo].[SubjectMaster] ([Subject]) VALUES ('.NET Core')

--Select * from [dbo].[SubjectMaster]


GO

