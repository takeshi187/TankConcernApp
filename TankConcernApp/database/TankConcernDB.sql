USE [master]
GO
/****** Object:  Database [TankConcernDB]    Script Date: 04.06.2025 23:44:31 ******/
CREATE DATABASE [TankConcernDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TankConcernDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TankConcernDB.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TankConcernDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\TankConcernDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [TankConcernDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TankConcernDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TankConcernDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TankConcernDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TankConcernDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TankConcernDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TankConcernDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TankConcernDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TankConcernDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TankConcernDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TankConcernDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TankConcernDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TankConcernDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TankConcernDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TankConcernDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TankConcernDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TankConcernDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TankConcernDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TankConcernDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TankConcernDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TankConcernDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TankConcernDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TankConcernDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TankConcernDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TankConcernDB] SET RECOVERY FULL 
GO
ALTER DATABASE [TankConcernDB] SET  MULTI_USER 
GO
ALTER DATABASE [TankConcernDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TankConcernDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TankConcernDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TankConcernDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TankConcernDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TankConcernDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TankConcernDB', N'ON'
GO
ALTER DATABASE [TankConcernDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [TankConcernDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [TankConcernDB]
GO
/****** Object:  Table [dbo].[Brigades]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brigades](
	[BrigadeId] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK_Brigades] PRIMARY KEY CLUSTERED 
(
	[BrigadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BrigadeWorkshopAssignments]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BrigadeWorkshopAssignments](
	[WorkshopId] [bigint] NOT NULL,
	[BrigadeId] [bigint] NOT NULL,
	[AssignmentDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
 CONSTRAINT [PK_BrigadeWorkshopAssignments] PRIMARY KEY CLUSTERED 
(
	[WorkshopId] ASC,
	[BrigadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerTypeId] [bigint] NOT NULL,
	[CustomerName] [nvarchar](max) NOT NULL,
	[Country] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerTypes]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerTypes](
	[CustomerTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerTypeName] [nvarchar](max) NULL,
 CONSTRAINT [PK_CustomerTypes] PRIMARY KEY CLUSTERED 
(
	[CustomerTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeBrigade]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeBrigade](
	[EBId] [bigint] IDENTITY(1,1) NOT NULL,
	[BrigadeId] [bigint] NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[LastUpdate] [date] NOT NULL,
 CONSTRAINT [PK_EmployeeBrigade] PRIMARY KEY CLUSTERED 
(
	[EBId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_EmployeeBrigade] UNIQUE NONCLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeePosts]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeePosts](
	[EmployeePostId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeePostName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[EmployeePostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeePostId] [bigint] NOT NULL,
	[EmployeeStatusId] [bigint] NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[MiddleName] [nvarchar](100) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Salary] [decimal](18, 0) NOT NULL,
	[Phone] [decimal](11, 0) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Employees] UNIQUE NONCLUSTERED 
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeStatuses]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeStatuses](
	[EmployeeStatusId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeStatusName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_EmployeeStatuses] PRIMARY KEY CLUSTERED 
(
	[EmployeeStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[OrderDate] [date] NOT NULL,
	[OrderStatusId] [bigint] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[Count] [int] NOT NULL,
	[TotalPrice] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatuses]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatuses](
	[OrderStatusId] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderStatusName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_OrderStatuses] PRIMARY KEY CLUSTERED 
(
	[OrderStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PartsInventory]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartsInventory](
	[InventoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[TankPartId] [bigint] NOT NULL,
	[Count] [bigint] NOT NULL,
	[LastUpdate] [date] NOT NULL,
	[PartTypeId] [bigint] NOT NULL,
 CONSTRAINT [PK_PartsInventory] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PartTypes]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartTypes](
	[PartTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[PartTypeName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PartTypes] PRIMARY KEY CLUSTERED 
(
	[PartTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[ProductCategoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductCategoryName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[ProductCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionLogs]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionLogs](
	[LogId] [bigint] IDENTITY(1,1) NOT NULL,
	[WorkshopId] [bigint] NOT NULL,
	[BrigadeId] [bigint] NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[ProductStageId] [bigint] NOT NULL,
	[ProductStageTypeId] [bigint] NOT NULL,
	[Date] [date] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_ProductionLogs] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductCategoryId] [bigint] NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductStages]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductStages](
	[ProductStageId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductStageTypeId] [bigint] NOT NULL,
	[WorkshopId] [bigint] NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_ProductStages] PRIMARY KEY CLUSTERED 
(
	[ProductStageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductStageTypes]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductStageTypes](
	[ProductStageTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductStageTypeName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_ProductStageTypes] PRIMARY KEY CLUSTERED 
(
	[ProductStageTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TankParts]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TankParts](
	[TankPartId] [bigint] IDENTITY(1,1) NOT NULL,
	[TankPartName] [nvarchar](max) NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[PartTypeId] [bigint] NOT NULL,
 CONSTRAINT [PK_TankParts] PRIMARY KEY CLUSTERED 
(
	[TankPartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[RoleId] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[Login] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[LastLogin] [date] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Users] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workshops]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workshops](
	[WorkshopId] [bigint] IDENTITY(1,1) NOT NULL,
	[WorkshopTypeId] [bigint] NOT NULL,
	[Location] [nvarchar](max) NOT NULL,
	[WorkshopArea] [int] NOT NULL,
	[Workplaces] [int] NOT NULL,
	[ProductionCapacity] [int] NOT NULL,
 CONSTRAINT [PK_Workshops] PRIMARY KEY CLUSTERED 
(
	[WorkshopId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkshopTypes]    Script Date: 04.06.2025 23:44:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkshopTypes](
	[WorkshopTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[WorkshopTypeName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_WorkshopTypes] PRIMARY KEY CLUSTERED 
(
	[WorkshopTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_EmployeeStatusId]  DEFAULT ((1)) FOR [EmployeeStatusId]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF_Orders_OrderStatusId]  DEFAULT ((1)) FOR [OrderStatusId]
GO
ALTER TABLE [dbo].[BrigadeWorkshopAssignments]  WITH CHECK ADD  CONSTRAINT [FK_BrigadeWorkshopAssignments_Workshops] FOREIGN KEY([WorkshopId])
REFERENCES [dbo].[Workshops] ([WorkshopId])
GO
ALTER TABLE [dbo].[BrigadeWorkshopAssignments] CHECK CONSTRAINT [FK_BrigadeWorkshopAssignments_Workshops]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_CustomerTypes] FOREIGN KEY([CustomerTypeId])
REFERENCES [dbo].[CustomerTypes] ([CustomerTypeId])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_CustomerTypes]
GO
ALTER TABLE [dbo].[EmployeeBrigade]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeBrigade_Brigades] FOREIGN KEY([BrigadeId])
REFERENCES [dbo].[Brigades] ([BrigadeId])
GO
ALTER TABLE [dbo].[EmployeeBrigade] CHECK CONSTRAINT [FK_EmployeeBrigade_Brigades]
GO
ALTER TABLE [dbo].[EmployeeBrigade]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeBrigade_Employees1] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[EmployeeBrigade] CHECK CONSTRAINT [FK_EmployeeBrigade_Employees1]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_EmployeePosts] FOREIGN KEY([EmployeePostId])
REFERENCES [dbo].[EmployeePosts] ([EmployeePostId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_EmployeePosts]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_EmployeeStatuses] FOREIGN KEY([EmployeeStatusId])
REFERENCES [dbo].[EmployeeStatuses] ([EmployeeStatusId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_EmployeeStatuses]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_OrderStatuses] FOREIGN KEY([OrderStatusId])
REFERENCES [dbo].[OrderStatuses] ([OrderStatusId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_OrderStatuses]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Products]
GO
ALTER TABLE [dbo].[PartsInventory]  WITH CHECK ADD  CONSTRAINT [FK_PartsInventory_PartTypes] FOREIGN KEY([PartTypeId])
REFERENCES [dbo].[PartTypes] ([PartTypeId])
GO
ALTER TABLE [dbo].[PartsInventory] CHECK CONSTRAINT [FK_PartsInventory_PartTypes]
GO
ALTER TABLE [dbo].[PartsInventory]  WITH CHECK ADD  CONSTRAINT [FK_PartsInventory_TankParts] FOREIGN KEY([TankPartId])
REFERENCES [dbo].[TankParts] ([TankPartId])
GO
ALTER TABLE [dbo].[PartsInventory] CHECK CONSTRAINT [FK_PartsInventory_TankParts]
GO
ALTER TABLE [dbo].[ProductionLogs]  WITH CHECK ADD  CONSTRAINT [FK_ProductionLogs_BrigadeWorkshopAssignments1] FOREIGN KEY([WorkshopId], [BrigadeId])
REFERENCES [dbo].[BrigadeWorkshopAssignments] ([WorkshopId], [BrigadeId])
GO
ALTER TABLE [dbo].[ProductionLogs] CHECK CONSTRAINT [FK_ProductionLogs_BrigadeWorkshopAssignments1]
GO
ALTER TABLE [dbo].[ProductionLogs]  WITH CHECK ADD  CONSTRAINT [FK_ProductionLogs_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[ProductionLogs] CHECK CONSTRAINT [FK_ProductionLogs_Orders]
GO
ALTER TABLE [dbo].[ProductionLogs]  WITH CHECK ADD  CONSTRAINT [FK_ProductionLogs_ProductStages1] FOREIGN KEY([ProductStageId])
REFERENCES [dbo].[ProductStages] ([ProductStageId])
GO
ALTER TABLE [dbo].[ProductionLogs] CHECK CONSTRAINT [FK_ProductionLogs_ProductStages1]
GO
ALTER TABLE [dbo].[ProductionLogs]  WITH CHECK ADD  CONSTRAINT [FK_ProductionLogs_ProductStageTypes] FOREIGN KEY([ProductStageTypeId])
REFERENCES [dbo].[ProductStageTypes] ([ProductStageTypeId])
GO
ALTER TABLE [dbo].[ProductionLogs] CHECK CONSTRAINT [FK_ProductionLogs_ProductStageTypes]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductCategory] FOREIGN KEY([ProductCategoryId])
REFERENCES [dbo].[ProductCategory] ([ProductCategoryId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductCategory]
GO
ALTER TABLE [dbo].[ProductStages]  WITH CHECK ADD  CONSTRAINT [FK_ProductStages_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[ProductStages] CHECK CONSTRAINT [FK_ProductStages_Orders]
GO
ALTER TABLE [dbo].[ProductStages]  WITH CHECK ADD  CONSTRAINT [FK_ProductStages_ProductStageTypes] FOREIGN KEY([ProductStageTypeId])
REFERENCES [dbo].[ProductStageTypes] ([ProductStageTypeId])
GO
ALTER TABLE [dbo].[ProductStages] CHECK CONSTRAINT [FK_ProductStages_ProductStageTypes]
GO
ALTER TABLE [dbo].[ProductStages]  WITH CHECK ADD  CONSTRAINT [FK_ProductStages_Workshops] FOREIGN KEY([WorkshopId])
REFERENCES [dbo].[Workshops] ([WorkshopId])
GO
ALTER TABLE [dbo].[ProductStages] CHECK CONSTRAINT [FK_ProductStages_Workshops]
GO
ALTER TABLE [dbo].[TankParts]  WITH CHECK ADD  CONSTRAINT [FK_TankParts_PartTypes] FOREIGN KEY([PartTypeId])
REFERENCES [dbo].[PartTypes] ([PartTypeId])
GO
ALTER TABLE [dbo].[TankParts] CHECK CONSTRAINT [FK_TankParts_PartTypes]
GO
ALTER TABLE [dbo].[TankParts]  WITH CHECK ADD  CONSTRAINT [FK_TankParts_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[TankParts] CHECK CONSTRAINT [FK_TankParts_Products]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Employees]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[UserRoles] ([RoleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserRoles]
GO
ALTER TABLE [dbo].[Workshops]  WITH CHECK ADD  CONSTRAINT [FK_Workshops_WorkshopTypes] FOREIGN KEY([WorkshopTypeId])
REFERENCES [dbo].[WorkshopTypes] ([WorkshopTypeId])
GO
ALTER TABLE [dbo].[Workshops] CHECK CONSTRAINT [FK_Workshops_WorkshopTypes]
GO
USE [master]
GO
ALTER DATABASE [TankConcernDB] SET  READ_WRITE 
GO
