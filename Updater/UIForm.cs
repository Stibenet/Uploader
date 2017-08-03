using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;
using Updater.Utils;

namespace Updater
{
    /// <summary>
    ///     Summary description for Form1.
    /// </summary>
    public class UIForm : Form
    {
        protected const string SqlConnectionErrorMessage = "Отсутствует подключение к серверу. {}Строка подключения:{0}";
        private Label _lblItemCount;
        private MainMenu _mnu;
        private MenuItem _mnuAboout;
        private MenuItem _mnuExite;
        private OpenFileDialog _openFileDialog;
        private IContainer components;
        private MenuItem menuItem1;
        private MenuItem menuItem3;
        private Label label2;
        private Button button2;
        public static bool flag = false;
        private readonly List<FileInfo> _filesForLoad;
        private const int BytesInMegabyte = 1048573;
        private DataGridView dataGridView1;

        public UIForm()
        {
            //
            // Required for Windows Form Designer support
            //
            _filesForLoad = new List<FileInfo>();
            InitializeComponent();
        }

        public List<FileInfo> FilesForLoad
        {
            get { return _filesForLoad; }
        }


        private static string VersionStorageName
        {
            get { return ConfigurationSettings.AppSettings["VersionStorage"]; }
        }

        private static string ConnectionString
        {
            get { return ConfigurationSettings.AppSettings["ConnectionString"]; }
        }

        private static string StartUpFileName
        {
            get { return ConfigurationSettings.AppSettings["StartUpFile"]; }
        }

        /// <summary>
        ///     Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            try
            {
                TaskbarUtils.Init();

                var connectionChecker = new SqlConnectionChecker(ConnectionString);
                if (!connectionChecker.CheckResult)
                {
                    MessageBox.Show(connectionChecker.ErrorMessage, "Ошибка при проверке подключения к SQL серверу", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }

                if (args.Length == 0)
                {
                    StartUpdate(false);
                }

                else
                {
                    switch (args[0])
                    {
                        case "-u":
                            Application.Run(new UIForm());
                            break;

                        case "-all":
                            StartUpdate(true);
                            break;
                    }
                }
            }
            catch (Exception exception)
            {

                string _errorMessage = string.Format("Ошибка. {0}{1}{2}", exception.Message, Environment.NewLine, exception.StackTrace);
                MessageBox.Show("Ошибка запуска приложения:\r\n", _errorMessage);
            }

        }

        private static void StartUpdate(Boolean downloadAllFiles)
        {
            try
            {
                var UE = new UploaderEngine(ConnectionString, VersionStorageName, downloadAllFiles);
                bool isBreak = false;

                if (UE.IsNeedUpdate())
                {
                    UE.StartDownload(ref isBreak);
                }

                if (!isBreak)
                {
                    Process.Start(Application.StartupPath + @"\" + StartUpFileName);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage(ex);
            }
        }

        private static void ErrorMessage(Exception ex)
        {
            MessageBox.Show("Ошибка запуска приложения:\r\n" + ex, "Ошибка запуска приложения " + StartUpFileName);
        }

        private void _mnuAboout_Click(object sender, EventArgs e)
        {
            var frm = new frmAbout();
            frm.ShowDialog(this);
            frm.Dispose();
        }


        #region Прерывание загрузки клавишей Escape
        private void UIForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
        #endregion

        #region Windows Form Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            this._openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._mnu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this._mnuExite = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this._mnuAboout = new System.Windows.Forms.MenuItem();
            this._lblItemCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // _mnu
            // 
            this._mnu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem3});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this._mnuExite});
            this.menuItem1.Text = "Файл";
            // 
            // _mnuExite
            // 
            this._mnuExite.Index = 0;
            this._mnuExite.Text = "Выход";
            this._mnuExite.Click += new System.EventHandler(this._mnuExite_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this._mnuAboout});
            this.menuItem3.Text = "Справка";
            // 
            // _mnuAboout
            // 
            this._mnuAboout.Index = 0;
            this._mnuAboout.Text = "О программе";
            this._mnuAboout.Click += new System.EventHandler(this._mnuAboout_Click);
            // 
            // _lblItemCount
            // 
            this._lblItemCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._lblItemCount.AutoSize = true;
            this._lblItemCount.Location = new System.Drawing.Point(4, 211);
            this._lblItemCount.Name = "_lblItemCount";
            this._lblItemCount.Size = new System.Drawing.Size(0, 13);
            this._lblItemCount.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Загружаемые файлы из БД";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(443, 188);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Получить файлы";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this._cmdInLoad_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dataGridView1.Location = new System.Drawing.Point(3, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(551, 147);
            this.dataGridView1.TabIndex = 12;
            // 
            // UIForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(566, 236);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._lblItemCount);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Menu = this._mnu;
            this.Name = "UIForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Работа с файлами базы данных";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UIForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Buttons
        private void _mnuOpen_Click(object sender, EventArgs e)
        {
            _openFileDialog.ShowDialog(this);
        }

        private void _mnuExite_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Загрузка файлов из БД
        private void _cmdInLoad_Click(object sender, EventArgs e)
        {
            String connString;
            connString = ConfigurationSettings.AppSettings["ConnectionString"];
            String versionStorageName;
            versionStorageName = ConfigurationSettings.AppSettings["VersionStorage"];

            SqlConnection cnn = MiscFunction.OpenConnection(connString);
            const string sqlStr = "SELECT idFile, version, name, DATALENGTH(binaryData) AS fileSize FROM Srv_ProgramFile";
            var cmd = new SqlCommand(sqlStr, cnn);
            SqlDataReader sdr = cmd.ExecuteReader();

            int version = sdr.GetOrdinal("version");
            int fileName = sdr.GetOrdinal("name");
            int fileSize = sdr.GetOrdinal("fileSize");

            var clientVersionManager = new ClientVersionManager(versionStorageName);

            FilesForLoad.Clear();
            while (sdr.Read())
            {
                var fileInfo = new FileInfo(sdr.GetString(fileName), clientVersionManager.GetVersion(sdr.GetString(fileName)), sdr.GetInt32(version), sdr.GetInt32(fileSize));

                if (fileInfo.NeedUpdate)
                {
                    FilesForLoad.Add(fileInfo);
                }
            }
            sdr.Close();
            cnn.Close();

            //Вывод в таблицу информации при загрузке файлов из БД
            using (SqlDataAdapter a = new SqlDataAdapter("SELECT version, name, Date FROM Srv_ProgramFile", cnn))
            {
                SqlCommandBuilder cb = new SqlCommandBuilder(a);
                DataSet ds = new DataSet();
                a.Fill(ds, "Srv_ProgramFile");
                dataGridView1.DataSource = ds.Tables[0];
            }

            #region Процесс загрузки
            try
            {
                var filesManager = new FilesManager(connString);
                var CVManager = new ClientVersionManager(versionStorageName);

                long totalSize = 0;
                FilesForLoad.ForEach(fileInfo => totalSize += fileInfo.FileSize);

                var progressForm = new frmProgress((int)totalSize);
                progressForm.Show();
                progressForm.BringToFront();

                var downloadProgress = new DownloadProgress(totalSize);

                downloadProgress.ProgressChanged +=
                    (senders, ex) => progressForm.Tick(ex.ChangeSize, string.Format("{0,3:#.#}/{1,3:#.#} MБ ({2})", (decimal)downloadProgress.DownloadedSize / BytesInMegabyte,
                       (decimal)downloadProgress.TotalSize / BytesInMegabyte, downloadProgress.CurrentFileName));

                foreach (FileInfo fileInfo in FilesForLoad)
                {
                    try
                    {
                        downloadProgress.SetCurrentFileName(fileInfo.FileName);
                        filesManager.Download(fileInfo.FileName, downloadProgress);
                        CVManager.SetVersion(fileInfo.FileName, fileInfo.ServerVersion);

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
        #endregion
        #endregion

        #region Boxs

        private void _lstInloadFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void _lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}