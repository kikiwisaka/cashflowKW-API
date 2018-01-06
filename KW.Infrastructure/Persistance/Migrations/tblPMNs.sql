USE [Advansys_v1.6.0]
GO

/****** Object:  Table [dbo].[tblPMNs]    Script Date: 11/16/2017 1:45:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblPMNs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[pmnToModalDasarCap] [int] NULL,
	[recourseDelay] [decimal](18, 2) NULL,
	[delayYears] [decimal](18, 2) NULL,
	[opexGrowth] [decimal](18, 2) NULL,
	[opex] [decimal](18, 2) NULL,
	[createBy] [int] NULL,
	[createDate] [datetime2](7) NULL,
	[updateBy] [int] NULL,
	[updateDate] [datetime2](7) NULL,
	[isDelete] [bit] NULL,
	[deleteDate] [datetime2](7) NULL,
	[status] [bit] NULL
) ON [PRIMARY]

GO


