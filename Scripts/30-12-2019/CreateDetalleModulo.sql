USE [BDDicon]
GO

/****** Object:  Table [dbo].[DetalleModulo]    Script Date: 27/12/2019 06:17:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DetalleModulo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[modulosid] [int] NULL,
	[modulo] [int] NULL
) ON [PRIMARY]

GO


