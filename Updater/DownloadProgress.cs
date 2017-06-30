using System;

namespace Updater
{
    public delegate void DownloadProgressChangedHandler(object sender, DownloadProgressChangedEventArgs e);

    public class DownloadProgress
    {
        private readonly long _totalSize;
        private long _downloadedSize;
        private string _currentFileName;

        public DownloadProgress(long totalSize)
        {
            _totalSize = totalSize;
        }

        public long TotalSize
        {
            get { return _totalSize; }
        }

        public long DownloadedSize
        {
            get { return _downloadedSize; }
        }

        public string CurrentFileName
        {
            get { return _currentFileName; }
        }

        public event DownloadProgressChangedHandler ProgressChanged;

        private void RaiseChanged(int downloadedSize)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, new DownloadProgressChangedEventArgs(downloadedSize));
            }
        }

        public void InicrementDownloadedSize(int downloadedSize)
        {
            _downloadedSize += downloadedSize;

            RaiseChanged(downloadedSize);
        }

        public void SetCurrentFileName(string currentFileName)
        {
            _currentFileName = currentFileName;
        }
    }

    public class DownloadProgressChangedEventArgs : EventArgs
    {
        private readonly int _changeSize;

        public DownloadProgressChangedEventArgs(int changeSize)
        {
            _changeSize = changeSize;
        }

        public int ChangeSize
        {
            get { return _changeSize; }
        }
    }
}