USE [Advansys_v1.6.0]
GO

/****** Object:  Table [dbo].[tblCorrelationRiskAntarSectors]    Script Date: 20/11/2017 15:11:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblCorrelationRiskAntarSectors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[scenarioId] [int] NULL,
	[createBy] [int] NULL,
	[createDate] [datetime2](7) NULL,
	[updateBy] [int] NULL,
	[updateDate] [datetime2](7) NULL,
	[isDelete] [bit] NULL,
	[deleteDate] [datetime2](7) NULL,
	[status] [int] NULL,
 CONSTRAINT [PK_dbo.tblCorrelationRiskAntarSectors] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


