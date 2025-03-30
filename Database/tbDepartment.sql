USE [TeamManagementDb]
GO

/****** Object:  Table [dbo].[tbDepartment]    Script Date: 30.03.2025 14:46:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbDepartment](
    [Id] [uniqueidentifier] NOT NULL,
    [Name] [nvarchar](100) NOT NULL,
    [Icon] [nvarchar](100) NULL,
    CONSTRAINT [PK_tbDepartment] PRIMARY KEY CLUSTERED
(
[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]
    GO

ALTER TABLE [dbo].[tbDepartment] ADD  DEFAULT (newid()) FOR [Id]
    GO


