/*Таблица для хранения файлов*/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Srv_ProgramFile]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Srv_ProgramFile]
GO

CREATE TABLE [dbo].[Srv_ProgramFile] (
	[idFile] [int] IDENTITY (1, 1) NOT NULL ,
	[version] [int] NOT NULL ,
	[name] [varchar] (512) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[author] [varchar] (64) COLLATE Cyrillic_General_CI_AS NOT NULL ,
	[binaryData] [image] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE  INDEX [IX_Srv_ProgramFile] ON [dbo].[Srv_ProgramFile]([name]) ON [PRIMARY]
GO


exec sp_addextendedproperty N'MS_Description', N'Хранение файлов в БД', N'user', N'dbo', N'table', N'Srv_ProgramFile'
exec sp_addextendedproperty N'MS_Description', N'Идентификато', N'user', N'dbo', N'table', N'Srv_ProgramFile', N'column', N'idFile'
exec sp_addextendedproperty N'MS_Description', N'Версия', N'user', N'dbo', N'table', N'Srv_ProgramFile', N'column', N'version'
exec sp_addextendedproperty N'MS_Description', N'имя файла', N'user', N'dbo', N'table', N'Srv_ProgramFile', N'column', N'name'
exec sp_addextendedproperty N'MS_Description', N'Автор закачки', N'user', N'dbo', N'table', N'Srv_ProgramFile', N'column', N'author'
exec sp_addextendedproperty N'MS_Description', N'Тело файла', N'user', N'dbo', N'table', N'Srv_ProgramFile', N'column', N'binaryData'
/*КОНЕЦ Таблица для хранения файлов*/