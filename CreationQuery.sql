create table UserRole
	(
		RoleID int UNIQUE not null,
		RoleName nvarchar(50) not null,
		constraint PK_Role primary key(RoleID)
	)

create table UserProfile(
		ID int identity(1,1),
		Username nvarchar(50) UNIQUE not null,
		[Password] nvarchar(128) not null,
		Email nvarchar(128) not null,
		RoleID int,
		constraint PK_UserProfile primary key(ID),
		constraint FK_UserProfile_Role foreign key(RoleID) references UserRole(RoleID)
	)

create table StudentsToTeachers
	(
		StudentID int,
		TeacherID int,
		constraint PK_StudentsToTeachers primary key(StudentID),
		constraint FK_StudentsToTeachers_UserProfileStudent foreign key(StudentID) references UserProfile(ID),
		constraint FK_StudentsToTeachers_UserProfileTeacher foreign key(TeacherID) references UserProfile(ID)
	)

create table Homework
	(
		HomeworkID int,
		TeacherUserID int,
		[Description] nvarchar(511),
		Deadline datetime,
		constraint PK_Homework primary key(HomeworkID),
		constraint FK_Homework_Teacher foreign key(TeacherUserID) references UserProfile(ID)
	)

create table StudentToHomework
	(
		StudentUserID int,
		HomeworkID int,
		UploadID int identity(1,1),
		UploadDate datetime,
		Comment nvarchar(255),
		[Status] nvarchar(12),
		Grade int,
		constraint PK_StudentToHomework primary key(UploadID),
		constraint FK_StudentToHomework_Student foreign key(StudentUserID) references UserProfile(ID),
		constraint FK_StudentToHomework_Homework foreign key(HomeworkID) references Homework(HomeworkID)
	)

create table HomeworkFile
	(
		UploadID int,
		NumeFisier nvarchar(50),
		constraint PK_HomeworkFiles primary key(UploadID),
		constraint FK_HomeworkFiles_StudentToHomework foreign key(UploadID) references StudentToHomework(UploadID)
	)