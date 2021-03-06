USE [master]
GO
/****** Object:  Database [TwoDriveDB]    Script Date: 21/11/2019 10:58:11 ******/
CREATE DATABASE [TwoDriveDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TwoDriveDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\TwoDriveDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TwoDriveDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\TwoDriveDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [TwoDriveDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TwoDriveDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TwoDriveDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TwoDriveDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TwoDriveDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TwoDriveDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TwoDriveDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TwoDriveDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [TwoDriveDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TwoDriveDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TwoDriveDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TwoDriveDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TwoDriveDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TwoDriveDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TwoDriveDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TwoDriveDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TwoDriveDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TwoDriveDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TwoDriveDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TwoDriveDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TwoDriveDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TwoDriveDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TwoDriveDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TwoDriveDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TwoDriveDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TwoDriveDB] SET  MULTI_USER 
GO
ALTER DATABASE [TwoDriveDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TwoDriveDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TwoDriveDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TwoDriveDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TwoDriveDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TwoDriveDB] SET QUERY_STORE = OFF
GO
USE [TwoDriveDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 21/11/2019 10:58:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 21/11/2019 10:58:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OwnerId] [bigint] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ParentId] [bigint] NULL,
	[Content] [nvarchar](max) NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[LastModifiedDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Folders]    Script Date: 21/11/2019 10:58:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Folders](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ParentId] [bigint] NULL,
	[OwnerId] [bigint] NOT NULL,
 CONSTRAINT [PK_Folders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 21/11/2019 10:58:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[Date] [datetime2](7) NULL,
	[Count] [int] NOT NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 21/11/2019 10:58:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Username] [nvarchar](450) NULL,
	[Password] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[UserId] [bigint] NULL,
	[RootFolderId] [bigint] NULL,
	[FileId] [bigint] NULL,
	[Role] [nvarchar](max) NULL,
	[Token] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Files_ParentId]    Script Date: 21/11/2019 10:58:12 ******/
CREATE NONCLUSTERED INDEX [IX_Files_ParentId] ON [dbo].[Files]
(
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Folders_ParentId]    Script Date: 21/11/2019 10:58:12 ******/
CREATE NONCLUSTERED INDEX [IX_Folders_ParentId] ON [dbo].[Folders]
(
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_FileId]    Script Date: 21/11/2019 10:58:12 ******/
CREATE NONCLUSTERED INDEX [IX_Users_FileId] ON [dbo].[Users]
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_RootFolderId]    Script Date: 21/11/2019 10:58:12 ******/
CREATE NONCLUSTERED INDEX [IX_Users_RootFolderId] ON [dbo].[Users]
(
	[RootFolderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_UserId]    Script Date: 21/11/2019 10:58:12 ******/
CREATE NONCLUSTERED INDEX [IX_Users_UserId] ON [dbo].[Users]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Username]    Script Date: 21/11/2019 10:58:12 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Username] ON [dbo].[Users]
(
	[Username] ASC
)
WHERE ([Username] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Logs] ADD  DEFAULT ((0)) FOR [Count]
GO
ALTER TABLE [dbo].[Files]  WITH CHECK ADD  CONSTRAINT [FK_Files_Folders_ParentId] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Folders] ([Id])
GO
ALTER TABLE [dbo].[Files] CHECK CONSTRAINT [FK_Files_Folders_ParentId]
GO
ALTER TABLE [dbo].[Folders]  WITH CHECK ADD  CONSTRAINT [FK_Folders_Folders_ParentId] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Folders] ([Id])
GO
ALTER TABLE [dbo].[Folders] CHECK CONSTRAINT [FK_Folders_Folders_ParentId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Files_FileId] FOREIGN KEY([FileId])
REFERENCES [dbo].[Files] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Files_FileId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Folders_RootFolderId] FOREIGN KEY([RootFolderId])
REFERENCES [dbo].[Folders] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Folders_RootFolderId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [TwoDriveDB] SET  READ_WRITE 
GO
