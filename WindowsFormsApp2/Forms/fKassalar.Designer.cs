namespace WindowsFormsApp2.Forms
{
    partial class fKassalar
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tMerchantId = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lookUser = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lookKassa = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.tIpAddress = new DevExpress.XtraEditors.TextEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.bDelete = new DevExpress.XtraEditors.SimpleButton();
            this.bPing = new DevExpress.XtraEditors.SimpleButton();
            this.bAdd = new DevExpress.XtraEditors.SimpleButton();
            this.lookBank = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tMerchantId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKassa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tIpAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookBank.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.groupControl1.Controls.Add(this.lookBank);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.tMerchantId);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.lookUser);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.lookKassa);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.tIpAddress);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.LookAndFeel.SkinName = "WXI";
            this.groupControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl1.MaximumSize = new System.Drawing.Size(0, 170);
            this.groupControl1.MinimumSize = new System.Drawing.Size(0, 130);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(881, 130);
            this.groupControl1.TabIndex = 16;
            this.groupControl1.Text = "layoutControlGroup1";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(27, 134);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(91, 16);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "MERCHANT ID";
            this.labelControl1.Visible = false;
            // 
            // tMerchantId
            // 
            this.tMerchantId.Location = new System.Drawing.Point(119, 127);
            this.tMerchantId.Margin = new System.Windows.Forms.Padding(4);
            this.tMerchantId.Name = "tMerchantId";
            this.tMerchantId.Size = new System.Drawing.Size(387, 30);
            this.tMerchantId.TabIndex = 9;
            this.tMerchantId.Visible = false;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(27, 96);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(83, 16);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "İSTİFADƏÇİ ";
            // 
            // lookUser
            // 
            this.lookUser.Location = new System.Drawing.Point(119, 89);
            this.lookUser.Margin = new System.Windows.Forms.Padding(4);
            this.lookUser.Name = "lookUser";
            this.lookUser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUser.Properties.NullText = "--Seçin--";
            this.lookUser.Size = new System.Drawing.Size(387, 30);
            this.lookUser.TabIndex = 6;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(27, 20);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(59, 16);
            this.labelControl3.TabIndex = 11;
            this.labelControl3.Text = "E-KASSA";
            // 
            // lookKassa
            // 
            this.lookKassa.Location = new System.Drawing.Point(119, 13);
            this.lookKassa.Margin = new System.Windows.Forms.Padding(4);
            this.lookKassa.Name = "lookKassa";
            this.lookKassa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookKassa.Properties.NullText = "--Seçin--";
            this.lookKassa.Size = new System.Drawing.Size(387, 30);
            this.lookKassa.TabIndex = 6;
            this.lookKassa.TextChanged += new System.EventHandler(this.lookKassa_TextChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(27, 58);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(62, 16);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "KASSA İP";
            // 
            // tIpAddress
            // 
            this.tIpAddress.Location = new System.Drawing.Point(119, 51);
            this.tIpAddress.Margin = new System.Windows.Forms.Padding(4);
            this.tIpAddress.Name = "tIpAddress";
            this.tIpAddress.Size = new System.Drawing.Size(387, 30);
            this.tIpAddress.TabIndex = 3;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Location = new System.Drawing.Point(5, 192);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1075, 504);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.EvenRow.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gridView1.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridView1.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.OddRow.Options.UseBackColor = true;
            this.gridView1.DetailHeight = 431;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsEditForm.PopupEditFormWidth = 1067;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridView1_RowCellClick);
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.Controls.Add(this.bDelete);
            this.groupControl2.Controls.Add(this.bPing);
            this.groupControl2.Controls.Add(this.bAdd);
            this.groupControl2.Location = new System.Drawing.Point(0, 134);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(1087, 52);
            this.groupControl2.TabIndex = 17;
            this.groupControl2.Text = "groupControl2";
            // 
            // bDelete
            // 
            this.bDelete.AllowFocus = false;
            this.bDelete.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.bDelete.Appearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Bold);
            this.bDelete.Appearance.Options.UseBackColor = true;
            this.bDelete.Appearance.Options.UseFont = true;
            this.bDelete.Location = new System.Drawing.Point(180, 5);
            this.bDelete.Name = "bDelete";
            this.bDelete.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bDelete.Size = new System.Drawing.Size(162, 40);
            this.bDelete.TabIndex = 63;
            this.bDelete.Text = "SİL";
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // bPing
            // 
            this.bPing.AllowFocus = false;
            this.bPing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bPing.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.bPing.Appearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Bold);
            this.bPing.Appearance.Options.UseBackColor = true;
            this.bPing.Appearance.Options.UseFont = true;
            this.bPing.Location = new System.Drawing.Point(705, 5);
            this.bPing.Name = "bPing";
            this.bPing.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bPing.Size = new System.Drawing.Size(170, 40);
            this.bPing.TabIndex = 63;
            this.bPing.Text = "ƏLAQƏNİ YOXLA";
            this.bPing.Click += new System.EventHandler(this.bPing_Click);
            // 
            // bAdd
            // 
            this.bAdd.AllowFocus = false;
            this.bAdd.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bAdd.Appearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Bold);
            this.bAdd.Appearance.Options.UseBackColor = true;
            this.bAdd.Appearance.Options.UseFont = true;
            this.bAdd.Location = new System.Drawing.Point(12, 6);
            this.bAdd.Name = "bAdd";
            this.bAdd.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bAdd.Size = new System.Drawing.Size(162, 40);
            this.bAdd.TabIndex = 63;
            this.bAdd.Text = "ƏLAVƏ ET";
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // lookBank
            // 
            this.lookBank.Location = new System.Drawing.Point(525, 13);
            this.lookBank.Name = "lookBank";
            this.lookBank.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookBank.Properties.DropDownRows = 4;
            this.lookBank.Properties.NullText = "--Bank seçimi--";
            this.lookBank.Properties.ShowFooter = false;
            this.lookBank.Properties.ShowHeader = false;
            this.lookBank.Properties.ShowLines = false;
            this.lookBank.Size = new System.Drawing.Size(350, 30);
            this.lookBank.TabIndex = 13;
            // 
            // fKassalar
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(881, 699);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(883, 733);
            this.MinimumSize = new System.Drawing.Size(883, 733);
            this.Name = "fKassalar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kassalar";
            this.Load += new System.EventHandler(this.fKassalar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tMerchantId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookKassa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tIpAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookBank.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit tMerchantId;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit lookUser;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lookKassa;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit tIpAddress;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton bDelete;
        private DevExpress.XtraEditors.SimpleButton bAdd;
        private DevExpress.XtraEditors.SimpleButton bPing;
        private DevExpress.XtraEditors.LookUpEdit lookBank;
    }
}