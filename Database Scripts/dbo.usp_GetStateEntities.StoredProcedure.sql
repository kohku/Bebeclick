USE [BebeclickStorage]
GO

/****** Object:  StoredProcedure [dbo].[usp_GetStateEntities]    Script Date: 05/07/2014 17:45:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetStateEntities]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[usp_GetStateEntities]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetStateEntities]
	@ID uniqueidentifier = NULL,
	@Name nvarchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM StateEntity
	WHERE (@ID IS NULL OR ID = @ID)
		AND (@Name IS NULL OR Name LIKE @Name)
	ORDER BY Name

END
GO
