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
	FileExtension varchar(55),
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

--Composers
INSERT INTO [dbo].[Composers] ([Id], [Name], [Complete_Name]) VALUES (80, N'Brahms', N'Johannes Brahms')
INSERT INTO [dbo].[Composers] ([Id], [Name], [Complete_Name]) VALUES (87, N'Bach', N'Johann Sebastian Bach')
INSERT INTO [dbo].[Composers] ([Id], [Name], [Complete_Name]) VALUES (145, N'Beethoven', N'Ludwig van Beethoven')
INSERT INTO [dbo].[Composers] ([Id], [Name], [Complete_Name]) VALUES (196, N'Mozart', N'Wolfgang Amadeus Mozart')

--Works
INSERT INTO [dbo].[Works] ([Id], [Composer_Id], [Title]) VALUES (9334, 87, N'Cantata no. 140, "Wachet auf, ruft uns die Stimme", BWV.140')

--Users
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([ID], [Username], [Password], [Email]) VALUES (1, N'kiki123', N'12345', N'kiki@gmail.com')
INSERT INTO [dbo].[Users] ([ID], [Username], [Password], [Email]) VALUES (2, N'kaka321', N'6969', N'kaka@kaka')
INSERT INTO [dbo].[Users] ([ID], [Username], [Password], [Email]) VALUES (3, N'vulu', N'31415', N'vulu@gmail')
INSERT INTO [dbo].[Users] ([ID], [Username], [Password], [Email]) VALUES (4, N'darkgoomer', N'mevichdor', N'darkgoomer@gmail.com')
INSERT INTO [dbo].[Users] ([ID], [Username], [Password], [Email]) VALUES (5, N'IDDB', N'Gay123', N'idan.belfer.2006@gmail.com')
SET IDENTITY_INSERT [dbo].[Users] OFF


--Posts
SET IDENTITY_INSERT [dbo].[Posts] ON
INSERT INTO [dbo].[Posts] ([ID], [CreatorID], [Title], [Content], [UploadDateTime], [FileExtension], [WorkId], [ComposerId]) VALUES (14, 1, N'brahms post', N'brahms is c composers', N'2023-12-16 15:56:19', N'.jpg', NULL, 80)
INSERT INTO [dbo].[Posts] ([ID], [CreatorID], [Title], [Content], [UploadDateTime], [FileExtension], [WorkId], [ComposerId]) VALUES (16, 1, N'pe n', N'peni', N'2024-01-04 11:57:22', N'.png', 9334, NULL)
INSERT INTO [dbo].[Posts] ([ID], [CreatorID], [Title], [Content], [UploadDateTime], [FileExtension], [WorkId], [ComposerId]) VALUES (18, 4, N'masterpeice', N'the best post ever', N'2024-01-08 11:29:06', N'.png', NULL, NULL)
INSERT INTO [dbo].[Posts] ([ID], [CreatorID], [Title], [Content], [UploadDateTime], [FileExtension], [WorkId], [ComposerId]) VALUES (19, 5, N'who''s in paris', N'niggasssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssss', N'2024-03-27 13:17:13', N'.png', NULL, NULL)
INSERT INTO [dbo].[Posts] ([ID], [CreatorID], [Title], [Content], [UploadDateTime], [FileExtension], [WorkId], [ComposerId]) VALUES (20, 1, N'k,jh', NULL, N'2024-04-01 10:06:11', N'.mp4', NULL, NULL)
INSERT INTO [dbo].[Posts] ([ID], [CreatorID], [Title], [Content], [UploadDateTime], [FileExtension], [WorkId], [ComposerId]) VALUES (21, 1, N'ghjghj', NULL, N'2024-04-01 10:18:27', N'.mp4', NULL, NULL)
INSERT INTO [dbo].[Posts] ([ID], [CreatorID], [Title], [Content], [UploadDateTime], [FileExtension], [WorkId], [ComposerId]) VALUES (22, 1, N'ljhlkj', NULL, N'2024-04-01 10:22:23', N'.mp3', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Posts] OFF

GO

--scaffold-dbcontext "Server=localhost\sqlexpress;Database=OpusOneDB;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models –force