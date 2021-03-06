USE [TeamADatabase]
GO
/****** Object:  View [dbo].[vwDeadLineStatus]    Script Date: 01-Apr-16 10:03:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create view [dbo].[vwDeadLineStatus]
as
SELECT ROW_NUMBER()  over 
( order by  t.UploadId  desc) as RowId,t.Uploadid from 
 (Select k.uploadid from
(select  StudentUserID,Uploadid,HomeworkId,DateDiff(MINUTE,p.UploadDate,p.Deadline)as DeadLineLimit,[Status]
 from (Select Homework.DeadLine,Uploadid,UploadDate,StudentUserID,studenttohomework.HomeworkID as SHomeworkID,Homework.HomeworkID,[Status]
  from StudentToHomework 
join Homework on Homework.HomeworkID=StudentToHomework.HomeworkId) p 
where HomeworkId=SHomeworkID and [status]!= 'Rejected' and [status]!= 'Accepted') k where DeadLineLimit < 0) t

GO
/****** Object:  View [dbo].[vwStudentToTeacher]    Script Date: 01-Apr-16 10:03:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[vwStudentToTeacher]
as
 Select Username as Student,Email as StudentEmail ,teacherId from userprofile 
join StudentsToTeachers
on  StudentsToTeachers.StudentId=Userprofile.Id 
GO
