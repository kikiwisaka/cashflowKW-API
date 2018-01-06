USE [Advansys_v1.6.0]
GO

/****** Object:  Table [dbo].[tblMaksimumProjectValues]    Script Date: 11/20/2017 11:14:01 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblMaksimumProjectValues](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[scenarioId] [int] NULL,
	[projectId] [int] NULL,
	[tahun] [int] NULL,
	[nilaiMaximum] [decimal](18, 2) NULL,
	[createBy] [int] NULL,
	[createDate] [datetime2](7) NULL,
	[updateBy] [int] NULL,
	[updateDate] [datetime2](7) NULL,
	[isDelete] [bit] NULL,
	[deleteDate] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


