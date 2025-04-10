namespace WindowsFormsApp2.Forms
{
    partial class fPrintBarcode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fPrintBarcode));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.lookPrinters = new DevExpress.XtraEditors.LookUpEdit();
            this.lookPrintType = new DevExpress.XtraEditors.LookUpEdit();
            this.bRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.bPrint = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlProducts = new DevExpress.XtraGrid.GridControl();
            this.gridProducts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSupplierId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coLSalePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBarcode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEdv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bPrintCount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colPrintButton = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bGridPrint = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookPrinters.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookPrintType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bPrintCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bGridPrint)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 9F)});
            this.tablePanel1.Controls.Add(this.panelControl2);
            this.tablePanel1.Controls.Add(this.gridControlProducts);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 57F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(955, 634);
            this.tablePanel1.TabIndex = 0;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // panelControl2
            // 
            this.tablePanel1.SetColumn(this.panelControl2, 0);
            this.tablePanel1.SetColumnSpan(this.panelControl2, 2);
            this.panelControl2.Controls.Add(this.lookPrinters);
            this.panelControl2.Controls.Add(this.lookPrintType);
            this.panelControl2.Controls.Add(this.bRefresh);
            this.panelControl2.Controls.Add(this.bPrint);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(1);
            this.panelControl2.Name = "panelControl2";
            this.tablePanel1.SetRow(this.panelControl2, 0);
            this.panelControl2.Size = new System.Drawing.Size(951, 55);
            this.panelControl2.TabIndex = 3;
            // 
            // lookPrinters
            // 
            this.lookPrinters.Location = new System.Drawing.Point(10, 14);
            this.lookPrinters.Name = "lookPrinters";
            this.lookPrinters.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookPrinters.Properties.DropDownRows = 3;
            this.lookPrinters.Properties.NullText = "PRİNTER SEÇİMİ";
            this.lookPrinters.Properties.ShowFooter = false;
            this.lookPrinters.Properties.ShowHeader = false;
            this.lookPrinters.Properties.ShowLines = false;
            this.lookPrinters.Size = new System.Drawing.Size(202, 30);
            this.lookPrinters.TabIndex = 2;
            // 
            // lookPrintType
            // 
            this.lookPrintType.Location = new System.Drawing.Point(228, 14);
            this.lookPrintType.Name = "lookPrintType";
            this.lookPrintType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookPrintType.Properties.DropDownRows = 3;
            this.lookPrintType.Properties.NullText = "ÇAP ÖLÇÜLƏRİ";
            this.lookPrintType.Properties.ShowFooter = false;
            this.lookPrintType.Properties.ShowHeader = false;
            this.lookPrintType.Properties.ShowLines = false;
            this.lookPrintType.Size = new System.Drawing.Size(202, 30);
            this.lookPrintType.TabIndex = 2;
            // 
            // bRefresh
            // 
            this.bRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bRefresh.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.bRefresh.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.bRefresh.Appearance.Options.UseBackColor = true;
            this.bRefresh.Appearance.Options.UseFont = true;
            this.bRefresh.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bRefresh.ImageOptions.SvgImage")));
            this.bRefresh.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.bRefresh.Location = new System.Drawing.Point(656, 6);
            this.bRefresh.Name = "bRefresh";
            this.bRefresh.Size = new System.Drawing.Size(138, 46);
            this.bRefresh.TabIndex = 0;
            this.bRefresh.Text = "YENİLƏ";
            this.bRefresh.Click += new System.EventHandler(this.bRefresh_Click);
            // 
            // bPrint
            // 
            this.bPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bPrint.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bPrint.Appearance.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold);
            this.bPrint.Appearance.Options.UseBackColor = true;
            this.bPrint.Appearance.Options.UseFont = true;
            this.bPrint.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bPrint.ImageOptions.SvgImage")));
            this.bPrint.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.bPrint.Location = new System.Drawing.Point(802, 6);
            this.bPrint.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.bPrint.Name = "bPrint";
            this.bPrint.Size = new System.Drawing.Size(145, 46);
            this.bPrint.TabIndex = 1;
            this.bPrint.Text = "ÇAP ET";
            this.bPrint.Click += new System.EventHandler(this.bPrint_Click);
            // 
            // gridControlProducts
            // 
            this.tablePanel1.SetColumn(this.gridControlProducts, 0);
            this.gridControlProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlProducts.Location = new System.Drawing.Point(4, 61);
            this.gridControlProducts.LookAndFeel.SkinName = "WXI";
            this.gridControlProducts.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControlProducts.MainView = this.gridProducts;
            this.gridControlProducts.Name = "gridControlProducts";
            this.gridControlProducts.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.bGridPrint,
            this.bPrintCount});
            this.tablePanel1.SetRow(this.gridControlProducts, 1);
            this.gridControlProducts.Size = new System.Drawing.Size(947, 569);
            this.gridControlProducts.TabIndex = 1;
            this.gridControlProducts.TabStop = false;
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
            this.colSupplierId,
            this.colSupplierName,
            this.colProductId,
            this.colProductName,
            this.colProductCode,
            this.coLSalePrice,
            this.colAmount,
            this.colBarcode,
            this.colEdv,
            this.gridColumn1,
            this.colPrintButton});
            this.gridProducts.DetailHeight = 294;
            this.gridProducts.GridControl = this.gridControlProducts;
            this.gridProducts.Name = "gridProducts";
            this.gridProducts.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridProducts.OptionsBehavior.ReadOnly = true;
            this.gridProducts.OptionsEditForm.PopupEditFormWidth = 1067;
            this.gridProducts.OptionsNavigation.AutoFocusNewRow = true;
            this.gridProducts.OptionsScrollAnnotations.ShowFocusedRow = DevExpress.Utils.DefaultBoolean.False;
            this.gridProducts.OptionsSelection.MultiSelect = true;
            this.gridProducts.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridProducts.OptionsView.ShowIndicator = false;
            // 
            // colSupplierId
            // 
            this.colSupplierId.Caption = "TECHIZATCI_ID";
            this.colSupplierId.FieldName = "TECHIZATCI_ID";
            this.colSupplierId.Name = "colSupplierId";
            this.colSupplierId.OptionsColumn.AllowEdit = false;
            this.colSupplierId.OptionsColumn.ReadOnly = true;
            // 
            // colSupplierName
            // 
            this.colSupplierName.Caption = "TƏCHİZATÇI";
            this.colSupplierName.FieldName = "TƏCHİZATÇI";
            this.colSupplierName.Name = "colSupplierName";
            this.colSupplierName.OptionsColumn.AllowEdit = false;
            this.colSupplierName.OptionsColumn.ReadOnly = true;
            // 
            // colProductId
            // 
            this.colProductId.Caption = "MAL_ALISI_DETAILS_ID";
            this.colProductId.FieldName = "MAL_ALISI_DETAILS_ID";
            this.colProductId.Name = "colProductId";
            this.colProductId.OptionsColumn.AllowEdit = false;
            this.colProductId.OptionsColumn.ReadOnly = true;
            // 
            // colProductName
            // 
            this.colProductName.Caption = "MƏHSUL ADI";
            this.colProductName.FieldName = "MƏHSUL ADI";
            this.colProductName.Name = "colProductName";
            this.colProductName.OptionsColumn.AllowEdit = false;
            this.colProductName.OptionsColumn.ReadOnly = true;
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 1;
            this.colProductName.Width = 363;
            // 
            // colProductCode
            // 
            this.colProductCode.Caption = "MƏHSUL KODU";
            this.colProductCode.FieldName = "MƏHSUL KODU";
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.OptionsColumn.AllowEdit = false;
            this.colProductCode.OptionsColumn.ReadOnly = true;
            // 
            // coLSalePrice
            // 
            this.coLSalePrice.AppearanceCell.Options.UseTextOptions = true;
            this.coLSalePrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.coLSalePrice.Caption = "SATIŞ QİYMƏTİ";
            this.coLSalePrice.DisplayFormat.FormatString = "C2";
            this.coLSalePrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.coLSalePrice.FieldName = "SATIŞ QİYMƏTİ";
            this.coLSalePrice.Name = "coLSalePrice";
            this.coLSalePrice.OptionsColumn.AllowEdit = false;
            this.coLSalePrice.OptionsColumn.ReadOnly = true;
            this.coLSalePrice.Visible = true;
            this.coLSalePrice.VisibleIndex = 3;
            this.coLSalePrice.Width = 233;
            // 
            // colAmount
            // 
            this.colAmount.Caption = "ANBAR QALIĞI";
            this.colAmount.FieldName = "ANBAR QALIĞI";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.OptionsColumn.ReadOnly = true;
            // 
            // colBarcode
            // 
            this.colBarcode.Caption = "MƏHSUL BARKOD";
            this.colBarcode.FieldName = "MƏHSUL BARKOD";
            this.colBarcode.Name = "colBarcode";
            this.colBarcode.OptionsColumn.AllowEdit = false;
            this.colBarcode.OptionsColumn.ReadOnly = true;
            this.colBarcode.Visible = true;
            this.colBarcode.VisibleIndex = 2;
            this.colBarcode.Width = 260;
            // 
            // colEdv
            // 
            this.colEdv.Caption = "ƏDV";
            this.colEdv.FieldName = "EDV";
            this.colEdv.Name = "colEdv";
            this.colEdv.OptionsColumn.AllowEdit = false;
            this.colEdv.OptionsColumn.ReadOnly = true;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.Caption = "ÇAP SAYI";
            this.gridColumn1.ColumnEdit = this.bPrintCount;
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.MiddleLeft;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Width = 139;
            // 
            // bPrintCount
            // 
            this.bPrintCount.AutoHeight = false;
            this.bPrintCount.Name = "bPrintCount";
            // 
            // colPrintButton
            // 
            this.colPrintButton.Caption = "gridColumn10";
            this.colPrintButton.ColumnEdit = this.bGridPrint;
            this.colPrintButton.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.MiddleLeft;
            this.colPrintButton.Name = "colPrintButton";
            this.colPrintButton.OptionsColumn.ShowCaption = false;
            this.colPrintButton.Visible = true;
            this.colPrintButton.VisibleIndex = 4;
            this.colPrintButton.Width = 133;
            // 
            // bGridPrint
            // 
            this.bGridPrint.AutoHeight = false;
            this.bGridPrint.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "ÇAP ET", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.bGridPrint.Name = "bGridPrint";
            this.bGridPrint.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.bGridPrint.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bGridPrint_ButtonClick);
            // 
            // fPrintBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(955, 634);
            this.Controls.Add(this.tablePanel1);
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.Name = "fPrintBarcode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BARKOD ÇAP";
            this.Load += new System.EventHandler(this.fPrintBarcode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookPrinters.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookPrintType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bPrintCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bGridPrint)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraGrid.GridControl gridControlProducts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridProducts;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton bPrint;
        private DevExpress.XtraEditors.SimpleButton bRefresh;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierId;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplierName;
        private DevExpress.XtraGrid.Columns.GridColumn colProductId;
        private DevExpress.XtraGrid.Columns.GridColumn colProductName;
        private DevExpress.XtraGrid.Columns.GridColumn colProductCode;
        private DevExpress.XtraGrid.Columns.GridColumn coLSalePrice;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colBarcode;
        private DevExpress.XtraGrid.Columns.GridColumn colEdv;
        private DevExpress.XtraGrid.Columns.GridColumn colPrintButton;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit bGridPrint;
        private DevExpress.XtraEditors.LookUpEdit lookPrintType;
        private DevExpress.XtraEditors.LookUpEdit lookPrinters;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit bPrintCount;
    }
}