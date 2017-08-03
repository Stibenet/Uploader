using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace Updater
{
    /// <summary>
    /// Выгрузка файлов из БД
    /// </summary>
    public class FilesManager
    {
        private String _connectionString;

        public String LastError;

        public FilesManager(String ConnectionString)
        {
            _connectionString = ConnectionString;
        }

        #region Загрузка файлов из БД
        /// <summary>
        /// Загрузка файла на клиента из БД
        /// </summary>
        /// <param name="fileName">Имя файла для загрузки</param>
        public void Download(String fileName, DownloadProgress progress)
        {
            Download(fileName, AppDomain.CurrentDomain.BaseDirectory + @"/" + fileName, progress);
        }


        /// <summary>
        /// Загрузка файла из БД и сохранение под другим именем
        /// </summary>
        /// <param name="fileName">Файл для загрузки из БД</param>
        /// <param name="fullFileName">Полный путь и имя файла для сохранения</param>
        private void Download(String fileName, String fullFileName, DownloadProgress progress)
        {
            SqlConnection connection = MiscFunction.OpenConnection(_connectionString);
            var strSql = "SELECT binaryData FROM Srv_ProgramFile WHERE name = '" + MiscFunction.GetFileName(fileName) + "'";
            var cmd = new SqlCommand(strSql, connection);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
            reader.Read();

            var fs = new FileStream(fullFileName, FileMode.Create, FileAccess.Write);
            var writer = new BinaryWriter(fs);
            int startIndex = 0;
            const int bufferSize = 1000;
            var outByte = new byte[bufferSize];

            long retval = reader.GetBytes(0, startIndex, outByte, 0, bufferSize);

            while (retval == bufferSize)
            {
                writer.Write(outByte);
                writer.Flush();

                startIndex += bufferSize;

                retval = reader.GetBytes(0, startIndex, outByte, 0, bufferSize);

                progress.InicrementDownloadedSize((int)retval);
            }

            writer.Write(outByte, 0, (int)retval);
            writer.Flush();

            writer.Close();
            fs.Close();

            reader.Close();
            connection.Close();
        }

        #endregion
    }
}
