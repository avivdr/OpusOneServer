Use master

Create Database OpusOneDB
GO

Use OpusOneDB;
GO

Create Table Users (
	ID int IDENTITY PRIMARY KEY,
	Username nvarchar(100) NOT NULL,
	[Password] nvarchar(100) NOT NULL,
	Email nvarchar(100) NOT NULL,
	CONSTRAINT UC_Username UNIQUE(Username)
)

Create Table Posts(
	ID int IDENTITY PRIMARY KEY,
	CreatorID int NOT NULL,
	Title nvarchar(100) NOT NULL,
	Content nvarchar(1000) ,
	UploadDateTime datetime NOT NULL,
	WorkId int,
	ComposerId int
)

Create Table Comments(
	ID int IDENTITY PRIMARY KEY,
	PostID int NOT NULL,
	CreatorID int NOT NULL,
	Content nvarchar(1000) NOT NULL,
	UploadDateTime datetime NOT NULL,
)

Create Table Forums(
	ID int IDENTITY PRIMARY KEY,
	Title nvarchar(255) NOT NULL,
	ForumDescription nvarchar(255) NOT NULL,
	CreatorID int NOT NULL,
	CreatedDateTime datetime NOT NULL,	
	WorkId int,
	ComposerId int,
)

Create Table ForumComments(
	ID int IDENTITY PRIMARY KEY,
	ForumID int NOT NULL,
	CreatorID int NOT NULL,
	Content nvarchar(1000) NOT NULL,
	UploadDateTime datetime NOT NULL,
)

Create Table Composers(
	Id int PRIMARY KEY NOT NULL,
	[Name] varchar(255) NOT NULL,
	Complete_Name varchar(255) NOT NULL,
	CONSTRAINT UC_Composer_Id UNIQUE (Id)
)

Create Table Works(
	Id int PRIMARY KEY NOT NULL,
	Composer_Id int NOT NULL,
	Title nvarchar(255) NOT NULL,
	Genre tinyint NOT NULL,	
	CONSTRAINT UC_Work_Id UNIQUE (Id)
)

Create Table Works_Users(
	ID int IDENTITY PRIMARY KEY,
	UserID int NOT NULL,
	WorkId int NOT NULL,
)

GO


Alter Table Posts Add CONSTRAINT FK_Posts_CreatorID FOREIGN KEY (CreatorID) REFERENCES Users(ID)
Alter Table Posts Add CONSTRAINT FK_Posts_WorkId FOREIGN KEY (WorkId) REFERENCES Works(Id)
Alter Table Posts Add CONSTRAINT FK_Posts_ComposerId FOREIGN KEY (ComposerId) REFERENCES Composers(Id)

Alter Table Comments Add CONSTRAINT FK_Comments_PostID FOREIGN KEY (PostID) REFERENCES Posts(ID)
Alter Table Comments Add CONSTRAINT FK_Comments_CreatorID FOREIGN KEY (CreatorID) REFERENCES Users(ID)

Alter Table Forums Add CONSTRAINT FK_Forums_CreatorID FOREIGN KEY (CreatorID) REFERENCES Users(ID)
Alter Table Forums Add CONSTRAINT FK_Forums_WorkId FOREIGN KEY (WorkId) REFERENCES Works(Id)
Alter Table Forums Add CONSTRAINT FK_FOrums_ComposerId FOREIGN KEY (ComposerId) REFERENCES Composers(Id)

Alter Table ForumComments Add CONSTRAINT FK_ForumComments_ForumID FOREIGN KEY (ForumID) REFERENCES Forums(ID)
Alter Table ForumComments Add CONSTRAINT Fk_ForumComments_CreatorID FOREIGN KEY (CreatorID) REFERENCES Users(ID)

Alter Table Works Add CONSTRAINT FK_Works_Composer_Id FOREIGN KEY (Composer_Id) REFERENCES Composers(Id) 

Alter Table Works_Users Add CONSTRAINT Fk_Works_Users_WorkId FOREIGN KEY (WorkId) REFERENCES Works(Id)
Alter Table Works_Users Add CONSTRAINT FK_Works_Users_UserID FOREIGN KEY (UserID) REFERENCES Users(ID)

GO

INSERT INTO Users Values('kiki123', '12345', 'kiki@gmail.com');

GO

--scaffold-dbcontext "Server=localhost\sqlexpress;Database=OpusOneDB;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models –force