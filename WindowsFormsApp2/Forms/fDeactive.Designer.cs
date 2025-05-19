namespace WindowsFormsApp2.Forms
{
    partial class fDeactive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fDeactive));
            this.tablePanel3 = new DevExpress.Utils.Layout.TablePanel();
            this.lProductID = new DevExpress.XtraEditors.LabelControl();
            this.lVersion = new DevExpress.XtraEditors.LabelControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bExit = new DevExpress.XtraEditors.SimpleButton();
            this.bLicenceControl = new DevExpress.XtraEditors.SimpleButton();
            this.lHeader = new DevExpress.XtraEditors.LabelControl();
            this.lMessage = new DevExpress.XtraEditors.LabelControl();
            this.pic2 = new DevExpress.XtraEditors.PictureEdit();
            this.picLogo = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel3)).BeginInit();
            this.tablePanel3.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel3
            // 
            this.tablePanel3.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25.2F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 29.96F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 54.84F)});
            this.tablePanel3.Controls.Add(this.lProductID);
            this.tablePanel3.Controls.Add(this.lVersion);
            this.tablePanel3.Controls.Add(this.panel3);
            this.tablePanel3.Controls.Add(this.pic2);
            this.tablePanel3.Controls.Add(this.picLogo);
            this.tablePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel3.Location = new System.Drawing.Point(0, 0);
            this.tablePanel3.Name = "tablePanel3";
            this.tablePanel3.Padding = new System.Windows.Forms.Padding(1);
            this.tablePanel3.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 66F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 519F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 47F)});
            this.tablePanel3.Size = new System.Drawing.Size(920, 535);
            this.tablePanel3.TabIndex = 13;
            this.tablePanel3.UseSkinIndents = true;
            // 
            // lProductID
            // 
            this.lProductID.Appearance.Font = new System.Drawing.Font("Century Gothic", 14F);
            this.lProductID.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lProductID.Appearance.Options.UseFont = true;
            this.lProductID.Appearance.Options.UseForeColor = true;
            this.lProductID.Appearance.Options.UseTextOptions = true;
            this.lProductID.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tablePanel3.SetColumn(this.lProductID, 1);
            this.tablePanel3.SetColumnSpan(this.lProductID, 2);
            this.lProductID.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lProductID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lProductID.Location = new System.Drawing.Point(211, 487);
            this.lProductID.Margin = new System.Windows.Forms.Padding(0);
            this.lProductID.Name = "lProductID";
            this.lProductID.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tablePanel3.SetRow(this.lProductID, 2);
            this.lProductID.Size = new System.Drawing.Size(708, 47);
            this.lProductID.TabIndex = 4;
            this.lProductID.Text = "ID";
            // 
            // lVersion
            // 
            this.lVersion.Appearance.Font = new System.Drawing.Font("Nunito Light", 14F);
            this.lVersion.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lVersion.Appearance.Options.UseFont = true;
            this.lVersion.Appearance.Options.UseForeColor = true;
            this.lVersion.Appearance.Options.UseTextOptions = true;
            this.lVersion.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.lVersion.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.tablePanel3.SetColumn(this.lVersion, 0);
            this.lVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lVersion.Location = new System.Drawing.Point(1, 487);
            this.lVersion.Margin = new System.Windows.Forms.Padding(0);
            this.lVersion.Name = "lVersion";
            this.lVersion.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.tablePanel3.SetRow(this.lVersion, 2);
            this.lVersion.Size = new System.Drawing.Size(210, 47);
            this.lVersion.TabIndex = 6;
            this.lVersion.Text = "Version:";
            // 
            // panel3
            // 
            this.tablePanel3.SetColumn(this.panel3, 0);
            this.tablePanel3.SetColumnSpan(this.panel3, 2);
            this.panel3.Controls.Add(this.bExit);
            this.panel3.Controls.Add(this.bLicenceControl);
            this.panel3.Controls.Add(this.lHeader);
            this.panel3.Controls.Add(this.lMessage);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(4, 59);
            this.panel3.Name = "panel3";
            this.tablePanel3.SetRow(this.panel3, 1);
            this.panel3.Size = new System.Drawing.Size(454, 425);
            this.panel3.TabIndex = 0;
            // 
            // bExit
            // 
            this.bExit.AllowFocus = false;
            this.bExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bExit.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(148)))));
            this.bExit.Appearance.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(46)))), ((int)(((byte)(148)))));
            this.bExit.Appearance.Font = new System.Drawing.Font("Nunito", 18F, System.Drawing.FontStyle.Bold);
            this.bExit.Appearance.Options.UseBackColor = true;
            this.bExit.Appearance.Options.UseFont = true;
            this.bExit.Location = new System.Drawing.Point(13, 361);
            this.bExit.Name = "bExit";
            this.bExit.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bExit.Size = new System.Drawing.Size(201, 41);
            this.bExit.TabIndex = 6;
            this.bExit.Text = "Çıxış";
            this.bExit.Click += new System.EventHandler(this.bExit_Click);
            // 
            // bLicenceControl
            // 
            this.bLicenceControl.AllowFocus = false;
            this.bLicenceControl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bLicenceControl.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(168)))), ((int)(((byte)(138)))));
            this.bLicenceControl.Appearance.Font = new System.Drawing.Font("Nunito", 18F, System.Drawing.FontStyle.Bold);
            this.bLicenceControl.Appearance.Options.UseBackColor = true;
            this.bLicenceControl.Appearance.Options.UseFont = true;
            this.bLicenceControl.Location = new System.Drawing.Point(220, 361);
            this.bLicenceControl.Name = "bLicenceControl";
            this.bLicenceControl.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bLicenceControl.Size = new System.Drawing.Size(213, 41);
            this.bLicenceControl.TabIndex = 6;
            this.bLicenceControl.Text = "Lisenziya yoxla";
            this.bLicenceControl.Click += new System.EventHandler(this.bLicenceControl_Click);
            // 
            // lHeader
            // 
            this.lHeader.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lHeader.Appearance.Font = new System.Drawing.Font("Nunito Black", 30F, System.Drawing.FontStyle.Bold);
            this.lHeader.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            this.lHeader.Appearance.Options.UseBackColor = true;
            this.lHeader.Appearance.Options.UseFont = true;
            this.lHeader.Appearance.Options.UseForeColor = true;
            this.lHeader.Appearance.Options.UseTextOptions = true;
            this.lHeader.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lHeader.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.lHeader.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lHeader.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lHeader.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lHeader.Location = new System.Drawing.Point(19, 32);
            this.lHeader.Name = "lHeader";
            this.lHeader.Size = new System.Drawing.Size(386, 120);
            this.lHeader.TabIndex = 3;
            this.lHeader.Text = "LİSENZİYA DEAKTİV EDİLDİ";
            // 
            // lMessage
            // 
            this.lMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lMessage.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lMessage.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.lMessage.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.lMessage.Appearance.Font = new System.Drawing.Font("Nunito", 18F);
            this.lMessage.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
            this.lMessage.Appearance.Options.UseBackColor = true;
            this.lMessage.Appearance.Options.UseBorderColor = true;
            this.lMessage.Appearance.Options.UseFont = true;
            this.lMessage.Appearance.Options.UseForeColor = true;
            this.lMessage.Appearance.Options.UseTextOptions = true;
            this.lMessage.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.lMessage.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lMessage.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lMessage.AppearanceDisabled.BackColor = System.Drawing.Color.Transparent;
            this.lMessage.AppearanceDisabled.BackColor2 = System.Drawing.Color.Transparent;
            this.lMessage.AppearanceDisabled.BorderColor = System.Drawing.Color.Transparent;
            this.lMessage.AppearanceDisabled.Options.UseBackColor = true;
            this.lMessage.AppearanceDisabled.Options.UseBorderColor = true;
            this.lMessage.AppearanceHovered.BackColor = System.Drawing.Color.Transparent;
            this.lMessage.AppearanceHovered.BackColor2 = System.Drawing.Color.Transparent;
            this.lMessage.AppearanceHovered.BorderColor = System.Drawing.Color.Transparent;
            this.lMessage.AppearanceHovered.Options.UseBackColor = true;
            this.lMessage.AppearanceHovered.Options.UseBorderColor = true;
            this.lMessage.AppearancePressed.BackColor = System.Drawing.Color.Transparent;
            this.lMessage.AppearancePressed.BackColor2 = System.Drawing.Color.Transparent;
            this.lMessage.AppearancePressed.BorderColor = System.Drawing.Color.Transparent;
            this.lMessage.AppearancePressed.Options.UseBackColor = true;
            this.lMessage.AppearancePressed.Options.UseBorderColor = true;
            this.lMessage.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lMessage.Location = new System.Drawing.Point(13, 158);
            this.lMessage.Name = "lMessage";
            this.lMessage.Padding = new System.Windows.Forms.Padding(10, 5, 5, 0);
            this.lMessage.Size = new System.Drawing.Size(438, 197);
            this.lMessage.TabIndex = 4;
            this.lMessage.Text = "Ətraflı məlumat üçün (055-206-23-66) nömrəsi ilə əlaqə saxlayın.";
            // 
            // pic2
            // 
            this.tablePanel3.SetColumn(this.pic2, 2);
            this.pic2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic2.EditValue = ((object)(resources.GetObject("pic2.EditValue")));
            this.pic2.Location = new System.Drawing.Point(464, 59);
            this.pic2.Name = "pic2";
            this.pic2.Properties.AllowFocused = false;
            this.pic2.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pic2.Properties.Appearance.Options.UseBackColor = true;
            this.pic2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pic2.Properties.NullText = " ";
            this.pic2.Properties.ShowMenu = false;
            this.pic2.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pic2.Properties.SvgImageSize = new System.Drawing.Size(1200, 690);
            this.pic2.Properties.ZoomPercent = 10D;
            this.tablePanel3.SetRow(this.pic2, 1);
            this.pic2.Size = new System.Drawing.Size(452, 425);
            this.pic2.TabIndex = 5;
            // 
            // picLogo
            // 
            this.tablePanel3.SetColumn(this.picLogo, 0);
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogo.EditValue = ((object)(resources.GetObject("picLogo.EditValue")));
            this.picLogo.Location = new System.Drawing.Point(4, 4);
            this.picLogo.Name = "picLogo";
            this.picLogo.Properties.AllowFocused = false;
            this.picLogo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.Properties.Appearance.Options.UseBackColor = true;
            this.picLogo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picLogo.Properties.NullText = " ";
            this.picLogo.Properties.ShowMenu = false;
            this.picLogo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.tablePanel3.SetRow(this.picLogo, 0);
            this.picLogo.Size = new System.Drawing.Size(204, 49);
            this.picLogo.TabIndex = 5;
            this.picLogo.DoubleClick += new System.EventHandler(this.picLogo_DoubleClick);
            // 
            // fDeactive
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(920, 535);
            this.Controls.Add(this.tablePanel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.Name = "fDeactive";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mpos";
            this.Load += new System.EventHandler(this.fDeactive_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel3)).EndInit();
            this.tablePanel3.ResumeLayout(false);
            this.tablePanel3.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel3;
        private DevExpress.XtraEditors.LabelControl lProductID;
        private DevExpress.XtraEditors.LabelControl lVersion;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.SimpleButton bExit;
        private DevExpress.XtraEditors.SimpleButton bLicenceControl;
        private DevExpress.XtraEditors.LabelControl lHeader;
        private DevExpress.XtraEditors.LabelControl lMessage;
        private DevExpress.XtraEditors.PictureEdit pic2;
        private DevExpress.XtraEditors.PictureEdit picLogo;
    }
}