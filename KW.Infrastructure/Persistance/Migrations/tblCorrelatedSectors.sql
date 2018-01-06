USE [RBC_PII]
GO

/****** Object:  Table [dbo].[tblCorrelatedSektors]    Script Date: 1/2/2018 4:10:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblCorrelatedSektors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[namaSektor] [varchar](50) NULL,
	[createBy] [int] NULL,
	[createDate] [datetime2](7) NULL,
	[updateBy] [int] NULL,
	[updateDate] [datetime2](7) NULL,
	[isDelete] [bit] NULL,
	[deleteDate] [datetime2](7) NULL,
	[scenarioId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


