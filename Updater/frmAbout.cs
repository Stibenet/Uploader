using System;
using System.Windows.Forms;

public class frmAbout : System.Windows.Forms.Form
{

#region " Windows Form Designer generated code "

	public frmAbout() 
	{
		//This call is required by the Windows Form Designer.
		InitializeComponent();
		//Add any initialization after the InitializeComponent() call
	}

	//Form overrides dispose to clean up the component list.
	protected override void Dispose(bool disposing) {
		if (disposing) {
			if (components != null) {
				components.Dispose();
			}
		}
		base.Dispose(disposing);
	}

	//Required by the Windows Form Designer
	private System.ComponentModel.IContainer components = null;

	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.  
	//Do not modify it using the code editor.
	private System.Windows.Forms.PictureBox pbIcon;

	private System.Windows.Forms.Label lblTitle;

	private System.Windows.Forms.Label lblVersion;

	private System.Windows.Forms.Label lblDescription;

	private System.Windows.Forms.Button cmdOK;

	private System.Windows.Forms.Label lblCopyright;

	private System.Windows.Forms.Label lblCodebase;

	private void InitializeComponent() {

		this.pbIcon = new System.Windows.Forms.PictureBox();

		this.lblTitle = new System.Windows.Forms.Label();

		this.lblVersion = new System.Windows.Forms.Label();

		this.lblDescription = new System.Windows.Forms.Label();

		this.cmdOK = new System.Windows.Forms.Button();

		this.lblCopyright = new System.Windows.Forms.Label();

		this.lblCodebase = new System.Windows.Forms.Label();

		this.SuspendLayout();

		//

		//pbIcon

		//

		this.pbIcon.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;

		this.pbIcon.ImeMode = System.Windows.Forms.ImeMode.NoControl;

		this.pbIcon.Location = new System.Drawing.Point(16, 16);

		this.pbIcon.Name = "pbIcon";

		this.pbIcon.Size = new System.Drawing.Size(32, 32);

		this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

		this.pbIcon.TabIndex = 0;

		this.pbIcon.TabStop = false;

		//

		//lblTitle

		//

		this.lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;

		this.lblTitle.Location = new System.Drawing.Point(72, 16);

		this.lblTitle.Name = "lblTitle";

		this.lblTitle.Size = new System.Drawing.Size(360, 23);

		this.lblTitle.TabIndex = 1;

		this.lblTitle.Text = "Application Title";

		//

		//lblVersion

		//

		this.lblVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;

		this.lblVersion.Location = new System.Drawing.Point(72, 40);

		this.lblVersion.Name = "lblVersion";

		this.lblVersion.Size = new System.Drawing.Size(360, 23);

		this.lblVersion.TabIndex = 2;

		this.lblVersion.Text = "Application Version";

		//

		//lblDescription

		//

		this.lblDescription.ImeMode = System.Windows.Forms.ImeMode.NoControl;

		this.lblDescription.Location = new System.Drawing.Point(72, 80);

		this.lblDescription.Name = "lblDescription";

		this.lblDescription.Size = new System.Drawing.Size(360, 46);

		this.lblDescription.TabIndex = 3;

		this.lblDescription.Text = "Application Description";

		//

		//cmdOK

		//

		this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;

		this.cmdOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;

		this.cmdOK.Location = new System.Drawing.Point(352, 200);

		this.cmdOK.Name = "cmdOK";

		this.cmdOK.TabIndex = 4;

		this.cmdOK.Text = "OK";

		//

		//lblCopyright

		//

		this.lblCopyright.ImeMode = System.Windows.Forms.ImeMode.NoControl;

		this.lblCopyright.Location = new System.Drawing.Point(72, 56);

		this.lblCopyright.Name = "lblCopyright";

		this.lblCopyright.Size = new System.Drawing.Size(360, 23);

		this.lblCopyright.TabIndex = 5;

		this.lblCopyright.Text = "Application Copyright";

		//

		//lblCodebase

		//

		this.lblCodebase.ImeMode = System.Windows.Forms.ImeMode.NoControl;

		this.lblCodebase.Location = new System.Drawing.Point(72, 128);

		this.lblCodebase.Name = "lblCodebase";

		this.lblCodebase.Size = new System.Drawing.Size(360, 64);

		this.lblCodebase.TabIndex = 6;

		this.lblCodebase.Text = "Application Codebase";

		//

		//frmAbout

		//

		this.AcceptButton = this.cmdOK;

		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);

		this.CancelButton = this.cmdOK;

		this.ClientSize = new System.Drawing.Size(440, 232);

		this.Controls.AddRange(new System.Windows.Forms.Control[] {this.lblCodebase, this.lblCopyright, this.cmdOK, this.lblDescription, this.lblVersion, this.lblTitle, this.pbIcon});

		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;

		this.MaximizeBox = false;

		this.MinimizeBox = false;

		this.Name = "frmAbout";

		this.ShowInTaskbar = false;

		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

		this.Text = "About ...";

		this.ResumeLayout(false);

		this.Load += new System.EventHandler(frmAbout_Load);

	}

#endregion


	private void frmAbout_Load(object sender, System.EventArgs e) {

		try {

			this.Text = "About " + this.Owner.Text;
			this.Icon = this.Owner.Icon;
			this.pbIcon.Image = this.Owner.Icon.ToBitmap();

			//AssemblyInfo ainfo = new AssemblyInfo();

			this.lblTitle.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

			this.lblVersion.Text = "Текущая версия: " + 
				System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

			this.lblCopyright.Text = "Данный продукт принадлежит СПП ОРВиС ИнкоСистемс";

			this.lblDescription.Text = "Программа для закачки обновленых версий файлов на рабочие места";

			Type myType = typeof(frmAbout);
			this.lblCodebase.Text = myType.Assembly.CodeBase;


		} catch(System.Exception exp) {

			// This catch will trap any unexpected error.

			MessageBox.Show(exp.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);

		}

	}

}

