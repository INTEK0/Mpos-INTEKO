
namespace WindowsFormsApp2
{
    partial class EXCELL_IMPORT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EXCELL_IMPORT));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chControlImport = new DevExpress.XtraEditors.CheckEdit();
            this.chDirectControl = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.bHelp = new DevExpress.XtraEditors.SimpleButton();
            this.bExcelDownload = new DevExpress.XtraEditors.SimpleButton();
            this.tFilePath = new DevExpress.XtraEditors.ButtonEdit();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chControlImport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chDirectControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tFilePath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Səhifə";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Faylın yolu";
            // 
            // simpleButton2
            // 
            this.simpleButton2.AllowFocus = false;
            this.simpleButton2.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Nunito", 10.25F, System.Drawing.FontStyle.Bold);
            this.simpleButton2.Appearance.ForeColor = System.Drawing.Color.White;
            this.simpleButton2.Appearance.Options.UseBackColor = true;
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Appearance.Options.UseForeColor = true;
            this.simpleButton2.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton2.ImageOptions.SvgImage")));
            this.simpleButton2.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.simpleButton2.Location = new System.Drawing.Point(345, 116);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(4);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.simpleButton2.Size = new System.Drawing.Size(115, 29);
            this.simpleButton2.TabIndex = 6;
            this.simpleButton2.Text = "Yüklə";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.tablePanel1.SetColumn(this.dataGridView1, 0);
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(4, 180);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.tablePanel1.SetRow(this.dataGridView1, 1);
            this.dataGridView1.Size = new System.Drawing.Size(1234, 590);
            this.dataGridView1.TabIndex = 7;
            // 
            // chControlImport
            // 
            this.chControlImport.Location = new System.Drawing.Point(9, 141);
            this.chControlImport.Name = "chControlImport";
            this.chControlImport.Properties.AllowFocused = false;
            this.chControlImport.Properties.Caption = "Sətirləri yoxlayaraq əlavə et";
            this.chControlImport.Properties.RadioGroupIndex = 1;
            this.chControlImport.Size = new System.Drawing.Size(217, 22);
            this.chControlImport.TabIndex = 8;
            this.chControlImport.TabStop = false;
            this.chControlImport.Tag = "ExcelImport_Control";
            // 
            // chDirectControl
            // 
            this.chDirectControl.EditValue = true;
            this.chDirectControl.Location = new System.Drawing.Point(9, 113);
            this.chDirectControl.Name = "chDirectControl";
            this.chDirectControl.Properties.AllowFocused = false;
            this.chDirectControl.Properties.Caption = "Birbaşa əlavə et";
            this.chDirectControl.Properties.RadioGroupIndex = 1;
            this.chDirectControl.Size = new System.Drawing.Size(142, 22);
            this.chDirectControl.TabIndex = 8;
            this.chDirectControl.Tag = "ExcelImport_Direct";
            // 
            // groupControl1
            // 
            this.tablePanel1.SetColumn(this.groupControl1, 0);
            this.groupControl1.Controls.Add(this.lookUpEdit1);
            this.groupControl1.Controls.Add(this.separatorControl1);
            this.groupControl1.Controls.Add(this.bHelp);
            this.groupControl1.Controls.Add(this.bExcelDownload);
            this.groupControl1.Controls.Add(this.simpleButton2);
            this.groupControl1.Controls.Add(this.chControlImport);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.chDirectControl);
            this.groupControl1.Controls.Add(this.tFilePath);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(3, 3);
            this.groupControl1.Name = "groupControl1";
            this.tablePanel1.SetRow(this.groupControl1, 0);
            this.groupControl1.Size = new System.Drawing.Size(1236, 170);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Excel ilə məhsulların əlavəsi";
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Enabled = false;
            this.lookUpEdit1.Location = new System.Drawing.Point(89, 68);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.NullText = "";
            this.lookUpEdit1.Properties.NullValuePrompt = "Səhifə seçimini edin";
            this.lookUpEdit1.Properties.ShowFooter = false;
            this.lookUpEdit1.Size = new System.Drawing.Size(245, 30);
            this.lookUpEdit1.TabIndex = 10;
            this.lookUpEdit1.EditValueChanged += new System.EventHandler(this.lookUpEdit1_EditValueChanged);
            // 
            // separatorControl1
            // 
            this.separatorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorControl1.AutoSizeMode = true;
            this.separatorControl1.Location = new System.Drawing.Point(5, 104);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Padding = new System.Windows.Forms.Padding(3);
            this.separatorControl1.Size = new System.Drawing.Size(1226, 7);
            this.separatorControl1.TabIndex = 9;
            // 
            // bHelp
            // 
            this.bHelp.AllowFocus = false;
            this.bHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bHelp.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.bHelp.Appearance.Font = new System.Drawing.Font("Nunito", 10.25F, System.Drawing.FontStyle.Bold);
            this.bHelp.Appearance.ForeColor = System.Drawing.Color.White;
            this.bHelp.Appearance.Options.UseBackColor = true;
            this.bHelp.Appearance.Options.UseFont = true;
            this.bHelp.Appearance.Options.UseForeColor = true;
            this.bHelp.Appearance.Options.UseTextOptions = true;
            this.bHelp.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.bHelp.Enabled = false;
            this.bHelp.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bHelp.ImageOptions.SvgImage")));
            this.bHelp.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.bHelp.Location = new System.Drawing.Point(1086, 116);
            this.bHelp.Margin = new System.Windows.Forms.Padding(4);
            this.bHelp.Name = "bHelp";
            this.bHelp.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bHelp.Size = new System.Drawing.Size(144, 47);
            this.bHelp.TabIndex = 6;
            this.bHelp.Text = "İstifadə qaydası";
            this.bHelp.Visible = false;
            this.bHelp.Click += new System.EventHandler(this.bHelp_Click);
            // 
            // bExcelDownload
            // 
            this.bExcelDownload.AllowFocus = false;
            this.bExcelDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bExcelDownload.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bExcelDownload.Appearance.Font = new System.Drawing.Font("Nunito", 10.25F, System.Drawing.FontStyle.Bold);
            this.bExcelDownload.Appearance.ForeColor = System.Drawing.Color.White;
            this.bExcelDownload.Appearance.Options.UseBackColor = true;
            this.bExcelDownload.Appearance.Options.UseFont = true;
            this.bExcelDownload.Appearance.Options.UseForeColor = true;
            this.bExcelDownload.Appearance.Options.UseTextOptions = true;
            this.bExcelDownload.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.bExcelDownload.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bExcelDownload.ImageOptions.SvgImage")));
            this.bExcelDownload.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.bExcelDownload.Location = new System.Drawing.Point(1086, 33);
            this.bExcelDownload.Margin = new System.Windows.Forms.Padding(4);
            this.bExcelDownload.Name = "bExcelDownload";
            this.bExcelDownload.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bExcelDownload.Size = new System.Drawing.Size(144, 65);
            this.bExcelDownload.TabIndex = 6;
            this.bExcelDownload.Text = "Nümunəvi excel faylını yüklə";
            this.bExcelDownload.Click += new System.EventHandler(this.bExcelDownload_Click);
            // 
            // tFilePath
            // 
            this.tFilePath.Location = new System.Drawing.Point(89, 32);
            this.tFilePath.Name = "tFilePath";
            this.tFilePath.Properties.Appearance.Font = new System.Drawing.Font("Nunito", 8.25F);
            this.tFilePath.Properties.Appearance.Options.UseFont = true;
            serializableAppearanceObject5.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Bold);
            serializableAppearanceObject5.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Hyperlink;
            serializableAppearanceObject5.Options.UseFont = true;
            serializableAppearanceObject5.Options.UseForeColor = true;
            this.tFilePath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Axtar", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.tFilePath.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.tFilePath.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.tFilePath_Properties_ButtonClick);
            this.tFilePath.Size = new System.Drawing.Size(371, 30);
            this.tFilePath.TabIndex = 1;
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F)});
            this.tablePanel1.Controls.Add(this.groupControl1);
            this.tablePanel1.Controls.Add(this.dataGridView1);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 176F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(1242, 774);
            this.tablePanel1.TabIndex = 10;
            // 
            // EXCELL_IMPORT
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1242, 774);
            this.Controls.Add(this.tablePanel1);
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(327, 327);
            this.Name = "EXCELL_IMPORT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excel ilə yüklə";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chControlImport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chDirectControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tFilePath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DevExpress.XtraEditors.CheckEdit chControlImport;
        private DevExpress.XtraEditors.CheckEdit chDirectControl;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ButtonEdit tFilePath;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.SimpleButton bExcelDownload;
        private DevExpress.XtraEditors.SimpleButton bHelp;
    }
}