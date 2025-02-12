namespace WindowsFormsApp2.Forms
{
    partial class fAdminPassword
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
            this.groupControl1.Size = new System.Drawing.Size(423, 111);
            this.groupControl1.TabIndex = 7;
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
            this.tPassword.Size = new System.Drawing.Size(406, 30);
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
            this.bSubmit.Location = new System.Drawing.Point(272, 75);
            this.bSubmit.Name = "bSubmit";
            this.bSubmit.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bSubmit.Size = new System.Drawing.Size(146, 27);
            this.bSubmit.TabIndex = 1;
            this.bSubmit.Text = "Təsdiq et";
            this.bSubmit.Click += new System.EventHandler(this.bSubmit_Click);
            // 
            // fAdminPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(423, 111);
            this.Controls.Add(this.groupControl1);
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(425, 145);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(425, 145);
            this.Name = "fAdminPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mpos - Admin";
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