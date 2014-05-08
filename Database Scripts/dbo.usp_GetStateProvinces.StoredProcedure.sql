USE [BebeclickStorage]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetStateProvinces]    Script Date: 05/07/2014 17:45:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetStateProvinces]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[usp_GetStateProvinces]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetStateProvinces]
	@ID uniqueidentifier = NULL,
	@Name nvarchar(200) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM StateProvince
	WHERE @ID IS NULL OR ID = @ID
		AND @Name IS NULL OR Name LIKE @Name
	ORDER BY Name

END
GO
