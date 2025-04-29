USE [master]
GO

CREATE DATABASE [PRN212_PROJECT_BL5]
GO
USE [PRN212_PROJECT_BL5]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Acc_Role](
	[account_id] [nvarchar](15) NOT NULL,
	[role_id] [int] NOT NULL,
	[address] [nvarchar](max) NULL,
 CONSTRAINT [PK_Acc_Role_1] PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[name] [nvarchar](max) NOT NULL,
	[email] [nvarchar](max) NOT NULL,
	[phoneNumber] [nvarchar](15) NOT NULL,
	[password] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[phoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[cart_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[cart_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[food_id] [int] IDENTITY(1,1) NOT NULL,
	[food_name] [nvarchar](max) NOT NULL,
	[typef_id] [int] NOT NULL,
	[price] [int] NOT NULL,
	[image] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Food] PRIMARY KEY CLUSTERED 
(
	[food_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food_cart](
	[food_cart] [int] IDENTITY(1,1) NOT NULL,
	[cart_id] [int] NOT NULL,
	[food_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_Food_cart] PRIMARY KEY CLUSTERED 
(
	[food_cart] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food_order](
	[food_order] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NOT NULL,
	[food_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_Food_order] PRIMARY KEY CLUSTERED 
(
	[food_order] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[account_id] [nvarchar](15) NOT NULL,
	[status] [int] NOT NULL,
	[rate] [int] NOT NULL,
	[Time_oder] [datetime] NULL,
	[Time_finish] [datetime] NULL,
	[total] [int] NOT NULL,
	[Comment] [nvarchar](max) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[role_id] [int] NOT NULL,
	[role_define] [nvarchar](15) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[status_id] [int] NOT NULL,
	[name] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[status_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type_Food](
	[typef_id] [int] IDENTITY(1,1) NOT NULL,
	[typename] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Type_Food] PRIMARY KEY CLUSTERED 
(
	[typef_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Acc_Role] ([account_id], [role_id], [address]) VALUES (N'0941879612', 1, N'')
GO
INSERT [dbo].[Acc_Role] ([account_id], [role_id], [address]) VALUES (N'0951256194', 2, N'50 Nguyen Chi Thanh,Ba Dinh')
GO
INSERT [dbo].[Acc_Role] ([account_id], [role_id], [address]) VALUES (N'0978235672', 2, N'30 Tran Duy Hung')
GO
INSERT [dbo].[Acc_Role] ([account_id], [role_id], [address]) VALUES (N'09784562378', 2, N'30 Cau Giay')
GO
INSERT [dbo].[Acc_Role] ([account_id], [role_id], [address]) VALUES (N'0978563029', 3, N'')
GO
INSERT [dbo].[Acc_Role] ([account_id], [role_id], [address]) VALUES (N'0978568168', 2, N'60 Nguyen Phong Sac')
GO
INSERT [dbo].[Acc_Role] ([account_id], [role_id], [address]) VALUES (N'0995925418', 4, N'')
GO
INSERT [dbo].[Acc_Role] ([account_id], [role_id], [address]) VALUES (N'0998541619', 5, N'')
GO
INSERT [dbo].[Account] ([name], [email], [phoneNumber], [password]) VALUES (N'Nam', N'nam@gmail.com', N'0941879612', N'12345678')
GO
INSERT [dbo].[Account] ([name], [email], [phoneNumber], [password]) VALUES (N'Van', N'van@gmail.com', N'0951256194', N'12345678')
GO
INSERT [dbo].[Account] ([name], [email], [phoneNumber], [password]) VALUES (N'son', N'son@gmail.com', N'0978235672', N'12345678')
GO
INSERT [dbo].[Account] ([name], [email], [phoneNumber], [password]) VALUES (N'vu', N'vu@gmail.com', N'09784562378', N'12345678')
GO
INSERT [dbo].[Account] ([name], [email], [phoneNumber], [password]) VALUES (N'Tien', N'tien@gmail.com', N'0978563029', N'12345678')
GO
INSERT [dbo].[Account] ([name], [email], [phoneNumber], [password]) VALUES (N'minh', N'minh@gmail.com', N'0978568168', N'12345678')
GO
INSERT [dbo].[Account] ([name], [email], [phoneNumber], [password]) VALUES (N'Quynh', N'quynh@gmail.com', N'0995925418', N'12345678')
GO
INSERT [dbo].[Account] ([name], [email], [phoneNumber], [password]) VALUES (N'Minh', N'minh@gmail.com', N'0998541619', N'12345678')
GO
SET IDENTITY_INSERT [dbo].[Cart] ON 
GO
INSERT [dbo].[Cart] ([cart_id], [account_id]) VALUES (5, N'0978235672')
GO
SET IDENTITY_INSERT [dbo].[Cart] OFF
GO
SET IDENTITY_INSERT [dbo].[Food] ON 
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (1, N'Bún chả', 1, 50000, N'https://ik.imagekit.io/tvlk/blog/2024/10/bun-cha-ha-noi-1-1024x682.jpeg?tr=q-70,c-at_max,w-500,h-300,dpr-2')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (2, N'Bún bò Huế', 1, 60000, N'https://www.google.com/imgres?q=B%C3%BAn%20b%C3%B2%20Hu%E1%BA%BF&imgurl=https%3A%2F%2Fsatrafoods.com.vn%2Fuploads%2FImages%2Fmon-ngon-moi-ngay%2Fbun-bo-hue.jpg&imgrefurl=https%3A%2F%2Fsatrafoods.com.vn%2Fvn%2Fmon-ngon-moi-ngay%2Fcach-nau-bun-bo-thom-ngon-dam-da-chuan-vi-hue.html&docid=X0t5lXZ1HTtMkM&tbnid=_92-https://cdn-i.vtcnews.vn/resize/th/upload/2023/04/15/bun-bo-hue-https://cdn-i.vtcnews.vn/resize/th/upload/2023/04/15/bun-bo-hue-15240814.jpeg')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (3, N'Bún riêu', 1, 40000, N'https://www.google.com/imgres?q=B%C3%BAn%20ri%C3%AAu&imgurl=https%3A%2F%2Fcdn.tgdd.vn%2F2020%2F08%2FCookProduct%2FUntitled-1-1200x676-10.jpg&imgrefurl=https%3A%2F%2Fwww.dienmayxanh.com%2Fvao-bep%2Fcach-nau-bun-rieu-cua-chay-cuc-ngon-dam-da-cho-bua-an-00349&docid=WFMvKe-AWD_BxM&tbnid=H8lgIMS-https://i1-giadinh.vnecdn.net/2024/02/17/Buoc-8-Thanh-pham-1-1-2167-1708158993.jpg?w=1020&h=0&q=100&dpr=1&fit=crop&s=2xgfBP9nJS3-Y1xFBcNPAQ')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (4, N'Phở bò', 2, 50000, N'https://www.google.com/imgres?q=Ph%E1%BB%9F%20b%C3%B2&imgurl=https%3A%2F%2Fcdn2.fptshop.com.vn%2Funsafe%2F1920x0%2Ffilters%3Aformat(webp)%3Aquality(75)%2Fcach_nau_pho_bo_nam_dinh_0_1d94be153c.png&imgrefurl=https%3A%2F%2Ffptshop.com.vn%2Ftin-tuc%2Fdien-may%2Fcach-nau-pho-bo-nam-dinh-https://media-cdn-v2.laodong.vn/storage/newsportal/2024/10/27/1413556/Pho-Ngoc-Vuong-3-Min.jpg')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (5, N'Phở gà', 2, 45000, N'https://www.google.com/imgres?q=Ph%E1%BB%9F%20g%C3%A0&imgurl=https%3A%2F%2Fphogaphocobaophuc.com%2Fwp-content%2Fuploads%2F2022%2F11%2Fpho-ga-dui-logo.jpg&imgrefurl=https%3A%2F%2Fphogaphocobaophuc.com%2Fco-the-ban-chua-biet-nguon-goc-cua-pho-gahttps://i1-giadinh.vnecdn.net/2021/09/11/Phoga1-1631342846-8318-1631342910.jpg?w=0&h=0&q=100&dpr=2&fit=crop&s=lGvobJhbawgiJL4uvOjDYw')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (6, N'Cà phê đen', 3, 20000, N'https://www.google.com/imgres?q=C%C3%A0%20ph%C3%AA%20%C4%91en&imgurl=https%3A%2F%2Fbillballcoffeetea.com%2Fupload%2Fproduct%2Fimg3589-3619-8202.jpg&imgrefurl=https%3A%2F%2Fbillballcoffeetea.com%2Fca-phe-den-nong-https://cdn.nhathuoclongchau.com.vn/unsafe/800x0/https://cms-prod.s3-sgn09.fptcloud.com/bai_vietca_phe_den_bao_nhieu_calo_uong_nhieu_co_tot_khong_html_1_ebb28c9c42.png')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (7, N'Cà phê sữa', 3, 25000, N'https://www.google.com/imgres?q=C%C3%A0%20ph%C3%AA%20s%E1%BB%AFa&imgurl=https%3A%2F%2Fcdn.tgdd.vn%2F2021%2F08%2FCookRecipe%2FGalleryStep%2Fthanh-pham-314.jpg&imgrefurl=https%3A%2F%2Fwww.dienmayxanh.com%2Fvao-bep%2Fcach-lam-ca-phe-sua-dua-thom-beo-uong-cuc-ngon-thu-la-ghien-10704&docid=t20C4bu5ERgeLM&tbnid=KJAFFPZrKBby-https://tiki.vn/blog/wp-content/uploads/2023/10/iced-coffee-on-table-1024x703.jpg')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (8, N'Trà sữa trân châu', 4, 30000, N'https://www.google.com/imgres?q=Tr%C3%A0%20s%E1%BB%AFa%20tr%C3%A2n%20ch%C3%A2u&imgurl=https%3A%2F%2Fstore.longphuong.vn%2Fwp-content%2Fuploads%2F2022%2F09%2Ftra-sua-tran-chau-duong-den.jpg&imgrefurl=https%3A%2F%2Fstore.longphuong.vn%2Fcach-lam-tra-sua-tran-chau-duong-den-don-gian-ma-ngon-tai-nhahttps://channel.mediacdn.vn/thumb_w/640/prupload/879/2018/10/img20181020192656096.jpg')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (9, N'Trà sữa matcha', 4, 35000, N'https://www.google.com/imgres?q=Tr%C3%A0%20s%E1%BB%AFa%20matcha&imgurl=https%3A%2F%2Fbizweb.dktcdn.net%2F100%2F421%2F036%2Ffiles%2Fcach-lam-tra-sua-matcha-kem-cheese-jpeg.jpg%3Fv%3D1639127701404&imgrefurl=https%3A%2F%2Fchophache.com%2Fcach-pha-tra-sua-matcha-thom-ngon-tai-https://bizweb.dktcdn.net/100/421/036/files/cach-lam-tra-sua-matcha-kem-cheese-jpeg.jpg?v=1639127701404')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (10, N'Pizza Margherita', 5, 120000, N'https://www.google.com/imgres?q=Pizza%20Margherita&imgurl=https%3A%2F%2Fsafrescobaldistatic.blob.core.windows.net%2Fmedia%2F2022%2F11%2FPIZZA-MARGHERITA.jpg&imgrefurl=https%3A%2F%2Fwww.danzantewines.com%2Frecipes%2Fmargherita-pizzahttps://bakewithzoha.com/wp-content/uploads/2023/08/pizza-margherita-7-1152x1536.jpg')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (11, N'Pizza Pepperoni', 5, 150000, N'https://www.google.com/imgres?q=Pizza%20Pepperoni&imgurl=http%3A%2F%2Fwww.perfectitaliano.com.au%2Fcontent%2Fdam%2Fperfectitaliano-aus%2Frecipe%2Fpepperoni_pizza_standard.jpg&imgrefurl=https%3A%2F%2Fwww.perfectitaliano.com.au%2Fen%2Frecipes%2Fpepperoni-https://www.perfectitaliano.com.au/content/dam/perfectitaliano-aus/recipe/pepperoni_pizza_standard.jpg')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (12, N'Bánh mì thịt', 6, 25000, N'https://www.google.com/imgres?q=B%C3%A1nh%20m%C3%AC%20th%E1%BB%8Bt&imgurl=https%3A%2F%2Fwww.huongnghiepaau.com%2Fwp-content%2Fuploads%2F2019%2F08%2Fbanh-mi-kep-thit-nuong-thom-phuc.jpg&imgrefurl=https%3A%2F%2Fwww.huongnghiepaau.com%2Fbanh-mi-kep-thit-viet-https://deliciousvietnam.net/wp-content/uploads/BanhMi_NgonVietnam01-1200x750.jpg')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (13, N'Bánh mì chả', 6, 20000, N'https://www.google.com/imgres?q=B%C3%A1nh%20m%C3%AC%20ch%E1%BA%A3&imgurl=https%3A%2F%2Fpatecotden.net%2Fwp-content%2Fuploads%2F2023%2F08%2Fbanh-mi-cha-nong-ngon-3.png&imgrefurl=https%3A%2F%2Fpatecotden.net%2Fbanh-mi-cha-nong-ngon%2F&docid=fUjU8UZ_jJliXM&tbnid=D5-4gzw-tag_iM&vet=12ahUKEwjXjs3-__WMAxWL7DQHHVvRAqYQM3oECHgQAA..i&w=602&h=451&hcb=2&ved=2ahUKEwjXjs3-https://patecotden.net/wp-content/uploads/2023/08/banh-mi-cha-nong-ngon-3.png')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (14, N'Mì xào', 7, 35000, N'https://i1-giadinh.vnecdn.net/2022/07/30/Thanh-pham-1-1-2409-1659167237.jpg?w=1020&h=0&q=100&dpr=1&fit=crop&s=qvlTDfY5CySCUsPOSvG_Gw')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (15, N'Mì quảng', 7, 40000, N'https://www.google.com/imgres?q=M%C3%AC%20qu%E1%BA%A3ng&imgurl=https%3A%2F%2Fvietnamtrips.com%2Ffiles%2Fphotos%2Farticle1276%2Fmi-quang-noodle.jpg&imgrefurl=https%3A%2F%2Fvietnamtrips.com%2Fmi-quang-noodle&docid=9Ib3RUCtvxv9dM&tbnid=woU7cBHCn-https://static.vinwonders.com/2022/10/mi-quang-hoi-an-02.jpg')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (16, N'Cơm tấm', 8, 30000, N'https://www.google.com/imgres?q=C%C6%A1m%20t%E1%BA%A5m&imgurl=https%3A%2F%2Fstatic.vinwonders.com%2Fproduction%2Fcom-tam-sai-gon-2.jpg&imgrefurl=https%3A%2F%2Fvinwonders.com%2Fvi%2Fwonderpedia%2Fnews%2Fcom-tam-sai-gonhttps://static.vinwonders.com/production/com-tam-sai-gon-banner.jpg')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (17, N'Cơm gà', 8, 35000, N'https://www.google.com/imgres?q=c%C6%A1m%20g%C3%A0&imgurl=https%3A%2F%2Fi-giadinh.vnecdn.net%2F2021%2F01%2F29%2Fcom2-1611892464-7028-1611892596.jpg&imgrefurl=https%3A%2F%2Fvnexpress.net%2Fcach-lam-com-ga-hoi-an-https://i1-giadinh.vnecdn.net/2021/01/29/com2-1611892464-7028-1611892596.jpg?w=0&h=0&q=100&dpr=2&fit=crop&s=fSiuKQfCjNiGoPz8k46N-Q')
GO
INSERT [dbo].[Food] ([food_id], [food_name], [typef_id], [price], [image]) VALUES (18, N'Bun hai san', 1, 50000, N'https://cdn.tgdd.vn/2021/08/CookProduct/Duanmoi(7)-1200x676.jpg')
GO
SET IDENTITY_INSERT [dbo].[Food] OFF
GO
SET IDENTITY_INSERT [dbo].[Food_cart] ON 
GO
INSERT [dbo].[Food_cart] ([food_cart], [cart_id], [food_id], [quantity]) VALUES (11, 5, 2, 1)
GO
SET IDENTITY_INSERT [dbo].[Food_cart] OFF
GO
SET IDENTITY_INSERT [dbo].[Food_order] ON 
GO
INSERT [dbo].[Food_order] ([food_order], [order_id], [food_id], [quantity]) VALUES (1, 1, 1, 2)
GO
INSERT [dbo].[Food_order] ([food_order], [order_id], [food_id], [quantity]) VALUES (2, 1, 2, 1)
GO
INSERT [dbo].[Food_order] ([food_order], [order_id], [food_id], [quantity]) VALUES (3, 1, 3, 1)
GO
INSERT [dbo].[Food_order] ([food_order], [order_id], [food_id], [quantity]) VALUES (4, 2, 5, 1)
GO
INSERT [dbo].[Food_order] ([food_order], [order_id], [food_id], [quantity]) VALUES (5, 2, 6, 1)
GO
INSERT [dbo].[Food_order] ([food_order], [order_id], [food_id], [quantity]) VALUES (6, 3, 6, 2)
GO
INSERT [dbo].[Food_order] ([food_order], [order_id], [food_id], [quantity]) VALUES (7, 3, 5, 1)
GO
INSERT [dbo].[Food_order] ([food_order], [order_id], [food_id], [quantity]) VALUES (8, 4, 2, 1)
GO
INSERT [dbo].[Food_order] ([food_order], [order_id], [food_id], [quantity]) VALUES (9, 4, 6, 2)
GO
INSERT [dbo].[Food_order] ([food_order], [order_id], [food_id], [quantity]) VALUES (10, 4, 4, 1)
GO
INSERT [dbo].[Food_order] ([food_order], [order_id], [food_id], [quantity]) VALUES (11, 5, 2, 1)
GO
INSERT [dbo].[Food_order] ([food_order], [order_id], [food_id], [quantity]) VALUES (12, 5, 3, 2)
GO
INSERT [dbo].[Food_order] ([food_order], [order_id], [food_id], [quantity]) VALUES (13, 5, 5, 2)
GO
SET IDENTITY_INSERT [dbo].[Food_order] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 
GO
INSERT [dbo].[Order] ([order_id], [account_id], [status], [rate], [Time_oder], [Time_finish], [total], [Comment]) VALUES (1, N'0951256194', 8, 5, CAST(N'2025-04-27T21:28:57.000' AS DateTime), CAST(N'2025-04-28T22:27:19.243' AS DateTime), 200000, N'Món ăn rất ngon ')
GO
INSERT [dbo].[Order] ([order_id], [account_id], [status], [rate], [Time_oder], [Time_finish], [total], [Comment]) VALUES (2, N'0951256194', 8, 4, CAST(N'2025-04-28T22:41:02.027' AS DateTime), CAST(N'2025-04-28T22:44:22.513' AS DateTime), 65000, N'Món ăn rất ngon nhưng ship hơi lâu')
GO
INSERT [dbo].[Order] ([order_id], [account_id], [status], [rate], [Time_oder], [Time_finish], [total], [Comment]) VALUES (3, N'0951256194', 8, 3, CAST(N'2025-04-29T13:41:41.877' AS DateTime), CAST(N'2025-04-29T13:43:32.193' AS DateTime), 85000, N'')
GO
INSERT [dbo].[Order] ([order_id], [account_id], [status], [rate], [Time_oder], [Time_finish], [total], [Comment]) VALUES (4, N'0978568168', 8, 4, CAST(N'2025-04-29T13:51:16.977' AS DateTime), CAST(N'2025-04-29T13:53:09.780' AS DateTime), 150000, N'')
GO
INSERT [dbo].[Order] ([order_id], [account_id], [status], [rate], [Time_oder], [Time_finish], [total], [Comment]) VALUES (5, N'0951256194', 8, 3, CAST(N'2025-04-29T15:31:18.940' AS DateTime), CAST(N'2025-04-29T15:33:26.847' AS DateTime), 230000, N'cvd')
GO
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
INSERT [dbo].[Role] ([role_id], [role_define]) VALUES (1, N'Staff')
GO
INSERT [dbo].[Role] ([role_id], [role_define]) VALUES (2, N'Customer')
GO
INSERT [dbo].[Role] ([role_id], [role_define]) VALUES (3, N'KitchenEmploy')
GO
INSERT [dbo].[Role] ([role_id], [role_define]) VALUES (4, N'OrderEmploy')
GO
INSERT [dbo].[Role] ([role_id], [role_define]) VALUES (5, N'Shipper')
GO
INSERT [dbo].[Status] ([status_id], [name]) VALUES (1, N'Order')
GO
INSERT [dbo].[Status] ([status_id], [name]) VALUES (2, N'Pay successful')
GO
INSERT [dbo].[Status] ([status_id], [name]) VALUES (3, N'Accept order')
GO
INSERT [dbo].[Status] ([status_id], [name]) VALUES (4, N'Done food')
GO
INSERT [dbo].[Status] ([status_id], [name]) VALUES (5, N'Give to shipper')
GO
INSERT [dbo].[Status] ([status_id], [name]) VALUES (6, N'Shipping')
GO
INSERT [dbo].[Status] ([status_id], [name]) VALUES (7, N'Recive order')
GO
INSERT [dbo].[Status] ([status_id], [name]) VALUES (8, N'Finish')
GO
SET IDENTITY_INSERT [dbo].[Type_Food] ON 
GO
INSERT [dbo].[Type_Food] ([typef_id], [typename]) VALUES (1, N'Bún')
GO
INSERT [dbo].[Type_Food] ([typef_id], [typename]) VALUES (2, N'Phở')
GO
INSERT [dbo].[Type_Food] ([typef_id], [typename]) VALUES (3, N'Cà phê')
GO
INSERT [dbo].[Type_Food] ([typef_id], [typename]) VALUES (4, N'Trà sữa')
GO
INSERT [dbo].[Type_Food] ([typef_id], [typename]) VALUES (5, N'Pizza')
GO
INSERT [dbo].[Type_Food] ([typef_id], [typename]) VALUES (6, N'Bánh mì')
GO
INSERT [dbo].[Type_Food] ([typef_id], [typename]) VALUES (7, N'Mì')
GO
INSERT [dbo].[Type_Food] ([typef_id], [typename]) VALUES (8, N'Cơm')
GO
SET IDENTITY_INSERT [dbo].[Type_Food] OFF
GO
ALTER TABLE [dbo].[Acc_Role]  WITH CHECK ADD  CONSTRAINT [FK_Acc_Role_Account] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([phoneNumber])
GO
ALTER TABLE [dbo].[Acc_Role] CHECK CONSTRAINT [FK_Acc_Role_Account]
GO
ALTER TABLE [dbo].[Acc_Role]  WITH CHECK ADD  CONSTRAINT [FK_Acc_Role_Role] FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([role_id])
GO
ALTER TABLE [dbo].[Acc_Role] CHECK CONSTRAINT [FK_Acc_Role_Role]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Account] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([phoneNumber])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Account]
GO
ALTER TABLE [dbo].[Food]  WITH CHECK ADD  CONSTRAINT [FK_Food_Type_Food] FOREIGN KEY([typef_id])
REFERENCES [dbo].[Type_Food] ([typef_id])
GO
ALTER TABLE [dbo].[Food] CHECK CONSTRAINT [FK_Food_Type_Food]
GO
ALTER TABLE [dbo].[Food_cart]  WITH CHECK ADD  CONSTRAINT [FK_Food_cart_Cart] FOREIGN KEY([cart_id])
REFERENCES [dbo].[Cart] ([cart_id])
GO
ALTER TABLE [dbo].[Food_cart] CHECK CONSTRAINT [FK_Food_cart_Cart]
GO
ALTER TABLE [dbo].[Food_cart]  WITH CHECK ADD  CONSTRAINT [FK_Food_cart_Food] FOREIGN KEY([food_id])
REFERENCES [dbo].[Food] ([food_id])
GO
ALTER TABLE [dbo].[Food_cart] CHECK CONSTRAINT [FK_Food_cart_Food]
GO
ALTER TABLE [dbo].[Food_order]  WITH CHECK ADD  CONSTRAINT [FK_Food_order_Food] FOREIGN KEY([food_id])
REFERENCES [dbo].[Food] ([food_id])
GO
ALTER TABLE [dbo].[Food_order] CHECK CONSTRAINT [FK_Food_order_Food]
GO
ALTER TABLE [dbo].[Food_order]  WITH CHECK ADD  CONSTRAINT [FK_Food_order_Order] FOREIGN KEY([order_id])
REFERENCES [dbo].[Order] ([order_id])
GO
ALTER TABLE [dbo].[Food_order] CHECK CONSTRAINT [FK_Food_order_Order]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Account] FOREIGN KEY([account_id])
REFERENCES [dbo].[Account] ([phoneNumber])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Account]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Status] FOREIGN KEY([status])
REFERENCES [dbo].[Status] ([status_id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Status]
GO
USE [master]
GO
ALTER DATABASE [PRN212_PROJECT_BL5] SET  READ_WRITE 
GO
