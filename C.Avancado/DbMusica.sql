CREATE DATABASE [DbMusica]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [DbMusica].[dbo].[Musica](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](50) NOT NULL,
	[Cantor] [varchar](50) NULL,
	[Album] [varchar](50) NULL,
	[Ano] [int] NULL,
	[Genero] [varchar](50) NULL,
 CONSTRAINT [PK_Musica] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

DELETE FROM [DbMusica].[dbo].[Musica]

DBCC CHECKIDENT ('[DbMusica].[dbo].[Musica]', RESEED, 0);
