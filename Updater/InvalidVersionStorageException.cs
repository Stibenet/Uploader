using System;

namespace Updater
{
	/// <summary>
	/// Возбуждаетсся при испорченом файле хранения версий.
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
