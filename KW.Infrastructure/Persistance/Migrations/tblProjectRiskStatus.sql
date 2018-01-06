USE [Advansys_v1.6.0]
GO

/****** Object:  Table [dbo].[tblProjectRiskStatus]    Script Date: 04/12/2017 11:59:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblProjectRiskStatus](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[kodeMRisk] [varchar](10) NULL,
	[namaCategoryRisk] [varchar](50) NULL,
	[definisi] [varchar](max) NULL,
	[isProjectUsed] [bit] NULL,
	[projectId] [int] NULL,
	[riskRegistrasiId] [int] NULL,
 CONSTRAINT [PK_tblProjectRiskStatus] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


