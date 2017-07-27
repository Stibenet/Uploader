 #if DEBUG

using System;
using System.IO;

using NUnit.Framework;

namespace Updater.Tests
{
	/// <summary>
	/// Проверка запуска приложения в режиме обновления
	/// </summary>
	[TestFixture]
	public class TestStartUpload
	{
		// TODO: Добавить тесты для проверки запуска

		[TestFixtureSetUpAttribute]public void CopyTestConfig()
		{
			System.IO.File.Copy( TestSetUp.config, "Updater.exe.config", true );
		}
		
		[Test]public void TestDownloadAndStart()
		{
			DelFile( TestSetUp.sturtupExecTest );
			DelFile( TestSetUp.sturtupTxtTest );

			FilesManager FM = new FilesManager( TestSetUp.ConnectionString );


			String startUpFile = System.IO.Path.GetFullPath( @"..\..\bin\Debug\Updater.exe" );
			
			try
			{
				System.Diagnostics.Process.Start( startUpFile, "-u" );
			}
			catch( ArgumentException ex )
			{
				Console.WriteLine( "ArgumentException - Тестирование : " + ex.Message + System.Environment.NewLine + startUpFile );
			}
			catch ( System.ComponentModel.Win32Exception ex )
			{
				Console.WriteLine( "Win32Exception - Тестирование : " + ex.Message + System.Environment.NewLine + startUpFile );
			}
			catch ( ObjectDisposedException ex )
			{
				Console.WriteLine( "ObjectDisposedException - Тестирование : " + ex.Message + System.Environment.NewLine + startUpFile );
			}
			catch( Exception ex )
			{
				Console.WriteLine( "Exception  - Тестирование : " + ex.Message + System.Environment.NewLine + startUpFile );
			}
			
			System.Threading.Thread.Sleep( 2000 );
			Assert.IsTrue( File.Exists( TestSetUp.sturtupTxtTest ), "Нужноге приложение не запустилось! Или рано смотрим файл." );
		}

		/// <summary>
		/// Удаление файлов с проверкой
		/// </summary>
		/// <param name="FileName"></param>
		private void DelFile( String FileName )
		{
			if( File.Exists( FileName ) )
				File.Delete( FileName );
		
		}
	}

}

#endif
