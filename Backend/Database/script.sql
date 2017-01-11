USE [master]
GO
/****** Object:  Database [noriexpress]    Script Date: 1/11/2017 7:14:53 AM ******/
CREATE DATABASE [noriexpress] ON  PRIMARY 
( NAME = N'noriexpress', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\noriexpress.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'noriexpress_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\noriexpress_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [noriexpress].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [noriexpress] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [noriexpress] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [noriexpress] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [noriexpress] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [noriexpress] SET ARITHABORT OFF 
GO
ALTER DATABASE [noriexpress] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [noriexpress] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [noriexpress] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [noriexpress] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [noriexpress] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [noriexpress] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [noriexpress] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [noriexpress] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [noriexpress] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [noriexpress] SET  DISABLE_BROKER 
GO
ALTER DATABASE [noriexpress] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [noriexpress] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [noriexpress] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [noriexpress] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [noriexpress] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [noriexpress] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [noriexpress] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [noriexpress] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [noriexpress] SET  MULTI_USER 
GO
ALTER DATABASE [noriexpress] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [noriexpress] SET DB_CHAINING OFF 
GO
USE [noriexpress]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Articles]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articles](
	[ArticleId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Description] [nvarchar](1000) NULL,
	[Content] [nvarchar](max) NULL,
	[Keyword] [nvarchar](200) NULL,
	[Active] [bit] NOT NULL,
	[Order] [smallint] NOT NULL,
	[MainImage] [nvarchar](200) NULL,
	[Banner] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[PublicDate] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[PageURL] [nvarchar](max) NULL,
	[BannerImage] [nvarchar](200) NULL,
 CONSTRAINT [PK_dbo.Articles] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Banners]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banners](
	[BannerId] [int] IDENTITY(1,1) NOT NULL,
	[Link] [nvarchar](200) NULL,
	[Filename] [nvarchar](200) NULL,
	[Heading1] [nvarchar](200) NULL,
	[Heading2] [nvarchar](200) NULL,
	[Description] [nvarchar](1000) NULL,
	[Tag] [nvarchar](200) NULL,
	[Align] [nvarchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Banners] PRIMARY KEY CLUSTERED 
(
	[BannerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Carts]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carts](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[CartId] [nvarchar](max) NULL,
	[ProductId] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Carts] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Description] [nvarchar](1000) NULL,
	[Tag] [nvarchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
	[PageURL] [nvarchar](max) NULL,
	[SortNumber] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comments]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](200) NULL,
	[JobTitle] [nvarchar](200) NULL,
	[Description] [nvarchar](1000) NULL,
	[Tag] [nvarchar](200) NULL,
	[Order] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Comments] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FAQs]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAQs](
	[FAQId] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](500) NULL,
	[Author] [nvarchar](200) NULL,
	[Description] [nvarchar](4000) NULL,
	[Tag] [nvarchar](200) NULL,
	[Order] [int] NOT NULL,
 CONSTRAINT [PK_dbo.FAQs] PRIMARY KEY CLUSTERED 
(
	[FAQId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menus]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NOT NULL,
	[Text] [nvarchar](200) NULL,
	[Link] [nvarchar](200) NULL,
	[Tag] [nvarchar](200) NULL,
	[Order] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Menus] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Messages]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Content] [nvarchar](2000) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Messages] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_dbo.OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[PostalCode] [nvarchar](max) NULL,
	[Country] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[Note] [nvarchar](max) NULL,
	[PaymentType] [decimal](18, 2) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pictures]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pictures](
	[PictureId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Src] [nvarchar](200) NULL,
	[Tags] [nvarchar](200) NULL,
	[IsCover] [bit] NOT NULL,
	[MoreInfo] [nvarchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Size] [float] NOT NULL,
 CONSTRAINT [PK_dbo.Pictures] PRIMARY KEY CLUSTERED 
(
	[PictureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Code] [nvarchar](200) NULL,
	[CountryId] [int] NOT NULL,
	[BrandId] [int] NOT NULL,
	[MaterialId] [int] NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Content] [nvarchar](max) NULL,
	[Tags] [nvarchar](200) NULL,
	[Active] [bit] NOT NULL,
	[IsSpecial] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[NumofViews] [int] NOT NULL,
	[BasePrice] [int] NOT NULL,
	[Discount] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[MainImage] [nvarchar](200) NULL,
	[Folder] [nvarchar](50) NULL,
	[ImagesJson] [nvarchar](max) NULL,
	[CategoryId] [int] NOT NULL,
	[PageURL] [nvarchar](max) NULL,
	[Brand_CategoryId] [int] NULL,
	[Category_CategoryId] [int] NULL,
	[Country_CategoryId] [int] NULL,
	[Material_CategoryId] [int] NULL,
	[Avatar] [nvarchar](200) NULL,
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QuickLinks]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuickLinks](
	[QuickLinkId] [int] IDENTITY(1,1) NOT NULL,
	[Link] [nvarchar](200) NULL,
	[Filename] [nvarchar](200) NULL,
	[Title] [nvarchar](200) NULL,
	[Order] [int] NOT NULL,
	[Tag] [nvarchar](200) NULL,
	[Description] [nvarchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.QuickLinks] PRIMARY KEY CLUSTERED 
(
	[QuickLinkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserProfiles]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfiles](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[ConfirmPassword] [nvarchar](max) NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UserProfiles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Videos]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Videos](
	[VideoId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[Link] [nvarchar](200) NULL,
	[Filename] [nvarchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Videos] PRIMARY KEY CLUSTERED 
(
	[VideoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_CategoryId]    Script Date: 1/11/2017 7:14:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_CategoryId] ON [dbo].[Articles]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductId]    Script Date: 1/11/2017 7:14:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductId] ON [dbo].[Carts]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderId]    Script Date: 1/11/2017 7:14:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_OrderId] ON [dbo].[OrderDetails]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductId]    Script Date: 1/11/2017 7:14:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductId] ON [dbo].[OrderDetails]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductId]    Script Date: 1/11/2017 7:14:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductId] ON [dbo].[Pictures]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Brand_CategoryId]    Script Date: 1/11/2017 7:14:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Brand_CategoryId] ON [dbo].[Products]
(
	[Brand_CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Category_CategoryId]    Script Date: 1/11/2017 7:14:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Category_CategoryId] ON [dbo].[Products]
(
	[Category_CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Country_CategoryId]    Script Date: 1/11/2017 7:14:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Country_CategoryId] ON [dbo].[Products]
(
	[Country_CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Material_CategoryId]    Script Date: 1/11/2017 7:14:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Material_CategoryId] ON [dbo].[Products]
(
	[Material_CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Articles_dbo.Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_dbo.Articles_dbo.Categories_CategoryId]
GO
ALTER TABLE [dbo].[Carts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Carts_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Carts] CHECK CONSTRAINT [FK_dbo.Carts_dbo.Products_ProductId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_dbo.OrderDetails_dbo.Orders_OrderId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrderDetails_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_dbo.OrderDetails_dbo.Products_ProductId]
GO
ALTER TABLE [dbo].[Pictures]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Pictures_dbo.Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pictures] CHECK CONSTRAINT [FK_dbo.Pictures_dbo.Products_ProductId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Products_dbo.Categories_Brand_CategoryId] FOREIGN KEY([Brand_CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_dbo.Products_dbo.Categories_Brand_CategoryId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Products_dbo.Categories_Category_CategoryId] FOREIGN KEY([Category_CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_dbo.Products_dbo.Categories_Category_CategoryId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Products_dbo.Categories_Country_CategoryId] FOREIGN KEY([Country_CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_dbo.Products_dbo.Categories_Country_CategoryId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Products_dbo.Categories_Material_CategoryId] FOREIGN KEY([Material_CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_dbo.Products_dbo.Categories_Material_CategoryId]
GO
/****** Object:  StoredProcedure [dbo].[getTopProduct]    Script Date: 1/11/2017 7:14:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getTopProduct] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT p.ProductId, p.MainImage, c.CategoryId, c.Name as CategoryName,   p.Price, p.Code, p.PageURL, c.PageURL as CategoryPageURL 
	from Products p
	inner join Categories c on p.CategoryId = c.CategoryId
	order by c.CategoryId, p.ProductId


END

GO
USE [master]
GO
ALTER DATABASE [noriexpress] SET  READ_WRITE 
GO
