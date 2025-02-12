
namespace WindowsFormsApp2
{
    partial class prenagdkart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(prenagdkart));
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.tCash = new DevExpress.XtraEditors.TextEdit();
            this.tCard = new DevExpress.XtraEditors.TextEdit();
            this.tTotal = new DevExpress.XtraEditors.TextEdit();
            this.tQaliq = new DevExpress.XtraEditors.TextEdit();
            this.tOdenilenNagd = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.bEnter = new DevExpress.XtraEditors.SimpleButton();
            this.chClinicModul = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tCash.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCard.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tTotal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tQaliq.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tOdenilenNagd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chClinicModul.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(51, 90);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(46, 19);
            this.labelControl2.TabIndex = 15;
            this.labelControl2.Text = "KART";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(51, 150);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 19);
            this.labelControl1.TabIndex = 16;
            this.labelControl1.Text = "NAĞD";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(40, 30);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(70, 19);
            this.labelControl4.TabIndex = 16;
            this.labelControl4.Text = "MƏBLƏĞ";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(50, 270);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(50, 18);
            this.labelControl3.TabIndex = 14;
            this.labelControl3.Text = "QALIQ";
            // 
            // tCash
            // 
            this.tCash.Location = new System.Drawing.Point(124, 132);
            this.tCash.Name = "tCash";
            this.tCash.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F);
            this.tCash.Properties.Appearance.Options.UseFont = true;
            this.tCash.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.tCash.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.tCash.Properties.MaskSettings.Set("mask", "f");
            this.tCash.Properties.NullText = "0";
            this.tCash.Properties.UseMaskAsDisplayFormat = true;
            this.tCash.Size = new System.Drawing.Size(139, 54);
            this.tCash.TabIndex = 1;
            this.tCash.EditValueChanged += new System.EventHandler(this.tCash_EditValueChanged);
            this.tCash.TextChanged += new System.EventHandler(this.textEdit4_TextChanged);
            this.tCash.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEdit4_KeyDown);
            this.tCash.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textEdit4_KeyPress);
            // 
            // tCard
            // 
            this.tCard.Location = new System.Drawing.Point(124, 72);
            this.tCard.Name = "tCard";
            this.tCard.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F);
            this.tCard.Properties.Appearance.Options.UseFont = true;
            this.tCard.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.tCard.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.tCard.Properties.MaskSettings.Set("mask", "f");
            this.tCard.Properties.NullText = "0";
            this.tCard.Properties.UseMaskAsDisplayFormat = true;
            this.tCard.Size = new System.Drawing.Size(139, 54);
            this.tCard.TabIndex = 0;
            this.tCard.EditValueChanged += new System.EventHandler(this.tCard_EditValueChanged);
            // 
            // tTotal
            // 
            this.tTotal.Location = new System.Drawing.Point(124, 12);
            this.tTotal.Name = "tTotal";
            this.tTotal.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F);
            this.tTotal.Properties.Appearance.ForeColor = System.Drawing.Color.Red;
            this.tTotal.Properties.Appearance.Options.UseFont = true;
            this.tTotal.Properties.Appearance.Options.UseForeColor = true;
            this.tTotal.Properties.NullText = "0";
            this.tTotal.Properties.ReadOnly = true;
            this.tTotal.Size = new System.Drawing.Size(139, 54);
            this.tTotal.TabIndex = 13;
            // 
            // tQaliq
            // 
            this.tQaliq.Location = new System.Drawing.Point(124, 252);
            this.tQaliq.Name = "tQaliq";
            this.tQaliq.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F);
            this.tQaliq.Properties.Appearance.Options.UseFont = true;
            this.tQaliq.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.tQaliq.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.tQaliq.Properties.MaskSettings.Set("mask", "f");
            this.tQaliq.Properties.NullText = "0";
            this.tQaliq.Properties.ReadOnly = true;
            this.tQaliq.Properties.UseMaskAsDisplayFormat = true;
            this.tQaliq.Size = new System.Drawing.Size(139, 54);
            this.tQaliq.TabIndex = 11;
            this.tQaliq.TextChanged += new System.EventHandler(this.textEdit4_TextChanged);
            this.tQaliq.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEdit4_KeyDown);
            this.tQaliq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textEdit4_KeyPress);
            // 
            // tOdenilenNagd
            // 
            this.tOdenilenNagd.Location = new System.Drawing.Point(124, 192);
            this.tOdenilenNagd.Name = "tOdenilenNagd";
            this.tOdenilenNagd.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F);
            this.tOdenilenNagd.Properties.Appearance.Options.UseFont = true;
            this.tOdenilenNagd.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.tOdenilenNagd.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.tOdenilenNagd.Properties.MaskSettings.Set("mask", "f");
            this.tOdenilenNagd.Properties.NullText = "0";
            this.tOdenilenNagd.Properties.UseMaskAsDisplayFormat = true;
            this.tOdenilenNagd.Size = new System.Drawing.Size(139, 54);
            this.tOdenilenNagd.TabIndex = 2;
            this.tOdenilenNagd.EditValueChanged += new System.EventHandler(this.tOdenilenNagd_EditValueChanged);
            this.tOdenilenNagd.TextChanged += new System.EventHandler(this.textEdit4_TextChanged);
            this.tOdenilenNagd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEdit4_KeyDown);
            this.tOdenilenNagd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textEdit4_KeyPress);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseTextOptions = true;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl5.Location = new System.Drawing.Point(28, 200);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(88, 38);
            this.labelControl5.TabIndex = 16;
            this.labelControl5.Text = "ÖDƏNİLƏN\r\nNAĞD";
            // 
            // bEnter
            // 
            this.bEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bEnter.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bEnter.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bEnter.Appearance.Options.UseBackColor = true;
            this.bEnter.Appearance.Options.UseFont = true;
            this.bEnter.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bEnter.ImageOptions.SvgImage")));
            this.bEnter.Location = new System.Drawing.Point(269, 12);
            this.bEnter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bEnter.Name = "bEnter";
            this.bEnter.Size = new System.Drawing.Size(147, 294);
            this.bEnter.TabIndex = 22;
            this.bEnter.Text = "ÖDƏNİŞ ET";
            this.bEnter.Click += new System.EventHandler(this.bEnter_Click);
            // 
            // chClinicModul
            // 
            this.chClinicModul.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chClinicModul.Location = new System.Drawing.Point(12, 312);
            this.chClinicModul.Name = "chClinicModul";
            this.chClinicModul.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 11F);
            this.chClinicModul.Properties.Appearance.Options.UseFont = true;
            this.chClinicModul.Properties.Caption = "(A4 Sənədi çap et)";
            this.chClinicModul.Properties.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgToggle1;
            this.chClinicModul.Properties.CheckBoxOptions.SvgImageSize = new System.Drawing.Size(32, 28);
            this.chClinicModul.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chClinicModul.Size = new System.Drawing.Size(251, 32);
            this.chClinicModul.TabIndex = 25;
            // 
            // prenagdkart
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(428, 316);
            this.Controls.Add(this.chClinicModul);
            this.Controls.Add(this.bEnter);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.tQaliq);
            this.Controls.Add(this.tOdenilenNagd);
            this.Controls.Add(this.tCash);
            this.Controls.Add(this.tCard);
            this.Controls.Add(this.tTotal);
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(430, 350);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(430, 350);
            this.Name = "prenagdkart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NAĞD KART";
            this.Load += new System.EventHandler(this.nagd_kart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tCash.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCard.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tTotal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tQaliq.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tOdenilenNagd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chClinicModul.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit tCard;
        private DevExpress.XtraEditors.TextEdit tTotal;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit tCash;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit tQaliq;
        private DevExpress.XtraEditors.TextEdit tOdenilenNagd;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton bEnter;
        private DevExpress.XtraEditors.CheckEdit chClinicModul;
    }
}