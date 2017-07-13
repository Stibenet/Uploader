using System;

namespace Updater
{
    public class FileInfo
    {
        private readonly int _clientVersion;
        private readonly string _fileName;
        private readonly int _serverVersion;
        private readonly int _fileSize;

        public FileInfo(string fileName, int clientVersion, int serverVersion, int fileSize)
        {
            _fileName = fileName;
            _clientVersion = clientVersion;
            _serverVersion = serverVersion;
            _fileSize = fileSize;
        }

        public string FileName
        {
            get { return _fileName; }
        }

        public int ClientVersion
        {
            get { return _clientVersion; }
        }

        public int ServerVersion
        {
            get { return _serverVersion; }
        }

        public bool NeedUpdate
        {
            get { return (_clientVersion != _serverVersion); }
        }

        public int FileSize
        {
            get { return _fileSize; }
        }
    }
}