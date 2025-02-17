namespace WindowsFormsApp2.Forms
{
    partial class fCardAndOtherPay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fCardAndOtherPay));
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.bCard = new DevExpress.XtraEditors.SimpleButton();
            this.bOtherPay = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F)});
            this.tablePanel1.Controls.Add(this.bCard);
            this.tablePanel1.Controls.Add(this.bOtherPay);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(458, 116);
            this.tablePanel1.TabIndex = 0;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // bCard
            // 
            this.bCard.AllowFocus = false;
            this.bCard.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.bCard.Appearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bCard.Appearance.Options.UseBackColor = true;
            this.bCard.Appearance.Options.UseFont = true;
            this.bCard.Appearance.Options.UseTextOptions = true;
            this.bCard.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bCard.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.tablePanel1.SetColumn(this.bCard, 0);
            this.bCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bCard.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bCard.ImageOptions.SvgImage")));
            this.bCard.Location = new System.Drawing.Point(4, 3);
            this.bCard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bCard.Name = "bCard";
            this.tablePanel1.SetRow(this.bCard, 0);
            this.bCard.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bCard.Size = new System.Drawing.Size(222, 110);
            this.bCard.TabIndex = 23;
            this.bCard.Text = "NAĞDSIZ ÖDƏNİŞ";
            this.bCard.Click += new System.EventHandler(this.bCard_Click);
            // 
            // bOtherPay
            // 
            this.bOtherPay.AllowFocus = false;
            this.bOtherPay.Appearance.BackColor = System.Drawing.Color.Gray;
            this.bOtherPay.Appearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bOtherPay.Appearance.Options.UseBackColor = true;
            this.bOtherPay.Appearance.Options.UseFont = true;
            this.bOtherPay.Appearance.Options.UseTextOptions = true;
            this.bOtherPay.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bOtherPay.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.tablePanel1.SetColumn(this.bOtherPay, 1);
            this.bOtherPay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bOtherPay.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bOtherPay.ImageOptions.SvgImage")));
            this.bOtherPay.Location = new System.Drawing.Point(232, 3);
            this.bOtherPay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bOtherPay.Name = "bOtherPay";
            this.tablePanel1.SetRow(this.bOtherPay, 0);
            this.bOtherPay.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bOtherPay.Size = new System.Drawing.Size(222, 110);
            this.bOtherPay.TabIndex = 23;
            this.bOtherPay.Text = "DİGƏR \r\n(Online ödənişlər)";
            this.bOtherPay.Click += new System.EventHandler(this.bOtherPay_Click);
            // 
            // fCardAndOtherPay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(458, 116);
            this.Controls.Add(this.tablePanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximumSize = new System.Drawing.Size(460, 150);
            this.MinimumSize = new System.Drawing.Size(460, 150);
            this.Name = "fCardAndOtherPay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mpos";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fCardAndOtherPay_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.SimpleButton bCard;
        private DevExpress.XtraEditors.SimpleButton bOtherPay;
    }
}