using System;
using System.ComponentModel;
using System.Windows.Forms;
using Updater.Utils;

namespace Updater
{
    /// <summary>
    /// Summary description for frmProgress.
    /// </summary>
    public class frmProgress : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        private ProgressBar _progress;
        private Label _lblInfo;

        #region Свойства

        /// <summary>Максимальное значение для прогресс-Бара</summary>
        public Int32 ProgressMaxValue
        {
            get { return _progress.Maximum; }
            set { _progress.Maximum = value; }
        }


        /// <summary>Текущее значение прогресс-Бара</summary>
        public Int32 ProgressValue
        {
            get { return _progress.Value; }
            set
            {
                _progress.Value = value;

                if (!TaskbarUtils.Win7TaskBarNotSupported)
                {
                    TaskbarUtils.SetProgressValue(Handle, (ulong)value, (ulong)ProgressMaxValue);
                }
            }
        }

        public String LabelText
        {
            get { return _lblInfo.Text; }
            set { _lblInfo.Text = value; }
        }

        #endregion

        /// <summary>Увеличивает значение прогресса на 1</summary>
        /// <summary>Увеличивает значение прогресса на 1</summary>
        public void Tick(int tickSize)
        {
            if (ProgressValue == ProgressMaxValue)
            {
                ProgressValue = 1;
            }
            else
            {
                ProgressValue += tickSize;
            }
        }

        public void Fail()
        {
            if (!TaskbarUtils.Win7TaskBarNotSupported)
            {
                TaskbarUtils.SetProgressState(Handle, TaskbarUtils.ThumbnailProgressState.Error);
            }
        }

        /// <summary>Изменяет значение текста в сообщении и увеличивает значение прогресса на 1</summary>
        /// <param name="LblText">Значение текста в текстовом поле</param>
        public void Tick(int tickSize, String LblText)
        {
            LabelText = LblText;
            Tick(tickSize);
            Application.DoEvents();
        }

        #region Конструкторы

        public frmProgress(Int32 MaxValue)
        {
            InitializeComponent();
            ProgressMaxValue = MaxValue;
            ProgressValue = 0;
            LabelText = "";
            KeyPreview = true;

            KeyDown += frmProgress_KeyDown;
        }

        void frmProgress_KeyDown(object sender, KeyEventArgs e)
        {
            FormKeyCode = e.KeyCode;
        }

        public Keys FormKeyCode { get; set; }

        public frmProgress()
            : this(0)
        { }

        #endregion

        /// <summary>
        /// Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProgress));
            this._progress = new System.Windows.Forms.ProgressBar();
            this._lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _progress
            // 
            this._progress.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this._progress.Location = new System.Drawing.Point(4, 16);
            this._progress.Name = "_progress";
            this._progress.Size = new System.Drawing.Size(452, 28);
            this._progress.TabIndex = 0;
            // 
            // _lblInfo
            // 
            this._lblInfo.AutoSize = true;
            this._lblInfo.Location = new System.Drawing.Point(8, 52);
            this._lblInfo.Name = "_lblInfo";
            this._lblInfo.Size = new System.Drawing.Size(35, 13);
            this._lblInfo.TabIndex = 1;
            this._lblInfo.Text = "ghjghj";
            // 
            // frmProgress
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(460, 81);
            this.Controls.Add(this._lblInfo);
            this.Controls.Add(this._progress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProgress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Загрузка файлов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Отключение кнопки "закрыть"

        public const int CS_NOCLOSE = 0x0200;

        protected override CreateParams CreateParams
        {
            get
            {
                var createParams = base.CreateParams;
                createParams.ClassStyle = createParams.ClassStyle | CS_NOCLOSE;
                return createParams;
            }
        }

        #endregion
    }
}