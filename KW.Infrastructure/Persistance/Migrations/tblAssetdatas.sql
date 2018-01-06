USE [Advansys_v1.6.0]
GO

/****** Object:  Table [dbo].[tblAssetDatas]    Script Date: 11/16/2017 1:43:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblAssetDatas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[assetClass] [varchar](max) NULL,
	[termAwal] [int] NULL,
	[termAkhir] [int] NULL,
	[assumentReturn] [decimal](18, 2) NULL,
	[outstandingStartYears] [int] NULL,
	[outstandingEndYears] [int] NULL,
	[assetValue] [decimal](18, 2) NULL,
	[porpotion] [decimal](18, 2) NULL,
	[assumedReturnPercentage] [decimal](18, 2) NULL,
	[AssumedReturn] [decimal](18, 2) NULL,
	[createBy] [int] NULL,
	[createDate] [datetime2](7) NULL,
	[updateBy] [int] NULL,
	[updateDate] [datetime2](7) NULL,
	[isDelete] [bit] NULL,
	[deleteDate] [datetime2](7) NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_tblAssetData] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


