namespace WindowsFormsApp2.Forms
{
    partial class fPrinterSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fPrinterSettings));
            this.tFilePath = new DevExpress.XtraEditors.TextEdit();
            this.bSearch = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tFilePath.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tFilePath
            // 
            this.tFilePath.Location = new System.Drawing.Point(12, 12);
            this.tFilePath.Name = "tFilePath";
            this.tFilePath.Properties.NullValuePrompt = "TSPL Fayl yolu";
            this.tFilePath.Size = new System.Drawing.Size(398, 30);
            this.tFilePath.TabIndex = 0;
            // 
            // bSearch
            // 
            this.bSearch.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.bSearch.Appearance.Font = new System.Drawing.Font("Nunito", 11F);
            this.bSearch.Appearance.Options.UseBackColor = true;
            this.bSearch.Appearance.Options.UseFont = true;
            this.bSearch.Location = new System.Drawing.Point(416, 12);
            this.bSearch.Name = "bSearch";
            this.bSearch.Size = new System.Drawing.Size(64, 30);
            this.bSearch.TabIndex = 1;
            this.bSearch.Text = "Axtar";
            this.bSearch.Click += new System.EventHandler(this.bSearch_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Nunito", 11F);
            this.simpleButton2.Appearance.Options.UseBackColor = true;
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.AutoSize = true;
            this.simpleButton2.Location = new System.Drawing.Point(657, 483);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(91, 28);
            this.simpleButton2.TabIndex = 1;
            this.simpleButton2.Text = "Yadda saxla";
            // 
            // fPrinterSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(760, 523);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.bSearch);
            this.Controls.Add(this.tFilePath);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("fPrinterSettings.IconOptions.SvgImage")));
            this.Name = "fPrinterSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PRİNTER AYARLARI";
            ((System.ComponentModel.ISupportInitialize)(this.tFilePath.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit tFilePath;
        private DevExpress.XtraEditors.SimpleButton bSearch;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}