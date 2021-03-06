USE [DBUploader]
GO
/****** Object:  Table [dbo].[Srv_ProgramFile]    Script Date: 03.08.2017 19:00:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Srv_ProgramFile](
	[idFile] [int] IDENTITY(1,1) NOT NULL,
	[version] [int] NOT NULL,
	[name] [varchar](512) NOT NULL,
	[author] [varchar](64) NOT NULL,
	[binaryData] [image] NOT NULL,
	[Date] [date] NULL,
	[hashCode] [varchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Идентификато' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Srv_ProgramFile', @level2type=N'COLUMN',@level2name=N'idFile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Версия' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Srv_ProgramFile', @level2type=N'COLUMN',@level2name=N'version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'имя файла' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Srv_ProgramFile', @level2type=N'COLUMN',@level2name=N'name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Автор закачки' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Srv_ProgramFile', @level2type=N'COLUMN',@level2name=N'author'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Тело файла' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Srv_ProgramFile', @level2type=N'COLUMN',@level2name=N'binaryData'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Хранение файлов в БД' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Srv_ProgramFile'
GO
