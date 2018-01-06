USE [Advansys_v1.6.0]
GO

/****** Object:  Table [dbo].[tblSubRiskRegistrasis]    Script Date: 03/11/2017 15:01:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblSubRiskRegistrasis](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[mRiskId] [int] NULL,
	[kodeRisk] [nvarchar](50) NULL,
	[riskEvenClaim] [nvarchar](50) NULL,
	[descriptionRiskEvenClaim] [nvarchar](max) NULL,
	[sugestionMigration] [nvarchar](max) NULL,
	[createBy] [int] NULL,
	[createDate] [datetime2](7) NULL,
	[updateBy] [int] NULL,
	[updateDate] [datetime2](7) NULL,
	[isDelete] [bit] NULL,
	[deleteDate] [datetime2](7) NULL,
 CONSTRAINT [PK_tblSubRiskRegistrasi] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


