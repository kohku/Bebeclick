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

INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'cff124a2-7ecf-489d-ae3b-00a3b86378a7', N'3d22bbb0-33b7-456a-8bd7-c0d6cb9baa10', N'Deportivas', CAST(0x0000A32901211C53 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'd2ede900-cb41-4af4-be17-018b22b2a99d', N'7d202c2b-6422-46f6-8cc6-637d8faa4a00', N'Cunas', CAST(0x0000A32901217922 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'c63e7ecf-6e58-49db-b626-021230539277', N'ca734dfb-3b3b-4c7d-b464-296b8fa5d4c7', N'Blogs', CAST(0x0000A329011F8E8B AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'f1fc0378-bd54-4cd5-a2b8-021e2aa53713', N'c7bdc4e1-7bca-4790-a16d-2c13b195eaad', N'Blogs', CAST(0x0000A329011F3CE1 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'8fa0e9c6-e8f2-4158-8972-050014993471', N'4167ffed-3784-439f-a095-cab996cb2228', N'Invitaciones', CAST(0x0000A329011FF831 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'5e98f95f-2b62-4974-ae6f-05d55862f70f', N'4167ffed-3784-439f-a095-cab996cb2228', N'Eventos especiales', CAST(0x0000A329011FAAFE AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'fc7e444c-f04b-462d-99f0-127b962fb981', N'7d202c2b-6422-46f6-8cc6-637d8faa4a00', N'Autoasientos', CAST(0x0000A32901215F95 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'59fc2722-9c24-42f0-b552-1739d542baa0', N'4167ffed-3784-439f-a095-cab996cb2228', N'Jugueterías', CAST(0x0000A3290120073B AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'ba4fd5f7-0269-4174-b939-1859135ebbb8', N'74c496ba-fdbe-4d4d-86dc-0b6f79302bae', N'Pañales', CAST(0x0000A3290120CB0B AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'cdca88c4-8354-4760-a58c-1a701b51c26b', N'7d202c2b-6422-46f6-8cc6-637d8faa4a00', N'Blogs', CAST(0x0000A32901216CFA AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'd2ff89ff-a522-4f67-bdba-1c0b7898bea3', N'585fb409-e6f4-4303-b136-960985bde61c', N'Blogs', CAST(0x0000A32901209DB0 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'4fd4adfb-a6a7-49dc-a4ce-1ed52c3f499b', N'4167ffed-3784-439f-a095-cab996cb2228', N'Shows', CAST(0x0000A3290120445B AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'404223ed-ba17-4412-86c6-23e3eea15686', N'c7bdc4e1-7bca-4790-a16d-2c13b195eaad', N'Pinturas y acabados', CAST(0x0000A329011F6598 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'1b45b247-d8d8-4e78-88df-2aa64a05201e', N'c7bdc4e1-7bca-4790-a16d-2c13b195eaad', N'Otros', CAST(0x0000A329011F6E31 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'fd30b848-4b6d-4351-bde5-30963bbfcd60', N'4167ffed-3784-439f-a095-cab996cb2228', N'Ideas únicas', CAST(0x0000A329011FE979 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'0bcda11c-0e6d-4fbe-86e7-315c8f538576', N'585fb409-e6f4-4303-b136-960985bde61c', N'Atención Especial', CAST(0x0000A329012097D1 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'4c8dd004-0a00-4fa0-b808-3f4343553c2c', N'c7bdc4e1-7bca-4790-a16d-2c13b195eaad', N'Muebles', CAST(0x0000A329011F5C9E AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'9c9a00de-239d-4170-98ec-41cd6869eb65', N'3d22bbb0-33b7-456a-8bd7-c0d6cb9baa10', N'Culturales', CAST(0x0000A329012114C1 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'346acc84-7f97-4352-9f9a-465427745004', N'ca734dfb-3b3b-4c7d-b464-296b8fa5d4c7', N'Pañales', CAST(0x0000A329011F9D6F AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'7dd1c744-be0c-4981-9421-4abd60c461f0', N'f6869d3b-a3fc-4990-b326-cbab4145cb51', N'Revistas', CAST(0x0000A3290120EEBA AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'6c7a5030-279e-4ccc-87b3-4b8cf857bab4', N'4167ffed-3784-439f-a095-cab996cb2228', N'Payasos y Magos', CAST(0x0000A32901203850 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'372c3426-07b3-48a0-b680-53a07e9e86dc', N'4167ffed-3784-439f-a095-cab996cb2228', N'Blogs', CAST(0x0000A329011FC0DB AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'7067c19c-c9e0-4d65-8af9-54b8b9f52fd9', N'ca734dfb-3b3b-4c7d-b464-296b8fa5d4c7', N'Baño', CAST(0x0000A329011F8760 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'9b6f5e38-72d9-44de-89c3-55b92ace0f95', N'4167ffed-3784-439f-a095-cab996cb2228', N'Foto y video', CAST(0x0000A329011FE1CB AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'3648b804-c40d-4abd-845e-5648c186a586', N'585fb409-e6f4-4303-b136-960985bde61c', N'Artículos', CAST(0x0000A3290120ABB2 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'7e53706d-9234-4eee-96a6-570417e0e4a9', N'c7bdc4e1-7bca-4790-a16d-2c13b195eaad', N'Hazlo tu mismo', CAST(0x0000A329011F52E7 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'e22f32ab-d211-4cdf-aa28-583370aaba26', N'4167ffed-3784-439f-a095-cab996cb2228', N'Pasteles', CAST(0x0000A32901202C45 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'96c867c4-a4bc-44eb-a0cc-6386a4822f79', N'4167ffed-3784-439f-a095-cab996cb2228', N'Fiestas temáticas', CAST(0x0000A329011FD973 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'3768cc54-953c-40df-9ce1-6e282dafee25', N'585fb409-e6f4-4303-b136-960985bde61c', N'Idiomas', CAST(0x0000A3290120B1DF AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'251c6a9a-b29a-4f28-8c83-6f73801d4a76', N'7d202c2b-6422-46f6-8cc6-637d8faa4a00', N'Juguetes', CAST(0x0000A32901218B6E AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'ce8aa36e-accd-4385-8605-70d013e6e028', N'4167ffed-3784-439f-a095-cab996cb2228', N'Disfraces', CAST(0x0000A329011FCC71 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'13a7b439-49b8-48bf-ab8e-74b6feaa571d', N'4167ffed-3784-439f-a095-cab996cb2228', N'Piñatas', CAST(0x0000A32901203F53 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'3df45fba-82bd-4318-b8ca-77fa4f0f1010', N'7d202c2b-6422-46f6-8cc6-637d8faa4a00', N'Biberones y chupones', CAST(0x0000A3290121678E AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'e4d107e9-9393-45dd-9275-7fd864c92677', N'4167ffed-3784-439f-a095-cab996cb2228', N'Banquetes', CAST(0x0000A329011FB346 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'dc209a2b-248d-4db1-8606-847f47ee00c8', N'4167ffed-3784-439f-a095-cab996cb2228', N'Organizadores', CAST(0x0000A32901201D9B AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'b08cff78-74ef-4296-a415-8a1ac1332f59', N'f6869d3b-a3fc-4990-b326-cbab4145cb51', N'Blogs', CAST(0x0000A3290120F423 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'e36c1aa5-4a62-4978-852b-8a7c0e4f375e', N'74c496ba-fdbe-4d4d-86dc-0b6f79302bae', N'0-24 meses', CAST(0x0000A3290120DAAA AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'82794a52-18ef-4b98-bf6c-9233ade45399', N'74c496ba-fdbe-4d4d-86dc-0b6f79302bae', N'Accesorios', CAST(0x0000A3290120BFA3 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'f4269e71-fffb-4836-bdf6-947c5c464724', N'4167ffed-3784-439f-a095-cab996cb2228', N'Parques', CAST(0x0000A32901202678 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'7778041a-dd52-4d32-a052-951b44dffbe0', N'4167ffed-3784-439f-a095-cab996cb2228', N'Inflables', CAST(0x0000A329011FF10A AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'65a77a72-04f4-42f0-8a03-951bd71e79be', N'c7bdc4e1-7bca-4790-a16d-2c13b195eaad', N'Artículos', CAST(0x0000A329011F34B7 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'9424ae82-c444-4566-8d32-9935a24df87f', N'3d22bbb0-33b7-456a-8bd7-c0d6cb9baa10', N'Gimnasios y estimulación', CAST(0x0000A329012128BE AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'1a5ddbe7-9571-4d1f-8b64-a58aea42b97c', N'ca734dfb-3b3b-4c7d-b464-296b8fa5d4c7', N'Alimentación', CAST(0x0000A329011F7FB3 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'06e833b7-4aaf-476b-8501-a88edb8961b2', N'ca734dfb-3b3b-4c7d-b464-296b8fa5d4c7', N'Lactancia', CAST(0x0000A329011F975B AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'ec9ef501-3558-4ab7-b061-bc0380eea876', N'7d202c2b-6422-46f6-8cc6-637d8faa4a00', N'Carriolas', CAST(0x0000A329012173C5 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'5b721ed9-f332-469c-b026-bf98ba8fed01', N'4167ffed-3784-439f-a095-cab996cb2228', N'Jardines y Salones', CAST(0x0000A329011FFF75 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'21976bbf-fb56-4c6d-9919-c33dbb270905', N'4167ffed-3784-439f-a095-cab996cb2228', N'Mobiliario', CAST(0x0000A32901200ED7 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'51cd79de-5e15-4d36-ac8b-cea269c27372', N'74c496ba-fdbe-4d4d-86dc-0b6f79302bae', N'2-6 años', CAST(0x0000A3290120E214 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'c72ee815-84b4-477a-98e3-da04a7db9aab', N'3d22bbb0-33b7-456a-8bd7-c0d6cb9baa10', N'Blogs', CAST(0x0000A32901210DA3 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'9ab825f8-0be3-47c9-adfa-da3710c800a2', N'c7bdc4e1-7bca-4790-a16d-2c13b195eaad', N'Diseño de interiores', CAST(0x0000A329011F480A AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'877d029e-cf47-4dcc-ab50-da7bf2f2bc04', N'585fb409-e6f4-4303-b136-960985bde61c', N'Libros', CAST(0x0000A3290120A3DB AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'90202564-d2f7-4698-8038-e7be477b6719', N'4167ffed-3784-439f-a095-cab996cb2228', N'Bautizo', CAST(0x0000A329011FBA99 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'69bf4b85-dec2-469e-853d-ed33b60d8da6', N'4167ffed-3784-439f-a095-cab996cb2228', N'Música y animación', CAST(0x0000A329012016A7 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[Service] ([ID], [ProductID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'8931b2a3-3ab4-41bd-b5ba-ee67b14b02a5', N'7d202c2b-6422-46f6-8cc6-637d8faa4a00', N'Esterilizadores y calentadores', CAST(0x0000A32901218488 AS DateTime), N'dcruz', NULL, NULL, 1)
