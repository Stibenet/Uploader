#if DEBUG

using System;

using NUnit.Framework;

namespace Updater.Tests
{
	/// <summary>
	/// Тестирование движка обновления файлов
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

			Assert.AreEqual( 2, UE.FilesForUpdate.Count, "Неправильно определены файлы для закачки" );
		}

		[Test]public void TestCountFilesForUpdateAfterUpdate()
		{
			//Assert.IsFalse( true, "Надо доделать проверку правильности определения после обновления" );
			UploaderEngine UE = new UploaderEngine( TestSetUp.ConnectionString, TestSetUp.ClientVersionFileName, false );
		    var isBreak = false;
			UE.StartDownload(ref isBreak);
			Assert.IsFalse( UE.IsNeedUpdate() );

			ClientVersionManager CVM = new ClientVersionManager( TestSetUp.ClientVersionFileName );
			CVM.SetVersion( MiscFunction.GetFileName( TestSetUp.txtTest ), 0 );
			UE.GetFileList();
			Assert.AreEqual( 1, UE.FilesForUpdate.Count, "Неправильно определили количество файлов для загрузки после обновления" );
		}

		[Test]public void TestNeedUpdate()
		{
			UploaderEngine UE = new UploaderEngine( TestSetUp.ConnectionString, TestSetUp.ClientVersionFileName, false );

			Assert.IsTrue( UE.IsNeedUpdate(), "Нерпавильно определили отсутствие необходимости закачки файлов" );
		}


		[Test]public void TestDownload()
		{
			UploaderEngine UE = new UploaderEngine( TestSetUp.ConnectionString, TestSetUp.ClientVersionFileName, false );
			Assert.IsTrue( UE.IsNeedUpdate(), "Нерпавильно определили отсутствие необходимости закачки файлов" );
            var isBreak = false;
            UE.StartDownload(ref isBreak);

			UploaderEngine UE2 = new UploaderEngine( TestSetUp.ConnectionString, TestSetUp.ClientVersionFileName, false );
			Assert.IsFalse( UE2.IsNeedUpdate(), "Нерпавильно определили необходимость закачки файлов" );
		}

		[Test]public void DownloadAll()
		{
			UploaderEngine UE = new UploaderEngine( TestSetUp.ConnectionString, TestSetUp.ClientVersionFileName, true );
            var isBreak = false;
            UE.StartDownload(ref isBreak);

			System.IO.File.Delete( TestSetUp.bynTest );
			System.IO.File.Delete( TestSetUp.txtTest );
			Assert.IsFalse( System.IO.File.Exists( TestSetUp.bynTest ), "Не удалены файлы для проверки!" );
			Assert.IsFalse( System.IO.File.Exists( TestSetUp.txtTest ), "Не удалены файлы для проверки!" );


            isBreak = false;
            UE.StartDownload(ref isBreak);
            Assert.IsTrue(System.IO.File.Exists(TestSetUp.bynTest), "Все файлы не качаются!");
			Assert.IsTrue( System.IO.File.Exists( TestSetUp.txtTest ), "Все файлы не качаются!" );
		}
	}
}

#endif

