USE [sql_ventements_project]
GO
SET IDENTITY_INSERT [dbo].[category] ON 

INSERT [dbo].[category] ([id], [title], [categoryId]) VALUES (1, N'Hauts', NULL)
INSERT [dbo].[category] ([id], [title], [categoryId]) VALUES (2, N'Bas', NULL)
INSERT [dbo].[category] ([id], [title], [categoryId]) VALUES (3, N'Chaussures', NULL)
INSERT [dbo].[category] ([id], [title], [categoryId]) VALUES (4, N'Sweaters', 1)
INSERT [dbo].[category] ([id], [title], [categoryId]) VALUES (5, N'T-Shirts', 1)
INSERT [dbo].[category] ([id], [title], [categoryId]) VALUES (6, N'Chemises', 1)
INSERT [dbo].[category] ([id], [title], [categoryId]) VALUES (7, N'Pantalons', 2)
INSERT [dbo].[category] ([id], [title], [categoryId]) VALUES (8, N'Shorts', 2)
INSERT [dbo].[category] ([id], [title], [categoryId]) VALUES (9, N'Jupes', 2)
INSERT [dbo].[category] ([id], [title], [categoryId]) VALUES (10, N'Sneakers', 3)
INSERT [dbo].[category] ([id], [title], [categoryId]) VALUES (11, N'Chaussures de ville', 3)
SET IDENTITY_INSERT [dbo].[category] OFF
GO
SET IDENTITY_INSERT [dbo].[item] ON 

INSERT [dbo].[item] ([id], [label], [price], [imageItem], [descriptionItem], [categoryId]) VALUES (1, N'Dickies Cargo Pants', 99.989997863769531, N'https://www.bootbarn.com/dw/image/v2/BCCF_PRD/on/demandware.static/-/Sites-master-product-catalog-shp/default/dw253c445c/images/995/2000221995_021_D1.JPG?sw=980&sh=980&sm=fit', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.', 7)
INSERT [dbo].[item] ([id], [label], [price], [imageItem], [descriptionItem], [categoryId]) VALUES (2, N'Dickies Vermont Green', 59.9900016784668, N'http://i3.stycdn.net/images/2016/06/25/article/dickies/ks12c00304/dickies-vermont-sweater-oliv-1230-zoom-0.jpg', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.', 4)
INSERT [dbo].[item] ([id], [label], [price], [imageItem], [descriptionItem], [categoryId]) VALUES (3, N'Puma Thunder Spectra', 119.98999786376953, N'https://stockx-360.imgix.net/Puma-Thunder-Spectra/Images/Puma-Thunder-Spectra/Lv2/img01.jpg?auto=format,compress&w=559&q=90&dpr=2&updated_at=1538080256', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.', 10)
SET IDENTITY_INSERT [dbo].[item] OFF
GO
SET IDENTITY_INSERT [dbo].[addressuserv] ON 

INSERT [dbo].[addressuserv] ([id], [street], [homeNumber], [zip], [city]) VALUES (1, N'Boggess Street', 3035, N'75034', N'Frisco')
SET IDENTITY_INSERT [dbo].[addressuserv] OFF
GO
SET IDENTITY_INSERT [dbo].[userv] ON 

INSERT [dbo].[userv] ([id], [firstname], [lastname], [birthdate], [email], [encryptedPassword], [gender], [administrator], [addressId]) VALUES (1, N'John', N'Doe', CAST(N'2000-01-01T00:00:00.000' AS DateTime), N'doejohn@gmail.com', N'AQAAAAEAACcQAAAAEGKmZ5XXkYw6JlMOR4PCcfOv783y8zGnNair270woMiPeQ7mOANJQEwiq5REehRxYg==', N'M', 1, 1)
SET IDENTITY_INSERT [dbo].[userv] OFF
GO
SET IDENTITY_INSERT [dbo].[review] ON 

INSERT [dbo].[review] ([id], [stars], [title], [descriptionReview], [itemId], [uservId]) VALUES (1, 4, N'Très bon pantalon', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 1, 1)
INSERT [dbo].[review] ([id], [stars], [title], [descriptionReview], [itemId], [uservId]) VALUES (2, 5, N'Génial', N'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore.', 2, 1)
SET IDENTITY_INSERT [dbo].[review] OFF
GO
SET IDENTITY_INSERT [dbo].[baggedItem] ON 

INSERT [dbo].[baggedItem] ([id], [addedAt], [quantity], [size], [uservId], [itemId]) VALUES (1, CAST(N'2020-12-03T12:43:04.760' AS DateTime), 2, N'54', 1, 1)
INSERT [dbo].[baggedItem] ([id], [addedAt], [quantity], [size], [uservId], [itemId]) VALUES (2, CAST(N'2020-12-03T12:43:15.317' AS DateTime), 1, N'L', 1, 2)
INSERT [dbo].[baggedItem] ([id], [addedAt], [quantity], [size], [uservId], [itemId]) VALUES (3, CAST(N'2020-12-03T12:43:25.167' AS DateTime), 1, N'44', 1, 3)
SET IDENTITY_INSERT [dbo].[baggedItem] OFF
GO
SET IDENTITY_INSERT [dbo].[orderv] ON 

INSERT [dbo].[orderv] ([id], [isPaid], [orderedAt], [uservid]) VALUES (1, 0, CAST(N'2020-12-05T16:58:53.033' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[orderv] OFF
GO
SET IDENTITY_INSERT [dbo].[orderedItem] ON 

INSERT [dbo].[orderedItem] ([id], [quantity], [size], [ordervId], [itemId]) VALUES (1, 2, N'54', 1, 1)
INSERT [dbo].[orderedItem] ([id], [quantity], [size], [ordervId], [itemId]) VALUES (2, 1, N'L', 1, 2)
SET IDENTITY_INSERT [dbo].[orderedItem] OFF
GO
