#if DEBUG

using System;
        
using NUnit.Framework;

namespace Updater.Tests
{
	/// <summary>
	/// ������������ ������ ���������� ����������� �� ������� �� ���������� ������
	/// </summary>
	[TestFixture]
	public class TestsVersionManager
	{

		[TestFixtureSetUpAttribute]public void CopyTestConfig()
		{
			System.IO.File.Copy( TestSetUp.config, "Updater.exe.config", true );
		}


		[SetUp]public void Setup()
		{
			System.IO.File.Delete( TestSetUp.ClientVersionFileName );
		}

		[Test] public void TestStoreCreate()
		{			
			Assert.IsFalse( System.IO.File.Exists( TestSetUp.ClientVersionFileName ), "���� �� �������� - ���� ����� ���������� �� ���������!" );

			ClientVersionManager CVM = new ClientVersionManager( TestSetUp.ClientVersionFileName );

			Assert.IsTrue( System.IO.File.Exists( TestSetUp.ClientVersionFileName ), "���� �� ������! �� �������� �������������� �������� �����!" );
		}

		private void SaveVersion( Int32 version )
		{
			ClientVersionManager CVM = new ClientVersionManager( TestSetUp.ClientVersionFileName );
			CVM.SetVersion( TestSetUp.bynTest, version );
			Assert.AreEqual( version, CVM.GetVersion( TestSetUp.bynTest ), "������ ������ �� ���������������!" );
		}

		[Test] public void TestRecordCreate()
		{
			SaveVersion( 100 );
		}

		[Test] public void TestRecordManage()
		{
			SaveVersion( 100 );
			SaveVersion( 101 );
		}

		[Test] public void TestEmptyFileVersion()
		{
			ClientVersionManager CVM = new ClientVersionManager( TestSetUp.ClientVersionFileName );

			Assert.AreEqual( 0, CVM.GetVersion( "unexistsFile" ) );
		}
	}
}

#endif