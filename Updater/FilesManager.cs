using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace Updater
{
    /// <summary>
    /// �������� - �������� ������ � ��
    /// </summary>
    public class FilesManager
    {
        private String _connectionString;

        public String LastError;

        public FilesManager(String ConnectionString)
        {
            _connectionString = ConnectionString;
        }



        #region �������� ������ � ��
        /// <summary>
        /// �������� ����� � �� 
        /// </summary>
        /// <param name="fileName">������ ���� � ��� ����� ��� �������� � ��</param>
        public void Upload(String fullFileName)
        {
            SqlConnection CN = MiscFunction.OpenConnection(_connectionString);
            RIPEMD160 myRIPEMD160 = RIPEMD160Managed.Create();
            String fileName = MiscFunction.GetFileName(fullFileName);

            FileStream fs = new System.IO.FileStream(fullFileName, FileMode.Open, FileAccess.Read);
            Byte[] imageData = new Byte[fs.Length];
            fs.Read(imageData, 0, Convert.ToInt32(fs.Length));

            //SHA-1 
            byte[] hashValue;
            hashValue = myRIPEMD160.ComputeHash(fs);         
            PrintByteArray(hashValue);

            fs.Close();

            String strSQL;

            if (!ChekExists(CN, fileName))
            {//INSERT
                strSQL = "INSERT INTO Srv_ProgramFile (version, name, author, binaryData, Date) " +
                    "VALUES (1, @name, @autor, @binaryData, @Date)";
            }
            else
            {//UPDATE
                strSQL = "UPDATE Srv_ProgramFile SET version=version+1, name=@name, author=@autor, binaryData=@binaryData " +
                    "WHERE name=@name";
            }

            SqlCommand cmd = new SqlCommand(strSQL, CN) {CommandTimeout = 60};
            cmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 128)).Value = fileName;
            cmd.Parameters.Add(new SqlParameter("@autor", SqlDbType.NVarChar, 50)).Value = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            cmd.Parameters.Add(new SqlParameter("@binaryData", SqlDbType.Image)).Value = imageData;
            cmd.Parameters.Add("@Date", DateTime.Now.Date);
            cmd.ExecuteNonQuery();

            CN.Close();
        }

        public static void PrintByteArray(byte[] array)
        {
            int i;
            for (i = 0; i < array.Length; i++)
            {
                System.IO.File.WriteAllText("C:\\Users\\�������\\Documents\\Uploader\\Updater\\bin\\Debug\\TestFile.txt", String.Format("{0:X2}", array[i]));
            }
        }


        /// <summary>
        ///�������� ������� ����� � �� ��� �����������: ��������� ���������� ��� ���������� ����� ������
        /// </summary>
        /// <param name="CN"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private Boolean ChekExists(SqlConnection CN, String fileName)
        {
            Boolean res = false;

            String strSQL = "SELECT idFile FROM Srv_ProgramFile WHERE name = '" + fileName + "'";
            SqlCommand cmd = new SqlCommand(strSQL, CN);
            SqlDataReader SDR = cmd.ExecuteReader();

            Int32 idFile = 0;

            while (SDR.Read())
            {
                idFile = Convert.ToInt32(SDR.GetValue(0));
            }

            SDR.Close();

            if (idFile != 0)
                res = true;

            return res;
        }
        #endregion


        #region �������� ������ �� ��
        /// <summary>
        /// �������� ����� �� ������� �� ��
        /// </summary>
        /// <param name="fileName">��� ����� ��� ��������</param>
        public void Download(String fileName, DownloadProgress progress)
        {
            Download(fileName, AppDomain.CurrentDomain.BaseDirectory + @"/" + fileName, progress);
        }


        /// <summary>
        /// �������� ����� �� �� � ���������� ��� ������ ������
        /// </summary>
        /// <param name="fileName">���� ��� �������� �� ��</param>
        /// <param name="fullFileName">������ ���� � ��� ����� ��� ����������</param>
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
