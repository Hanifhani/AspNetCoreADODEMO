USE [DaviGold]
GO
/****** Object:  UserDefinedFunction [dbo].[CSVToTable]    Script Date: 9/12/2022 2:42:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[CSVToTable]
(
    @in_string VARCHAR(MAX),
    @delimeter VARCHAR(1)
)
RETURNS @list TABLE(tuple VARCHAR(100))
AS
BEGIN
        WHILE LEN(@in_string) > 0
        BEGIN
            INSERT INTO @list(tuple)
            SELECT left(@in_string, charindex(@delimeter, @in_string+',') -1) as tuple
    
            SET @in_string = stuff(@in_string, 1, charindex(@delimeter, @in_string + @delimeter), '')
        end
    RETURN 
END
GO
/****** Object:  Table [dbo].[Company]    Script Date: 9/12/2022 2:42:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Country] [varchar](100) NULL,
	[ZipCode] [varchar](16) NULL,
	[City] [varchar](100) NULL,
	[State] [varchar](100) NULL,
	[CompanyMainLine] [varchar](100) NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NULL,
	[Name] [varchar](100) NULL,
	[Type] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 9/12/2022 2:42:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Title] [varchar](10) NULL,
	[MiddleName] [varchar](100) NULL,
	[Notes] [text] NULL,
	[CompanyId] [int] NULL,
	[Position] [varchar](50) NULL,
	[Department] [varchar](50) NULL,
	[BusinessEmail] [varchar](100) NULL,
	[DirectLine] [varchar](50) NULL,
	[PrimaryMobile] [varchar](50) NULL,
	[SecondaryMobile] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[Address] [varchar](100) NULL,
	[ZIPCode] [varchar](16) NULL,
	[City] [varchar](100) NULL,
	[State] [varchar](100) NULL,
	[PersonalMobile1] [varchar](100) NULL,
	[PersonalMobile2] [varchar](100) NULL,
	[HomePhone] [varchar](100) NULL,
	[PersonalEmail] [varchar](100) NULL,
	[Linkedin] [varchar](100) NULL,
	[DateOfBirth] [date] NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactTags]    Script Date: 9/12/2022 2:42:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactTags](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactId] [int] NULL,
	[Tag] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactTypes]    Script Date: 9/12/2022 2:42:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactId] [int] NULL,
	[Type] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeamKnowledge]    Script Date: 9/12/2022 2:42:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeamKnowledge](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContactId] [int] NULL,
	[UserId] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 9/12/2022 2:42:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Login] [nvarchar](40) NOT NULL,
	[PasswordHash] [binary](64) NOT NULL,
	[Salt] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](40) NULL,
	[LastName] [nvarchar](40) NULL,
 CONSTRAINT [PK_User_UserID] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([Id], [Country], [ZipCode], [City], [State], [CompanyMainLine], [CreatedDate], [ModifiedDate], [Name], [Type]) VALUES (1, N'UK', N'1222', N'London', N'Lonfon', N'1222', CAST(N'2022-09-11T21:11:15.9966667' AS DateTime2), NULL, N'Test Gold', 0)
INSERT [dbo].[Company] ([Id], [Country], [ZipCode], [City], [State], [CompanyMainLine], [CreatedDate], [ModifiedDate], [Name], [Type]) VALUES (2, N'United Kingdom', N'1222', N'London', N'Lonfon', N'1222', CAST(N'2022-09-11T21:24:20.5066667' AS DateTime2), NULL, N'DAVITEST1708', 0)
SET IDENTITY_INSERT [dbo].[Company] OFF
SET IDENTITY_INSERT [dbo].[Contacts] ON 

INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Title], [MiddleName], [Notes], [CompanyId], [Position], [Department], [BusinessEmail], [DirectLine], [PrimaryMobile], [SecondaryMobile], [Country], [Address], [ZIPCode], [City], [State], [PersonalMobile1], [PersonalMobile2], [HomePhone], [PersonalEmail], [Linkedin], [DateOfBirth], [CreatedDate], [ModifiedDate]) VALUES (4, N'Benjamin ', N'Storn', N'MR', N'string', N'string', 1, N'string', N'string', N'string', N'string', N'string', N'string', N'UK', N'string', N'string', N'London', N'London', N'1234443', N'string', N'string', N'test@gmail.com', N'string', CAST(N'2022-09-11' AS Date), CAST(N'2022-09-11T21:17:37.7866667' AS DateTime2), NULL)
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Title], [MiddleName], [Notes], [CompanyId], [Position], [Department], [BusinessEmail], [DirectLine], [PrimaryMobile], [SecondaryMobile], [Country], [Address], [ZIPCode], [City], [State], [PersonalMobile1], [PersonalMobile2], [HomePhone], [PersonalEmail], [Linkedin], [DateOfBirth], [CreatedDate], [ModifiedDate]) VALUES (5, N'John ', N'Doe', N'MR', N'string', N'string', 2, N'string', N'string', N'string', N'string', N'string', N'string', N'UK', N'string', N'string', N'London', N'London', N'1234443', N'string', N'string', N'test@gmail.com', N'string', CAST(N'2022-09-11' AS Date), CAST(N'2022-09-11T21:24:55.2700000' AS DateTime2), NULL)
INSERT [dbo].[Contacts] ([Id], [FirstName], [LastName], [Title], [MiddleName], [Notes], [CompanyId], [Position], [Department], [BusinessEmail], [DirectLine], [PrimaryMobile], [SecondaryMobile], [Country], [Address], [ZIPCode], [City], [State], [PersonalMobile1], [PersonalMobile2], [HomePhone], [PersonalEmail], [Linkedin], [DateOfBirth], [CreatedDate], [ModifiedDate]) VALUES (6, N'John ', N'Doe', N'MR', N'string', N'string', 0, N'string', N'string', N'string', N'string', N'string', N'string', N'US', N'string', N'string', N'London', N'London', N'1234443', N'string', N'string', N'test@gmail.com', N'string', CAST(N'2022-09-11' AS Date), CAST(N'2022-09-11T21:25:20.8733333' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[Contacts] OFF
SET IDENTITY_INSERT [dbo].[ContactTags] ON 

INSERT [dbo].[ContactTags] ([Id], [ContactId], [Tag]) VALUES (5, 4, N'abc')
INSERT [dbo].[ContactTags] ([Id], [ContactId], [Tag]) VALUES (6, 4, N'123')
INSERT [dbo].[ContactTags] ([Id], [ContactId], [Tag]) VALUES (7, 5, N'abc')
INSERT [dbo].[ContactTags] ([Id], [ContactId], [Tag]) VALUES (8, 5, N'123')
INSERT [dbo].[ContactTags] ([Id], [ContactId], [Tag]) VALUES (9, 6, N'abc')
INSERT [dbo].[ContactTags] ([Id], [ContactId], [Tag]) VALUES (10, 6, N'123')
SET IDENTITY_INSERT [dbo].[ContactTags] OFF
SET IDENTITY_INSERT [dbo].[ContactTypes] ON 

INSERT [dbo].[ContactTypes] ([Id], [ContactId], [Type]) VALUES (5, 4, N'string')
INSERT [dbo].[ContactTypes] ([Id], [ContactId], [Type]) VALUES (6, 5, N'1')
INSERT [dbo].[ContactTypes] ([Id], [ContactId], [Type]) VALUES (7, 6, N'1')
SET IDENTITY_INSERT [dbo].[ContactTypes] OFF
SET IDENTITY_INSERT [dbo].[TeamKnowledge] ON 

INSERT [dbo].[TeamKnowledge] ([Id], [ContactId], [UserId]) VALUES (3, 4, N'3fa85f64-5717-4562-b3fc-2c963f66afa6')
INSERT [dbo].[TeamKnowledge] ([Id], [ContactId], [UserId]) VALUES (4, 5, N'3fa85f64-5717-4562-b3fc-2c963f66afa6')
INSERT [dbo].[TeamKnowledge] ([Id], [ContactId], [UserId]) VALUES (5, 6, N'3fa85f64-5717-4562-b3fc-2c963f66afa6')
SET IDENTITY_INSERT [dbo].[TeamKnowledge] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [Email], [PasswordHash], [Salt], [FirstName], [LastName]) VALUES (1, N'test@davigold.com', 0xF7FBB426510F74F1AA91F15748D2E909D607C0F42AA8467EEFBBEDB0C5A94C7537976259D45F657AFB54E4F113E0F20B45DC1E0CD8E2D875DA8D1028D7A54161, N'305f6229-9603-4e0b-a335-3f28e2777fa9', N'Davi', N'Gold')
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Company] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Contacts] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
/****** Object:  StoredProcedure [dbo].[uspAddUser]    Script Date: 9/12/2022 2:42:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspAddUser]
    @pLogin NVARCHAR(50), 
    @pPassword NVARCHAR(50),
    @pFirstName NVARCHAR(40) = NULL, 
    @pLastName NVARCHAR(40) = NULL,
    @responseMessage NVARCHAR(250) OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY

        INSERT INTO dbo.[User] (Email, PasswordHash, Salt, FirstName, LastName)
        VALUES(@pLogin, HASHBYTES('SHA2_512', @pPassword+CAST(@salt AS NVARCHAR(36))), @salt, @pFirstName, @pLastName)

       SET @responseMessage='Success'

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END
GO
