USE [Advansys_v1.6.0]
GO

/****** Object:  Table [dbo].[tblProjectRiskRegistrasis]    Script Date: 11/14/2017 1:45:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblProjectRiskRegistrasis](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[projectId] [int] NULL,
	[riskRegistrasiId] [int] NULL,
	[createBy] [int] NULL,
	[createDate] [datetime2](7) NULL,
	[updateBy] [int] NULL,
	[updateDate] [datetime2](7) NULL,
	[isDelete] [bit] NULL,
	[deleteDate] [datetime2](7) NULL,
 CONSTRAINT [PK_tblProjectRiskRegistrasis] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


