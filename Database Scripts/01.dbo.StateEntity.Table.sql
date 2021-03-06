USE [BebeclickStorage]
GO

/****** Object:  Table [dbo].[Product]    Script Date: 05/08/2014 08:37:27 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Product_StateEntity]') AND parent_object_id = OBJECT_ID(N'[dbo].[Product]'))
	ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_StateEntity]
GO

IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_StateEntity_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[StateEntity]'))
BEGIN
	IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_StateEntity_ID]') AND type = 'D')
	BEGIN
		ALTER TABLE [dbo].[StateEntity] DROP CONSTRAINT [DF_StateEntity_ID]
	END
END
GO

IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_StateEntity_DateCreated]') AND parent_object_id = OBJECT_ID(N'[dbo].[StateEntity]'))
BEGIN
	IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_StateEntity_DateCreated]') AND type = 'D')
	BEGIN
		ALTER TABLE [dbo].[StateEntity] DROP CONSTRAINT [DF_StateEntity_DateCreated]
	END
END
GO

IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_StateEntity_Visible]') AND parent_object_id = OBJECT_ID(N'[dbo].[StateEntity]'))
BEGIN
	IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_StateEntity_Visible]') AND type = 'D')
	BEGIN
		ALTER TABLE [dbo].[StateEntity] DROP CONSTRAINT [DF_StateEntity_Visible]
	END
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StateEntity]') AND type in (N'U'))
BEGIN
	DROP TABLE [dbo].[StateEntity]
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StateEntity]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[StateEntity](
		[ID] [uniqueidentifier] NOT NULL,
		[Name] [nvarchar](100) NOT NULL,
		[DateCreated] [datetime] NOT NULL,
		[CreatedBy] [nvarchar](256) NOT NULL,
		[LastUpdated] [datetime] NULL,
		[LastUpdatedBy] [nvarchar](256) NULL,
		[Visible] bit NOT NULL,
	 CONSTRAINT [PK_StateEntity] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

ALTER TABLE [dbo].[StateEntity] ADD  CONSTRAINT [DF_StateEntity_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[StateEntity] ADD  CONSTRAINT [DF_StateEntity_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

ALTER TABLE [dbo].[StateEntity] ADD  CONSTRAINT [DF_StateEntity_Visible]  DEFAULT (0) FOR [Visible]
GO

INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'9fdfc32d-1174-4727-9138-f528a4d48c12', N'Aguascalientes', CAST(0x0000A32400FF22D4 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'c98d0bdf-7c5b-4ce5-8aef-fa479f382fc5', N'Baja California', CAST(0x0000A32400FF3242 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'd5af39eb-e9a2-4d1f-8b9c-e257f2c79780', N'Baja California Sur', CAST(0x0000A32400FF3CA2 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'9cfc7e7f-a8f3-45ad-a9ae-ee536a0b5b67', N'Campeche', CAST(0x0000A32400FF5BA5 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'd8f092e6-d199-4bf2-9e5b-89d3000f6bc0', N'Chiapas', CAST(0x0000A32400FF6315 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'e1f1558c-ebce-47d1-b320-d40720da454b', N'Chihuahua', CAST(0x0000A32400FF6C31 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'8094d579-9aa9-4f74-823a-a27d03612c25', N'Coahuila de Zaragoza', CAST(0x0000A32400FF8C7E AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'cfe983c5-c252-40e0-b463-c71f118f2845', N'Colima', CAST(0x0000A32400FF9338 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'a6435448-991e-44ba-88af-3a52780cbbf6', N'Distrito Federal', CAST(0x0000A32400FF9B49 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'b00bf755-ce7e-41da-9eb4-98c576e0e9fe', N'Durango', CAST(0x0000A32400FFA041 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'c102566f-09ad-4c2e-b88a-ee7eb7a205e9', N'Guanajuato', CAST(0x0000A32400FFB041 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'3115cb8c-f712-4ff1-a93c-16774673c034', N'Guerrero', CAST(0x0000A32400FFB529 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'4d0765de-36b8-484e-8242-f3c630a75561', N'Hidalgo', CAST(0x0000A32400FFBB0B AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'9b6da83a-e3ff-450b-a56e-e21d6646b40e', N'Jalisco', CAST(0x0000A32400FFC17C AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'01edce0c-3a84-457a-bc51-b0cb0e9358a8', N'México', CAST(0x0000A32400FFC726 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'e094251e-7afa-4e60-99fe-1fe263c1b322', N'Michoacán de Ocampo', CAST(0x0000A32400FFCE64 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'740b0e58-5641-4f8a-b05a-303fe30d0b8e', N'Morelos', CAST(0x0000A32400FFD455 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'021c0eff-aafa-4077-a5bc-1ea58a107f06', N'Nayarit', CAST(0x0000A32400FFDFCE AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'cc796320-1833-4540-8155-b0688aea87c0', N'Nuevo León', CAST(0x0000A32400FFE680 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'da4008e4-dde4-45d1-8524-229a1de77572', N'Oaxaca', CAST(0x0000A32400FFEC72 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'a1609f06-e124-4477-9ff8-b52f4e465a71', N'Puebla', CAST(0x0000A32400FFF1E8 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'b0c048d5-e230-454b-b7e4-c4e0726110a4', N'Querétaro', CAST(0x0000A32400FFF6F2 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'c84384af-d6a6-4cc9-8992-d27f6582af01', N'Quintana Roo', CAST(0x0000A324010002E2 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'a2c5c64e-bb08-4716-8651-3e6ff713a335', N'San Luis Potosí', CAST(0x0000A32401000A05 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'ca10c32c-d395-4e19-b18f-2107fa50a28c', N'Sinaloa', CAST(0x0000A32401000FD1 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'8b46bb62-7583-447d-9dea-ee41b7ff6012', N'Sonora', CAST(0x0000A3240100149B AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'f70da953-6071-4dbf-94f3-61813989699f', N'Tabasco', CAST(0x0000A32401001A04 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'7df31c8d-8da0-48a8-a7c4-d2dfa6f97161', N'Tamaulipas', CAST(0x0000A3240100234C AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'c78a09f3-3578-4ab1-8414-6c0fda9eafb4', N'Tlaxcala', CAST(0x0000A32401002A7C AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'd491cc02-a5a4-4c10-b7cc-e2459282e772', N'Veracruz de Ignacio de la Llave', CAST(0x0000A3240100349F AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'12228e5b-5d9c-4fa3-afbd-4b37ab767163', N'Yucatán', CAST(0x0000A32401003C49 AS DateTime), N'dcruz', NULL, NULL, 1)
INSERT [dbo].[StateEntity] ([ID], [Name], [DateCreated], [CreatedBy], [LastUpdated], [LastUpdatedBy], [Visible]) VALUES (N'9e62829d-1078-4904-924f-e24a9c181c9d', N'Zacatecas', CAST(0x0000A324010043EB AS DateTime), N'dcruz', NULL, NULL, 1)
