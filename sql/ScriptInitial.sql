USE [WoopiSeguranca]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/10/2022 18:47:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionarioUsuario]    Script Date: 11/10/2022 18:47:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionarioUsuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[UserLoginId] [int] NOT NULL,
	[DataCriacao] [datetime2](7) NOT NULL,
	[Pergunta] [nvarchar](max) NOT NULL,
	[Resposta] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_QuestionarioUsuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 11/10/2022 18:47:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Senha] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLoginHistory]    Script Date: 11/10/2022 18:47:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLoginHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataCriacao] [datetime2](7) NOT NULL,
	[UserLoginId] [int] NOT NULL,
	[Senha] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_UserLoginHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[QuestionarioUsuario]  WITH CHECK ADD  CONSTRAINT [FK_QuestionarioUsuario_UserLogin_UserLoginId] FOREIGN KEY([UserLoginId])
REFERENCES [dbo].[UserLogin] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuestionarioUsuario] CHECK CONSTRAINT [FK_QuestionarioUsuario_UserLogin_UserLoginId]
GO
ALTER TABLE [dbo].[UserLoginHistory]  WITH CHECK ADD  CONSTRAINT [FK_UserLoginHistory_UserLogin_UserLoginId] FOREIGN KEY([UserLoginId])
REFERENCES [dbo].[UserLogin] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLoginHistory] CHECK CONSTRAINT [FK_UserLoginHistory_UserLogin_UserLoginId]
GO
