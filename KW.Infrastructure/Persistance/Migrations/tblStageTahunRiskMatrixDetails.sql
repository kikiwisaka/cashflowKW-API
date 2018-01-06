USE [Advansys_v1.6.0]
GO

/****** Object:  Table [dbo].[tblStageTahunRiskMatrixDetails]    Script Date: 05/12/2017 08:11:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblStageTahunRiskMatrixDetails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nilaiExpose] [decimal](18, 2) NULL,
	[stageTahunRiskMatrixId] [int] NULL,
	[riskRegistrasiId] [int] NULL,
	[likehoodDetailId] [int] NULL,
	[createBy] [int] NULL,
	[createDate] [datetime2](7) NULL,
	[updateBy] [int] NULL,
	[updateDate] [datetime2](7) NULL,
	[isUpdate] [bit] NULL,
	[deleteDate] [datetime2](7) NULL,
	[isDelete] [bit] NULL,
	[riskMatrixProjectId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


