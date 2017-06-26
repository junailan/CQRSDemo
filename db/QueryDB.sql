USE [master]
GO
/****** Object:  Database [QueryDB]    Script Date: 06/26/2017 18:33:37 ******/
CREATE DATABASE [QueryDB] ON  PRIMARY 
( NAME = N'QueryDB', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\QueryDB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QueryDB_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\QueryDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QueryDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QueryDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QueryDB] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [QueryDB] SET ANSI_NULLS OFF
GO
ALTER DATABASE [QueryDB] SET ANSI_PADDING OFF
GO
ALTER DATABASE [QueryDB] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [QueryDB] SET ARITHABORT OFF
GO
ALTER DATABASE [QueryDB] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [QueryDB] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [QueryDB] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [QueryDB] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [QueryDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [QueryDB] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [QueryDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [QueryDB] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [QueryDB] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [QueryDB] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [QueryDB] SET  DISABLE_BROKER
GO
ALTER DATABASE [QueryDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [QueryDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [QueryDB] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [QueryDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [QueryDB] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [QueryDB] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [QueryDB] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [QueryDB] SET  READ_WRITE
GO
ALTER DATABASE [QueryDB] SET RECOVERY FULL
GO
ALTER DATABASE [QueryDB] SET  MULTI_USER
GO
ALTER DATABASE [QueryDB] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [QueryDB] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'QueryDB', N'ON'
GO
USE [QueryDB]
GO
/****** Object:  Table [dbo].[BorrowRecord]    Script Date: 06/26/2017 18:33:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BorrowRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserAggregateRootId] [uniqueidentifier] NOT NULL,
	[BookAggregateRootId] [uniqueidentifier] NOT NULL,
	[Returned] [bit] NOT NULL,
	[BorrowedDate] [datetime] NULL,
	[ReturnedDate] [datetime] NULL,
 CONSTRAINT [PK_BorrowRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Transfer Direction, 0 stands for borrow, whereas 1 stands for return.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BorrowRecord', @level2type=N'COLUMN',@level2name=N'Returned'
GO
/****** Object:  Table [dbo].[Book]    Script Date: 06/26/2017 18:33:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AggregateRootId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Author] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ISBN] [nvarchar](255) NOT NULL,
	[Pages] [int] NOT NULL,
	[Inventory] [int] NOT NULL,
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 06/26/2017 18:33:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AggregateRootId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[ContactPhone] [nvarchar](100) NULL,
	[Address_Country] [nvarchar](50) NULL,
	[Address_State] [nvarchar](50) NULL,
	[Address_Street] [nvarchar](50) NULL,
	[Address_City] [nvarchar](50) NULL,
	[Address_Zip] [nvarchar](8) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[BorrowRecordView]    Script Date: 06/26/2017 18:33:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[BorrowRecordView]
AS
SELECT BR.Id,BR.UserAggregateRootId,BR.BookAggregateRootId,U.UserName,B.Title,BR.Returned,BR.BorrowedDate,BR.ReturnedDate FROM BorrowRecord BR
INNER JOIN [USER] U ON BR.UserAggregateRootId=U.AggregateRootId
INNER JOIN Book B ON BR.BookAggregateRootId=B.AggregateRootId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Book"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BorrowRecord"
            Begin Extent = 
               Top = 6
               Left = 249
               Bottom = 125
               Right = 445
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "User"
            Begin Extent = 
               Top = 6
               Left = 483
               Bottom = 125
               Right = 656
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'BorrowRecordView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'BorrowRecordView'
GO
