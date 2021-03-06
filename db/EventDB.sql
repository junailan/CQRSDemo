USE [EventDB]
GO
/****** Object:  Table [dbo].[Snapshot]    Script Date: 06/26/2017 18:33:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Snapshot](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AggregateRootId] [uniqueidentifier] NOT NULL,
	[AggregateRootType] [nvarchar](max) NOT NULL,
	[Version] [int] NOT NULL,
	[Data] [nvarchar](max) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_Snapshot] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Snapshot_AggregateRootId&Version] UNIQUE NONCLUSTERED 
(
	[AggregateRootId] ASC,
	[Version] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 06/26/2017 18:33:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AggregateRootId] [uniqueidentifier] NOT NULL,
	[AggregateRootType] [nvarchar](max) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[Version] [int] NOT NULL,
	[EventType] [nvarchar](max) NOT NULL,
	[Data] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Synchronized] [bit] NOT NULL,
	[FailedTimes] [int] NOT NULL,
	[FailedReason] [nvarchar](max) NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [IX_Event] UNIQUE NONCLUSTERED 
(
	[AggregateRootId] ASC,
	[Version] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Default [DF_Event_FailedTimes]    Script Date: 06/26/2017 18:33:11 ******/
ALTER TABLE [dbo].[Event] ADD  CONSTRAINT [DF_Event_FailedTimes]  DEFAULT ((0)) FOR [FailedTimes]
GO
