USE [Advansys_v1.6.0]
GO

/****** Object:  Table [dbo].[tblSektors]    Script Date: 03/11/2017 15:01:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblSektors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[namaSektor] [nvarchar](50) NULL,
	[minimum] [decimal](18, 2) NULL,
	[maximum] [decimal](18, 2) NULL,
	[definisi] [nvarchar](max) NULL,
	[createBy] [int] NULL,
	[createDate] [datetime2](7) NULL,
	[updateBy] [int] NULL,
	[updateDate] [datetime2](7) NULL,
	[isDelete] [bit] NULL,
	[deleteDate] [datetime2](7) NULL,
	[status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


