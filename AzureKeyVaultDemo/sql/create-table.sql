USE [demoEDMX]
GO

/****** Object:  Table [dbo].[BaseRecord]    Script Date: 6/17/2021 3:18:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BaseRecord](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL
) ON [PRIMARY]
GO


