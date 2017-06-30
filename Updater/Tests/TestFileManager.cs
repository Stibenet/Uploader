#if DEBUG

using System;

using NUnit.Framework;

namespace Updater.Tests
{
	/// <summary>
	/// ������������ ��������/�������� ������ � ��
	/// </summary>
	[TestFixture]
	public class TestFileManager
	{

		[TestFixtureSetUpAttribute]public void CopyTestConfig()
		{
			System.IO.File.Copy( TestSetUp.config, "Updater.exe.config", true );
		}


		 private void UpNDownloadFile( String tstFile, String tstFile2 )
		{	
			System.DateTime dtStart = System.DateTime.Now;
			System.DateTime dtEnd;

			FilesManager uploadedFile = new FilesManager( TestSetUp.ConnectionString );
			uploadedFile.Upload( tstFile );

			dtEnd = System.DateTime.Now;
			Console.WriteLine( "����� ������� ����� � �� = " + ( dtEnd - dtStart ) );
			

			dtStart = System.DateTime.Now;
			FilesManager downloadedFile = new FilesManager( TestSetUp.ConnectionString );
//			downloadedFile.Download( tstFile, tstFile2 );
			dtEnd = System.DateTime.Now;
			Console.WriteLine( "����� ������� ����� �� �� = " + ( dtEnd - dtStart ) );

			AssertFilesEqual(  tstFile, tstFile2 );
		}

		[Test] public void StartUpDownFileTest()
		{
			UpNDownloadFile( TestSetUp.bynOriginal, TestSetUp.bynTest );

			UpNDownloadFile( TestSetUp.txtOriginal, TestSetUp.txtTest );		
		}

		/// <summary>
		/// �������� ������������ 2-� ������
		/// </summary>
		/// <param name="fileName1"></param>
		/// <param name="fileName2"></param>
		private void AssertFilesEqual( String fileName1, String fileName2 )
		{
			//Assert.IsFalse(true);
			System.IO.FileStream file1, file2;
			file1 = new System.IO.FileStream( fileName1, System.IO.FileMode.Open, System.IO.FileAccess.Read );
			file2 = new System.IO.FileStream( fileName2, System.IO.FileMode.Open, System.IO.FileAccess.Read );

			Assert.AreEqual( file1.Length, file2.Length, "�� ��������� ������ ������" );

			Byte[] imageData1 = new Byte[file1.Length];
			file1.Read( imageData1, 0, Convert.ToInt32( file1.Length ) );
			
			Byte[] imageData2 = new Byte[file2.Length];
			file2.Read( imageData2, 0, Convert.ToInt32( file2.Length ) );

			for( long i = 0; i <= file1.Length-1; i++ )
			{
				Assert.AreEqual( imageData1[i], imageData2[i], "�� ��������� ����, �����=" + file1.Length.ToString() + ", ������� ���=" + i.ToString() );
			}
			file1.Close();
			file2.Close();
		}
	}
}

#endif