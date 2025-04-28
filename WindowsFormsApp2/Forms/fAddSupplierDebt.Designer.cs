namespace WindowsFormsApp2.Forms
{
    partial class fAddSupplierDebt
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
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.tAmount = new DevExpress.XtraEditors.TextEdit();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.tComment = new DevExpress.XtraEditors.MemoEdit();
            this.lHeader = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.bSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lookSupplier = new DevExpress.XtraEditors.LookUpEdit();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.tDebtBalance = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.tDebtNew = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.tDebtTotal = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookSupplier.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDebtBalance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDebtNew.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDebtTotal.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 33.99F)});
            this.tablePanel1.Controls.Add(this.panelControl3);
            this.tablePanel1.Controls.Add(this.lHeader);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(2, 2);
            this.tablePanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 51F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(497, 437);
            this.tablePanel1.TabIndex = 0;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // panelControl3
            // 
            this.tablePanel1.SetColumn(this.panelControl3, 0);
            this.panelControl3.Controls.Add(this.separatorControl1);
            this.panelControl3.Controls.Add(this.lookSupplier);
            this.panelControl3.Controls.Add(this.tDebtTotal);
            this.panelControl3.Controls.Add(this.tDebtNew);
            this.panelControl3.Controls.Add(this.tDebtBalance);
            this.panelControl3.Controls.Add(this.tAmount);
            this.panelControl3.Controls.Add(this.dateEdit1);
            this.panelControl3.Controls.Add(this.labelControl5);
            this.panelControl3.Controls.Add(this.labelControl7);
            this.panelControl3.Controls.Add(this.labelControl4);
            this.panelControl3.Controls.Add(this.labelControl6);
            this.panelControl3.Controls.Add(this.labelControl3);
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Controls.Add(this.tComment);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(1, 52);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl3.Name = "panelControl3";
            this.tablePanel1.SetRow(this.panelControl3, 1);
            this.panelControl3.Size = new System.Drawing.Size(495, 384);
            this.panelControl3.TabIndex = 1;
            // 
            // tAmount
            // 
            this.tAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tAmount.EditValue = "0";
            this.tAmount.Location = new System.Drawing.Point(104, 48);
            this.tAmount.Name = "tAmount";
            this.tAmount.Properties.AllowFocused = false;
            this.tAmount.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.tAmount.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.tAmount.Properties.MaskSettings.Set("mask", "f");
            this.tAmount.Properties.NullText = "0";
            this.tAmount.Properties.UseMaskAsDisplayFormat = true;
            this.tAmount.Size = new System.Drawing.Size(382, 28);
            this.tAmount.TabIndex = 1;
            this.tAmount.EditValueChanged += new System.EventHandler(this.tAmount_EditValueChanged);
            // 
            // dateEdit1
            // 
            this.dateEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(104, 213);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.AllowFocused = false;
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Size = new System.Drawing.Size(382, 28);
            this.dateEdit1.TabIndex = 3;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Verdana", 11F);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(7, 219);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 18);
            this.labelControl5.TabIndex = 3;
            this.labelControl5.Text = "Tarix";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Verdana", 11F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(7, 86);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(39, 18);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Qeyd";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Verdana", 11F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(7, 55);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 18);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Məbləğ";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Verdana", 11F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(7, 18);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(73, 18);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Təchizatçı";
            // 
            // tComment
            // 
            this.tComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tComment.Location = new System.Drawing.Point(104, 86);
            this.tComment.Name = "tComment";
            this.tComment.Properties.AllowFocused = false;
            this.tComment.Size = new System.Drawing.Size(382, 121);
            this.tComment.TabIndex = 2;
            // 
            // lHeader
            // 
            this.lHeader.Appearance.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold);
            this.lHeader.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            this.lHeader.Appearance.Options.UseFont = true;
            this.lHeader.Appearance.Options.UseForeColor = true;
            this.lHeader.Appearance.Options.UseTextOptions = true;
            this.lHeader.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lHeader.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.tablePanel1.SetColumn(this.lHeader, 0);
            this.lHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lHeader.Location = new System.Drawing.Point(4, 4);
            this.lHeader.Name = "lHeader";
            this.tablePanel1.SetRow(this.lHeader, 0);
            this.lHeader.Size = new System.Drawing.Size(489, 45);
            this.lHeader.TabIndex = 0;
            this.lHeader.Text = "TƏCHİZATÇI BORC ƏLAVƏSİ";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.bSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 454);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(503, 51);
            this.panelControl1.TabIndex = 3;
            // 
            // bSave
            // 
            this.bSave.AllowFocus = false;
            this.bSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.bSave.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bSave.Appearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Bold);
            this.bSave.Appearance.Options.UseBackColor = true;
            this.bSave.Appearance.Options.UseFont = true;
            this.bSave.Location = new System.Drawing.Point(348, 6);
            this.bSave.Name = "bSave";
            this.bSave.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bSave.Size = new System.Drawing.Size(144, 40);
            this.bSave.TabIndex = 0;
            this.bSave.Text = "Borc yarat";
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.tablePanel1);
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(501, 441);
            this.panelControl2.TabIndex = 4;
            // 
            // lookSupplier
            // 
            this.lookSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lookSupplier.Location = new System.Drawing.Point(104, 13);
            this.lookSupplier.Margin = new System.Windows.Forms.Padding(4);
            this.lookSupplier.Name = "lookSupplier";
            this.lookSupplier.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookSupplier.Properties.NullText = "";
            this.lookSupplier.Properties.NullValuePrompt = "Təchizatçı seçimi";
            this.lookSupplier.Properties.ShowFooter = false;
            this.lookSupplier.Size = new System.Drawing.Size(382, 28);
            this.lookSupplier.TabIndex = 0;
            this.lookSupplier.TextChanged += new System.EventHandler(this.lookSupplier_TextChanged);
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(7, 247);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(479, 23);
            this.separatorControl1.TabIndex = 8;
            // 
            // tDebtBalance
            // 
            this.tDebtBalance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tDebtBalance.Location = new System.Drawing.Point(104, 276);
            this.tDebtBalance.Name = "tDebtBalance";
            this.tDebtBalance.Properties.AllowFocused = false;
            this.tDebtBalance.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.tDebtBalance.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.tDebtBalance.Properties.MaskSettings.Set("mask", "f");
            this.tDebtBalance.Properties.NullText = "0";
            this.tDebtBalance.Properties.ReadOnly = true;
            this.tDebtBalance.Properties.UseMaskAsDisplayFormat = true;
            this.tDebtBalance.Size = new System.Drawing.Size(382, 28);
            this.tDebtBalance.TabIndex = 1;
            this.tDebtBalance.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Verdana", 11F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(7, 281);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(74, 18);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Qalıq borc";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Verdana", 11F);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(7, 315);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(68, 18);
            this.labelControl6.TabIndex = 3;
            this.labelControl6.Text = "Yeni borc";
            // 
            // tDebtNew
            // 
            this.tDebtNew.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tDebtNew.Location = new System.Drawing.Point(104, 310);
            this.tDebtNew.Name = "tDebtNew";
            this.tDebtNew.Properties.AllowFocused = false;
            this.tDebtNew.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.tDebtNew.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.tDebtNew.Properties.MaskSettings.Set("mask", "f");
            this.tDebtNew.Properties.NullText = "0";
            this.tDebtNew.Properties.ReadOnly = true;
            this.tDebtNew.Properties.UseMaskAsDisplayFormat = true;
            this.tDebtNew.Size = new System.Drawing.Size(382, 28);
            this.tDebtNew.TabIndex = 1;
            this.tDebtNew.TabStop = false;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Verdana", 11F);
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(7, 352);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(83, 18);
            this.labelControl7.TabIndex = 3;
            this.labelControl7.Text = "Yekun borc";
            // 
            // tDebtTotal
            // 
            this.tDebtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tDebtTotal.Location = new System.Drawing.Point(104, 344);
            this.tDebtTotal.Name = "tDebtTotal";
            this.tDebtTotal.Properties.AllowFocused = false;
            this.tDebtTotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.tDebtTotal.Properties.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            this.tDebtTotal.Properties.Appearance.Options.UseFont = true;
            this.tDebtTotal.Properties.Appearance.Options.UseForeColor = true;
            this.tDebtTotal.Properties.AppearanceReadOnly.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            this.tDebtTotal.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.tDebtTotal.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.tDebtTotal.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.tDebtTotal.Properties.MaskSettings.Set("mask", "f");
            this.tDebtTotal.Properties.NullText = "0";
            this.tDebtTotal.Properties.ReadOnly = true;
            this.tDebtTotal.Properties.UseMaskAsDisplayFormat = true;
            this.tDebtTotal.Size = new System.Drawing.Size(382, 34);
            this.tDebtTotal.TabIndex = 1;
            this.tDebtTotal.TabStop = false;
            // 
            // fAddSupplierDebt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(503, 505);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl2);
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.Name = "fAddSupplierDebt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TƏCHİZATÇI BORC ƏLAVƏSİ";
            this.Load += new System.EventHandler(this.fAddSupplierDebt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookSupplier.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDebtBalance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDebtNew.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDebtTotal.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.MemoEdit tComment;
        private DevExpress.XtraEditors.LabelControl lHeader;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton bSave;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LookUpEdit lookSupplier;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit tAmount;
        private DevExpress.XtraEditors.TextEdit tDebtTotal;
        private DevExpress.XtraEditors.TextEdit tDebtNew;
        private DevExpress.XtraEditors.TextEdit tDebtBalance;
    }
}