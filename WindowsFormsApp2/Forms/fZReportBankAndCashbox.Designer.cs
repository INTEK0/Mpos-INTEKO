namespace WindowsFormsApp2.Forms
{
    partial class fZReportBankAndCashbox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fZReportBankAndCashbox));
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.bTerminal = new DevExpress.XtraEditors.SimpleButton();
            this.bCashbox = new DevExpress.XtraEditors.SimpleButton();
            this.bTerminalAndCashbox = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F)});
            this.tablePanel1.Controls.Add(this.bTerminalAndCashbox);
            this.tablePanel1.Controls.Add(this.bTerminal);
            this.tablePanel1.Controls.Add(this.bCashbox);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 90F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 100F)});
            this.tablePanel1.Size = new System.Drawing.Size(448, 166);
            this.tablePanel1.TabIndex = 1;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // bTerminal
            // 
            this.bTerminal.AllowFocus = false;
            this.bTerminal.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.bTerminal.Appearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bTerminal.Appearance.Options.UseBackColor = true;
            this.bTerminal.Appearance.Options.UseFont = true;
            this.bTerminal.Appearance.Options.UseTextOptions = true;
            this.bTerminal.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bTerminal.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.tablePanel1.SetColumn(this.bTerminal, 0);
            this.bTerminal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bTerminal.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bCard.ImageOptions.SvgImage")));
            this.bTerminal.Location = new System.Drawing.Point(4, 3);
            this.bTerminal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bTerminal.Name = "bTerminal";
            this.tablePanel1.SetRow(this.bTerminal, 0);
            this.bTerminal.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bTerminal.Size = new System.Drawing.Size(217, 86);
            this.bTerminal.TabIndex = 23;
            this.bTerminal.Text = "POS TERMİNAL";
            // 
            // bCashbox
            // 
            this.bCashbox.AllowFocus = false;
            this.bCashbox.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.bCashbox.Appearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bCashbox.Appearance.Options.UseBackColor = true;
            this.bCashbox.Appearance.Options.UseFont = true;
            this.bCashbox.Appearance.Options.UseTextOptions = true;
            this.bCashbox.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bCashbox.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.tablePanel1.SetColumn(this.bCashbox, 1);
            this.bCashbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bCashbox.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bOtherPay.ImageOptions.SvgImage")));
            this.bCashbox.Location = new System.Drawing.Point(227, 3);
            this.bCashbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bCashbox.Name = "bCashbox";
            this.tablePanel1.SetRow(this.bCashbox, 0);
            this.bCashbox.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bCashbox.Size = new System.Drawing.Size(217, 86);
            this.bCashbox.TabIndex = 23;
            this.bCashbox.Text = "KASSA";
            // 
            // bTerminalAndCashbox
            // 
            this.bTerminalAndCashbox.AllowFocus = false;
            this.bTerminalAndCashbox.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bTerminalAndCashbox.Appearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bTerminalAndCashbox.Appearance.Options.UseBackColor = true;
            this.bTerminalAndCashbox.Appearance.Options.UseFont = true;
            this.bTerminalAndCashbox.Appearance.Options.UseTextOptions = true;
            this.bTerminalAndCashbox.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bTerminalAndCashbox.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.tablePanel1.SetColumn(this.bTerminalAndCashbox, 0);
            this.tablePanel1.SetColumnSpan(this.bTerminalAndCashbox, 3);
            this.bTerminalAndCashbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bTerminalAndCashbox.Location = new System.Drawing.Point(4, 93);
            this.bTerminalAndCashbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bTerminalAndCashbox.Name = "bTerminalAndCashbox";
            this.tablePanel1.SetRow(this.bTerminalAndCashbox, 1);
            this.bTerminalAndCashbox.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bTerminalAndCashbox.Size = new System.Drawing.Size(440, 70);
            this.bTerminalAndCashbox.TabIndex = 24;
            this.bTerminalAndCashbox.Text = "POSTERMİNAL && KASSA";
            // 
            // fZReportBankAndCashbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(448, 166);
            this.Controls.Add(this.tablePanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fZReportBankAndCashbox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gün sonu hesabat (Z Report)";
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.SimpleButton bTerminal;
        private DevExpress.XtraEditors.SimpleButton bCashbox;
        private DevExpress.XtraEditors.SimpleButton bTerminalAndCashbox;
    }
}