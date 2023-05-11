USE [SirketOtomasyonu]
GO
/****** Object:  Table [dbo].[Birim]    Script Date: 5/11/2023 8:46:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Birim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Adi] [nvarchar](50) NULL,
	[Aciklama] [nvarchar](50) NULL,
 CONSTRAINT [PK_Birim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personel]    Script Date: 5/11/2023 8:46:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BirimId] [int] NOT NULL,
	[Adi] [nvarchar](50) NOT NULL,
	[Soyadi] [nvarchar](50) NOT NULL,
	[Tel1] [nvarchar](50) NULL,
	[Tel2] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Adres] [nvarchar](50) NULL,
	[Maas] [decimal](18, 2) NULL,
	[Resim] [nvarchar](50) NULL,
	[Aciklama] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[GirisTarihi] [date] NULL,
 CONSTRAINT [PK_Personel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Personel] ADD  CONSTRAINT [DF_Personel_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
