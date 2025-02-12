
namespace WindowsFormsApp2
{
    partial class bank
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(bank));
            this.tPaid = new DevExpress.XtraEditors.TextEdit();
            this.tQaliq = new DevExpress.XtraEditors.TextEdit();
            this.tTotal = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.bEnter = new DevExpress.XtraEditors.SimpleButton();
            this.chClinicModul = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tPaid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tQaliq.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chClinicModul.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tPaid
            // 
            this.tPaid.Location = new System.Drawing.Point(133, 75);
            this.tPaid.Margin = new System.Windows.Forms.Padding(4);
            this.tPaid.Name = "tPaid";
            this.tPaid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F);
            this.tPaid.Properties.Appearance.Options.UseFont = true;
            this.tPaid.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.tPaid.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.tPaid.Properties.MaskSettings.Set("mask", "f");
            this.tPaid.Properties.NullText = "0";
            this.tPaid.Properties.UseMaskAsDisplayFormat = true;
            this.tPaid.Size = new System.Drawing.Size(176, 54);
            this.tPaid.TabIndex = 1;
            this.tPaid.EditValueChanged += new System.EventHandler(this.textEdit4_TextChanged);
            this.tPaid.TextChanged += new System.EventHandler(this.textEdit4_TextChanged);
            this.tPaid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEdit4_KeyDown);
            this.tPaid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textEdit4_KeyPress);
            // 
            // tQaliq
            // 
            this.tQaliq.Location = new System.Drawing.Point(133, 137);
            this.tQaliq.Margin = new System.Windows.Forms.Padding(4);
            this.tQaliq.Name = "tQaliq";
            this.tQaliq.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F);
            this.tQaliq.Properties.Appearance.Options.UseFont = true;
            this.tQaliq.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.tQaliq.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.tQaliq.Properties.MaskSettings.Set("mask", "f");
            this.tQaliq.Properties.NullText = "0";
            this.tQaliq.Properties.ReadOnly = true;
            this.tQaliq.Properties.UseMaskAsDisplayFormat = true;
            this.tQaliq.Size = new System.Drawing.Size(176, 54);
            this.tQaliq.TabIndex = 2;
            // 
            // tTotal
            // 
            this.tTotal.Location = new System.Drawing.Point(132, 13);
            this.tTotal.Margin = new System.Windows.Forms.Padding(4);
            this.tTotal.Name = "tTotal";
            this.tTotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F);
            this.tTotal.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.tTotal.Properties.Appearance.Options.UseFont = true;
            this.tTotal.Properties.Appearance.Options.UseForeColor = true;
            this.tTotal.Properties.NullText = "0";
            this.tTotal.Size = new System.Drawing.Size(176, 54);
            this.tTotal.TabIndex = 3;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(13, 27);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(84, 26);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "MƏBLƏĞ";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(13, 89);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(103, 26);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "ÖDƏNİLƏN";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(13, 151);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 26);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "QALIQ";
            // 
            // bEnter
            // 
            this.bEnter.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bEnter.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bEnter.Appearance.Options.UseBackColor = true;
            this.bEnter.Appearance.Options.UseFont = true;
            this.bEnter.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bEnter.ImageOptions.SvgImage")));
            this.bEnter.Location = new System.Drawing.Point(316, 13);
            this.bEnter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bEnter.Name = "bEnter";
            this.bEnter.Size = new System.Drawing.Size(135, 178);
            this.bEnter.TabIndex = 23;
            this.bEnter.Text = "ÖDƏNİŞ ET";
            this.bEnter.Click += new System.EventHandler(this.bEnter_Click);
            // 
            // chClinicModul
            // 
            this.chClinicModul.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chClinicModul.Location = new System.Drawing.Point(11, 198);
            this.chClinicModul.Name = "chClinicModul";
            this.chClinicModul.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 11F);
            this.chClinicModul.Properties.Appearance.Options.UseFont = true;
            this.chClinicModul.Properties.Caption = "(A4 Sənədi çap et)";
            this.chClinicModul.Properties.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgToggle1;
            this.chClinicModul.Properties.CheckBoxOptions.SvgImageSize = new System.Drawing.Size(32, 28);
            this.chClinicModul.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chClinicModul.Size = new System.Drawing.Size(298, 32);
            this.chClinicModul.TabIndex = 24;
            // 
            // bank
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(458, 196);
            this.Controls.Add(this.chClinicModul);
            this.Controls.Add(this.bEnter);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.tPaid);
            this.Controls.Add(this.tQaliq);
            this.Controls.Add(this.tTotal);
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(460, 230);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(460, 230);
            this.Name = "bank";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NAĞD ÖDƏNİŞ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.nagd_FormClosing);
            this.Load += new System.EventHandler(this.nagd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tPaid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tQaliq.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chClinicModul.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit tPaid;
        private DevExpress.XtraEditors.TextEdit tQaliq;
        private DevExpress.XtraEditors.TextEdit tTotal;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.SimpleButton bEnter;
        private DevExpress.XtraEditors.CheckEdit chClinicModul;
    }
}