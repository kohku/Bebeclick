USE [BebeclickStorage]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetStateProvince]    Script Date: 05/07/2014 17:45:59 ******/
DROP PROCEDURE [dbo].[usp_GetStateProvince]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetStateProvince]
AS
BEGIN
	SET NOCOUNT ON;

	select *
	from StateProvince

END
GO
