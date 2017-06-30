using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;

namespace Updater
{
    public class SqlConnectionChecker
    {
        private readonly string _connectionString;
        private string _errorMessage = string.Empty;
        private BackgroundWorker _backgroundWorker;
        private bool _checkResult;

        public bool CheckResult
        {
            get { return _checkResult; }
        }

        private WaitForm _waitForm;

        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        public SqlConnectionChecker(string connectionString)
        {
            try
            {
                _connectionString = connectionString;

                _backgroundWorker = new BackgroundWorker();

                _backgroundWorker.DoWork += _backgroundWorker_DoWork;
                _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

                _waitForm = new WaitForm {TopMost = true};

                _waitForm.Shown += (_, __) => _backgroundWorker.RunWorkerAsync();

                _waitForm.ShowDialog();
            }
            catch (NullReferenceException exception)
            {
                _errorMessage = string.Format("Ошибка. {0}{1}{2}", exception.Message, Environment.NewLine, exception.StackTrace);
            }
            catch (Exception exception)
            {
                _errorMessage = string.Format("Ошибка. {0}{1}{2}", exception.Message, Environment.NewLine, exception.StackTrace);
            }
        }

        void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
             _waitForm.FadingClose();
        }

        void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _checkResult = CheckConnectionToServer();
        }

        private bool CheckConnectionToServer()
        {
            SqlConnection connection = null;
            
            try
            {
                connection = new SqlConnection(_connectionString);
                connection.Open();
                connection.Close();
                return true;
            }
            catch (SqlException e)
            {
                _errorMessage = string.Format("Отсутствует подключение к серверу!{0}{0}Строка подключения:{0}{1}{0}{0}{2}",
                    Environment.NewLine, _connectionString, e.Message);
                return false;
            }
            catch (Exception e)
            {
                _errorMessage = string.Format("При проверке связи с сервером произошла ошибка. {0}{1}", Environment.NewLine,
                    e.Message);
                return false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
    }
}
