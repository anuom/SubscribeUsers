USE [SubscriberDetails]
GO

/****** Object:  Table [dbo].[subusers]    Script Date: 8/19/2019 2:57:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[subusers](
	[Uid] [int] IDENTITY(1,1) NOT NULL,
	[Uname] [varchar](100) NULL,
	[Uemail] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

