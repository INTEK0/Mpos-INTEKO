namespace Licence.Forms
{
    partial class fAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fAdmin));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.tPassword = new DevExpress.XtraEditors.TextEdit();
            this.bSubmit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tPassword.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.tPassword);
            this.groupControl1.Controls.Add(this.bSubmit);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(448, 116);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "Admin şifrəsini daxil edin";
            // 
            // tPassword
            // 
            this.tPassword.EditValue = "";
            this.tPassword.Location = new System.Drawing.Point(12, 39);
            this.tPassword.Name = "tPassword";
            this.tPassword.Properties.Appearance.BackColor = System.Drawing.Color.Snow;
            this.tPassword.Properties.Appearance.Options.UseBackColor = true;
            this.tPassword.Properties.LookAndFeel.SkinName = "WXI";
            this.tPassword.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            this.tPassword.Properties.NullText = "Login";
            this.tPassword.Properties.UseSystemPasswordChar = true;
            this.tPassword.Size = new System.Drawing.Size(431, 28);
            this.tPassword.TabIndex = 0;
            this.tPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tPassword_KeyDown);
            // 
            // bSubmit
            // 
            this.bSubmit.AllowFocus = false;
            this.bSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSubmit.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bSubmit.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bSubmit.Appearance.Options.UseBackColor = true;
            this.bSubmit.Appearance.Options.UseFont = true;
            this.bSubmit.Location = new System.Drawing.Point(297, 75);
            this.bSubmit.Name = "bSubmit";
            this.bSubmit.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bSubmit.Size = new System.Drawing.Size(146, 27);
            this.bSubmit.TabIndex = 1;
            this.bSubmit.Text = "Təsdiq et";
            this.bSubmit.Click += new System.EventHandler(this.bSubmit_Click);
            // 
            // fAdmin
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(448, 116);
            this.Controls.Add(this.groupControl1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("fAdmin.IconOptions.Image")));
            this.Name = "fAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İnteko - Support";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tPassword.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit tPassword;
        private DevExpress.XtraEditors.SimpleButton bSubmit;
    }
}