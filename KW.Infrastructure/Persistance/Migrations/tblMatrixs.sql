USE [Advansys_v1.6.0]
GO
/****** Object:  Table [dbo].[tblMatrixs]    Script Date: 11/22/2017 2:11:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblMatrixs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[namaMatrix] [varchar](50) NULL,
	[namaFormula] [varchar](max) NULL,
	[createBy] [int] NULL,
	[createDate] [datetime2](7) NULL,
	[updateBy] [int] NULL,
	[updateDate] [datetime2](7) NULL,
	[isDelete] [bit] NULL,
	[deleteDate] [datetime2](7) NULL,
 CONSTRAINT [PK_tblMatrix] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblMatrixs] ON 

INSERT [dbo].[tblMatrixs] ([id], [namaMatrix], [namaFormula], [createBy], [createDate], [updateBy], [updateDate], [isDelete], [deleteDate]) VALUES (1, N'Diversification Ratio', N'undiversified risk capital/fully diversified risk capital', 1, CAST(N'2017-11-10 11:04:40.0000000' AS DateTime2), NULL, NULL, 0, NULL)
INSERT [dbo].[tblMatrixs] ([id], [namaMatrix], [namaFormula], [createBy], [createDate], [updateBy], [updateDate], [isDelete], [deleteDate]) VALUES (2, N'Migration Effectiveness', N'maximum exposure/fully diversified risk capital', 1, CAST(N'2017-11-10 11:02:23.0000000' AS DateTime2), NULL, NULL, 0, NULL)
INSERT [dbo].[tblMatrixs] ([id], [namaMatrix], [namaFormula], [createBy], [createDate], [updateBy], [updateDate], [isDelete], [deleteDate]) VALUES (3, N'Leverage Ratio', N'available capital/fully diversified risk capital', 1, CAST(N'2017-11-10 11:04:26.0000000' AS DateTime2), NULL, NULL, 0, NULL)
INSERT [dbo].[tblMatrixs] ([id], [namaMatrix], [namaFormula], [createBy], [createDate], [updateBy], [updateDate], [isDelete], [deleteDate]) VALUES (4, N'Liquidity', N'liquid capital available at certain year', 1, CAST(N'2017-11-10 11:05:20.0000000' AS DateTime2), NULL, NULL, 0, NULL)
INSERT [dbo].[tblMatrixs] ([id], [namaMatrix], [namaFormula], [createBy], [createDate], [updateBy], [updateDate], [isDelete], [deleteDate]) VALUES (5, N'Risk Budget (by sector)', N'maximum proportion of total risk allocated to certain sector (next year)', 1, CAST(N'2017-11-10 11:09:28.0000000' AS DateTime2), NULL, NULL, 0, NULL)
INSERT [dbo].[tblMatrixs] ([id], [namaMatrix], [namaFormula], [createBy], [createDate], [updateBy], [updateDate], [isDelete], [deleteDate]) VALUES (6, N'Risk Budget (by project', N'maximum proportion of total risk allocated to certain project(next year)', 1, NULL, NULL, NULL, 0, NULL)
SET IDENTITY_INSERT [dbo].[tblMatrixs] OFF
