USE [BebeclickStorage]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Service_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[Service]'))
	ALTER TABLE [dbo].[Service] DROP CONSTRAINT [FK_Service_Product]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_StateProvince]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
	ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_StateProvince]
GO

IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Product_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
BEGIN
	IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Product_ID]') AND type = 'D')
	BEGIN
	ALTER TABLE [dbo].[Product] DROP CONSTRAINT [DF_Product_ID]
	END
END
GO

IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Product_DateCreated]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
	BEGIN
	IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Product_DateCreated]') AND type = 'D')
	BEGIN
		ALTER TABLE [dbo].[Product] DROP CONSTRAINT [DF_Product_DateCreated]
	END
END
GO

IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Product_Visible]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
BEGIN
	IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Product_Visible]') AND type = 'D')
	BEGIN
		ALTER TABLE [dbo].[Product] DROP CONSTRAINT [DF_Product_Visible]
	END
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
	DROP TABLE [dbo].[Product]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Product](
	[ID] [uniqueidentifier] NOT NULL,
	[StateID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [nvarchar](256) NULL,
	[Visible] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_StateProvince]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
	ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_StateProvince] FOREIGN KEY([StateID])
	REFERENCES [dbo].[StateProvince] ([ID])
	ON UPDATE CASCADE
	ON DELETE CASCADE
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_StateProvince]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
	ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_StateProvince]
GO

IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Product_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Product_ID]') AND type = 'D')
	BEGIN
		ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_ID]  DEFAULT (newid()) FOR [ID]
	END
END
GO

IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Product_DateCreated]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Product_DateCreated]') AND type = 'D')
	BEGIN
		ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
	END
END
GO

IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Product_Visible]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
BEGIN
	IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Product_Visible]') AND type = 'D')
	BEGIN
		ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Visible]  DEFAULT ((0)) FOR [Visible]
	END
END
GO
