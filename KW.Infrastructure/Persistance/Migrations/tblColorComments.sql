USE [Advansys_v1.6.0]
GO
/****** Object:  Table [dbo].[tblColorComments]    Script Date: 11/23/2017 3:14:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblColorComments](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[warna] [varchar](50) NULL,
	[createBy] [int] NULL,
	[createDate] [datetime2](7) NULL,
	[updateBy] [int] NULL,
	[updateDate] [datetime2](7) NULL,
	[isDelete] [bit] NULL,
	[deleteDate] [datetime2](7) NULL,
 CONSTRAINT [PK_tblColorComment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblColorComments] ON 

GO
INSERT [dbo].[tblColorComments] ([id], [warna], [createBy], [createDate], [updateBy], [updateDate], [isDelete], [deleteDate]) VALUES (1, N'Green', 1, NULL, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[tblColorComments] ([id], [warna], [createBy], [createDate], [updateBy], [updateDate], [isDelete], [deleteDate]) VALUES (2, N'Yellow', 1, NULL, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[tblColorComments] ([id], [warna], [createBy], [createDate], [updateBy], [updateDate], [isDelete], [deleteDate]) VALUES (3, N'Red', 1, NULL, NULL, NULL, 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[tblColorComments] OFF
GO
