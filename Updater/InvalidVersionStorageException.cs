using System;

namespace Updater
{
	/// <summary>
	/// ������������� ��� ���������� ����� �������� ������.
	/// </summary>
	public class InvalidVersionStorageException : ApplicationException
	{
		public InvalidVersionStorageException()
		{
		}

		public InvalidVersionStorageException( String message )
			: base( message )
		{
		}
	}
}
