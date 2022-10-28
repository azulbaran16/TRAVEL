USE [master]
GO

/****** Object:  Database [TRAVEL]    Script Date: 27/10/2022 10:22:29 p. m. ******/
CREATE DATABASE [TRAVEL]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TRAVEL', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TRAVEL.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TRAVEL_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\TRAVEL_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TRAVEL].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [TRAVEL] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [TRAVEL] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [TRAVEL] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [TRAVEL] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [TRAVEL] SET ARITHABORT OFF 
GO

ALTER DATABASE [TRAVEL] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [TRAVEL] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [TRAVEL] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [TRAVEL] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [TRAVEL] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [TRAVEL] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [TRAVEL] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [TRAVEL] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [TRAVEL] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [TRAVEL] SET  DISABLE_BROKER 
GO

ALTER DATABASE [TRAVEL] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [TRAVEL] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [TRAVEL] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [TRAVEL] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [TRAVEL] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [TRAVEL] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [TRAVEL] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [TRAVEL] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [TRAVEL] SET  MULTI_USER 
GO

ALTER DATABASE [TRAVEL] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [TRAVEL] SET DB_CHAINING OFF 
GO

ALTER DATABASE [TRAVEL] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [TRAVEL] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [TRAVEL] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [TRAVEL] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [TRAVEL] SET QUERY_STORE = OFF
GO

ALTER DATABASE [TRAVEL] SET  READ_WRITE 
GO
USE [TRAVEL]
GO
/****** Object:  Table [dbo].[autores]    Script Date: 27/10/2022 10:22:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[autores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](45) NOT NULL,
	[apellidos] [varchar](45) NOT NULL,
 CONSTRAINT [PK_autores] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[autores_has_libros]    Script Date: 27/10/2022 10:22:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[autores_has_libros](
	[autores_id] [int] NOT NULL,
	[libros_ISBN] [int] NOT NULL,
 CONSTRAINT [PK_autores_has_libros] PRIMARY KEY CLUSTERED 
(
	[autores_id] ASC,
	[libros_ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[editoriales]    Script Date: 27/10/2022 10:22:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[editoriales](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](45) NOT NULL,
	[sede] [varchar](45) NOT NULL,
 CONSTRAINT [PK_editoriales] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[libros]    Script Date: 27/10/2022 10:22:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[libros](
	[ISBN] [int] IDENTITY(1,1) NOT NULL,
	[editoriales_id] [int] NOT NULL,
	[titulo] [varchar](45) NOT NULL,
	[sinopsis] [text] NOT NULL,
	[n_paginas] [varchar](45) NOT NULL,
 CONSTRAINT [PK_libros] PRIMARY KEY CLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[libros]  WITH CHECK ADD  CONSTRAINT [FK_libros_editoriales] FOREIGN KEY([editoriales_id])
REFERENCES [dbo].[editoriales] ([id])
GO
ALTER TABLE [dbo].[libros] CHECK CONSTRAINT [FK_libros_editoriales]
GO
