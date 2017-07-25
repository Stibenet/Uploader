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
        protected const string SqlConnectionErrorMessage = "����������� ����������� � �������. {}������ �����������:{0}";
        private Label _lblItemCount;
        private ListBox _lstFiles;
        private MainMenu _mnu;
        private MenuItem _mnuAboout;
        private MenuItem _mnuExite;
        private OpenFileDialog _openFileDialog;
        private IContainer components;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        private MenuItem menuItem8;
        private MenuItem menuItem3;
        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
        public static bool flag = false;
        private readonly List<FileInfo> _filesForLoad;
        private const int BytesInMegabyte = 1048573;
        private DataGridView dataGridView1;
        private readonly string _XMLFileName;

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
                    MessageBox.Show(connectionChecker.ErrorMessage, "������ ��� �������� ����������� � SQL �������", MessageBoxButtons.OK,
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

                string _errorMessage = string.Format("������. {0}{1}{2}", exception.Message, Environment.NewLine, exception.StackTrace);
                MessageBox.Show("������ ������� ����������:\r\n", _errorMessage);
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
            MessageBox.Show("������ ������� ����������:\r\n" + ex, "������ ������� ���������� " + StartUpFileName);
        }

        private void UIForm_Load(object sender, EventArgs e)
        {
            InitOpenFileDialog();
            _openFileDialog.FileOk += _openFileDialog_FileOk;
        }

        #region �������� ����� �� ������
        private void _lstFiles_DoubleClick(object sender, EventArgs e)
        {
            if (_lstFiles.SelectedIndex < 0)
            {
                return;
            }

            DialogResult result;
            result = MessageBox.Show(this, "������� ���� �� ������?", "������������� ��������", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _lstFiles.Items.Remove(_lstFiles.Items[_lstFiles.SelectedIndex]);
            }

            _lstFiles.Refresh();
        }
        #endregion

        private void _mnuAboout_Click(object sender, EventArgs e)
        {
            var frm = new frmAbout();
            frm.ShowDialog(this);
            frm.Dispose();
        }


        #region ���������� �������� �������� Escape
        private void UIForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
        #endregion

        #region ����� ������
        /// <summary>��������� �������</summary>
        private void InitOpenFileDialog()
        {
            _openFileDialog.Multiselect = true;
            _openFileDialog.Filter =
                "����������� ����� (*.EXE;*.DLL)|*.EXE;*.DLL|" +
                "����� �������� (*.TXT;*.XML)|*.TXT;*.XML|" +
                "��� ����� (*.*)|*.*";
            _openFileDialog.FilterIndex = 3;
        }

        /// <summary>��������� ����� ������� �� ������ OK � �������</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            Activate();
            String[] files = _openFileDialog.FileNames;

            foreach (String file in files)
            {
                if (_lstFiles.FindStringExact(file) < 0)
                {
                    _lstFiles.Items.Add(file);
                }
            }

            _lblItemCount.Text = "������ �������: " + _lstFiles.Items.Count;
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
            this._lstFiles = new System.Windows.Forms.ListBox();
            this._mnu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this._mnuExite = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this._mnuAboout = new System.Windows.Forms.MenuItem();
            this._lblItemCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // _lstFiles
            // 
            this._lstFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lstFiles.Location = new System.Drawing.Point(7, 31);
            this._lstFiles.Name = "_lstFiles";
            this._lstFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this._lstFiles.Size = new System.Drawing.Size(541, 121);
            this._lstFiles.TabIndex = 2;
            this._lstFiles.SelectedIndexChanged += new System.EventHandler(this._lstFiles_SelectedIndexChanged);
            this._lstFiles.DoubleClick += new System.EventHandler(this._lstFiles_DoubleClick);
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
            this.menuItem2,
            this.menuItem8,
            this._mnuExite});
            this.menuItem1.Text = "����";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "�������";
            this.menuItem2.Click += new System.EventHandler(this._mnuOpen_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 1;
            this.menuItem8.Text = "��������";
            this.menuItem8.Click += new System.EventHandler(this._mnuClear_Click);
            // 
            // _mnuExite
            // 
            this._mnuExite.Index = 2;
            this._mnuExite.Text = "�����";
            this._mnuExite.Click += new System.EventHandler(this._mnuExite_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this._mnuAboout});
            this.menuItem3.Text = "�������";
            // 
            // _mnuAboout
            // 
            this._mnuAboout.Index = 0;
            this._mnuAboout.Text = "� ���������";
            this._mnuAboout.Click += new System.EventHandler(this._mnuAboout_Click);
            // 
            // _lblItemCount
            // 
            this._lblItemCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._lblItemCount.AutoSize = true;
            this._lblItemCount.Location = new System.Drawing.Point(4, 368);
            this._lblItemCount.Name = "_lblItemCount";
            this._lblItemCount.Size = new System.Drawing.Size(0, 13);
            this._lblItemCount.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(309, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "��������������� �������� ������ ����� ��������� � ��";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "����������� ����� �� ��";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 360);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "��������� � ��";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this._cmdUpLoad_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(404, 360);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "�������� �����";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this._cmdInLoad_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dataGridView1.Location = new System.Drawing.Point(7, 194);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(541, 138);
            this.dataGridView1.TabIndex = 12;
            // 
            // UIForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(549, 393);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._lblItemCount);
            this.Controls.Add(this._lstFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Menu = this._mnu;
            this.Name = "UIForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "������ � ������� ���� ������";
            this.Load += new System.EventHandler(this.UIForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UIForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region ������
        private void _mnuOpen_Click(object sender, EventArgs e)
        {
            _openFileDialog.ShowDialog(this);
        }

        private void _mnuDel_Click(object sender, EventArgs e)
        {
            foreach (object item in new ArrayList(_lstFiles.SelectedItems))
            {
                _lstFiles.Items.Remove(item);
            }
        }

        private void _mnuClear_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "�������� ������ ������?", "������������� ��������", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                _lstFiles.Items.Clear();
            }
        }

        private void _mnuExite_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region �������� ��������� ������ � ��

        /// <summary>�������� ��������� ������ � ��</summary>
        private void _cmdUpLoad_Click(object sender, EventArgs e)
        {
            if (_lstFiles.Items.Count == 0)
            {
                return;
            }

            String connString;
            connString = ConfigurationSettings.AppSettings["ConnectionString"];
            var FM = new FilesManager(connString);

            var progress = new frmProgress(_lstFiles.Items.Count);
            progress.Show();

            for (Int32 i = 0; i <= _lstFiles.Items.Count - 1; i++)
            {
                progress.Tick(1, _lstFiles.Items[i].ToString());
                FM.Upload(_lstFiles.Items[i].ToString());
            }

            progress.Close();
            _lstFiles.Items.Clear();


        }
        #endregion


        #region �������� ������ �� ��
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

            //����� � ������� ���������� ��� �������� ������ �� ��
            using (SqlDataAdapter a = new SqlDataAdapter("SELECT version, name, Date FROM Srv_ProgramFile", cnn))
            {
                SqlCommandBuilder cb = new SqlCommandBuilder(a);
                DataSet ds = new DataSet();
                a.Fill(ds, "Srv_ProgramFile");
                dataGridView1.DataSource = ds.Tables[0];
            }

            #region ������� ��������
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
                    (senders, ex) => progressForm.Tick(ex.ChangeSize, string.Format("{0,3:#.#}/{1,3:#.#} M� ({2})", (decimal)downloadProgress.DownloadedSize / BytesInMegabyte,
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

                string _errorMessage = string.Format("������. {0}{1}{2}", exception.Message, Environment.NewLine, exception.StackTrace);
                MessageBox.Show("������ ������� ����������:\r\n", _errorMessage);
            }
        }
        #endregion
        #endregion

        #region �����
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}