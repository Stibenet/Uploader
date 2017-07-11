using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using Updater.Utils;

namespace Updater
{
    /// <summary>
    /// Непосредственно механизм обновления файлов на клиента (БЛ)
    /// </summary>
    public class UploaderEngine
    {
        private const int BytesInMegabyte = 1048573;

        private readonly string _connectionString;
        private readonly string _xmlFileName;

        private readonly List<FileInfo> _filesForUpdate;
        private readonly Boolean _downloadAllFiles;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="xmlFileName"></param>
        /// <param name="downloadAllFiles">TRUE  - грузить все файлы, без проверки на версию,
        /// FALSE - загружать только файлы с изменившейся версией.</param>
        public UploaderEngine(string connectionString, string xmlFileName, bool downloadAllFiles)
        {
            _connectionString = connectionString;
            _xmlFileName = xmlFileName;
            _downloadAllFiles = downloadAllFiles;

            _filesForUpdate = new List<FileInfo>();
        }

        public List<FileInfo> FilesForUpdate
        {
            get { return _filesForUpdate; }
        }


        /// <summary>
        /// Проверка наличия в БД новых версий файлов и заполнение коллекции именами и версиями этих файлов
        /// </summary>
        public void GetFileList()
        {
            SqlConnection cnn = MiscFunction.OpenConnection(_connectionString);
            const string sqlStr = "SELECT idFile, version, name, DATALENGTH(binaryData) AS fileSize FROM Srv_ProgramFile";
            var cmd = new SqlCommand(sqlStr, cnn);
            SqlDataReader sdr = cmd.ExecuteReader();

            int version = sdr.GetOrdinal("version");
            int fileName = sdr.GetOrdinal("name");
            int fileSize = sdr.GetOrdinal("fileSize");

            var clientVersionManager = new ClientVersionManager(_xmlFileName);

            FilesForUpdate.Clear();

            while (sdr.Read())
            {
                var fileInfo = new FileInfo(sdr.GetString(fileName), clientVersionManager.GetVersion(sdr.GetString(fileName)), sdr.GetInt32(version), sdr.GetInt32(fileSize));

                if (_downloadAllFiles || fileInfo.NeedUpdate)
                {
                    FilesForUpdate.Add(fileInfo);
                }
            }

            sdr.Close();
            cnn.Close();
        }


        /// <summary> Проверка необходимости запуска процедуры обновления</summary>
        /// <returns>TRUE - если надо запускать обновление</returns>
        public Boolean IsNeedUpdate()
        {
            GetFileList();

            return (FilesForUpdate.Count != 0);
        }


        /// <summary>Первоначальная проверка наличия необходимости запуска процедуры обновления
        /// с последующим запуском обновления</summary>
        public void StartDownload(ref bool breaked)
        {
            try
            {
                if (FilesForUpdate.Count == 0)
                {
                    if (!IsNeedUpdate())
                        return;
                }

                var filesManager = new FilesManager(_connectionString);
                var clientVersionManager = new ClientVersionManager(_xmlFileName);

                long totalSize = 0;
                FilesForUpdate.ForEach(fileInfo => totalSize += fileInfo.FileSize); 

                var progressForm = new frmProgress((int)totalSize);
                progressForm.Show();
                progressForm.BringToFront();

                var downloadProgress = new DownloadProgress(totalSize);

                downloadProgress.ProgressChanged +=
                    (sender, e) => progressForm.Tick(e.ChangeSize, string.Format("{0,3:#.#}/{1,3:#.#} MБ ({2})", (decimal)downloadProgress.DownloadedSize / BytesInMegabyte,
                       (decimal)downloadProgress.TotalSize / BytesInMegabyte, downloadProgress.CurrentFileName));

                foreach (FileInfo fileInfo in FilesForUpdate)
                {
                    try
                    {
                        downloadProgress.SetCurrentFileName(fileInfo.FileName);
                        filesManager.Download(fileInfo.FileName, downloadProgress);
                        clientVersionManager.SetVersion(fileInfo.FileName, fileInfo.ServerVersion);

                        if (progressForm.FormKeyCode == Keys.Escape)
                        {
                            breaked = true;
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        progressForm.Fail();
                        throw;
                    }

                }

                progressForm.Close();
            }
            catch (Exception exception)
            {

                string _errorMessage = string.Format("Ошибка. {0}{1}{2}", exception.Message, Environment.NewLine, exception.StackTrace);
                MessageBox.Show("Ошибка запуска приложения:\r\n", _errorMessage);
            }
           
        }
    }
}