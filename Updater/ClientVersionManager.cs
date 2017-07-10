using System;
using System.Xml;

namespace Updater
{
	/// <summary>
	/// Управление информацией по версиям клиентских файлов.
	/// </summary>
	public class ClientVersionManager
	{


		/// <summary>Имя файла для хранения информации</summary>
		public readonly String _XMLFileName;// = "versionInfo.xml";
		private XmlDocument _xDoc;

		
		private XmlDocument Document
		{
			get{ return _xDoc; }
		}

		const String rootTagName = "AppFileVersions";
		const String filesTagName = "Files";


		public ClientVersionManager( String XMLFileName )
		{
			_XMLFileName = XMLFileName;
			InitStoraqe();
		}


		#region Проверка наличия, создание нового файла, инициализация хранилища версий
		/// <summary>Проверка наличия файла</summary>
		private void InitStoraqe()
		{
			_xDoc = new XmlDocument();
			if( System.IO.File.Exists( _XMLFileName ) )
			{
				_xDoc.Load( _XMLFileName );
			}
			else
			{
				CreateNewStore();
			}
		}

		/// <summary>Создание нового файла-хранилища</summary>
		private void CreateNewStore()
		{
			XmlProcessingInstruction xPI;
			XmlComment xComment;
			XmlElement xElmntRoot;

			xPI = Document.CreateProcessingInstruction("xml", "version='1.0'");
			Document.AppendChild(xPI);
			xComment = Document.CreateComment("Информация о версиях файлов приложения");
			Document.AppendChild(xComment);
			xComment = Document.CreateComment("Создан: " +System.DateTime.Now.ToString() );
			Document.AppendChild(xComment);

			xElmntRoot = Document.CreateElement( rootTagName );
			Document.AppendChild(xElmntRoot);

			xElmntRoot.AppendChild(Document.CreateElement(filesTagName));

			Document.Save(_XMLFileName);
		}
		#endregion

		public String CombineFilesNodePath()
		{
			return "/" + rootTagName + "/" + filesTagName;
		}

		public String CombineFileNodePath( String fileName )
		{
			return CombineFilesNodePath() + "/file[@name='" + fileName + "']";
		}

		/// <summary>
		/// Установка версий файлов в хранилище
		/// </summary>
		/// <param name="fileName">Файл, для которого нужно зафиксировать версию</param>
		/// <param name="version">Версия файла</param>
		public void SetVersion( String fileName, Int32 version )
		{
			if( GetVersion( fileName ) == 0 )
			{//Создание записи
				XmlElement filesNode = (XmlElement)Document.SelectSingleNode( CombineFilesNodePath() );
				XmlElement file = Document.CreateElement( "file" );
				file.SetAttribute( "name", fileName );
				file.SetAttribute( "version", version.ToString() );
				filesNode.AppendChild( file );
			}
			else
			{//Обновить версию
				GetFileNode(fileName).SetAttribute( "version", version.ToString() );
			}

			Document.Save(_XMLFileName);
		}

		/// <summary>
		/// Получение номера версии файла установленного на клиенте
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public Int32 GetVersion( String fileName )
		{
			if (GetFileNode(fileName) == null)
				return 0;

			return Int32.Parse( GetFileNode(fileName).GetAttribute("version") );
		}

		/// <summary>
		/// Получение нужного узла для файла
		/// </summary>
		/// <param name="fileName">Имя файла</param>
		/// <returns></returns>
		private XmlElement GetFileNode( String fileName )
		{
			XmlNodeList files = Document.SelectNodes( CombineFileNodePath( fileName ) );
			if (files.Count == 0)
				return null;

			if (files.Count > 1)
				throw new InvalidVersionStorageException( "Найдено " + files.Count + " запис(и,ей)! Это слишком много!" );

			return (XmlElement)files[0];
		}
	}
}
