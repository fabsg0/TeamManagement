USE [TeamManagementDb]
GO

/****** Object:  Table [dbo].[tbDepartmentMember]    Script Date: 31.03.2025 08:39:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbDepartmentMember]
(
    [Id]           [uniqueidentifier] NOT NULL,
    [MemberId]     [uniqueidentifier] NULL,
    [DepartmentId] [uniqueidentifier] NULL,
    CONSTRAINT [PK_tbDepartmentMember] PRIMARY KEY CLUSTERED
        (
         [Id] ASC
            ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbDepartmentMember]
    ADD DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[tbDepartmentMember]
    WITH CHECK ADD CONSTRAINT [FK_tbDepartmentMember_tbDepartment] FOREIGN KEY ([DepartmentId])
        REFERENCES [dbo].[tbDepartment] ([Id])
GO

ALTER TABLE [dbo].[tbDepartmentMember]
    CHECK CONSTRAINT [FK_tbDepartmentMember_tbDepartment]
GO

ALTER TABLE [dbo].[tbDepartmentMember]
    WITH CHECK ADD CONSTRAINT [FK_tbDepartmentMember_tbMembers] FOREIGN KEY ([MemberId])
        REFERENCES [dbo].[tbMembers] ([Id])
GO

ALTER TABLE [dbo].[tbDepartmentMember]
    CHECK CONSTRAINT [FK_tbDepartmentMember_tbMembers]
GO

