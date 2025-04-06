USE [TeamManagementDb]
GO

/****** Object:  Table [dbo].[tbMembershipFeePayments]    Script Date: 06.04.2025 09:54:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbMembershipFeePayments](
	[Id] [uniqueidentifier] NOT NULL,
	[MemberId] [uniqueidentifier] NULL,
	[PaymentAmount] [int] NOT NULL,
	[PaymentPeriod] [int] NULL,
 CONSTRAINT [PK_tbMemberId] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbMembershipFeePayments] ADD  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[tbMembershipFeePayments] ADD  DEFAULT (datepart(year,getdate())) FOR [PaymentPeriod]
GO

ALTER TABLE [dbo].[tbMembershipFeePayments]  WITH CHECK ADD  CONSTRAINT [FK_tbMembershipFeePayments_tbMembers] FOREIGN KEY([MemberId])
REFERENCES [dbo].[tbMembers] ([Id])
GO

ALTER TABLE [dbo].[tbMembershipFeePayments] CHECK CONSTRAINT [FK_tbMembershipFeePayments_tbMembers]
GO


