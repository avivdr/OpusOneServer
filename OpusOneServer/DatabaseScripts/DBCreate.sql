Use master
Create Database OpusOneDB
GO

Use OpusOneDB;
GO

Create Table Users (
	ID int Identity PRIMARY KEY,
	Username nvarchar(100) NOT NULL,
	Pwsd nvarchar(100) NOT NULL,
	Email nvarchar(100) NOT NULL,
	CONSTRAINT UC_Username UNIQUE(Username)
);

GO


Create Table Posts(
	ID int Identity PRIMARY KEY,
	CreatorID int NOT NULL FOREIGN KEY REFERENCES Users(ID),
	Title nvarchar(100) NOT NULL,
	Content nvarchar(1000) ,
	FilePath nvarchar(250),
	UploadDateTime datetime NOT NULL,
	Work int,
	Composer int,
);

GO

Create Table Comments(
	ID int Identity PRIMARY KEY,
	PostID int NOT NULL FOREIGN KEY REFERENCES Posts(ID),
	CreatorID int NOT NULL FOREIGN KEY REFERENCES Users(ID),
	Content nvarchar(1000) NOT NULL,
	UploadDateTime datetime NOT NULL,
);

GO

Create Table Forum(
	ID int Identity PRIMARY KEY,
	Title nvarchar(255) NOT NULL,
	CreatorID int NOT NULL FOREIGN KEY REFERENCES Users(ID),
	CreatedDateTime datetime NOT NULL,
)

INSERT INTO Users Values('kiki123', '12345', 'kiki@gmail.com')

GO