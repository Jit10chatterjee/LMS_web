USE [LMS]
GO
/****** Object:  Table [dbo].[CourseDetails]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseDetails](
	[CourseDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[CourseMasterId] [int] NOT NULL,
	[CourseName] [nvarchar](max) NULL,
	[CourseProvider] [nvarchar](max) NULL,
	[CourseImage] [nvarchar](max) NULL,
	[CourseFees] [numeric](18, 2) NULL,
	[IsLatest] [bit] NULL,
	[IsDemanding] [bit] NULL,
	[IsFree] [bit] NULL,
	[Duration] [int] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__CourseDe__3AF4790CBDE5D10B] PRIMARY KEY CLUSTERED 
(
	[CourseDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseMaster]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseMaster](
	[CourseMasterId] [int] IDENTITY(1,1) NOT NULL,
	[CourseMasterType] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CourseMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseMedia]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseMedia](
	[CourseMediaId] [int] IDENTITY(1,1) NOT NULL,
	[CourseDetailsId] [int] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[PosterLink] [nvarchar](max) NULL,
	[VideoLink] [nvarchar](max) NULL,
	[CompletionPercentage] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CourseMediaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseSpecialization]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseSpecialization](
	[CourseSpecializationId] [int] IDENTITY(1,1) NOT NULL,
	[CourseDetailsId] [int] NOT NULL,
	[SpecializationId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CourseSpecializationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CouserStatus]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CouserStatus](
	[CouserStatusId] [int] IDENTITY(1,1) NOT NULL,
	[CourseStatusCode] [nvarchar](200) NOT NULL,
	[CourseStatusName] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CouserStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Instructor]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instructor](
	[InstructorId] [int] IDENTITY(1,1) NOT NULL,
	[InstructorName] [nvarchar](max) NULL,
	[Designation] [nvarchar](max) NULL,
	[FacebookLink] [nvarchar](max) NULL,
	[InstagramLink] [nvarchar](max) NULL,
	[XLink] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[InstructorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentStatus]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentStatus](
	[PaymentStatusId] [int] IDENTITY(1,1) NOT NULL,
	[PaymentStatusCode] [nvarchar](200) NOT NULL,
	[PaymentStatusName] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skill]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skill](
	[SkillId] [int] IDENTITY(1,1) NOT NULL,
	[SkillName] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SkillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialization]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialization](
	[SpecializationId] [int] IDENTITY(1,1) NOT NULL,
	[Specialization] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SpecializationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCourse]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCourse](
	[UserCourseId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CourseDetailsId] [int] NOT NULL,
	[PaymentStatusId] [int] NULL,
	[IsCourseContinue] [bit] NULL,
	[CourseStatusId] [int] NULL,
	[CompletionPercentage] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserCourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetails](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[LMSUserId] [nvarchar](450) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDocument]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDocument](
	[UserDocumentId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[OrginalFileName] [nvarchar](max) NOT NULL,
	[UploadFileName] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserDocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserExperienceDetails]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserExperienceDetails](
	[UserExperienceDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[UserProfileId] [int] NOT NULL,
	[IsInternShip] [bit] NULL,
	[IsPartTime] [bit] NULL,
	[IsFullTime] [bit] NULL,
	[CompanyName] [nvarchar](max) NULL,
	[DateOfJoining] [datetime] NOT NULL,
	[LastWorkingDay] [datetime] NULL,
	[IsCurrentCompnay] [bit] NULL,
	[DesignNation] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserExperienceDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[UserProfileId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserProfileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserQualificationDetails]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserQualificationDetails](
	[UserQualificationDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[UserProfileId] [int] NOT NULL,
	[IsSchool] [bit] NULL,
	[Class] [int] NULL,
	[IsCollege] [bit] NULL,
	[NameOfSchool] [nvarchar](max) NULL,
	[NameOfCollge] [nvarchar](max) NULL,
	[CGPA] [decimal](5, 2) NULL,
	[Percentage] [decimal](5, 2) NULL,
	[PassoutYear] [datetime] NULL,
	[IsYearGap] [bit] NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedOn] [datetime] NULL,
	[ModifiedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserQualificationDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Videos]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Videos](
	[VideosId] [int] IDENTITY(1,1) NOT NULL,
	[CourseDetailsId] [int] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Poster] [nvarchar](max) NULL,
	[Link] [nvarchar](max) NULL,
	[CompletetionPercentage] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[VideosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CourseDetails] ON 
GO
INSERT [dbo].[CourseDetails] ([CourseDetailsId], [CourseMasterId], [CourseName], [CourseProvider], [CourseImage], [CourseFees], [IsLatest], [IsDemanding], [IsFree], [Duration], [IsActive]) VALUES (1, 1, N'C fundamentals with pointers', N'Edsphere', N'https://i.ytimg.com/vi/hAzTp1Ojw88/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLB-GMUiU27PBXZOOfICZ0cLjXtzVA', CAST(0.00 AS Numeric(18, 2)), 0, 0, 1, 45, 1)
GO
SET IDENTITY_INSERT [dbo].[CourseDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[CourseMaster] ON 
GO
INSERT [dbo].[CourseMaster] ([CourseMasterId], [CourseMasterType]) VALUES (1, N'Programming Languages')
GO
INSERT [dbo].[CourseMaster] ([CourseMasterId], [CourseMasterType]) VALUES (2, N'Databases')
GO
INSERT [dbo].[CourseMaster] ([CourseMasterId], [CourseMasterType]) VALUES (3, N'Backend Development')
GO
INSERT [dbo].[CourseMaster] ([CourseMasterId], [CourseMasterType]) VALUES (4, N'Frontend Development')
GO
INSERT [dbo].[CourseMaster] ([CourseMasterId], [CourseMasterType]) VALUES (5, N'Gen AI')
GO
SET IDENTITY_INSERT [dbo].[CourseMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[CourseMedia] ON 
GO
INSERT [dbo].[CourseMedia] ([CourseMediaId], [CourseDetailsId], [Title], [PosterLink], [VideoLink], [CompletionPercentage]) VALUES (1, 1, N'Introduction to C', N'https://media.licdn.com/dms/image/v2/D4D12AQEmzusF9C5JfA/article-cover_image-shrink_600_2000/article-cover_image-shrink_600_2000/0/1683178954137?e=2147483647&v=beta&t=nsmAzz64DpJaO7qloxqYXjv4Wgk3dXZdzUw1_KQvyLE', N'https://www.youtube.com/watch?v=EjavYOFoJJ0&list=PLdo5W4Nhv31a8UcMN9-35ghv8qyFWD9_S&index=1', 10)
GO
INSERT [dbo].[CourseMedia] ([CourseMediaId], [CourseDetailsId], [Title], [PosterLink], [VideoLink], [CompletionPercentage]) VALUES (2, 1, N'Features of C', N'https://media.licdn.com/dms/image/v2/D4D12AQEmzusF9C5JfA/article-cover_image-shrink_600_2000/article-cover_image-shrink_600_2000/0/1683178954137?e=2147483647&v=beta&t=nsmAzz64DpJaO7qloxqYXjv4Wgk3dXZdzUw1_KQvyLE', N'https://www.youtube.com/watch?v=i3SWaOhjPCY&list=PLdo5W4Nhv31a8UcMN9-35ghv8qyFWD9_S&index=4', 20)
GO
INSERT [dbo].[CourseMedia] ([CourseMediaId], [CourseDetailsId], [Title], [PosterLink], [VideoLink], [CompletionPercentage]) VALUES (3, 1, N'Variables in C', N'https://media.licdn.com/dms/image/v2/D4D12AQEmzusF9C5JfA/article-cover_image-shrink_600_2000/article-cover_image-shrink_600_2000/0/1683178954137?e=2147483647&v=beta&t=nsmAzz64DpJaO7qloxqYXjv4Wgk3dXZdzUw1_KQvyLE', N'https://www.youtube.com/watch?v=dhh5lrXXXYw&list=PLdo5W4Nhv31a8UcMN9-35ghv8qyFWD9_S&index=8', 20)
GO
INSERT [dbo].[CourseMedia] ([CourseMediaId], [CourseDetailsId], [Title], [PosterLink], [VideoLink], [CompletionPercentage]) VALUES (4, 1, N'Keywords and Identifiers in C', N'https://media.licdn.com/dms/image/v2/D4D12AQEmzusF9C5JfA/article-cover_image-shrink_600_2000/article-cover_image-shrink_600_2000/0/1683178954137?e=2147483647&v=beta&t=nsmAzz64DpJaO7qloxqYXjv4Wgk3dXZdzUw1_KQvyLE', N'https://www.youtube.com/watch?v=Ywnv78X7TAg&list=PLdo5W4Nhv31a8UcMN9-35ghv8qyFWD9_S&index=9', 20)
GO
INSERT [dbo].[CourseMedia] ([CourseMediaId], [CourseDetailsId], [Title], [PosterLink], [VideoLink], [CompletionPercentage]) VALUES (5, 1, N'C Zero to Hero', N'https://i.ytimg.com/vi/YXcgD8hRHYY/hq720.jpg?sqp=-oaymwEnCNAFEJQDSFryq4qpAxkIARUAAIhCGAHYAQHiAQoIGBACGAY4AUAB&rs=AOn4CLBs-YBQRPvWj_BXEFTnpyn_Lxpfvg', N'https://www.youtube.com/watch?v=YXcgD8hRHYY', 30)
GO
SET IDENTITY_INSERT [dbo].[CourseMedia] OFF
GO
SET IDENTITY_INSERT [dbo].[CourseSpecialization] ON 
GO
INSERT [dbo].[CourseSpecialization] ([CourseSpecializationId], [CourseDetailsId], [SpecializationId]) VALUES (1, 1, 2)
GO
INSERT [dbo].[CourseSpecialization] ([CourseSpecializationId], [CourseDetailsId], [SpecializationId]) VALUES (2, 1, 3)
GO
SET IDENTITY_INSERT [dbo].[CourseSpecialization] OFF
GO
SET IDENTITY_INSERT [dbo].[CouserStatus] ON 
GO
INSERT [dbo].[CouserStatus] ([CouserStatusId], [CourseStatusCode], [CourseStatusName]) VALUES (1, N'ACTIVE', N'Avalibale')
GO
INSERT [dbo].[CouserStatus] ([CouserStatusId], [CourseStatusCode], [CourseStatusName]) VALUES (2, N'INACTIVE', N'Not Available')
GO
SET IDENTITY_INSERT [dbo].[CouserStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[PaymentStatus] ON 
GO
INSERT [dbo].[PaymentStatus] ([PaymentStatusId], [PaymentStatusCode], [PaymentStatusName]) VALUES (2, N'PAY_D', N'Paid')
GO
INSERT [dbo].[PaymentStatus] ([PaymentStatusId], [PaymentStatusCode], [PaymentStatusName]) VALUES (3, N'PAY_R', N'Payment Required')
GO
SET IDENTITY_INSERT [dbo].[PaymentStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[Skill] ON 
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (1, N'C')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (2, N'C++')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (3, N'C#')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (4, N'JAVA')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (5, N'Python')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (6, N'MySQL')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (7, N'PL/SQL')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (8, N'JavaScript')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (9, N'HTML')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (10, N'CSS')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (11, N'JQuery')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (12, N'Angular')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (13, N'React')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (14, N'Django')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (15, N'.NET')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (16, N'Spring Boot')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (17, N'DSA')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (18, N'Machine Learning')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (19, N'AI')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (20, N'Node JS')
GO
INSERT [dbo].[Skill] ([SkillId], [SkillName]) VALUES (21, N'.NET Core')
GO
SET IDENTITY_INSERT [dbo].[Skill] OFF
GO
SET IDENTITY_INSERT [dbo].[Specialization] ON 
GO
INSERT [dbo].[Specialization] ([SpecializationId], [Specialization]) VALUES (1, N'Object Oriented Program concept')
GO
INSERT [dbo].[Specialization] ([SpecializationId], [Specialization]) VALUES (2, N'Clean code structure')
GO
INSERT [dbo].[Specialization] ([SpecializationId], [Specialization]) VALUES (3, N'Optimization fast response')
GO
INSERT [dbo].[Specialization] ([SpecializationId], [Specialization]) VALUES (4, N'Debugging')
GO
SET IDENTITY_INSERT [dbo].[Specialization] OFF
GO
SET IDENTITY_INSERT [dbo].[UserDetails] ON 
GO
INSERT [dbo].[UserDetails] ([UserId], [LMSUserId], [CreatedOn]) VALUES (1, N'54bf2afc-e9e9-4746-b6de-9bd18cf921dd', CAST(N'2025-08-23T17:19:57.860' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[UserDetails] OFF
GO
ALTER TABLE [dbo].[UserCourse] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[UserProfile] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[UserQualificationDetails] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[CourseDetails]  WITH CHECK ADD  CONSTRAINT [FK_CourseDetails_CourseMaster] FOREIGN KEY([CourseMasterId])
REFERENCES [dbo].[CourseMaster] ([CourseMasterId])
GO
ALTER TABLE [dbo].[CourseDetails] CHECK CONSTRAINT [FK_CourseDetails_CourseMaster]
GO
ALTER TABLE [dbo].[CourseMedia]  WITH CHECK ADD  CONSTRAINT [FK_CourseMedia_CourseDetails] FOREIGN KEY([CourseDetailsId])
REFERENCES [dbo].[CourseDetails] ([CourseDetailsId])
GO
ALTER TABLE [dbo].[CourseMedia] CHECK CONSTRAINT [FK_CourseMedia_CourseDetails]
GO
ALTER TABLE [dbo].[CourseSpecialization]  WITH CHECK ADD  CONSTRAINT [FK_CourseSpecialization_CourseDetails] FOREIGN KEY([CourseDetailsId])
REFERENCES [dbo].[CourseDetails] ([CourseDetailsId])
GO
ALTER TABLE [dbo].[CourseSpecialization] CHECK CONSTRAINT [FK_CourseSpecialization_CourseDetails]
GO
ALTER TABLE [dbo].[CourseSpecialization]  WITH CHECK ADD  CONSTRAINT [FK_CourseSpecialization_Specialization] FOREIGN KEY([SpecializationId])
REFERENCES [dbo].[Specialization] ([SpecializationId])
GO
ALTER TABLE [dbo].[CourseSpecialization] CHECK CONSTRAINT [FK_CourseSpecialization_Specialization]
GO
ALTER TABLE [dbo].[UserCourse]  WITH CHECK ADD  CONSTRAINT [FK_UserCourse_CourseDetails] FOREIGN KEY([CourseDetailsId])
REFERENCES [dbo].[CourseDetails] ([CourseDetailsId])
GO
ALTER TABLE [dbo].[UserCourse] CHECK CONSTRAINT [FK_UserCourse_CourseDetails]
GO
ALTER TABLE [dbo].[UserCourse]  WITH CHECK ADD  CONSTRAINT [FK_UserCourse_UserDetails] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserDetails] ([UserId])
GO
ALTER TABLE [dbo].[UserCourse] CHECK CONSTRAINT [FK_UserCourse_UserDetails]
GO
ALTER TABLE [dbo].[UserDocument]  WITH CHECK ADD  CONSTRAINT [FK_UserDocument_UserDetails] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserDetails] ([UserId])
GO
ALTER TABLE [dbo].[UserDocument] CHECK CONSTRAINT [FK_UserDocument_UserDetails]
GO
ALTER TABLE [dbo].[UserExperienceDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserExperienceDetails_UserExperienceDetails] FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[UserProfile] ([UserProfileId])
GO
ALTER TABLE [dbo].[UserExperienceDetails] CHECK CONSTRAINT [FK_UserExperienceDetails_UserExperienceDetails]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_UserDetails] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserDetails] ([UserId])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_UserDetails]
GO
ALTER TABLE [dbo].[UserQualificationDetails]  WITH CHECK ADD  CONSTRAINT [FK_UserQualificationDetails_UserProfile] FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[UserProfile] ([UserProfileId])
GO
ALTER TABLE [dbo].[UserQualificationDetails] CHECK CONSTRAINT [FK_UserQualificationDetails_UserProfile]
GO
ALTER TABLE [dbo].[Videos]  WITH CHECK ADD  CONSTRAINT [FK_Videos_CourseDetails] FOREIGN KEY([CourseDetailsId])
REFERENCES [dbo].[CourseDetails] ([CourseDetailsId])
GO
ALTER TABLE [dbo].[Videos] CHECK CONSTRAINT [FK_Videos_CourseDetails]
GO
/****** Object:  StoredProcedure [dbo].[lmsGetUserDetailsByEmailId]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[lmsGetUserDetailsByEmailId]
@IEmailId NVARCHAR(MAX)
AS
BEGIN
	SELECT * FROM UserDetails UD
	INNER JOIN AspNetUsers ANU ON ANU.Id = UD.LMSUserId
	WHERE Email = @IEmailId
END
GO
/****** Object:  StoredProcedure [dbo].[lmsUserDetailsSave]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[lmsUserDetailsSave]

@ILMSUserId NVARCHAR(450),

@OUserId INT OUT,
@OMsg NVARCHAR(MAX) = '' OUT

AS
BEGIN
	INSERT INTO [UserDetails](
		[LMSUserId],
		[CreatedOn]
	)
	VALUES(
		@ILMSUserId,
		GETDATE()
	)

	SET @OUserId = SCOPE_IDENTITY()
	SET @OMsg = 'User Details Successfully Saved'
END
GO
/****** Object:  StoredProcedure [dbo].[LmsUserProfileSummery]    Script Date: 05-10-2025 11:31:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[LmsUserProfileSummery]

--LmsUserProfileSummery 1
@IUserId INT

AS
BEGIN
	SELECT
		UD.UserId
		,ANU.FullName
		,ANU.Email
		,CASE WHEN (SELECT MAX(UQ.ModifiedOn) FROM UserDetails UD
					LEFT JOIN UserProfile UP ON UD.UserId = UP.UserId
					LEFT JOIN UserQualificationDetails UQ ON UP.UserProfileId = UQ.UserProfileId ) > (SELECT MAX(ModifiedOn) FROM UserDetails UD
					LEFT JOIN UserProfile UP ON UD.UserId = UP.UserProfileId
					LEFT JOIN UserExperienceDetails UE ON UP.UserProfileId = UE.UserProfileId) THEN (SELECT ISNULL(MAX(UQ.ModifiedOn),'') FROM UserDetails UD
					LEFT JOIN UserProfile UP ON UD.UserId = UP.UserId
					LEFT JOIN UserQualificationDetails UQ ON UP.UserProfileId = UQ.UserProfileId ) ELSE (SELECT ISNULL(MAX(ModifiedOn),'') FROM UserDetails UD
					LEFT JOIN UserProfile UP ON UD.UserId = UP.UserProfileId
					LEFT JOIN UserExperienceDetails UE ON UP.UserProfileId = UE.UserProfileId)
		END AS ModifiedOn
	FROM
		UserDetails UD
		INNER JOIN AspNetUsers ANU ON UD.LMSUserId = ANU.Id
	WHERE UD.UserId = @IUserId

	----------------------------------------------- EDUCATION HISTORY ----------------------------------------------------
	SELECT
		CASE WHEN ISNULL(UQ.IsSchool,0) = 1 THEN
			CASE WHEN ISNULL(UQ.Class,0) = 10 THEN 'Secondary' WHEN ISNULL(UQ.Class,0) = 12 THEN 'Higher Secondary' 
			ELSE 'School' END
		WHEN ISNULL(UQ.IsCollege,0) = 1 THEN CONVERT(NVARCHAR(100),ISNULL(UQ.Class,'')) 
		ELSE 'No Degree Found' END AS Degree
		,CASE WHEN ISNULL(UQ.IsSchool,0) = 1 THEN ISNULL(UQ.NameOfSchool,'') WHEN ISNULL(UQ.IsCollege,0) = 1 THEN ISNULL(UQ.NameOfCollge,'') ELSE 'Not Found' END AS NameOftheInstitution
		--,CONVERT(NVARCHAR(100),ISNULL(UQ.PassoutYear,'DD/MM/YYYY')) AS PassoutYear
		,UQ.IsYearGap
		,UQ.ModifiedOn
	FROM
		UserDetails UD
		LEFT JOIN UserProfile UP ON UD.UserId = UP.UserId
		LEFT JOIN UserQualificationDetails UQ ON UP.UserProfileId = UQ.UserProfileId
	WHERE
		UD.UserId = @IUserId

	----------------------------------------------- COURSE HISTORY ----------------------------------------------------
	
	SELECT
		CD.CourseDetailsId
		,CD.CourseName
		,UC.CreatedOn AS EnrollOn
		,CS.CourseStatusName AS CourseStatus
		,CD.CourseFees
		,CONVERT(NVARCHAR(100),ISNULL(UC.CompletionPercentage,0)) AS CompletionPercentage
		,UC.ModifiedOn
	FROM
		UserDetails UD
		LEFT JOIN UserCourse UC ON UD.UserId = UC.UserId
		LEFT JOIN CourseDetails CD ON CD.CourseDetailsId = UC.CourseDetailsId
		INNER JOIN CouserStatus CS ON CS.CouserStatusId = UC.CourseStatusId
		INNER JOIN PaymentStatus PS ON PS.PaymentStatusId = UC.PaymentStatusId
	WHERE
		UD.UserId = @IUserId


	----------------------------------------------- EXPERIENCE HISTORY ----------------------------------------------------
	SELECT
		UE.CompanyName
		,CASE WHEN ISNULL(UE.IsInternShip,0) = 1 THEN 'Internship' WHEN ISNULL(UE.IsPartTime,0) = 1 THEN 'Part Time' WHEN ISNULL(UE.IsFullTime,0) = 1 THEN 'Full Time' ELSE '' END AS WorkType
		,UE.DateOfJoining
		,UE.LastWorkingDay
		,UE.IsCurrentCompnay
		,ISNULL(UE.DesignNation,'Employee') AS Designation
		,ISNULL(UE.[Description],'') AS [Description]
	FROM
		UserDetails UD
		LEFT JOIN UserProfile UP ON UD.UserId = UP.UserProfileId
		LEFT JOIN UserExperienceDetails UE ON UP.UserProfileId = UE.UserProfileId
	WHERE
		UD.UserId = @IUserId


END


GO
