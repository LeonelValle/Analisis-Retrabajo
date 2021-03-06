USE [master]
GO
/****** Object:  Database [ayrDB]    Script Date: 11/19/2020 8:52:36 AM ******/
CREATE DATABASE [ayrDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ayrDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ayrDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ayrDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ayrDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ayrDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ayrDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ayrDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ayrDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ayrDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ayrDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ayrDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ayrDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ayrDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ayrDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ayrDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ayrDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ayrDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ayrDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ayrDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ayrDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ayrDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ayrDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ayrDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ayrDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ayrDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ayrDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ayrDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ayrDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ayrDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ayrDB] SET  MULTI_USER 
GO
ALTER DATABASE [ayrDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ayrDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ayrDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ayrDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ayrDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ayrDB] SET QUERY_STORE = OFF
GO
USE [ayrDB]
GO
/****** Object:  Table [dbo].[BITACORA]    Script Date: 11/19/2020 8:52:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BITACORA](
	[FK_Unidad] [int] NULL,
	[_Status] [varchar](50) NULL,
	[_Turno] [varchar](50) NULL,
	[_NumEmpleado] [varchar](50) NULL,
	[_Fecha] [datetime] NULL,
	[Falla] [varchar](max) NULL,
	[Defectos] [varchar](max) NULL,
	[Referencias] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_AREA]    Script Date: 11/19/2020 8:52:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_AREA](
	[Id_Area] [int] IDENTITY(1,1) NOT NULL,
	[Area] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Area] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_AREAINTERNA]    Script Date: 11/19/2020 8:52:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_AREAINTERNA](
	[Id_AreaInterna] [int] IDENTITY(1,1) NOT NULL,
	[AreaInt] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_AreaInterna] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_CLIENTE]    Script Date: 11/19/2020 8:52:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_CLIENTE](
	[Id_Cliente] [int] IDENTITY(1,1) NOT NULL,
	[Cliente] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_DEFECTO]    Script Date: 11/19/2020 8:52:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_DEFECTO](
	[Id_Defecto] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](30) NULL,
	[Descripcion] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Defecto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_NUMPARTE]    Script Date: 11/19/2020 8:52:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_NUMPARTE](
	[Id_NumParte] [int] IDENTITY(1,1) NOT NULL,
	[Num_Parte] [varchar](50) NULL,
	[Precio_unitario] [float] NULL,
	[FK_Cliente] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_NumParte] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_STATUS]    Script Date: 11/19/2020 8:52:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_STATUS](
	[Id_Status] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_status] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAT_TIPOUSUARIO]    Script Date: 11/19/2020 8:52:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAT_TIPOUSUARIO](
	[Id_TipoUsuario] [int] IDENTITY(1,1) NOT NULL,
	[TipoUsuario] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_TipoUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UNIDAD]    Script Date: 11/19/2020 8:52:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UNIDAD](
	[Id_Unidad] [int] IDENTITY(1,1) NOT NULL,
	[Work_Order] [varchar](30) NULL,
	[FK_Area] [int] NULL,
	[FK_PartNumber] [int] NULL,
	[Serial_Number] [varchar](50) NULL,
	[FK_Status] [int] NULL,
	[Falla] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Unidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UNIDAD_DEFECTO]    Script Date: 11/19/2020 8:52:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UNIDAD_DEFECTO](
	[FK_Unidad] [int] NOT NULL,
	[FK_Defecto] [int] NOT NULL,
	[Referencia] [varchar](30) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIO]    Script Date: 11/19/2020 8:52:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIO](
	[Id_Usuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Empleado] [varchar](50) NULL,
	[username] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[FK_AreaInt] [int] NULL,
	[FK_TipoUsuario] [int] NULL,
	[Num_Empleado] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BITACORA]  WITH CHECK ADD FOREIGN KEY([FK_Unidad])
REFERENCES [dbo].[UNIDAD] ([Id_Unidad])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CAT_NUMPARTE]  WITH CHECK ADD FOREIGN KEY([FK_Cliente])
REFERENCES [dbo].[CAT_CLIENTE] ([Id_Cliente])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UNIDAD]  WITH CHECK ADD FOREIGN KEY([FK_Area])
REFERENCES [dbo].[CAT_AREA] ([Id_Area])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UNIDAD]  WITH CHECK ADD FOREIGN KEY([FK_PartNumber])
REFERENCES [dbo].[CAT_NUMPARTE] ([Id_NumParte])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UNIDAD]  WITH CHECK ADD FOREIGN KEY([FK_Status])
REFERENCES [dbo].[CAT_STATUS] ([Id_Status])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UNIDAD_DEFECTO]  WITH CHECK ADD FOREIGN KEY([FK_Defecto])
REFERENCES [dbo].[CAT_DEFECTO] ([Id_Defecto])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UNIDAD_DEFECTO]  WITH CHECK ADD FOREIGN KEY([FK_Unidad])
REFERENCES [dbo].[UNIDAD] ([Id_Unidad])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD FOREIGN KEY([FK_AreaInt])
REFERENCES [dbo].[CAT_AREAINTERNA] ([Id_AreaInterna])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[USUARIO]  WITH CHECK ADD FOREIGN KEY([FK_TipoUsuario])
REFERENCES [dbo].[CAT_TIPOUSUARIO] ([Id_TipoUsuario])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
USE [master]
GO
ALTER DATABASE [ayrDB] SET  READ_WRITE 
GO
