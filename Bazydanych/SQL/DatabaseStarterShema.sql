EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'Carboat'
GO
use [master];
GO
USE [master]
GO
ALTER DATABASE [Carboat] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO
/****** Object:  Database [Carboat]    Script Date: 08.05.2023 19:15:11 ******/
DROP DATABASE [Carboat]
GO
CREATE DATABASE [Carboat]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Carboat', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQL2022\MSSQL\DATA\Carboat.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Carboat_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQL2022\MSSQL\DATA\Carboat_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Carboat] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Carboat].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Carboat] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Carboat] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Carboat] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Carboat] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Carboat] SET ARITHABORT OFF 
GO
ALTER DATABASE [Carboat] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Carboat] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Carboat] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Carboat] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Carboat] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Carboat] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Carboat] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Carboat] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Carboat] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Carboat] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Carboat] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Carboat] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Carboat] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Carboat] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Carboat] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Carboat] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Carboat] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Carboat] SET RECOVERY FULL 
GO
ALTER DATABASE [Carboat] SET  MULTI_USER 
GO
ALTER DATABASE [Carboat] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Carboat] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Carboat] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Carboat] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Carboat] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Carboat] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Carboat', N'ON'
GO
ALTER DATABASE [Carboat] SET QUERY_STORE = OFF
GO
USE [Carboat]
GO
/****** Object:  Table [dbo].[Contractor_location]    Script Date: 08.05.2023 19:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contractor_location](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Location_id] [int] NOT NULL,
	[Contractor_id] [int] NOT NULL,
 CONSTRAINT [PK_Contractor_location] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contractors]    Script Date: 08.05.2023 19:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contractors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[NIP] [nvarchar](50) NULL,
	[PESEL] [nvarchar](50) NULL,
	[LocationID] [int] NULL,
 CONSTRAINT [PK_Contractors] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 08.05.2023 19:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[login] [nvarchar](50) NOT NULL,
	[pass] [nvarchar](50) NOT NULL,
	[phone] [nvarchar](50) NULL,
	[licence] [nvarchar](50) NULL,
	[is_driver] [bit] NOT NULL,
	[is_in_base] [bit] NOT NULL,
	[pause_time] [int] NULL,
	[Token] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cars]    Script Date: 08.05.2023 19:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[driver] [int] NOT NULL,
	[registration_number] [varchar](50) NOT NULL,
	[mileage] [varchar](50) NOT NULL,
	[buy_date] [date] NOT NULL,
	[IS_truck] [bit] NOT NULL,
	[loadingsize] [int] NOT NULL,
	[is_available] [bit] NOT NULL,
	CONSTRAINT FK_UserKey FOREIGN KEY (driver) REFERENCES Users(id),
 CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Loading]    Script Date: 08.05.2023 19:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loading](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ContractorID] [int] NOT NULL,
	[TraceID] [int] NOT NULL,
	[carID] [int] NOT NULL,
	[pickupdate] [datetime] NULL,
	[time_to_loading] [time](7) NULL,
	[isplanned] [int] NULL,
 CONSTRAINT [PK_Loading] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 08.05.2023 19:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NULL,
	[street] [nvarchar](50) NULL,
	[number] [nvarchar](50) NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Planned_traces]    Script Date: 08.05.2023 19:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plannedtraces](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[traceid] [int] NOT NULL,
	[userid] [int] NOT NULL,
	[carid] [int] NOT NULL,
	[nextplannedtraceid] [int] NULL,
	[LoadingID] [int] NULL,
 CONSTRAINT [PK_Plannedtraces] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 08.05.2023 19:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[UserRole] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trace]    Script Date: 08.05.2023 19:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trace](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[contractor_id] [int] NOT NULL,
	[Startlocation] [int] NOT NULL,
	[Finishlocation] [int] NOT NULL,
	[distance] [int] NOT NULL,
	[travel_time] [time](7) NOT NULL,
 CONSTRAINT [PK_Trace] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UnLoading]    Script Date: 08.05.2023 19:10:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnLoading](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[time_to_unloading] [time](7) NULL,
	[loading_ID] [int] NOT NULL,
 CONSTRAINT [PK_UnLoading] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET IDENTITY_INSERT [dbo].[Contractor_location] ON 
GO
INSERT [dbo].[Contractor_location] ([ID], [Location_id], [Contractor_id]) VALUES (1, 1, 2)
GO
SET IDENTITY_INSERT [dbo].[Contractor_location] OFF
GO
SET IDENTITY_INSERT [dbo].[Contractors] ON 
GO
INSERT [dbo].[Contractors] ([ID], [name], [NIP], [PESEL], [LocationID]) VALUES (2, N'dawid', NULL, N'00222504616', 0)
GO
SET IDENTITY_INSERT [dbo].[Contractors] OFF
GO
SET IDENTITY_INSERT [dbo].[Location] ON 
GO
INSERT [dbo].[Location] ([ID], [Name], [City], [street], [number]) VALUES (1, N'NS', N'NS', N'123', N'123')
GO
SET IDENTITY_INSERT [dbo].[Location] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([ID], [UserID], [UserRole]) VALUES (1, 1, N'Admin')
GO
INSERT [dbo].[Roles] ([ID], [UserID], [UserRole]) VALUES (3, 3, N'User')
GO
INSERT [dbo].[Roles] ([ID], [UserID], [UserRole]) VALUES (4, 4, N'User')
GO
INSERT [dbo].[Roles] ([ID], [UserID], [UserRole]) VALUES (5, 5, N'Driver')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Trace] ON 
GO
INSERT [dbo].[Trace] ([ID], [contractor_id], [Start_location], [Finish_location], [distance], [travel_time]) VALUES (1, 2, 1, 1, 12, CAST(N'02:02:00' AS Time))
GO
SET IDENTITY_INSERT [dbo].[Trace] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([ID], [login], [pass], [phone], [licence], [is_driver], [is_in_base], [pause_time], [Token]) VALUES (1, N'Admin', N'6rKKXOPYG7UVjNGcDdat4M1S427v3vu3cnggkmfe8R/aKuJ3', N'1234567892', N'C', 1, 0, 0, NULL)
GO
INSERT [dbo].[Users] ([ID], [login], [pass], [phone], [licence], [is_driver], [is_in_base], [pause_time], [Token]) VALUES (3, N'123456', N'Q96sjgqYFYRorNj+f02EGJhOMN4GY/AuwvMkY2E7XZx8nrzf', NULL, N'B', 0, 1, 0, NULL)
GO
INSERT [dbo].[Users] ([ID], [login], [pass], [phone], [licence], [is_driver], [is_in_base], [pause_time], [Token]) VALUES (4, N'12eqe', N'HibY+xDL5K06KlYBAFMfond9ULsDclRUl8pK7NLaTuzTdHaR', NULL, N'B', 0, 1, 0, NULL)
GO
INSERT [dbo].[Users] ([ID], [login], [pass], [phone], [licence], [is_driver], [is_in_base], [pause_time], [Token]) VALUES (5, N'Dawid Kwiatkowski', N'/ilvi/FZi5xgPZrOJhjI2q2kbI70LMsA4ozyU28DjWahx1Jt', N'123332132', N'C', 1, 1, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Cars] ON 
GO
INSERT [dbo].[Cars] ([ID], [driver], [registration_number], [mileage], [buy_date], [IS_truck], [loadingsize], [is_available]) VALUES (3, 5, N'12', N'123', CAST(N'2023-03-05' AS Date), 0, 3123, 0)
GO
INSERT [dbo].[Cars] ([ID], [driver], [registration_number], [mileage], [buy_date], [IS_truck], [loadingsize], [is_available]) VALUES (4, 1, N'qweq12', N'12ew213', CAST(N'2023-05-04' AS Date), 0, 213213, 0)
GO
SET IDENTITY_INSERT [dbo].[Cars] OFF
GO
ALTER TABLE [dbo].[Contractor_location]  WITH CHECK ADD  CONSTRAINT [FK_Contractor_location_Contractors] FOREIGN KEY([Contractor_id])
REFERENCES [dbo].[Contractors] ([ID])
GO
ALTER TABLE [dbo].[Contractor_location] CHECK CONSTRAINT [FK_Contractor_location_Contractors]
GO
ALTER TABLE [dbo].[Contractor_location]  WITH CHECK ADD  CONSTRAINT [FK_Contractor_location_Location] FOREIGN KEY([Location_id])
REFERENCES [dbo].[Location] ([ID])
GO
ALTER TABLE [dbo].[Contractor_location] CHECK CONSTRAINT [FK_Contractor_location_Location]
GO
ALTER TABLE [dbo].[Loading]  WITH CHECK ADD  CONSTRAINT [FK_Loading_Cars] FOREIGN KEY([carID])
REFERENCES [dbo].[Cars] ([ID])
GO
ALTER TABLE [dbo].[Loading] CHECK CONSTRAINT [FK_Loading_Cars]
GO
ALTER TABLE [dbo].[Loading]  WITH CHECK ADD  CONSTRAINT [FK_Loading_Trace] FOREIGN KEY([TraceID])
REFERENCES [dbo].[Trace] ([ID])
GO
ALTER TABLE [dbo].[Loading] CHECK CONSTRAINT [FK_Loading_Trace]
GO
ALTER TABLE [dbo].[Plannedtraces]  WITH CHECK ADD  CONSTRAINT [FK_Planned_traces_Cars] FOREIGN KEY([car_id])
REFERENCES [dbo].[Cars] ([ID])
GO
ALTER TABLE [dbo].[Plannedtraces] CHECK CONSTRAINT [FK_Planned_traces_Cars]
GO
ALTER TABLE [dbo].[Plannedtraces]  WITH CHECK ADD  CONSTRAINT [FK_Planned_traces_Loading] FOREIGN KEY([LoadingID])
REFERENCES [dbo].[Loading] ([ID])
GO
ALTER TABLE [dbo].[Plannedtraces] CHECK CONSTRAINT [FK_Planned_traces_Loading]
GO
ALTER TABLE [dbo].[Plannedtraces]  WITH CHECK ADD  CONSTRAINT [FK_Planned_traces_Trace] FOREIGN KEY([trace_id])
REFERENCES [dbo].[Trace] ([ID])
GO
ALTER TABLE [dbo].[Plannedtraces] CHECK CONSTRAINT [FK_Planned_traces_Trace]
GO
ALTER TABLE [dbo].[Plannedtraces]  WITH CHECK ADD  CONSTRAINT [FK_Planned_traces_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Plannedtraces] CHECK CONSTRAINT [FK_Planned_traces_Users]
GO
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [FK_Roles_Users]
GO
ALTER TABLE [dbo].[Trace]  WITH CHECK ADD  CONSTRAINT [FK_Trace_Contractors] FOREIGN KEY([contractor_id])
REFERENCES [dbo].[Contractors] ([ID])
GO
ALTER TABLE [dbo].[Trace] CHECK CONSTRAINT [FK_Trace_Contractors]
GO
ALTER TABLE [dbo].[Trace]  WITH CHECK ADD  CONSTRAINT [FK_Trace_Location] FOREIGN KEY([Start_location])
REFERENCES [dbo].[Location] ([ID])
GO
ALTER TABLE [dbo].[Trace] CHECK CONSTRAINT [FK_Trace_Location]
GO
ALTER TABLE [dbo].[Trace]  WITH CHECK ADD  CONSTRAINT [FK_Trace_Location_02] FOREIGN KEY([Finish_location])
REFERENCES [dbo].[Location] ([ID])
GO
ALTER TABLE [dbo].[Trace] CHECK CONSTRAINT [FK_Trace_Location_02]
GO
ALTER TABLE [dbo].[UnLoading]  WITH CHECK ADD  CONSTRAINT [FK_UnLoading_Loading] FOREIGN KEY([loading_ID])
REFERENCES [dbo].[Loading] ([ID])
GO
ALTER TABLE [dbo].[UnLoading] CHECK CONSTRAINT [FK_UnLoading_Loading]
GO
USE [master]
GO
ALTER DATABASE [Carboat] SET  READ_WRITE 
GO
