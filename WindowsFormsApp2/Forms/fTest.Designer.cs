namespace WindowsFormsApp2.Forms
{
    partial class fTest
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
            this.gridControlProducts = new DevExpress.XtraGrid.GridControl();
            this.gridProducts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlProducts
            // 
            this.gridControlProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlProducts.Location = new System.Drawing.Point(0, 0);
            this.gridControlProducts.LookAndFeel.SkinName = "WXI";
            this.gridControlProducts.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControlProducts.MainView = this.gridProducts;
            this.gridControlProducts.Name = "gridControlProducts";
            this.gridControlProducts.Size = new System.Drawing.Size(1397, 743);
            this.gridControlProducts.TabIndex = 1;
            this.gridControlProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridProducts});
            // 
            // gridProducts
            // 
            this.gridProducts.Appearance.EvenRow.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gridProducts.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridProducts.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gridProducts.Appearance.OddRow.Options.UseBackColor = true;
            this.gridProducts.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn17,
            this.gridColumn14,
            this.gridColumn16,
            this.gridColumn15,
            this.gridColumn18,
            this.gridColumn19});
            this.gridProducts.DetailHeight = 294;
            this.gridProducts.GridControl = this.gridControlProducts;
            this.gridProducts.Name = "gridProducts";
            this.gridProducts.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridProducts.OptionsEditForm.PopupEditFormWidth = 1067;
            this.gridProducts.OptionsNavigation.AutoFocusNewRow = true;
            this.gridProducts.OptionsScrollAnnotations.ShowFocusedRow = DevExpress.Utils.DefaultBoolean.False;
            this.gridProducts.OptionsView.EnableAppearanceEvenRow = true;
            this.gridProducts.OptionsView.ShowIndicator = false;
            this.gridProducts.OptionsView.ShowViewCaption = true;
            this.gridProducts.ViewCaption = "Anbar qalığı";
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "TECHIZATCI_ID";
            this.gridColumn9.FieldName = "TECHIZATCI_ID";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "TƏCHİZATÇI";
            this.gridColumn10.FieldName = "TƏCHİZATÇI";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            this.gridColumn10.Width = 114;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "MAL_ALISI_DETAILS_ID";
            this.gridColumn11.FieldName = "MAL_ALISI_DETAILS_ID";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "MƏHSUL ADI";
            this.gridColumn12.FieldName = "MƏHSUL ADI";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 1;
            this.gridColumn12.Width = 235;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "MƏHSUL KODU";
            this.gridColumn13.FieldName = "MƏHSUL KODU";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 2;
            this.gridColumn13.Width = 134;
            // 
            // gridColumn17
            // 
            this.gridColumn17.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn17.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn17.Caption = "ALIŞ QİYMƏTİ";
            this.gridColumn17.DisplayFormat.FormatString = "C2";
            this.gridColumn17.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn17.FieldName = "ALIŞ QİYMƏTİ";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn14.Caption = "SATIŞ QİYMƏTİ";
            this.gridColumn14.DisplayFormat.FormatString = "C2";
            this.gridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn14.FieldName = "SATIŞ QİYMƏTİ";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 3;
            this.gridColumn14.Width = 134;
            // 
            // gridColumn16
            // 
            this.gridColumn16.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn16.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn16.Caption = "BARKOD";
            this.gridColumn16.FieldName = "MƏHSUL BARKOD";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 5;
            this.gridColumn16.Width = 92;
            // 
            // gridColumn15
            // 
            this.gridColumn15.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn15.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn15.Caption = "ANBAR QALIĞI";
            this.gridColumn15.DisplayFormat.FormatString = "N3";
            this.gridColumn15.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn15.FieldName = "ANBAR QALIĞI";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 4;
            this.gridColumn15.Width = 144;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "EDV";
            this.gridColumn18.FieldName = "EDV";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "CacheDate";
            this.gridColumn19.FieldName = "CacheDate";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.OptionsColumn.AllowEdit = false;
            // 
            // fTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1397, 743);
            this.Controls.Add(this.gridControlProducts);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "fTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "fTest";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlProducts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridProducts;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
    }
}