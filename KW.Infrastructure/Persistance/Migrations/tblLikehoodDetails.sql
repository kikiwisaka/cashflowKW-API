USE [Advansys_v1.6.0]
GO

/****** Object:  Table [dbo].[tblLikehoodDetails]    Script Date: 11/9/2017 10:48:26 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblLikehoodDetails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[definisiLikehood] [nvarchar](150) NULL,
	[lower] [decimal](18, 3) NULL,
	[upper] [decimal](18, 3) NULL,
	[average] [decimal](18, 3) NULL,
	[createBy] [int] NULL,
	[createDate] [datetime2](7) NULL,
	[updateBy] [int] NULL,
	[updateDate] [datetime2](7) NULL,
	[isDelete] [bit] NULL,
	[deleteDate] [datetime2](7) NULL,
	[status] [bit] NULL,
	[likehoodId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


