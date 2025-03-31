USE [TeamManagementDb]
GO
/****** Object:  StoredProcedure [dbo].[spSetAndUpdateMemberShipFees]    Script Date: 31.03.2025 08:46:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ==================================================
-- Author:		fabsg0
-- Create date: 2025-03-31
-- Description:	Stored procedure to update memberShipFees in TeamManagementDb.tbMembers
-- EXEC [dbo].[spSetAndUpdateMemberShipFees]
-- ===================================================

ALTER PROCEDURE [dbo].[spSetAndUpdateMemberShipFees]
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE tbMembers
    SET MembershipFee = 10
    WHERE DATEDIFF(YEAR, BirthDate, GETDATE()) < 18;

    UPDATE tbMembers
    SET MembershipFee = 20
    WHERE DATEDIFF(YEAR, BirthDate, GETDATE()) > 18;

END
