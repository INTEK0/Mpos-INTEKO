namespace WindowsFormsApp2.Forms
{
    partial class fInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fInfo));
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lWebLink = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.lVersion = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureEdit1.EditValue = global::WindowsFormsApp2.Properties.Resources.Logo__With__png;
            this.pictureEdit1.Location = new System.Drawing.Point(3, 0);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.NullText = "Logo";
            this.pictureEdit1.Properties.ShowEditMenuItem = DevExpress.Utils.DefaultBoolean.False;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.False;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.pictureEdit1.Properties.ZoomAcceleration = 50D;
            this.pictureEdit1.Size = new System.Drawing.Size(473, 70);
            this.pictureEdit1.TabIndex = 1;
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelControl1.Controls.Add(this.lVersion);
            this.panelControl1.Controls.Add(this.separatorControl1);
            this.panelControl1.Controls.Add(this.lWebLink);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(3, 76);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(483, 167);
            this.panelControl1.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Poppins", 11F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(9, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(382, 52);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "© 2021-{year} • İNTEKO MMC\r\nMüəllif hüquqları İNTEKO MMC şirkətinə məxsusdur.";
            // 
            // lWebLink
            // 
            this.lWebLink.Appearance.Font = new System.Drawing.Font("Poppins", 11F);
            this.lWebLink.Appearance.Options.UseFont = true;
            this.lWebLink.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lWebLink.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("hyperlinkLabelControl1.ImageOptions.SvgImage")));
            this.lWebLink.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.lWebLink.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.lWebLink.Location = new System.Drawing.Point(9, 74);
            this.lWebLink.Name = "lWebLink";
            this.lWebLink.Size = new System.Drawing.Size(128, 26);
            this.lWebLink.TabIndex = 1;
            this.lWebLink.Text = "www.inteko.az";
            this.lWebLink.Click += new System.EventHandler(this.hyperlinkLabelControl1_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Poppins", 11F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.labelControl2.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("labelControl2.ImageOptions.SvgImage")));
            this.labelControl2.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.labelControl2.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.labelControl2.Location = new System.Drawing.Point(250, 106);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(146, 26);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "(055)206-23-66";
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Poppins", 11F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.labelControl3.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("labelControl3.ImageOptions.SvgImage")));
            this.labelControl3.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.labelControl3.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.labelControl3.Location = new System.Drawing.Point(250, 74);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(223, 26);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "(012)311-15-11 (Qaynar xətt)";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Poppins", 11F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.labelControl4.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("labelControl4.ImageOptions.SvgImage")));
            this.labelControl4.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.None;
            this.labelControl4.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.labelControl4.Location = new System.Drawing.Point(250, 138);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(144, 26);
            this.labelControl4.TabIndex = 0;
            this.labelControl4.Text = "(055)206-23-55";
            // 
            // separatorControl1
            // 
            this.separatorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorControl1.AutoSizeMode = true;
            this.separatorControl1.Location = new System.Drawing.Point(3, 63);
            this.separatorControl1.Margin = new System.Windows.Forms.Padding(1);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Padding = new System.Windows.Forms.Padding(1);
            this.separatorControl1.Size = new System.Drawing.Size(475, 3);
            this.separatorControl1.TabIndex = 2;
            // 
            // lVersion
            // 
            this.lVersion.Appearance.Font = new System.Drawing.Font("Poppins", 11F);
            this.lVersion.Appearance.Options.UseFont = true;
            this.lVersion.ImageAlignToText = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.lVersion.Location = new System.Drawing.Point(9, 106);
            this.lVersion.Name = "lVersion";
            this.lVersion.Size = new System.Drawing.Size(58, 26);
            this.lVersion.TabIndex = 0;
            this.lVersion.Text = "Versiya:";
            // 
            // fInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(488, 246);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.panelControl1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(490, 300);
            this.MinimizeBox = false;
            this.Name = "fInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Haqqımızda - İNTEKO MMC";
            this.Load += new System.EventHandler(this.fInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.HyperlinkLabelControl lWebLink;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.LabelControl lVersion;
    }
}