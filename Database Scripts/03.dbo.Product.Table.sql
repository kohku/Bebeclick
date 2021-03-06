USE [BebeclickStorage]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Service_Product]') AND parent_object_id = OBJECT_ID(N'[dbo].[Service]'))
	ALTER TABLE [dbo].[Service] DROP CONSTRAINT [FK_Service_Product]
GO

--IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_StateEntity]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
--	ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_StateEntity]
--GO

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
	--[StateID] [uniqueidentifier] NULL,
	--[ProvinceID] [uniqueidentifier] NULL,
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

--IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_StateEntity]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
--	ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_StateEntity] FOREIGN KEY([StateID])
--	REFERENCES [dbo].[StateEntity] ([ID])
--	ON UPDATE CASCADE
--	ON DELETE CASCADE
--GO

--IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_StateEntity]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
--	ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_StateEntity]
--GO

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

INSERT [dbo].[Product] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'74c496ba-fdbe-4d4d-86dc-0b6f79302bae', N'Ropa', CAST(0x0000A329011B7B90 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Product] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'ca734dfb-3b3b-4c7d-b464-296b8fa5d4c7', N'Salud', CAST(0x0000A329011B4E1A AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Product] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'c7bdc4e1-7bca-4790-a16d-2c13b195eaad', N'Decoración', CAST(0x0000A329011B1C2C AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Product] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'2c46d5de-6748-4047-b3fe-3035e94793d6', N'No tan bebés', CAST(0x0000A3290121A444 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Product] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'7d202c2b-6422-46f6-8cc6-637d8faa4a00', N'Artículos', CAST(0x0000A329011B9840 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Product] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'585fb409-e6f4-4303-b136-960985bde61c', N'Educación', CAST(0x0000A329011B6E8E AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Product] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'3d22bbb0-33b7-456a-8bd7-c0d6cb9baa10', N'Actividades', CAST(0x0000A329011B91F1 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Product] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'4167ffed-3784-439f-a095-cab996cb2228', N'Eventos Especiales', CAST(0x0000A329011B5B0A AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Product] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'f6869d3b-a3fc-4990-b326-cbab4145cb51', N'Publicaciones', CAST(0x0000A329011B874F AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Product] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'ac57ca87-46de-4590-b849-ef81dc33c0e1', N'Regalos', CAST(0x0000A329011B8C9B AS DateTime), N'dcruz', NULL, NULL, 1)
