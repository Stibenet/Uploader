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

		/// <summary>Файл для проверки загрузки EXE - оригинал</summary>
		public static String bynOriginal = @"..\..\Tests\TestFiles\Planning.exe";
		/// <summary>Файл для проверки загрузки EXE</summary>
		public static String bynTest = @"Planning.exe";
		/// <summary>Файл для проверки загрузки TXT - оригинал</summary>
		public static String txtOriginal = @"..\..\Tests\TestFiles\appCode.txt";
		/// <summary>Файл для проверки загрузки TXT</summary>
		public static String txtTest = @"appCode.txt";


		/// <summary>Файл для проверки запуска приложения</summary>
		public static String sturtupExecOriginal = @"..\..\Tests\TestFiles\cunningFile.exe";
		/// <summary>Файл для проверки запуска приложения</summary>
		public static String sturtupExecTest = @"cunningFile.exe";
		public static String sturtupTxtTest = @"MyTest.txt";

		/// <summary>Конфиг для проведения тестов</summary>
		public static String config = @"..\..\Tests\TestFiles\ForTestUpdater.exe.config";

		public TestSetUp()
		{
		}
	
	}
}

#endif
