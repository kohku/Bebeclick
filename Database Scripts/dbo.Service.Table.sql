USE [BebeclickStorage]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Service_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[Service]'))
	ALTER TABLE [dbo].[Service] DROP CONSTRAINT [FK_Service_Product]
GO

IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Service_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Service]'))
BEGIN
	IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Service_ID]') AND type = 'D')
	BEGIN
		ALTER TABLE [dbo].[Service] DROP CONSTRAINT [DF_Service_ID]
	END
END
GO

IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Service_DateCreated]') AND parent_object_id = OBJECT_ID(N'[dbo].[Service]'))
BEGIN
	IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Service_DateCreated]') AND type = 'D')
	BEGIN
		ALTER TABLE [dbo].[Service] DROP CONSTRAINT [DF_Service_DateCreated]
	END
END
GO

IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Service_Visible]') AND parent_object_id = OBJECT_ID(N'[dbo].[Service]'))
BEGIN
	IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Service_Visible]') AND type = 'D')
	BEGIN
		ALTER TABLE [dbo].[Service] DROP CONSTRAINT [DF_Service_Visible]
	END
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Service]') AND type in (N'U'))
DROP TABLE [dbo].[Service]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Service]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Service](
	[ID] [uniqueidentifier] NOT NULL,
	[ProductID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [nvarchar](256) NULL,
	[Visible] [bit] NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Service_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[Service]'))
	ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_Product] FOREIGN KEY([ProductID])
	REFERENCES [dbo].[Product] ([ID])
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Service_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[Service]'))
	ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_Product]
GO

IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Service_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Service]'))
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Service_ID]') AND type = 'D')
	BEGIN
		ALTER TABLE [dbo].[Service] ADD  CONSTRAINT [DF_Service_ID]  DEFAULT (newid()) FOR [ID]
	END
END
GO

IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Service_DateCreated]') AND parent_object_id = OBJECT_ID(N'[dbo].[Service]'))
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Service_DateCreated]') AND type = 'D')
	BEGIN
		ALTER TABLE [dbo].[Service] ADD  CONSTRAINT [DF_Service_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
	END
END
GO

IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Service_Visible]') AND parent_object_id = OBJECT_ID(N'[dbo].[Service]'))
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Service_Visible]') AND type = 'D')
	BEGIN
		ALTER TABLE [dbo].[Service] ADD  CONSTRAINT [DF_Service_Visible]  DEFAULT ((0)) FOR [Visible]
	END
END
GO
