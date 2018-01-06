USE [Advansys_v1.6.0]
GO

/****** Object:  Table [dbo].[tblScenarioDetails]    Script Date: 11/13/2017 3:10:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblScenarioDetails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[scenarioId] [int] NULL,
	[projectId] [int] NULL,
	[createBy] [int] NULL,
	[createDate] [datetime2](7) NULL,
	[updateBy] [int] NULL,
	[updateDate] [datetime2](7) NULL,
	[isDelete] [bit] NULL,
	[deleteDate] [datetime2](7) NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_tblScenarioDetails] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


