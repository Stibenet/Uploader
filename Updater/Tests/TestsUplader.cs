#if DEBUG

using System;

using NUnit.Framework;

namespace Updater.Tests
{
	/// <summary>
	/// ������������ ������ ���������� ������
	/// </summary>
	[TestFixture]
	public class TestsUplader
	{
		[TestFixtureSetUpAttribute]public void CopyTestConfig()
		{
			System.IO.File.Copy( TestSetUp.config, "Updater.exe.config", true );
		}


		[SetUp]public void Setup()
		{
			System.IO.File.Delete( TestSetUp.bynTest );
			System.IO.File.Delete( TestSetUp.txtTest );
			System.IO.File.Delete( TestSetUp.ClientVersionFileName );
		}

		[Test]public void TestCountFilesForUpdate()
		{
			UploaderEngine UE = new UploaderEngine( TestSetUp.ConnectionString, TestSetUp.ClientVersionFileName, false );

			UE.GetFileList( );

			Assert.AreEqual( 2, UE.FilesForUpdate.Count, "����������� ���������� ����� ��� �������" );
		}

		[Test]public void TestCountFilesForUpdateAfterUpdate()
		{
			//Assert.IsFalse( true, "���� �������� �������� ������������ ����������� ����� ����������" );
			UploaderEngine UE = new UploaderEngine( TestSetUp.ConnectionString, TestSetUp.ClientVersionFileName, false );
		    var isBreak = false;
			UE.StartDownload(ref isBreak);
			Assert.IsFalse( UE.IsNeedUpdate() );

			ClientVersionManager CVM = new ClientVersionManager( TestSetUp.ClientVersionFileName );
			CVM.SetVersion( MiscFunction.GetFileName( TestSetUp.txtTest ), 0 );
			UE.GetFileList();
			Assert.AreEqual( 1, UE.FilesForUpdate.Count, "����������� ���������� ���������� ������ ��� �������� ����� ����������" );
		}

		[Test]public void TestNeedUpdate()
		{
			UploaderEngine UE = new UploaderEngine( TestSetUp.ConnectionString, TestSetUp.ClientVersionFileName, false );

			Assert.IsTrue( UE.IsNeedUpdate(), "����������� ���������� ���������� ������������� ������� ������" );
		}


		[Test]public void TestDownload()
		{
			UploaderEngine UE = new UploaderEngine( TestSetUp.ConnectionString, TestSetUp.ClientVersionFileName, false );
			Assert.IsTrue( UE.IsNeedUpdate(), "����������� ���������� ���������� ������������� ������� ������" );
            var isBreak = false;
            UE.StartDownload(ref isBreak);

			UploaderEngine UE2 = new UploaderEngine( TestSetUp.ConnectionString, TestSetUp.ClientVersionFileName, false );
			Assert.IsFalse( UE2.IsNeedUpdate(), "����������� ���������� ������������� ������� ������" );
		}

		[Test]public void DownloadAll()
		{
			UploaderEngine UE = new UploaderEngine( TestSetUp.ConnectionString, TestSetUp.ClientVersionFileName, true );
            var isBreak = false;
            UE.StartDownload(ref isBreak);

			System.IO.File.Delete( TestSetUp.bynTest );
			System.IO.File.Delete( TestSetUp.txtTest );
			Assert.IsFalse( System.IO.File.Exists( TestSetUp.bynTest ), "�� ������� ����� ��� ��������!" );
			Assert.IsFalse( System.IO.File.Exists( TestSetUp.txtTest ), "�� ������� ����� ��� ��������!" );


            isBreak = false;
            UE.StartDownload(ref isBreak);
            Assert.IsTrue(System.IO.File.Exists(TestSetUp.bynTest), "��� ����� �� ��������!");
			Assert.IsTrue( System.IO.File.Exists( TestSetUp.txtTest ), "��� ����� �� ��������!" );
		}
	}
}

#endif

