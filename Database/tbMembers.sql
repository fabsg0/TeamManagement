USE [MemberManagement_lwlbrf2_Db]
GO

/****** Object:  Table [dbo].[tbMembers]    Script Date: 31.03.2025 08:39:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbMembers]
(
    [Id]            [uniqueidentifier] NOT NULL,
    [FirstName]     [nvarchar](50)     NOT NULL,
    [LastName]      [nvarchar](50)     NOT NULL,
    [Birthdate]     [date]             NULL,
    [Email]         [nvarchar](250)    NULL,
    [Telephone]     [nvarchar](50)     NULL,
    [Street]        [nvarchar](100)    NULL,
    [Number]        [nvarchar](50)     NULL,
    [ZipCode]       [int]              NULL,
    [City]          [nvarchar](100)    NULL,
    [Status]        [nvarchar](50)     NOT NULL,
    [MembershipFee] [int]              NOT NULL,
    CONSTRAINT [PK_tbMembers] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbMembers]
    ADD DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[tbMembers]
    ADD DEFAULT ('active') FOR [Status]
GO

ALTER TABLE [dbo].[tbMembers]
    ADD DEFAULT ((0)) FOR [MembershipFee]
GO


