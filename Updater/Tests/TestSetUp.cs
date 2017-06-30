#if DEBUG

using System;

namespace Updater.Tests
{
	/// <summary>
	/// Summary description for TestSetUp.
	/// </summary>
	public class TestSetUp
	{
//		public static String ConnectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionStringTest"];
//		public static String ClientVersionFileName = System.Configuration.ConfigurationSettings.AppSettings["VersionStorageTest"];

		public static String ConnectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
		public static String ClientVersionFileName = System.Configuration.ConfigurationSettings.AppSettings["VersionStorage"];

		/// <summary>���� ��� �������� �������� EXE - ��������</summary>
		public static String bynOriginal = @"..\..\Tests\TestFiles\Planning.exe";
		/// <summary>���� ��� �������� �������� EXE</summary>
		public static String bynTest = @"Planning.exe";
		/// <summary>���� ��� �������� �������� TXT - ��������</summary>
		public static String txtOriginal = @"..\..\Tests\TestFiles\appCode.txt";
		/// <summary>���� ��� �������� �������� TXT</summary>
		public static String txtTest = @"appCode.txt";


		/// <summary>���� ��� �������� ������� ����������</summary>
		public static String sturtupExecOriginal = @"..\..\Tests\TestFiles\cunningFile.exe";
		/// <summary>���� ��� �������� ������� ����������</summary>
		public static String sturtupExecTest = @"cunningFile.exe";
		public static String sturtupTxtTest = @"MyTest.txt";

		/// <summary>������ ��� ���������� ������</summary>
		public static String config = @"..\..\Tests\TestFiles\ForTestUpdater.exe.config";

		public TestSetUp()
		{
		}
	
	}
}

#endif
