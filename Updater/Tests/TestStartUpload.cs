 #if DEBUG

using System;
using System.IO;

using NUnit.Framework;

namespace Updater.Tests
{
	/// <summary>
	/// �������� ������� ���������� � ������ ����������
	/// </summary>
	[TestFixture]
	public class TestStartUpload
	{
		// TODO: �������� ����� ��� �������� �������

		[TestFixtureSetUpAttribute]public void CopyTestConfig()
		{
			System.IO.File.Copy( TestSetUp.config, "Updater.exe.config", true );
		}
		
		[Test]public void TestDownloadAndStart()
		{
			DelFile( TestSetUp.sturtupExecTest );
			DelFile( TestSetUp.sturtupTxtTest );

			FilesManager FM = new FilesManager( TestSetUp.ConnectionString );
			FM.Upload( TestSetUp.sturtupExecOriginal );


			String startUpFile = System.IO.Path.GetFullPath( @"..\..\bin\Debug\Updater.exe" );
			
			try
			{
				System.Diagnostics.Process.Start( startUpFile, "-u" );
			}
			catch( ArgumentException ex )
			{
				Console.WriteLine( "ArgumentException - ������������ : " + ex.Message + System.Environment.NewLine + startUpFile );
			}
			catch ( System.ComponentModel.Win32Exception ex )
			{
				Console.WriteLine( "Win32Exception - ������������ : " + ex.Message + System.Environment.NewLine + startUpFile );
			}
			catch ( ObjectDisposedException ex )
			{
				Console.WriteLine( "ObjectDisposedException - ������������ : " + ex.Message + System.Environment.NewLine + startUpFile );
			}
			catch( Exception ex )
			{
				Console.WriteLine( "Exception  - ������������ : " + ex.Message + System.Environment.NewLine + startUpFile );
			}
			
			System.Threading.Thread.Sleep( 2000 );
			Assert.IsTrue( File.Exists( TestSetUp.sturtupTxtTest ), "������� ���������� �� �����������! ��� ���� ������� ����." );
		}

		/// <summary>
		/// �������� ������ � ���������
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
