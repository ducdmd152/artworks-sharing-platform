USE [master]
GO
/****** Object:  Database [ArtHub]    Script Date: 4/4/2024 8:06:19 PM ******/
CREATE DATABASE [ArtHub]
GO
USE [ArtHub]
GO
/****** Object:  Table [dbo].[account]    Script Date: 4/4/2024 8:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[account](
	[email] [varchar](256) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[first_name] [nvarchar](256) NOT NULL,
	[last_name] [nvarchar](100) NOT NULL,
	[gender] [varchar](100) NOT NULL,
	[status] [int] NOT NULL,
	[enabled] [bit] NOT NULL,
	[avatar] [varchar](256) NULL,
	[role_id] [int] NOT NULL,
	[created_date] [datetime] NOT NULL,
	[updated_date] [datetime] NOT NULL,
 CONSTRAINT [account_pk] PRIMARY KEY CLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[artist]    Script Date: 4/4/2024 8:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[artist](
	[email] [varchar](256) NOT NULL,
	[artist_name] [nvarchar](256) NOT NULL,
	[bio] [nvarchar](512) NULL,
	[total_subscribe] [int] NOT NULL,
	[created_date] [datetime] NOT NULL,
	[updated_date] [datetime] NOT NULL,
 CONSTRAINT [artist_pk] PRIMARY KEY CLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bookmark]    Script Date: 4/4/2024 8:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bookmark](
	[bookmark_id] [int] IDENTITY(1,1) NOT NULL,
	[delete_flag] [bit] NOT NULL,
	[post_id] [int] NOT NULL,
	[account_email] [varchar](256) NOT NULL,
	[created_date] [datetime] NOT NULL,
	[updated_date] [datetime] NOT NULL,
 CONSTRAINT [bookmark_pk] PRIMARY KEY CLUSTERED 
(
	[bookmark_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category]    Script Date: 4/4/2024 8:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [varchar](100) NOT NULL,
	[description] [nvarchar](512) NOT NULL,
	[created_date] [datetime] NOT NULL,
	[updated_date] [datetime] NOT NULL,
 CONSTRAINT [category_pk] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[fee]    Script Date: 4/4/2024 8:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fee](
	[fee_id] [int] IDENTITY(1,1) NOT NULL,
	[amount] [float] NOT NULL,
	[artist_email] [varchar](256) NOT NULL,
	[created_date] [datetime] NOT NULL,
	[updated_date] [datetime] NOT NULL,
 CONSTRAINT [fee_pk] PRIMARY KEY CLUSTERED 
(
	[fee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[image]    Script Date: 4/4/2024 8:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[image](
	[image_id] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](100) NOT NULL,
	[image_url] [varchar](256) NOT NULL,
	[delete_flag] [bit] NOT NULL,
	[post_id] [int] NOT NULL,
	[created_date] [datetime] NOT NULL,
	[updated_date] [datetime] NOT NULL,
 CONSTRAINT [image_pk] PRIMARY KEY CLUSTERED 
(
	[image_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[post]    Script Date: 4/4/2024 8:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[post](
	[post_id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](256) NOT NULL,
	[description] [nvarchar](512) NOT NULL,
	[status] [int] NOT NULL,
	[scope] [int] NOT NULL,
	[total_react] [int] NOT NULL,
	[total_view] [int] NOT NULL,
	[total_bookmark] [int] NOT NULL,
	[artist_email] [varchar](256) NOT NULL,
	[created_date] [datetime] NOT NULL,
	[updated_date] [datetime] NOT NULL,
	[note] [ntext] NULL,
 CONSTRAINT [post_pk] PRIMARY KEY CLUSTERED 
(
	[post_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[post_category]    Script Date: 4/4/2024 8:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[post_category](
	[category_id] [int] NOT NULL,
	[post_id] [int] NOT NULL,
	[created_date] [datetime] NOT NULL,
	[updated_date] [datetime] NOT NULL,
 CONSTRAINT [post_category_pk] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC,
	[post_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reaction]    Script Date: 4/4/2024 8:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reaction](
	[reaction_id] [int] IDENTITY(1,1) NOT NULL,
	[post_id] [int] NOT NULL,
	[account_email] [varchar](256) NOT NULL,
	[created_date] [datetime] NOT NULL,
	[updated_date] [datetime] NOT NULL,
 CONSTRAINT [reaction_pk] PRIMARY KEY CLUSTERED 
(
	[reaction_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[report]    Script Date: 4/4/2024 8:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[report](
	[report_id] [int] IDENTITY(1,1) NOT NULL,
	[reason] [nvarchar](256) NULL,
	[reporter_email] [varchar](256) NOT NULL,
	[post_id] [int] NOT NULL,
	[created_date] [datetime] NOT NULL,
	[updated_date] [datetime] NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [report_pk] PRIMARY KEY CLUSTERED 
(
	[report_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 4/4/2024 8:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](256) NOT NULL,
	[created_date] [datetime] NOT NULL,
	[updated_date] [datetime] NOT NULL,
 CONSTRAINT [role_pk] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[subscriber]    Script Date: 4/4/2024 8:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subscriber](
	[subscriber_id] [int] IDENTITY(1,1) NOT NULL,
	[email_user] [varchar](256) NOT NULL,
	[email_artist] [varchar](256) NOT NULL,
	[status] [int] NOT NULL,
	[expired_date] [datetime] NOT NULL,
	[created_date] [datetime] NOT NULL,
	[updated_date] [datetime] NOT NULL,
 CONSTRAINT [subscriber_pk] PRIMARY KEY CLUSTERED 
(
	[subscriber_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[system_config]    Script Date: 4/4/2024 8:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[system_config](
	[config_id] [int] IDENTITY(1,1) NOT NULL,
	[commision_rate] [float] NOT NULL,
	[created_date] [datetime] NOT NULL,
	[updated_date] [datetime] NOT NULL,
 CONSTRAINT [system_config_pk] PRIMARY KEY CLUSTERED 
(
	[config_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transaction]    Script Date: 4/4/2024 8:06:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transaction](
	[transaction_id] [int] IDENTITY(1,1) NOT NULL,
	[amount] [float] NOT NULL,
	[status] [int] NOT NULL,
	[type] [varchar](100) NOT NULL,
	[fee_id] [int] NOT NULL,
	[subscriber_id] [int] NOT NULL,
	[created_date] [datetime] NOT NULL,
	[updated_date] [datetime] NOT NULL,
	[subscription_paypal_id] [varchar](100) NULL,
 CONSTRAINT [transaction_pk] PRIMARY KEY CLUSTERED 
(
	[transaction_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'admin@gmail.com', N'5Hzghx8mdZs7r8BgULR4IQ==', N'Chau Doan', N'', N'Male', 1, 1, N'https://avatars.githubusercontent.com/u/90080923?v=4', 4, CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-03-29T16:18:31.573' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'creator@gmail.com', N'5Hzghx8mdZs7r8BgULR4IQ==', N'Alis Mary', N'', N'Female', 1, 1, N'https://plus.unsplash.com/premium_photo-1689551670902-19b441a6afde?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 2, CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-03-26T03:54:19.947' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'creator2@gmail.com', N'5Hzghx8mdZs7r8BgULR4IQ==', N'Susan', N'', N'Female', 1, 1, N'https://images.unsplash.com/photo-1548142813-c348350df52b?q=80&w=1889&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 2, CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-03-26T03:54:19.950' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'creator3@gmail.com', N'5Hzghx8mdZs7r8BgULR4IQ==', N'Halen Tra', N'', N'Female', 1, 1, N'https://images.unsplash.com/photo-1526413232644-8a40f03cc03b?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 2, CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-03-26T03:54:19.950' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'creator4@gmail.com', N'5Hzghx8mdZs7r8BgULR4IQ==', N'Nguyen Nguyen', N'', N'Female', 1, 1, N'https://images.unsplash.com/photo-1606849611912-b7e8a51d71d6?q=80&w=1771&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 2, CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-03-26T03:54:19.950' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'creator5@gmail.com', N'5Hzghx8mdZs7r8BgULR4IQ==', N'Levei Awro', N'', N'Male', 1, 1, N'https://images.unsplash.com/photo-1615109398623-88346a601842?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 2, CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-03-26T03:54:19.950' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'DavidWilliams@gmail.com', N'hsysMUxaHtWG5Mhd4ijaRQ==', N'David Williams', N'David Williams', N'Male', 1, 1, N'https://images.unsplash.com/photo-1499714608240-22fc6ad53fb2?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 2, CAST(N'2024-03-29T15:10:18.433' AS DateTime), CAST(N'2024-03-29T15:10:41.860' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'DavidWilliams2@gmail.com', N'hsysMUxaHtWG5Mhd4ijaRQ==', N'David Williams', N'David Williams', N'Male', 1, 1, N'https://images.unsplash.com/photo-1567784177951-6fa58317e16b?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 2, CAST(N'2024-03-29T15:10:18.433' AS DateTime), CAST(N'2024-03-29T15:10:41.860' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'DavidWilliams3@gmail.com', N'hsysMUxaHtWG5Mhd4ijaRQ==', N'David Williams', N'David Williams', N'Male', 1, 1, N'https://images.unsplash.com/photo-1591084728795-1149f32d9866?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 2, CAST(N'2024-03-29T15:10:18.433' AS DateTime), CAST(N'2024-03-29T15:10:41.860' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'ductien@gmail.com', N'5Hzghx8mdZs7r8BgULR4IQ==', N'Kidhood', N'', N'Male', 1, 1, N'https://images.unsplash.com/photo-1539571696357-5a69c17a67c6?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 2, CAST(N'2024-03-28T21:39:03.423' AS DateTime), CAST(N'2024-03-28T21:39:03.423' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'fwin@gmai.com', N'DYIPG+mvjflFGq9+ivj27w==', N'Finwe', N'Ng', N'Male', 1, 1, N'https://d28yx6l5j59h9f.cloudfront.net/Artwork/e0ed034d-1867-4ffd-9201-70af5186cff5.avif', 2, CAST(N'2024-03-29T15:06:39.600' AS DateTime), CAST(N'2024-03-29T15:37:55.867' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'Jessica@gmail.com', N'DYIPG+mvjflFGq9+ivj27w==', N'Jessica', N'Ng', N'Male', 1, 1, N'https://d28yx6l5j59h9f.cloudfront.net/Artwork/a0b478d0-66ad-429a-958d-684b2e681bd8.avif', 2, CAST(N'2024-03-29T12:50:08.080' AS DateTime), CAST(N'2024-03-29T13:28:50.863' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'mitolamit@gmail.com', N'DYIPG+mvjflFGq9+ivj27w==', N'Lam', N'Ng', N'Male', 1, 1, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTii3hM5abJEj_Zu0wumINDLGvHaT-MZaesc9dj-gXSUg&s', 2, CAST(N'2024-03-29T16:00:51.727' AS DateTime), CAST(N'2024-03-29T16:00:51.727' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'moderator@gmail.com', N'5Hzghx8mdZs7r8BgULR4IQ==', N'Van Thong', N'', N'Male', 1, 1, N'https://images.unsplash.com/photo-1464746133101-a2c3f88e0dd9?q=80&w=1743&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 3, CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-03-26T03:54:19.950' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'SarahJohnson@gmail.com', N'3SN36+nV661Mr316oOpxcQ==', N'Sarah Johnson', N'Sarah Johnson', N'Male', 1, 1, N'https://images.unsplash.com/photo-1506794778202-cad84cf45f1d?q=80&w=1887&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 2, CAST(N'2024-03-29T15:07:51.667' AS DateTime), CAST(N'2024-03-29T15:08:08.153' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'SarahJohnson2@gmail.com', N'3SN36+nV661Mr316oOpxcQ==', N'Sarah Johnson', N'Sarah Johnson', N'Male', 1, 1, N'https://images.unsplash.com/photo-1522529599102-193c0d76b5b6?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 2, CAST(N'2024-03-29T15:07:51.667' AS DateTime), CAST(N'2024-03-29T15:08:08.153' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'SarahJohnson3@gmail.com', N'3SN36+nV661Mr316oOpxcQ==', N'Sarah Johnson', N'Sarah Johnson', N'Male', 1, 1, N'https://d28yx6l5j59h9f.cloudfront.net/Artwork/c14b1342-e870-4822-9ea1-217105f6f3fb.jpg', 2, CAST(N'2024-03-29T15:07:51.667' AS DateTime), CAST(N'2024-03-29T15:08:08.153' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'test@gmail.com', N'3SN36+nV661Mr316oOpxcQ==', N'James ', N'Smith', N'Male', 1, 1, N'https://images.unsplash.com/photo-1507591064344-4c6ce005b128?q=80&w=1770&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 2, CAST(N'2024-03-29T15:06:27.127' AS DateTime), CAST(N'2024-03-29T15:07:04.737' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'test2@gmail.com', N'3SN36+nV661Mr316oOpxcQ==', N'James ', N'Smith', N'Male', 1, 1, N'https://d28yx6l5j59h9f.cloudfront.net/Artwork/1c39dadd-83f6-4dfa-86dd-a9a6ff5b5998.jpg', 2, CAST(N'2024-03-29T15:06:27.127' AS DateTime), CAST(N'2024-03-29T15:07:04.737' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'test3@gmail.com', N'3SN36+nV661Mr316oOpxcQ==', N'James ', N'Smith', N'Male', 1, 1, N'https://images.unsplash.com/photo-1564564244660-5d73c057f2d2?q=80&w=1776&auto=format&fit=crop&ixlib=rb-4.0.3...fA%3D%3D
', 2, CAST(N'2024-03-29T15:06:27.127' AS DateTime), CAST(N'2024-03-29T15:07:04.737' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'thongne@gmail.com', N'5Hzghx8mdZs7r8BgULR4IQ==', N'Thong', N'', N'Male', 1, 1, N'https://bird-trading-platform.s3.ap-southeast-1.amazonaws.com/Artwork/1b45fe09-d09d-4bdf-97ca-aa96ee1491d3.jpeg', 1, CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-03-26T03:54:19.950' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'user1@gmail.com', N'5Hzghx8mdZs7r8BgULR4IQ==', N'Duy Duc', N'Dao Mai', N'Male', 1, 1, N'https://d28yx6l5j59h9f.cloudfront.net/Artwork/30ae7275-2b4c-460a-8a09-f5a2682593f4.png', 1, CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-03-29T13:25:32.727' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'user2@gmail.com', N'5Hzghx8mdZs7r8BgULR4IQ==', N'Chau', N'', N'Female', 1, 1, N'https://images.unsplash.com/photo-1488161628813-04466f872be2?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 1, CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-03-26T03:54:19.950' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'user3@gmail.com', N'5Hzghx8mdZs7r8BgULR4IQ==', N'Mvea Tea', N'', N'Male', 1, 1, N'https://bird-trading-platform.s3.ap-southeast-1.amazonaws.com/Artwork/1b45fe09-d09d-4bdf-97ca-aa96ee1491d3.jpeg', 1, CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-03-26T03:54:19.950' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'user4@gmail.com', N'5Hzghx8mdZs7r8BgULR4IQ==', N'Trean', N'', N'Male', 1, 1, N'https://bird-trading-platform.s3.ap-southeast-1.amazonaws.com/Artwork/1b45fe09-d09d-4bdf-97ca-aa96ee1491d3.jpeg', 1, CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-03-26T03:54:19.950' AS DateTime))
GO
INSERT [dbo].[account] ([email], [password], [first_name], [last_name], [gender], [status], [enabled], [avatar], [role_id], [created_date], [updated_date]) VALUES (N'user5@gmail.com', N'5Hzghx8mdZs7r8BgULR4IQ==', N'Proew', N'', N'Male', 1, 1, N'https://bird-trading-platform.s3.ap-southeast-1.amazonaws.com/Artwork/1b45fe09-d09d-4bdf-97ca-aa96ee1491d3.jpeg', 1, CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-03-26T03:54:19.950' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'creator@gmail.com', N'Alis Mary', N'Bio of Alis Mary', -1, CAST(N'2024-03-18T16:07:22.567' AS DateTime), CAST(N'2024-03-29T16:07:42.220' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'creator2@gmail.com', N'Susan', N'Bio of Susan', 0, CAST(N'2024-03-18T16:07:22.567' AS DateTime), CAST(N'2024-03-18T16:07:22.567' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'creator3@gmail.com', N'Halen Tra', N'Bio of Halen Tra', 1, CAST(N'2024-03-18T16:07:22.567' AS DateTime), CAST(N'2024-03-29T15:36:34.543' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'creator4@gmail.com', N'Nguyen Nguyen', N'Bio Nguyen Nguyen', 0, CAST(N'2024-03-18T16:07:22.567' AS DateTime), CAST(N'2024-03-18T16:07:22.567' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'creator5@gmail.com', N'Levei Awro', N'Bio of Levei Awro.', 0, CAST(N'2024-03-18T16:07:22.567' AS DateTime), CAST(N'2024-03-18T16:07:22.567' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'DavidWilliams@gmail.com', N'DavidWilliam', N'', 0, CAST(N'2024-03-29T15:10:18.433' AS DateTime), CAST(N'2024-03-29T15:10:18.433' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'DavidWilliams2@gmail.com', N'DavidWilliam', N'', 0, CAST(N'2024-03-29T15:10:18.433' AS DateTime), CAST(N'2024-03-29T15:10:18.433' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'DavidWilliams3@gmail.com', N'Davididw William', N'', 0, CAST(N'2024-03-29T15:10:18.433' AS DateTime), CAST(N'2024-03-29T15:10:18.433' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'ductien@gmail.com', N'Kidhood', N'Bio of Kidhood.', 0, CAST(N'2024-03-28T21:39:03.427' AS DateTime), CAST(N'2024-03-28T21:39:03.427' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'fwin@gmai.com', N'Finwew', N'', 0, CAST(N'2024-03-29T15:06:39.603' AS DateTime), CAST(N'2024-03-29T15:06:39.603' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'Jessica@gmail.com', N'Jessicasty', N'', 0, CAST(N'2024-03-29T12:50:08.083' AS DateTime), CAST(N'2024-03-29T16:10:04.187' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'mitolamit@gmail.com', N'Lamit', N'', 1, CAST(N'2024-03-29T16:00:51.730' AS DateTime), CAST(N'2024-03-29T16:06:46.747' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'SarahJohnson@gmail.com', N'Sarah Johnson', N'', 0, CAST(N'2024-03-29T15:07:51.667' AS DateTime), CAST(N'2024-03-29T15:07:51.667' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'SarahJohnson2@gmail.com', N'Sarah Johnson', N'', 0, CAST(N'2024-03-29T15:07:51.667' AS DateTime), CAST(N'2024-03-29T15:07:51.667' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'SarahJohnson3@gmail.com', N'Sarah Truw', N'', 0, CAST(N'2024-03-29T15:07:51.667' AS DateTime), CAST(N'2024-03-29T15:07:51.667' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'test@gmail.com', N'Hoooo', N'', 0, CAST(N'2024-03-29T15:06:27.127' AS DateTime), CAST(N'2024-03-29T15:06:27.127' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'test2@gmail.com', N'Hoooo', N'', 0, CAST(N'2024-03-29T15:06:27.127' AS DateTime), CAST(N'2024-03-29T15:06:27.127' AS DateTime))
GO
INSERT [dbo].[artist] ([email], [artist_name], [bio], [total_subscribe], [created_date], [updated_date]) VALUES (N'test3@gmail.com', N'Hoooo', N'', 0, CAST(N'2024-03-29T15:06:27.127' AS DateTime), CAST(N'2024-03-29T15:06:27.127' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[bookmark] ON 
GO
INSERT [dbo].[bookmark] ([bookmark_id], [delete_flag], [post_id], [account_email], [created_date], [updated_date]) VALUES (2, 0, 2, N'user2@gmail.com', CAST(N'2024-03-18T16:07:22.600' AS DateTime), CAST(N'2024-03-18T16:07:22.600' AS DateTime))
GO
INSERT [dbo].[bookmark] ([bookmark_id], [delete_flag], [post_id], [account_email], [created_date], [updated_date]) VALUES (3, 0, 3, N'user3@gmail.com', CAST(N'2024-03-18T16:07:22.600' AS DateTime), CAST(N'2024-03-18T16:07:22.600' AS DateTime))
GO
INSERT [dbo].[bookmark] ([bookmark_id], [delete_flag], [post_id], [account_email], [created_date], [updated_date]) VALUES (4, 0, 4, N'user4@gmail.com', CAST(N'2024-03-18T16:07:22.600' AS DateTime), CAST(N'2024-03-18T16:07:22.600' AS DateTime))
GO
INSERT [dbo].[bookmark] ([bookmark_id], [delete_flag], [post_id], [account_email], [created_date], [updated_date]) VALUES (5, 0, 5, N'user5@gmail.com', CAST(N'2024-03-18T16:07:22.600' AS DateTime), CAST(N'2024-03-18T16:07:22.600' AS DateTime))
GO
INSERT [dbo].[bookmark] ([bookmark_id], [delete_flag], [post_id], [account_email], [created_date], [updated_date]) VALUES (6, 0, 1, N'user1@gmail.com', CAST(N'2024-03-26T00:32:47.797' AS DateTime), CAST(N'2024-03-26T00:32:47.797' AS DateTime))
GO
INSERT [dbo].[bookmark] ([bookmark_id], [delete_flag], [post_id], [account_email], [created_date], [updated_date]) VALUES (7, 0, 9, N'user1@gmail.com', CAST(N'2024-03-29T13:26:02.123' AS DateTime), CAST(N'2024-03-29T13:26:02.123' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[bookmark] OFF
GO
SET IDENTITY_INSERT [dbo].[category] ON 
GO
INSERT [dbo].[category] ([category_id], [category_name], [description], [created_date], [updated_date]) VALUES (1, N'Painting', N'Paintings and artworks', CAST(N'2024-03-18T16:07:22.613' AS DateTime), CAST(N'2024-03-18T16:07:22.613' AS DateTime))
GO
INSERT [dbo].[category] ([category_id], [category_name], [description], [created_date], [updated_date]) VALUES (2, N'Sculpture', N'Three-dimensional artwork', CAST(N'2024-03-18T16:07:22.613' AS DateTime), CAST(N'2024-03-18T16:07:22.613' AS DateTime))
GO
INSERT [dbo].[category] ([category_id], [category_name], [description], [created_date], [updated_date]) VALUES (3, N'Digital', N'Digital art creations', CAST(N'2024-03-18T16:07:22.613' AS DateTime), CAST(N'2024-03-18T16:07:22.613' AS DateTime))
GO
INSERT [dbo].[category] ([category_id], [category_name], [description], [created_date], [updated_date]) VALUES (4, N'Photography', N'Photographic works of art', CAST(N'2024-03-18T16:07:22.613' AS DateTime), CAST(N'2024-03-18T16:07:22.613' AS DateTime))
GO
INSERT [dbo].[category] ([category_id], [category_name], [description], [created_date], [updated_date]) VALUES (5, N'Mixed Media', N'Artworks featuring mixed media', CAST(N'2024-03-18T16:07:22.613' AS DateTime), CAST(N'2024-03-18T16:07:22.613' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[category] OFF
GO
SET IDENTITY_INSERT [dbo].[fee] ON 
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (1, 10, N'creator@gmail.com', CAST(N'2024-03-18T16:07:22.573' AS DateTime), CAST(N'2024-03-18T16:07:22.573' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (2, 15, N'creator2@gmail.com', CAST(N'2024-03-18T16:07:22.573' AS DateTime), CAST(N'2024-03-18T16:07:22.573' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (3, 20, N'creator3@gmail.com', CAST(N'2024-03-18T16:07:22.573' AS DateTime), CAST(N'2024-03-18T16:07:22.573' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (4, 25, N'creator4@gmail.com', CAST(N'2024-03-18T16:07:22.573' AS DateTime), CAST(N'2024-03-18T16:07:22.573' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (5, 30, N'creator5@gmail.com', CAST(N'2024-03-18T16:07:22.573' AS DateTime), CAST(N'2024-03-18T16:07:22.573' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (6, 10, N'Jessica@gmail.com', CAST(N'2024-03-29T12:50:08.083' AS DateTime), CAST(N'2024-03-29T12:50:08.083' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (7, 0, N'fwin@gmai.com', CAST(N'2024-03-29T15:06:39.603' AS DateTime), CAST(N'2024-03-29T15:06:39.603' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (8, 5, N'DavidWilliams@gmail.com', CAST(N'2024-03-29T15:10:18.433' AS DateTime), CAST(N'2024-03-29T15:10:18.433' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (9, 5, N'SarahJohnson@gmail.com', CAST(N'2024-03-29T15:07:51.667' AS DateTime), CAST(N'2024-03-29T15:07:51.667' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (10, 5, N'test@gmail.com', CAST(N'2024-03-29T15:06:27.127' AS DateTime), CAST(N'2024-03-29T15:06:27.127' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (11, 5, N'DavidWilliams2@gmail.com', CAST(N'2024-03-29T15:10:18.433' AS DateTime), CAST(N'2024-03-29T15:10:18.433' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (12, 5, N'SarahJohnson2@gmail.com', CAST(N'2024-03-29T15:07:51.667' AS DateTime), CAST(N'2024-03-29T15:07:51.667' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (13, 5, N'test2@gmail.com', CAST(N'2024-03-29T15:06:27.127' AS DateTime), CAST(N'2024-03-29T15:06:27.127' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (14, 5, N'DavidWilliams3@gmail.com', CAST(N'2024-03-29T15:10:18.433' AS DateTime), CAST(N'2024-03-29T15:10:18.433' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (15, 5, N'SarahJohnson3@gmail.com', CAST(N'2024-03-29T15:07:51.667' AS DateTime), CAST(N'2024-03-29T15:07:51.667' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (16, 5, N'test3@gmail.com', CAST(N'2024-03-29T15:06:27.127' AS DateTime), CAST(N'2024-03-29T15:06:27.127' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (17, 5, N'ductien@gmail.com', CAST(N'2024-03-29T15:10:18.433' AS DateTime), CAST(N'2024-03-29T15:10:18.433' AS DateTime))
GO
INSERT [dbo].[fee] ([fee_id], [amount], [artist_email], [created_date], [updated_date]) VALUES (18, 5, N'mitolamit@gmail.com', CAST(N'2024-03-29T16:00:51.730' AS DateTime), CAST(N'2024-03-29T16:00:51.730' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[fee] OFF
GO
SET IDENTITY_INSERT [dbo].[image] ON 
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (1, N'jpg', N'https://cdn.dribbble.com/userupload/10900054/file/original-7c9d917785e6ec6f0f67cafb2ac35424.jpg?resize=1200x725', 0, 1, CAST(N'2024-03-18T16:07:22.607' AS DateTime), CAST(N'2024-03-18T16:07:22.607' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (2, N'png', N'https://cdn.dribbble.com/userupload/10965831/file/original-8b1e4e20152fe54335af387cabf3c656.jpg?resize=1504x885', 0, 2, CAST(N'2024-03-18T16:07:22.607' AS DateTime), CAST(N'2024-03-18T16:07:22.607' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (3, N'jpg', N'https://cdn.dribbble.com/userupload/11374359/file/original-37264ca2ff3c6857d9f9295fe4039349.jpg?resize=1200x720', 0, 3, CAST(N'2024-03-18T16:07:22.607' AS DateTime), CAST(N'2024-03-18T16:07:22.607' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (4, N'png', N'https://cdn.dribbble.com/userupload/5183213/file/original-0b3a34dc69e1b0974d4f97009ac97fe2.jpg?resize=1200x840', 0, 4, CAST(N'2024-03-18T16:07:22.607' AS DateTime), CAST(N'2024-03-18T16:07:22.607' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (5, N'jpg', N'https://cdn.dribbble.com/users/3863141/screenshots/18179849/media/d51d58b24c83c229cea232e2d5d10e99.png?resize=1000x750&vertical=center', 0, 5, CAST(N'2024-03-18T16:07:22.607' AS DateTime), CAST(N'2024-03-18T16:07:22.607' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (6, N'jpg', N'https://cdn.dribbble.com/userupload/12685516/file/original-1aa08df9eb680947c23c8c99dee29f81.jpg?resize=1504x1128', 0, 6, CAST(N'2024-03-18T16:07:22.607' AS DateTime), CAST(N'2024-03-18T16:07:22.607' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (7, N'jpg', N'https://cdn.dribbble.com/users/3210321/screenshots/15290493/media/fc71406dce6d735a890c73178eeaaa5c.png?resize=1000x750&vertical=center', 0, 7, CAST(N'2024-03-18T16:07:22.607' AS DateTime), CAST(N'2024-03-18T16:07:22.607' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (8, N'jpg', N'https://cdn.dribbble.com/userupload/5801698/file/original-c56337af4016dd4e45b6248a2c46dde2.png?resize=1200x960', 0, 8, CAST(N'2024-03-18T16:07:22.607' AS DateTime), CAST(N'2024-03-18T16:07:22.607' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (9, N'jpg', N'https://d28yx6l5j59h9f.cloudfront.net/Artwork/f5cbb041-25c6-44dd-b56d-cddf7b5fc049.jpg', 0, 9, CAST(N'2024-03-29T12:53:11.020' AS DateTime), CAST(N'2024-03-29T12:53:11.020' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (10, N'jpg', N'https://d28yx6l5j59h9f.cloudfront.net/Artwork/1c0256e4-d84c-436f-b3e4-58f1e7926d9d.jpg', 0, 10, CAST(N'2024-03-29T12:54:31.200' AS DateTime), CAST(N'2024-03-29T12:54:31.200' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (11, N'png', N'https://d28yx6l5j59h9f.cloudfront.net/Artwork/82deef9c-fe1c-4a02-bc63-e9761ac3c5e3.png', 0, 11, CAST(N'2024-03-29T13:04:02.363' AS DateTime), CAST(N'2024-03-29T13:04:02.363' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (12, N'jpg', N'https://d28yx6l5j59h9f.cloudfront.net/Artwork/10a62c1f-1348-4aa1-9810-48db21879c65.jpg', 0, 12, CAST(N'2024-03-29T15:04:07.547' AS DateTime), CAST(N'2024-03-29T15:04:07.547' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (13, N'jpg', N'https://d28yx6l5j59h9f.cloudfront.net/Artwork/bbe4cf5c-8cb8-4eb8-b930-9a34fb547a01.jpg', 0, 13, CAST(N'2024-03-29T15:04:48.567' AS DateTime), CAST(N'2024-03-29T15:04:48.567' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (14, N'png', N'https://d28yx6l5j59h9f.cloudfront.net/Artwork/d57f0873-ae4f-40b3-85ab-25a347e7ec96.png', 0, 14, CAST(N'2024-03-29T15:06:10.570' AS DateTime), CAST(N'2024-03-29T15:06:10.570' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (15, N'png', N'https://d28yx6l5j59h9f.cloudfront.net/Artwork/6b4dea0f-e34f-467b-8c91-f134fe4380d3.png', 0, 15, CAST(N'2024-03-29T15:07:31.100' AS DateTime), CAST(N'2024-03-29T15:07:31.100' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (16, N'png', N'https://d28yx6l5j59h9f.cloudfront.net/Artwork/28b97de7-6437-40af-81db-28465a9b7cb7.png', 0, 16, CAST(N'2024-03-29T15:09:33.047' AS DateTime), CAST(N'2024-03-29T15:09:33.047' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (17, N'png', N'https://d28yx6l5j59h9f.cloudfront.net/Artwork/57769a7e-cdf8-4c6c-9e2b-fb39635cbee8.png', 0, 17, CAST(N'2024-03-29T15:09:59.760' AS DateTime), CAST(N'2024-03-29T15:09:59.760' AS DateTime))
GO
INSERT [dbo].[image] ([image_id], [type], [image_url], [delete_flag], [post_id], [created_date], [updated_date]) VALUES (18, N'jpg', N'https://d28yx6l5j59h9f.cloudfront.net/Artwork/9a393376-7264-494d-947e-d3a45e0bfb12.jpg', 0, 18, CAST(N'2024-03-29T16:02:27.017' AS DateTime), CAST(N'2024-03-29T16:02:27.017' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[image] OFF
GO
SET IDENTITY_INSERT [dbo].[post] ON 
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (1, N'Sunrise and sunset', N'Maecenas in hendrerit tortor, ac egestas risus. Nullam vel purus id urna pretium pellentesque eu egestas purus. Quisque a lectus eget est tempus ultricies sit amet in mauris.', 2, 1, 0, 1, 0, N'creator@gmail.com', CAST(N'2024-03-18T16:07:22.593' AS DateTime), CAST(N'2024-03-29T15:12:50.960' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (2, N'Maecenas in hendrerit tortor, ac egestas risus', N'Duis luctus elit nisi, nec pretium ipsum ornare in. Pellentesque sem nibh, porta in ornare vel, sollicitudin vel felis. Integer interdum leo eleifend nisl interdum, sit amet elementum ante mollis.', 2, 1, 1, 8, 0, N'creator@gmail.com', CAST(N'2024-03-18T16:07:22.593' AS DateTime), CAST(N'2024-03-29T16:07:10.013' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (3, N'Ethereal Reverie', N'Donec sit amet fringilla orci, eu convallis diam. Vestibulum auctor quam ac nulla tincidunt, vel rhoncus dolor sollicitudin.', 2, 1, 0, 5, 0, N'creator@gmail.com', CAST(N'2024-03-18T16:07:22.593' AS DateTime), CAST(N'2024-03-29T13:02:05.747' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (4, N'Whispers of the Cosmos', N'The atmosphere of quiet contemplation is enhanced by the subtle interplay of light and shadow, which imbues the scene with a sense of depth and dimensionality.', 2, 1, 0, 0, 0, N'creator2@gmail.com', CAST(N'2024-03-18T16:07:22.593' AS DateTime), CAST(N'2024-03-26T00:18:11.077' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (5, N'Echoes of Eternity', N'At the center of the composition stands a majestic cherry blossom tree, its branches gracefully reaching towards the sky. ', 2, 1, 0, 2, 0, N'creator3@gmail.com', CAST(N'2024-03-18T16:07:22.593' AS DateTime), CAST(N'2024-03-29T13:02:08.423' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (6, N'Symphony of Solitude', N'Donec sit amet fringilla orci, eu convallis diam. Vestibulum auctor quam ac nulla tincidunt, vel rhoncus dolor sollicitudin.', 2, 1, 0, 0, 0, N'creator4@gmail.com', CAST(N'2024-03-18T16:07:22.593' AS DateTime), CAST(N'2024-03-26T00:18:17.313' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (7, N'Journey into the Unknown', N'Donec sit amet fringilla orci, eu convallis diam. Vestibulum auctor quam ac nulla tincidunt, vel rhoncus dolor sollicitudin.', 2, 1, 0, 1, 0, N'creator5@gmail.com', CAST(N'2024-03-18T16:07:22.593' AS DateTime), CAST(N'2024-03-29T12:39:17.627' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (8, N'Whirlwind of Wonder', N'Donec sit amet fringilla orci, eu convallis diam. Vestibulum auctor quam ac nulla tincidunt, vel rhoncus dolor sollicitudin.', 2, 1, 0, 0, 0, N'creator5@gmail.com', CAST(N'2024-03-18T16:07:22.593' AS DateTime), CAST(N'2024-03-29T15:12:55.730' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (9, N'Whispering Winds', N'At the center of the composition stands a majestic cherry blossom tree, its branches gracefully reaching towards the sky.', 2, 2, 1, 3, 1, N'Jessica@gmail.com', CAST(N'2024-03-29T12:53:11.020' AS DateTime), CAST(N'2024-03-29T16:09:11.717' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (10, N'Harmony in Chaos', N'Proin sed mauris quis ante malesuada consequat eget ac neque. Phasellus porttitor fermentum lacinia. Nullam a nisl sit amet massa pulvinar mattis. ', 2, 2, 0, 2, 0, N'Jessica@gmail.com', CAST(N'2024-03-29T12:54:31.200' AS DateTime), CAST(N'2024-03-29T13:01:59.017' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (11, N'Glimmer of Hope', N'At the center of the composition stands a majestic cherry blossom tree, its branches gracefully reaching towards the sky. ', 2, 1, 0, 1, 0, N'creator3@gmail.com', CAST(N'2024-03-29T13:04:02.363' AS DateTime), CAST(N'2024-03-29T15:46:54.307' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (12, N'Morning light', N'Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.', 2, 1, 0, 0, 0, N'Jessica@gmail.com', CAST(N'2024-03-29T15:04:07.540' AS DateTime), CAST(N'2024-03-29T15:12:41.907' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (13, N'Purple Flower', N'Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.', 2, 2, 0, 2, 0, N'Jessica@gmail.com', CAST(N'2024-03-29T15:04:48.567' AS DateTime), CAST(N'2024-03-29T16:13:09.947' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (14, N'Maecenas neque ligula', N'Praesent porta ultricies ante, at porttitor justo consectetur eu. Ut ultricies massa ante, a egestas est luctus at. Donec nec libero ac elit elementum sagittis et nec orci.', 2, 1, 0, 1, 0, N'Jessica@gmail.com', CAST(N'2024-03-29T15:06:10.570' AS DateTime), CAST(N'2024-03-29T16:13:38.130' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (15, N'House River Land', N'Praesent porta ultricies ante, at porttitor justo consectetur eu. Ut ultricies massa ante, a egestas est luctus at. Donec nec libero ac elit elementum sagittis et nec orci.', 2, 1, 0, 0, 0, N'fwin@gmai.com', CAST(N'2024-03-29T15:07:31.100' AS DateTime), CAST(N'2024-03-29T15:12:33.177' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (16, N'Warm House Land', N'Praesent porta ultricies ante, at porttitor justo consectetur eu. Ut ultricies massa ante, a egestas est luctus at. Donec nec libero ac elit elementum sagittis et nec orci.', 2, 1, 0, 0, 0, N'fwin@gmai.com', CAST(N'2024-03-29T15:09:33.047' AS DateTime), CAST(N'2024-03-29T15:12:30.250' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (17, N'Disney Land', N'Praesent porta ultricies ante, at porttitor justo consectetur eu. Ut ultricies massa ante, a egestas est luctus at. Donec nec libero ac elit elementum sagittis et nec orci.', 2, 1, 0, 0, 0, N'fwin@gmai.com', CAST(N'2024-03-29T15:09:59.760' AS DateTime), CAST(N'2024-03-29T15:12:27.297' AS DateTime), N'')
GO
INSERT [dbo].[post] ([post_id], [title], [description], [status], [scope], [total_react], [total_view], [total_bookmark], [artist_email], [created_date], [updated_date], [note]) VALUES (18, N'Hoa Lan', N'abc', 2, 1, 0, 1, 0, N'mitolamit@gmail.com', CAST(N'2024-03-29T16:02:27.013' AS DateTime), CAST(N'2024-03-29T16:05:07.477' AS DateTime), N'')
GO
SET IDENTITY_INSERT [dbo].[post] OFF
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (1, 1, CAST(N'2024-03-18T16:07:22.620' AS DateTime), CAST(N'2024-03-18T16:07:22.620' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (1, 2, CAST(N'2024-03-18T16:07:22.620' AS DateTime), CAST(N'2024-03-18T16:07:22.620' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (1, 9, CAST(N'2024-03-29T16:09:11.717' AS DateTime), CAST(N'2024-03-29T16:09:11.717' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (1, 10, CAST(N'2024-03-29T12:54:31.200' AS DateTime), CAST(N'2024-03-29T12:54:31.200' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (1, 11, CAST(N'2024-03-29T13:04:02.363' AS DateTime), CAST(N'2024-03-29T13:04:02.363' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (1, 12, CAST(N'2024-03-29T15:04:07.547' AS DateTime), CAST(N'2024-03-29T15:04:07.547' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (1, 13, CAST(N'2024-03-29T16:09:45.330' AS DateTime), CAST(N'2024-03-29T16:09:45.330' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (1, 14, CAST(N'2024-03-29T15:06:10.570' AS DateTime), CAST(N'2024-03-29T15:06:10.570' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (1, 15, CAST(N'2024-03-29T15:07:31.100' AS DateTime), CAST(N'2024-03-29T15:07:31.100' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (1, 16, CAST(N'2024-03-29T15:09:33.047' AS DateTime), CAST(N'2024-03-29T15:09:33.047' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (1, 17, CAST(N'2024-03-29T15:09:59.760' AS DateTime), CAST(N'2024-03-29T15:09:59.760' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (1, 18, CAST(N'2024-03-29T16:02:27.017' AS DateTime), CAST(N'2024-03-29T16:02:27.017' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (2, 1, CAST(N'2024-03-18T16:07:22.620' AS DateTime), CAST(N'2024-03-18T16:07:22.620' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (2, 15, CAST(N'2024-03-29T15:07:31.100' AS DateTime), CAST(N'2024-03-29T15:07:31.100' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (2, 17, CAST(N'2024-03-29T15:09:59.760' AS DateTime), CAST(N'2024-03-29T15:09:59.760' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (3, 3, CAST(N'2024-03-18T16:07:22.620' AS DateTime), CAST(N'2024-03-18T16:07:22.620' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (3, 9, CAST(N'2024-03-29T16:09:11.717' AS DateTime), CAST(N'2024-03-29T16:09:11.717' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (3, 10, CAST(N'2024-03-29T12:54:31.200' AS DateTime), CAST(N'2024-03-29T12:54:31.200' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (3, 11, CAST(N'2024-03-29T13:04:02.363' AS DateTime), CAST(N'2024-03-29T13:04:02.363' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (3, 12, CAST(N'2024-03-29T15:04:07.547' AS DateTime), CAST(N'2024-03-29T15:04:07.547' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (3, 13, CAST(N'2024-03-29T16:09:45.333' AS DateTime), CAST(N'2024-03-29T16:09:45.333' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (3, 14, CAST(N'2024-03-29T15:06:10.570' AS DateTime), CAST(N'2024-03-29T15:06:10.570' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (3, 15, CAST(N'2024-03-29T15:07:31.100' AS DateTime), CAST(N'2024-03-29T15:07:31.100' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (3, 16, CAST(N'2024-03-29T15:09:33.047' AS DateTime), CAST(N'2024-03-29T15:09:33.047' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (3, 18, CAST(N'2024-03-29T16:02:27.017' AS DateTime), CAST(N'2024-03-29T16:02:27.017' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (4, 4, CAST(N'2024-03-18T16:07:22.620' AS DateTime), CAST(N'2024-03-18T16:07:22.620' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (4, 11, CAST(N'2024-03-29T13:04:02.363' AS DateTime), CAST(N'2024-03-29T13:04:02.363' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (4, 12, CAST(N'2024-03-29T15:04:07.547' AS DateTime), CAST(N'2024-03-29T15:04:07.547' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (4, 16, CAST(N'2024-03-29T15:09:33.047' AS DateTime), CAST(N'2024-03-29T15:09:33.047' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (5, 5, CAST(N'2024-03-18T16:07:22.620' AS DateTime), CAST(N'2024-03-18T16:07:22.620' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (5, 11, CAST(N'2024-03-29T13:04:02.363' AS DateTime), CAST(N'2024-03-29T13:04:02.363' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (5, 14, CAST(N'2024-03-29T15:06:10.570' AS DateTime), CAST(N'2024-03-29T15:06:10.570' AS DateTime))
GO
INSERT [dbo].[post_category] ([category_id], [post_id], [created_date], [updated_date]) VALUES (5, 16, CAST(N'2024-03-29T15:09:33.047' AS DateTime), CAST(N'2024-03-29T15:09:33.047' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[reaction] ON 
GO
INSERT [dbo].[reaction] ([reaction_id], [post_id], [account_email], [created_date], [updated_date]) VALUES (1, 1, N'user1@gmail.com', CAST(N'2024-03-18T16:07:22.610' AS DateTime), CAST(N'2024-03-18T16:07:22.610' AS DateTime))
GO
INSERT [dbo].[reaction] ([reaction_id], [post_id], [account_email], [created_date], [updated_date]) VALUES (2, 2, N'user2@gmail.com', CAST(N'2024-03-18T16:07:22.610' AS DateTime), CAST(N'2024-03-18T16:07:22.610' AS DateTime))
GO
INSERT [dbo].[reaction] ([reaction_id], [post_id], [account_email], [created_date], [updated_date]) VALUES (3, 3, N'user3@gmail.com', CAST(N'2024-03-18T16:07:22.610' AS DateTime), CAST(N'2024-03-18T16:07:22.610' AS DateTime))
GO
INSERT [dbo].[reaction] ([reaction_id], [post_id], [account_email], [created_date], [updated_date]) VALUES (4, 4, N'user4@gmail.com', CAST(N'2024-03-18T16:07:22.610' AS DateTime), CAST(N'2024-03-18T16:07:22.610' AS DateTime))
GO
INSERT [dbo].[reaction] ([reaction_id], [post_id], [account_email], [created_date], [updated_date]) VALUES (5, 5, N'user5@gmail.com', CAST(N'2024-03-18T16:07:22.610' AS DateTime), CAST(N'2024-03-18T16:07:22.610' AS DateTime))
GO
INSERT [dbo].[reaction] ([reaction_id], [post_id], [account_email], [created_date], [updated_date]) VALUES (6, 2, N'user1@gmail.com', CAST(N'2024-03-26T00:32:33.247' AS DateTime), CAST(N'2024-03-26T00:32:33.247' AS DateTime))
GO
INSERT [dbo].[reaction] ([reaction_id], [post_id], [account_email], [created_date], [updated_date]) VALUES (7, 9, N'user1@gmail.com', CAST(N'2024-03-29T13:26:01.593' AS DateTime), CAST(N'2024-03-29T13:26:01.593' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[reaction] OFF
GO
SET IDENTITY_INSERT [dbo].[report] ON 
GO
INSERT [dbo].[report] ([report_id], [reason], [reporter_email], [post_id], [created_date], [updated_date], [status]) VALUES (1, N'Reason for post 1', N'user1@gmail.com', 1, CAST(N'2024-03-18T16:07:22.623' AS DateTime), CAST(N'2024-03-26T21:39:45.053' AS DateTime), 2)
GO
INSERT [dbo].[report] ([report_id], [reason], [reporter_email], [post_id], [created_date], [updated_date], [status]) VALUES (2, N'Reason for post 2', N'user1@gmail.com', 2, CAST(N'2024-03-18T16:07:22.623' AS DateTime), CAST(N'2024-03-18T16:07:22.623' AS DateTime), 1)
GO
INSERT [dbo].[report] ([report_id], [reason], [reporter_email], [post_id], [created_date], [updated_date], [status]) VALUES (3, N'Reason for post 3', N'user1@gmail.com', 3, CAST(N'2024-03-18T16:07:22.623' AS DateTime), CAST(N'2024-03-26T21:21:03.390' AS DateTime), 2)
GO
INSERT [dbo].[report] ([report_id], [reason], [reporter_email], [post_id], [created_date], [updated_date], [status]) VALUES (4, N'Reason for post 4', N'user1@gmail.com', 4, CAST(N'2024-03-18T16:07:22.623' AS DateTime), CAST(N'2024-03-18T16:07:22.623' AS DateTime), 1)
GO
INSERT [dbo].[report] ([report_id], [reason], [reporter_email], [post_id], [created_date], [updated_date], [status]) VALUES (5, N'Reason for post 5', N'user1@gmail.com', 5, CAST(N'2024-03-18T16:07:22.623' AS DateTime), CAST(N'2024-03-18T16:07:22.623' AS DateTime), 1)
GO
INSERT [dbo].[report] ([report_id], [reason], [reporter_email], [post_id], [created_date], [updated_date], [status]) VALUES (6, N'it''s fake', N'user1@gmail.com', 13, CAST(N'2024-03-29T16:19:17.823' AS DateTime), CAST(N'2024-03-29T16:19:17.823' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[report] OFF
GO
SET IDENTITY_INSERT [dbo].[role] ON 
GO
INSERT [dbo].[role] ([role_id], [role_name], [created_date], [updated_date]) VALUES (1, N'Audience', CAST(N'2023-05-05T00:00:00.000' AS DateTime), CAST(N'2023-05-05T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[role] ([role_id], [role_name], [created_date], [updated_date]) VALUES (2, N'Creator', CAST(N'2023-05-05T00:00:00.000' AS DateTime), CAST(N'2023-05-05T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[role] ([role_id], [role_name], [created_date], [updated_date]) VALUES (3, N'Moderator', CAST(N'2023-05-05T00:00:00.000' AS DateTime), CAST(N'2023-05-05T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[role] ([role_id], [role_name], [created_date], [updated_date]) VALUES (4, N'Admin', CAST(N'2023-05-05T00:00:00.000' AS DateTime), CAST(N'2023-05-05T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[role] OFF
GO
SET IDENTITY_INSERT [dbo].[subscriber] ON 
GO
INSERT [dbo].[subscriber] ([subscriber_id], [email_user], [email_artist], [status], [expired_date], [created_date], [updated_date]) VALUES (1, N'user1@gmail.com', N'creator@gmail.com', 0, CAST(N'2025-03-18T16:07:22.580' AS DateTime), CAST(N'2024-03-18T16:07:22.580' AS DateTime), CAST(N'2024-03-29T16:07:42.220' AS DateTime))
GO
INSERT [dbo].[subscriber] ([subscriber_id], [email_user], [email_artist], [status], [expired_date], [created_date], [updated_date]) VALUES (2, N'user2@gmail.com', N'creator2@gmail.com', 1, CAST(N'2025-03-18T16:07:22.580' AS DateTime), CAST(N'2024-03-18T16:07:22.580' AS DateTime), CAST(N'2024-03-18T16:07:22.580' AS DateTime))
GO
INSERT [dbo].[subscriber] ([subscriber_id], [email_user], [email_artist], [status], [expired_date], [created_date], [updated_date]) VALUES (3, N'user3@gmail.com', N'creator3@gmail.com', 1, CAST(N'2025-03-18T16:07:22.580' AS DateTime), CAST(N'2024-03-18T16:07:22.580' AS DateTime), CAST(N'2024-03-18T16:07:22.580' AS DateTime))
GO
INSERT [dbo].[subscriber] ([subscriber_id], [email_user], [email_artist], [status], [expired_date], [created_date], [updated_date]) VALUES (4, N'user4@gmail.com', N'creator4@gmail.com', 1, CAST(N'2025-03-18T16:07:22.580' AS DateTime), CAST(N'2024-03-18T16:07:22.580' AS DateTime), CAST(N'2024-03-18T16:07:22.580' AS DateTime))
GO
INSERT [dbo].[subscriber] ([subscriber_id], [email_user], [email_artist], [status], [expired_date], [created_date], [updated_date]) VALUES (5, N'user5@gmail.com', N'creator5@gmail.com', 1, CAST(N'2025-03-18T16:07:22.580' AS DateTime), CAST(N'2024-03-18T16:07:22.580' AS DateTime), CAST(N'2024-03-18T16:07:22.580' AS DateTime))
GO
INSERT [dbo].[subscriber] ([subscriber_id], [email_user], [email_artist], [status], [expired_date], [created_date], [updated_date]) VALUES (6, N'user1@gmail.com', N'creator2@gmail.com', 0, CAST(N'2024-04-26T06:01:15.473' AS DateTime), CAST(N'2024-03-26T06:01:15.620' AS DateTime), CAST(N'2024-03-26T06:01:15.620' AS DateTime))
GO
INSERT [dbo].[subscriber] ([subscriber_id], [email_user], [email_artist], [status], [expired_date], [created_date], [updated_date]) VALUES (7, N'user1@gmail.com', N'creator2@gmail.com', 1, CAST(N'2024-04-26T06:01:41.417' AS DateTime), CAST(N'2024-03-26T06:01:41.460' AS DateTime), CAST(N'2024-03-26T06:01:41.460' AS DateTime))
GO
INSERT [dbo].[subscriber] ([subscriber_id], [email_user], [email_artist], [status], [expired_date], [created_date], [updated_date]) VALUES (8, N'user1@gmail.com', N'creator3@gmail.com', 1, CAST(N'2024-04-26T06:21:50.983' AS DateTime), CAST(N'2024-03-26T06:21:51.063' AS DateTime), CAST(N'2024-03-26T06:21:51.063' AS DateTime))
GO
INSERT [dbo].[subscriber] ([subscriber_id], [email_user], [email_artist], [status], [expired_date], [created_date], [updated_date]) VALUES (9, N'user1@gmail.com', N'creator4@gmail.com', 1, CAST(N'2024-04-26T06:25:22.857' AS DateTime), CAST(N'2024-03-26T06:25:22.940' AS DateTime), CAST(N'2024-03-26T06:25:22.940' AS DateTime))
GO
INSERT [dbo].[subscriber] ([subscriber_id], [email_user], [email_artist], [status], [expired_date], [created_date], [updated_date]) VALUES (11, N'user1@gmail.com', N'creator5@gmail.com', 1, CAST(N'2024-04-28T21:45:32.627' AS DateTime), CAST(N'2024-03-28T21:45:32.710' AS DateTime), CAST(N'2024-03-28T21:45:32.710' AS DateTime))
GO
INSERT [dbo].[subscriber] ([subscriber_id], [email_user], [email_artist], [status], [expired_date], [created_date], [updated_date]) VALUES (12, N'user1@gmail.com', N'Jessica@gmail.com', 0, CAST(N'2024-04-29T12:59:01.390' AS DateTime), CAST(N'2024-03-29T12:59:01.463' AS DateTime), CAST(N'2024-03-29T16:10:04.187' AS DateTime))
GO
INSERT [dbo].[subscriber] ([subscriber_id], [email_user], [email_artist], [status], [expired_date], [created_date], [updated_date]) VALUES (13, N'thongne@gmail.com', N'creator3@gmail.com', 1, CAST(N'2024-04-29T15:36:34.437' AS DateTime), CAST(N'2024-03-29T15:36:34.543' AS DateTime), CAST(N'2024-03-29T15:36:34.543' AS DateTime))
GO
INSERT [dbo].[subscriber] ([subscriber_id], [email_user], [email_artist], [status], [expired_date], [created_date], [updated_date]) VALUES (14, N'user1@gmail.com', N'mitolamit@gmail.com', 1, CAST(N'2024-04-29T16:06:46.740' AS DateTime), CAST(N'2024-03-29T16:06:46.747' AS DateTime), CAST(N'2024-03-29T16:06:46.747' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[subscriber] OFF
GO
SET IDENTITY_INSERT [dbo].[system_config] ON 
GO
INSERT [dbo].[system_config] ([config_id], [commision_rate], [created_date], [updated_date]) VALUES (2, 0.15, CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-05-05T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[system_config] OFF
GO
SET IDENTITY_INSERT [dbo].[transaction] ON 
GO
INSERT [dbo].[transaction] ([transaction_id], [amount], [status], [type], [fee_id], [subscriber_id], [created_date], [updated_date], [subscription_paypal_id]) VALUES (1, 50, 1, N'Paypal', 1, 1, CAST(N'2024-03-18T16:07:22.587' AS DateTime), CAST(N'2024-03-18T16:07:22.587' AS DateTime), N'')
GO
INSERT [dbo].[transaction] ([transaction_id], [amount], [status], [type], [fee_id], [subscriber_id], [created_date], [updated_date], [subscription_paypal_id]) VALUES (2, 60, 1, N'Paypal', 2, 2, CAST(N'2024-03-18T16:07:22.587' AS DateTime), CAST(N'2024-03-18T16:07:22.587' AS DateTime), N'')
GO
INSERT [dbo].[transaction] ([transaction_id], [amount], [status], [type], [fee_id], [subscriber_id], [created_date], [updated_date], [subscription_paypal_id]) VALUES (3, 70, 0, N'Paypal', 3, 3, CAST(N'2024-03-18T16:07:22.587' AS DateTime), CAST(N'2024-03-18T16:07:22.587' AS DateTime), N'')
GO
INSERT [dbo].[transaction] ([transaction_id], [amount], [status], [type], [fee_id], [subscriber_id], [created_date], [updated_date], [subscription_paypal_id]) VALUES (4, 80, 2, N'Paypal', 4, 4, CAST(N'2024-03-18T16:07:22.587' AS DateTime), CAST(N'2024-03-18T16:07:22.587' AS DateTime), N'')
GO
INSERT [dbo].[transaction] ([transaction_id], [amount], [status], [type], [fee_id], [subscriber_id], [created_date], [updated_date], [subscription_paypal_id]) VALUES (5, 90, 2, N'Paypal', 5, 5, CAST(N'2024-03-18T16:07:22.587' AS DateTime), CAST(N'2024-03-18T16:07:22.587' AS DateTime), N'')
GO
INSERT [dbo].[transaction] ([transaction_id], [amount], [status], [type], [fee_id], [subscriber_id], [created_date], [updated_date], [subscription_paypal_id]) VALUES (6, 150, 1, N'Paypal', 2, 6, CAST(N'2024-03-26T06:01:15.617' AS DateTime), CAST(N'2024-03-26T06:01:15.617' AS DateTime), N'')
GO
INSERT [dbo].[transaction] ([transaction_id], [amount], [status], [type], [fee_id], [subscriber_id], [created_date], [updated_date], [subscription_paypal_id]) VALUES (7, 150, 1, N'Paypal', 2, 7, CAST(N'2024-03-26T06:01:41.460' AS DateTime), CAST(N'2024-03-26T06:01:41.460' AS DateTime), N'')
GO
INSERT [dbo].[transaction] ([transaction_id], [amount], [status], [type], [fee_id], [subscriber_id], [created_date], [updated_date], [subscription_paypal_id]) VALUES (8, 200, 1, N'Paypal', 3, 8, CAST(N'2024-03-26T06:21:51.063' AS DateTime), CAST(N'2024-03-26T06:21:51.063' AS DateTime), N'')
GO
INSERT [dbo].[transaction] ([transaction_id], [amount], [status], [type], [fee_id], [subscriber_id], [created_date], [updated_date], [subscription_paypal_id]) VALUES (9, 250, 1, N'Paypal', 4, 9, CAST(N'2024-03-26T06:25:22.940' AS DateTime), CAST(N'2024-03-26T06:25:22.940' AS DateTime), N'')
GO
INSERT [dbo].[transaction] ([transaction_id], [amount], [status], [type], [fee_id], [subscriber_id], [created_date], [updated_date], [subscription_paypal_id]) VALUES (11, 300, 1, N'Paypal', 5, 11, CAST(N'2024-03-28T21:45:32.707' AS DateTime), CAST(N'2024-03-28T21:45:32.707' AS DateTime), N'')
GO
INSERT [dbo].[transaction] ([transaction_id], [amount], [status], [type], [fee_id], [subscriber_id], [created_date], [updated_date], [subscription_paypal_id]) VALUES (12, 10, 1, N'Paypal', 6, 12, CAST(N'2024-03-29T12:59:01.460' AS DateTime), CAST(N'2024-03-29T12:59:01.460' AS DateTime), N'')
GO
INSERT [dbo].[transaction] ([transaction_id], [amount], [status], [type], [fee_id], [subscriber_id], [created_date], [updated_date], [subscription_paypal_id]) VALUES (13, 20, 1, N'Paypal', 3, 13, CAST(N'2024-03-29T15:36:34.540' AS DateTime), CAST(N'2024-03-29T15:36:34.540' AS DateTime), N'I-CRCEEW85W01D')
GO
INSERT [dbo].[transaction] ([transaction_id], [amount], [status], [type], [fee_id], [subscriber_id], [created_date], [updated_date], [subscription_paypal_id]) VALUES (14, 5, 1, N'Paypal', 18, 14, CAST(N'2024-03-29T16:06:46.747' AS DateTime), CAST(N'2024-03-29T16:06:46.747' AS DateTime), N'I-3S7TRF23K2MB')
GO
SET IDENTITY_INSERT [dbo].[transaction] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [category_unique]    Script Date: 4/4/2024 8:06:20 PM ******/
ALTER TABLE [dbo].[category] ADD  CONSTRAINT [category_unique] UNIQUE NONCLUSTERED 
(
	[category_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[artist] ADD  DEFAULT ((0)) FOR [total_subscribe]
GO
ALTER TABLE [dbo].[fee] ADD  DEFAULT ((0)) FOR [amount]
GO
ALTER TABLE [dbo].[image] ADD  DEFAULT ((0)) FOR [delete_flag]
GO
ALTER TABLE [dbo].[post] ADD  DEFAULT ('') FOR [note]
GO
ALTER TABLE [dbo].[report] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[transaction] ADD  DEFAULT ((0)) FOR [amount]
GO
ALTER TABLE [dbo].[account]  WITH CHECK ADD  CONSTRAINT [account_role_FK] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([role_id])
GO
ALTER TABLE [dbo].[account] CHECK CONSTRAINT [account_role_FK]
GO
ALTER TABLE [dbo].[artist]  WITH CHECK ADD  CONSTRAINT [artist_account_FK] FOREIGN KEY([email])
REFERENCES [dbo].[account] ([email])
GO
ALTER TABLE [dbo].[artist] CHECK CONSTRAINT [artist_account_FK]
GO
ALTER TABLE [dbo].[bookmark]  WITH CHECK ADD  CONSTRAINT [bookmark_account_FK] FOREIGN KEY([account_email])
REFERENCES [dbo].[account] ([email])
GO
ALTER TABLE [dbo].[bookmark] CHECK CONSTRAINT [bookmark_account_FK]
GO
ALTER TABLE [dbo].[bookmark]  WITH CHECK ADD  CONSTRAINT [bookmark_post_FK] FOREIGN KEY([post_id])
REFERENCES [dbo].[post] ([post_id])
GO
ALTER TABLE [dbo].[bookmark] CHECK CONSTRAINT [bookmark_post_FK]
GO
ALTER TABLE [dbo].[fee]  WITH CHECK ADD  CONSTRAINT [fee_artist_FK] FOREIGN KEY([artist_email])
REFERENCES [dbo].[artist] ([email])
GO
ALTER TABLE [dbo].[fee] CHECK CONSTRAINT [fee_artist_FK]
GO
ALTER TABLE [dbo].[image]  WITH CHECK ADD  CONSTRAINT [image_post_FK] FOREIGN KEY([post_id])
REFERENCES [dbo].[post] ([post_id])
GO
ALTER TABLE [dbo].[image] CHECK CONSTRAINT [image_post_FK]
GO
ALTER TABLE [dbo].[post]  WITH CHECK ADD  CONSTRAINT [post_artist_FK] FOREIGN KEY([artist_email])
REFERENCES [dbo].[artist] ([email])
GO
ALTER TABLE [dbo].[post] CHECK CONSTRAINT [post_artist_FK]
GO
ALTER TABLE [dbo].[post_category]  WITH CHECK ADD  CONSTRAINT [post_category_category_FK] FOREIGN KEY([category_id])
REFERENCES [dbo].[category] ([category_id])
GO
ALTER TABLE [dbo].[post_category] CHECK CONSTRAINT [post_category_category_FK]
GO
ALTER TABLE [dbo].[post_category]  WITH CHECK ADD  CONSTRAINT [post_category_post_FK] FOREIGN KEY([post_id])
REFERENCES [dbo].[post] ([post_id])
GO
ALTER TABLE [dbo].[post_category] CHECK CONSTRAINT [post_category_post_FK]
GO
ALTER TABLE [dbo].[reaction]  WITH CHECK ADD  CONSTRAINT [reaction_account_FK] FOREIGN KEY([account_email])
REFERENCES [dbo].[account] ([email])
GO
ALTER TABLE [dbo].[reaction] CHECK CONSTRAINT [reaction_account_FK]
GO
ALTER TABLE [dbo].[reaction]  WITH CHECK ADD  CONSTRAINT [reaction_post_FK] FOREIGN KEY([post_id])
REFERENCES [dbo].[post] ([post_id])
GO
ALTER TABLE [dbo].[reaction] CHECK CONSTRAINT [reaction_post_FK]
GO
ALTER TABLE [dbo].[report]  WITH CHECK ADD  CONSTRAINT [report_account_FK] FOREIGN KEY([reporter_email])
REFERENCES [dbo].[account] ([email])
GO
ALTER TABLE [dbo].[report] CHECK CONSTRAINT [report_account_FK]
GO
ALTER TABLE [dbo].[report]  WITH CHECK ADD  CONSTRAINT [report_post_FK] FOREIGN KEY([post_id])
REFERENCES [dbo].[post] ([post_id])
GO
ALTER TABLE [dbo].[report] CHECK CONSTRAINT [report_post_FK]
GO
ALTER TABLE [dbo].[subscriber]  WITH CHECK ADD  CONSTRAINT [subscriber_account_FK] FOREIGN KEY([email_user])
REFERENCES [dbo].[account] ([email])
GO
ALTER TABLE [dbo].[subscriber] CHECK CONSTRAINT [subscriber_account_FK]
GO
ALTER TABLE [dbo].[subscriber]  WITH CHECK ADD  CONSTRAINT [subscriber_artist_FK] FOREIGN KEY([email_artist])
REFERENCES [dbo].[artist] ([email])
GO
ALTER TABLE [dbo].[subscriber] CHECK CONSTRAINT [subscriber_artist_FK]
GO
ALTER TABLE [dbo].[transaction]  WITH CHECK ADD  CONSTRAINT [transaction_fee_FK] FOREIGN KEY([fee_id])
REFERENCES [dbo].[fee] ([fee_id])
GO
ALTER TABLE [dbo].[transaction] CHECK CONSTRAINT [transaction_fee_FK]
GO
ALTER TABLE [dbo].[transaction]  WITH CHECK ADD  CONSTRAINT [transaction_subscriber_FK] FOREIGN KEY([subscriber_id])
REFERENCES [dbo].[subscriber] ([subscriber_id])
GO
ALTER TABLE [dbo].[transaction] CHECK CONSTRAINT [transaction_subscriber_FK]
GO
USE [master]
GO
ALTER DATABASE [ArtHub] SET  READ_WRITE 
GO
