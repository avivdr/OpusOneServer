Use master
Create Database OpusOneDB
GO

Use OpusOneDB;
GO


Create Table Users (
	ID int IDENTITY PRIMARY KEY,
	Username nvarchar(100) NOT NULL,
	Pwsd nvarchar(100) NOT NULL,
	Email nvarchar(100) NOT NULL, 
	CONSTRAINT UC_Username UNIQUE(Username)
)
GO

Create Table Works_Users(
	ID int IDENTITY PRIMARY KEY,
	UserID int NOT NULL FOREIGN KEY REFERENCES Users(ID),
	WorkID int NOT NULL,
)
GO

Create Table Posts(
	ID int IDENTITY PRIMARY KEY,
	CreatorID int NOT NULL FOREIGN KEY REFERENCES Users(ID),
	Title nvarchar(100) NOT NULL,
	Content nvarchar(1000) ,
	FilePath nvarchar(255),
	UploadDateTime datetime NOT NULL,
	Work int,
	Composer int,
)
GO

Create Table Comments(
	ID int IDENTITY PRIMARY KEY,
	PostID int NOT NULL FOREIGN KEY REFERENCES Posts(ID),
	CreatorID int NOT NULL FOREIGN KEY REFERENCES Users(ID),
	Content nvarchar(1000) NOT NULL,
	UploadDateTime datetime NOT NULL,
)
GO

Create Table Forums(
	ID int IDENTITY PRIMARY KEY,
	Title nvarchar(255) NOT NULL,
	ForumDescription nvarchar(255) NOT NULL,
	CreatorID int NOT NULL FOREIGN KEY REFERENCES Users(ID),
	CreatedDateTime datetime NOT NULL,	
	Work int,
	Composer int,
)
GO

Create Table ForumComment(
	ID int IDENTITY PRIMARY KEY,
	ForumID int NOT NULL FOREIGN KEY REFERENCES Forums(ID),
	CreatorID int NOT NULL FOREIGN KEY REFERENCES Users(ID),
	Content nvarchar(1000) NOT NULL,
	UploadDateTime datetime NOT NULL,
)
GO

INSERT INTO Users Values('kiki123', '12345', 'kiki@gmail.com');

GO

--scaffold-dbcontext "Server=localhost\sqlexpress;Database=OpusOneDB;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models –force