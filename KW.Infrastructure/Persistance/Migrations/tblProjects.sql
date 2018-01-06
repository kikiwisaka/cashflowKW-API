USE [Advansys_v1.6.0]
GO

/****** Object:  Table [dbo].[tblProjects]    Script Date: 11/7/2017 5:46:36 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblProjects](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[namaProject] [nvarchar](255) NULL,
	[userId] [int] NOT NULL,
	[tahapanId] [int] NOT NULL,
	[statusProject] [bit] NOT NULL,
	[minimum] [decimal](18, 2) NOT NULL,
	[maximum] [decimal](18, 2) NOT NULL,
	[sektorId] [int] NOT NULL,
	[keterangan] [nvarchar](255) NULL,
	[createBy] [int] NULL,
	[createDate] [datetime2](7) NULL,
	[updateBy] [int] NULL,
	[updateDate] [datetime2](7) NULL,
	[isDelete] [bit] NULL,
	[deleteDate] [datetime2](7) NULL,
	[riskRegistrasiId] [int] NULL,
	[tahunAwalProject] [datetime2](7) NULL,
	[TahunAkhirProject] [datetime2](7) NULL,
 CONSTRAINT [PK_dbo.tblProjects] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


