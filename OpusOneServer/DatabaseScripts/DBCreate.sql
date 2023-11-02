Use master
Create Database OpusOneDB
GO

Use OpusOneDB
GO

Create Table Users (
	ID int Identity primary key,
	Username nvarchar(100) not null,
	Pwsd nvarchar(100) not null,
	Email nvarchar(100) not null,
	CONSTRAINT UC_Username UNIQUE(Username)
)

GO

INSERT INTO Users Values('kiki123', '12345', 'kiki@gmail.com')

GO