using System;
using System.Data.SqlClient;

namespace Updater
{
	/// <summary>
	/// Статические методы.
	/// </summary>
	public class MiscFunction
	{
		public static String LastError;

		public MiscFunction()
		{
		}

		/// <summary>
		/// Подключение к БД
		/// </summary>
		/// <returns></returns>
		public static SqlConnection OpenConnection( String ConnectionString )
		{
			SqlConnection CN = new SqlConnection( ConnectionString );
			try
			{
				CN.Open();
			}
			catch ( SqlException e )
			{
				LastError = e.Message;
			}
			catch ( Exception e )
			{
				LastError = e.Message;
			}

			return CN;
		}


		/// <summary>
		/// Возвращает имя файла из полного пути файла
		/// </summary>
		/// <param name="fullFileName">Полный путь файла</param>
		/// <returns></returns>
		public static String GetFileName( String fullFileName )
		{
			String[] arrFilename = System.Text.RegularExpressions.Regex.Split( fullFileName, @"\\" );
			Array.Reverse(arrFilename);

			return arrFilename[0];
		}
	}
}
