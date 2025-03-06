namespace WindowsFormsApp2.Forms
{
    partial class fAddIncomeAndExpenses
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
            this.tHeader = new DevExpress.XtraEditors.TextEdit();
            this.tComment = new DevExpress.XtraEditors.MemoEdit();
            this.lHeader = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.bSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tHeader.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tComment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
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
            this.tablePanel1.Size = new System.Drawing.Size(497, 303);
            this.tablePanel1.TabIndex = 0;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // panelControl3
            // 
            this.tablePanel1.SetColumn(this.panelControl3, 0);
            this.panelControl3.Controls.Add(this.tAmount);
            this.panelControl3.Controls.Add(this.dateEdit1);
            this.panelControl3.Controls.Add(this.labelControl5);
            this.panelControl3.Controls.Add(this.labelControl4);
            this.panelControl3.Controls.Add(this.labelControl3);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Controls.Add(this.tHeader);
            this.panelControl3.Controls.Add(this.tComment);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(1, 52);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl3.Name = "panelControl3";
            this.tablePanel1.SetRow(this.panelControl3, 1);
            this.panelControl3.Size = new System.Drawing.Size(495, 250);
            this.panelControl3.TabIndex = 1;
            // 
            // tAmount
            // 
            this.tAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tAmount.Location = new System.Drawing.Point(91, 48);
            this.tAmount.Name = "tAmount";
            this.tAmount.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.tAmount.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.tAmount.Properties.MaskSettings.Set("mask", "f");
            this.tAmount.Properties.NullText = "0";
            this.tAmount.Properties.UseMaskAsDisplayFormat = true;
            this.tAmount.Size = new System.Drawing.Size(395, 30);
            this.tAmount.TabIndex = 1;
            // 
            // dateEdit1
            // 
            this.dateEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(91, 213);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Size = new System.Drawing.Size(395, 30);
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
            this.labelControl2.Size = new System.Drawing.Size(42, 18);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Başlıq";
            // 
            // tHeader
            // 
            this.tHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tHeader.Location = new System.Drawing.Point(91, 12);
            this.tHeader.Name = "tHeader";
            this.tHeader.Properties.ReadOnly = true;
            this.tHeader.Size = new System.Drawing.Size(395, 30);
            this.tHeader.TabIndex = 0;
            // 
            // tComment
            // 
            this.tComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tComment.Location = new System.Drawing.Point(91, 86);
            this.tComment.Name = "tComment";
            this.tComment.Size = new System.Drawing.Size(395, 121);
            this.tComment.TabIndex = 2;
            // 
            // lHeader
            // 
            this.lHeader.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(6)))));
            this.lHeader.Appearance.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold);
            this.lHeader.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(144)))));
            this.lHeader.Appearance.Options.UseBackColor = true;
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
            this.lHeader.Text = "GƏLİR ƏLAVƏSİ";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.bSave);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 320);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(503, 51);
            this.panelControl1.TabIndex = 1;
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
            this.bSave.Text = "Yadda saxla";
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.tablePanel1);
            this.panelControl2.Location = new System.Drawing.Point(2, 5);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(1);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(501, 307);
            this.panelControl2.TabIndex = 2;
            // 
            // fAddIncomeAndExpenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(503, 371);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.Glow;
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(505, 405);
            this.MinimumSize = new System.Drawing.Size(505, 405);
            this.Name = "fAddIncomeAndExpenses";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "IncomeAndExpense";
            this.Text = "Mpos";
            this.Load += new System.EventHandler(this.fAddIncomeAndExpenses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tHeader.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tComment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.LabelControl lHeader;
        private DevExpress.XtraEditors.SimpleButton bSave;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit tHeader;
        private DevExpress.XtraEditors.MemoEdit tComment;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit tAmount;
    }
}