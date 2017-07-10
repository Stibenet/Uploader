using System;
using System.Data.SqlClient;

namespace Updater
{
	/// <summary>
	/// ����������� ������.
	/// </summary>
	public class MiscFunction
	{
		public static String LastError;

		public MiscFunction()
		{
		}

		/// <summary>
		/// ����������� � ��
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
		/// ���������� ��� ����� �� ������� ���� �����
		/// </summary>
		/// <param name="fullFileName">������ ���� �����</param>
		/// <returns></returns>
		public static String GetFileName( String fullFileName )
		{
			String[] arrFilename = System.Text.RegularExpressions.Regex.Split( fullFileName, @"\\" );
			Array.Reverse(arrFilename);

			return arrFilename[0];
		}
	}
}
