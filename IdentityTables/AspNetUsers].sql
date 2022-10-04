USE [ECommerce]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2022-10-04 2:04:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'314a6c3d-87b1-4621-940f-8f29d71641b7', N'testusertwo@test.com', N'TESTUSERTWO@TEST.COM', N'testusertwo@test.com', N'TESTUSERTWO@TEST.COM', 0, N'AQAAAAEAACcQAAAAECZSDn98CcQoWvSaMFYtGZaYBjbmh28PJHjo6A+bYi0cF845QYF6hnbG0X3RTK5s+g==', N'6OE6XTR6E6VTYBHWQ5FEM7SB67VQCH47', N'c9a29a94-e446-4add-861d-1fc462018259', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3de7139f-61fd-466d-a434-b83dd836c85e', N'collick@company.com', N'COLLICK@COMPANY.COM', N'collick@company.com', N'COLLICK@COMPANY.COM', 1, N'AQAAAAEAACcQAAAAEJCISfyqHkn6w+GBq9dXxqmpFDS7jfF3/u1W+zkmxOQjK5gA+yttd40GSY6LJYd2RA==', N'V3JGX6IYMV2E7WVEC7SD4IDJIVKIIQR7', N'ed602b6b-ca85-47a8-9168-ff65913ecda2', N'851212321321', 1, 0, NULL, 1, 0)
GO
