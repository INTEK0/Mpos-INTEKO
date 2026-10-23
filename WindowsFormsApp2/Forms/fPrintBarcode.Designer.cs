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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.chMehsulAlisi = new DevExpress.XtraEditors.CheckEdit();
            this.lookPrinters = new DevExpress.XtraEditors.LookUpEdit();
            this.lookPrintType = new DevExpress.XtraEditors.LookUpEdit();
            this.bRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.bPrint = new DevExpress.XtraEditors.SimpleButton();
            this.gridControlProducts = new DevExpress.XtraGrid.GridControl();
            this.gridProducts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSupplierId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplierName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coLSalePrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBarcode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEdv = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUnitName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrintCount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinCount = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colPrintButton = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bGridPrint = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chMehsulAlisi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookPrinters.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookPrintType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bGridPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
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
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 63F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.ShowGrid = DevExpress.Utils.DefaultBoolean.False;
            this.tablePanel1.Size = new System.Drawing.Size(1201, 768);
            this.tablePanel1.TabIndex = 0;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // panelControl2
            // 
            this.tablePanel1.SetColumn(this.panelControl2, 0);
            this.tablePanel1.SetColumnSpan(this.panelControl2, 2);
            this.panelControl2.Controls.Add(this.checkEdit1);
            this.panelControl2.Controls.Add(this.dateEdit1);
            this.panelControl2.Controls.Add(this.chMehsulAlisi);
            this.panelControl2.Controls.Add(this.lookPrinters);
            this.panelControl2.Controls.Add(this.lookPrintType);
            this.panelControl2.Controls.Add(this.bRefresh);
            this.panelControl2.Controls.Add(this.bPrint);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(1);
            this.panelControl2.Name = "panelControl2";
            this.tablePanel1.SetRow(this.panelControl2, 0);
            this.panelControl2.Size = new System.Drawing.Size(1197, 61);
            this.panelControl2.TabIndex = 3;
            // 
            // dateEdit1
            // 
            this.dateEdit1.EditValue = null;
            this.dateEdit1.Location = new System.Drawing.Point(436, 28);
            this.dateEdit1.Margin = new System.Windows.Forms.Padding(4);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateEdit1.Properties.Appearance.Font = new System.Drawing.Font("Verdana", 10F);
            this.dateEdit1.Properties.Appearance.Options.UseFont = true;
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Size = new System.Drawing.Size(172, 30);
            this.dateEdit1.TabIndex = 13;
            this.dateEdit1.Visible = false;
            this.dateEdit1.EditValueChanged += new System.EventHandler(this.dateEdit1_EditValueChanged);
            // 
            // chMehsulAlisi
            // 
            this.chMehsulAlisi.Location = new System.Drawing.Point(436, 3);
            this.chMehsulAlisi.Name = "chMehsulAlisi";
            this.chMehsulAlisi.Properties.AutoWidth = true;
            this.chMehsulAlisi.Properties.Caption = "Məhsul alışlarını göstər";
            this.chMehsulAlisi.Size = new System.Drawing.Size(176, 22);
            this.chMehsulAlisi.TabIndex = 11;
            this.chMehsulAlisi.CheckedChanged += new System.EventHandler(this.chMehsulAlisi_CheckedChanged);
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
            this.lookPrintType.Location = new System.Drawing.Point(218, 14);
            this.lookPrintType.Name = "lookPrintType";
            this.lookPrintType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookPrintType.Properties.DropDownRows = 4;
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
            this.bRefresh.Location = new System.Drawing.Point(902, 6);
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
            this.bPrint.Location = new System.Drawing.Point(1048, 6);
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
            this.gridControlProducts.Location = new System.Drawing.Point(4, 67);
            this.gridControlProducts.LookAndFeel.SkinName = "WXI";
            this.gridControlProducts.LookAndFeel.UseDefaultLookAndFeel = false;
            this.gridControlProducts.MainView = this.gridProducts;
            this.gridControlProducts.Name = "gridControlProducts";
            this.gridControlProducts.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.bGridPrint,
            this.spinCount});
            this.tablePanel1.SetRow(this.gridControlProducts, 1);
            this.gridControlProducts.Size = new System.Drawing.Size(1193, 697);
            this.gridControlProducts.TabIndex = 1;
            this.gridControlProducts.TabStop = false;
            this.gridControlProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridProducts});
            // 
            // gridProducts
            // 
            this.gridProducts.Appearance.EvenRow.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gridProducts.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridProducts.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridProducts.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridProducts.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gridProducts.Appearance.OddRow.Options.UseBackColor = true;
            this.gridProducts.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSupplierId,
            this.colSupplierName,
            this.colProductId,
            this.gridColumn2,
            this.colProductName,
            this.colProductCode,
            this.coLSalePrice,
            this.gridColumn1,
            this.colQuantity,
            this.colBarcode,
            this.colEdv,
            this.colUnitName,
            this.colPrintCount,
            this.colPrintButton});
            this.gridProducts.DetailHeight = 294;
            this.gridProducts.GridControl = this.gridControlProducts;
            this.gridProducts.Name = "gridProducts";
            this.gridProducts.OptionsBehavior.KeepFocusedRowOnUpdate = false;
            this.gridProducts.OptionsEditForm.PopupEditFormWidth = 1067;
            this.gridProducts.OptionsNavigation.AutoFocusNewRow = true;
            this.gridProducts.OptionsScrollAnnotations.ShowFocusedRow = DevExpress.Utils.DefaultBoolean.False;
            this.gridProducts.OptionsSelection.MultiSelect = true;
            this.gridProducts.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridProducts.OptionsView.AutoCalcPreviewLineCount = true;
            this.gridProducts.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.gridProducts.OptionsView.RowAutoHeight = true;
            this.gridProducts.OptionsView.ShowIndicator = false;
            this.gridProducts.InvalidValueException += new DevExpress.XtraEditors.Controls.InvalidValueExceptionEventHandler(this.gridProducts_InvalidValueException);
            // 
            // colSupplierId
            // 
            this.colSupplierId.Caption = "TECHIZATCI_ID";
            this.colSupplierId.FieldName = "SupplierId";
            this.colSupplierId.Name = "colSupplierId";
            this.colSupplierId.OptionsColumn.AllowEdit = false;
            this.colSupplierId.OptionsColumn.ReadOnly = true;
            // 
            // colSupplierName
            // 
            this.colSupplierName.Caption = "TƏCHİZATÇI";
            this.colSupplierName.FieldName = "SupplierName";
            this.colSupplierName.Name = "colSupplierName";
            this.colSupplierName.OptionsColumn.AllowEdit = false;
            this.colSupplierName.OptionsColumn.ReadOnly = true;
            this.colSupplierName.Visible = true;
            this.colSupplierName.VisibleIndex = 1;
            this.colSupplierName.Width = 147;
            // 
            // colProductId
            // 
            this.colProductId.Caption = "MAL_ALISI_DETAILS_ID";
            this.colProductId.FieldName = "ProductId";
            this.colProductId.Name = "colProductId";
            this.colProductId.OptionsColumn.AllowEdit = false;
            this.colProductId.OptionsColumn.ReadOnly = true;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "KATEQORİYA";
            this.gridColumn2.FieldName = "CategoryName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 206;
            // 
            // colProductName
            // 
            this.colProductName.Caption = "MƏHSUL ADI";
            this.colProductName.FieldName = "ProductName";
            this.colProductName.Name = "colProductName";
            this.colProductName.OptionsColumn.AllowEdit = false;
            this.colProductName.OptionsColumn.ReadOnly = true;
            this.colProductName.Visible = true;
            this.colProductName.VisibleIndex = 3;
            this.colProductName.Width = 454;
            // 
            // colProductCode
            // 
            this.colProductCode.Caption = "MƏHSUL KODU";
            this.colProductCode.FieldName = "ProductCode";
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
            this.coLSalePrice.FieldName = "SalePrice";
            this.coLSalePrice.Name = "coLSalePrice";
            this.coLSalePrice.OptionsColumn.AllowEdit = false;
            this.coLSalePrice.OptionsColumn.ReadOnly = true;
            this.coLSalePrice.Visible = true;
            this.coLSalePrice.VisibleIndex = 6;
            this.coLSalePrice.Width = 169;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ENDİRİMLİ SATIŞ QİYMƏTİ";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Width = 114;
            // 
            // colQuantity
            // 
            this.colQuantity.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuantity.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.colQuantity.Caption = "ALIŞ MİQDARI";
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.AllowEdit = false;
            this.colQuantity.OptionsColumn.ReadOnly = true;
            this.colQuantity.Width = 192;
            // 
            // colBarcode
            // 
            this.colBarcode.Caption = "MƏHSUL BARKOD";
            this.colBarcode.FieldName = "ProductBarcode";
            this.colBarcode.Name = "colBarcode";
            this.colBarcode.OptionsColumn.AllowEdit = false;
            this.colBarcode.OptionsColumn.ReadOnly = true;
            this.colBarcode.Visible = true;
            this.colBarcode.VisibleIndex = 4;
            this.colBarcode.Width = 158;
            // 
            // colEdv
            // 
            this.colEdv.Caption = "ƏDV";
            this.colEdv.FieldName = "TaxName";
            this.colEdv.Name = "colEdv";
            this.colEdv.OptionsColumn.AllowEdit = false;
            this.colEdv.OptionsColumn.ReadOnly = true;
            // 
            // colUnitName
            // 
            this.colUnitName.Caption = "VAHİD";
            this.colUnitName.FieldName = "UnitName";
            this.colUnitName.Name = "colUnitName";
            this.colUnitName.OptionsColumn.AllowEdit = false;
            this.colUnitName.OptionsColumn.FixedWidth = true;
            this.colUnitName.Visible = true;
            this.colUnitName.VisibleIndex = 5;
            this.colUnitName.Width = 101;
            // 
            // colPrintCount
            // 
            this.colPrintCount.AppearanceCell.Options.UseTextOptions = true;
            this.colPrintCount.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPrintCount.AppearanceHeader.Options.UseTextOptions = true;
            this.colPrintCount.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPrintCount.Caption = "ÇAP SAYI";
            this.colPrintCount.ColumnEdit = this.spinCount;
            this.colPrintCount.FieldName = "PrintCount";
            this.colPrintCount.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.colPrintCount.MaxWidth = 120;
            this.colPrintCount.MinWidth = 120;
            this.colPrintCount.Name = "colPrintCount";
            this.colPrintCount.Visible = true;
            this.colPrintCount.VisibleIndex = 7;
            this.colPrintCount.Width = 120;
            // 
            // spinCount
            // 
            this.spinCount.AutoHeight = false;
            this.spinCount.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinCount.IsFloatValue = false;
            this.spinCount.MaskSettings.Set("mask", "N00");
            this.spinCount.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.spinCount.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinCount.Name = "spinCount";
            // 
            // colPrintButton
            // 
            this.colPrintButton.Caption = "gridColumn10";
            this.colPrintButton.ColumnEdit = this.bGridPrint;
            this.colPrintButton.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.MiddleLeft;
            this.colPrintButton.MaxWidth = 100;
            this.colPrintButton.MinWidth = 100;
            this.colPrintButton.Name = "colPrintButton";
            this.colPrintButton.OptionsColumn.FixedWidth = true;
            this.colPrintButton.OptionsColumn.ShowCaption = false;
            this.colPrintButton.Width = 100;
            // 
            // bGridPrint
            // 
            this.bGridPrint.AutoHeight = false;
            this.bGridPrint.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "ÇAP ET", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.bGridPrint.Name = "bGridPrint";
            this.bGridPrint.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.bGridPrint.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bGridPrint_ButtonClick);
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(636, 3);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Appearance.Options.UseTextOptions = true;
            this.checkEdit1.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.checkEdit1.Properties.AutoHeight = false;
            this.checkEdit1.Properties.Caption = "Alış miqdarı ilə çap sayını eyniləşdir";
            this.checkEdit1.Size = new System.Drawing.Size(156, 55);
            this.checkEdit1.TabIndex = 14;
            this.checkEdit1.Visible = false;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // fPrintBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1201, 768);
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
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chMehsulAlisi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookPrinters.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookPrintType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bGridPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colBarcode;
        private DevExpress.XtraGrid.Columns.GridColumn colEdv;
        private DevExpress.XtraGrid.Columns.GridColumn colPrintButton;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit bGridPrint;
        private DevExpress.XtraEditors.LookUpEdit lookPrintType;
        private DevExpress.XtraEditors.LookUpEdit lookPrinters;
        private DevExpress.XtraGrid.Columns.GridColumn colPrintCount;
        private DevExpress.XtraGrid.Columns.GridColumn colUnitName;
        private DevExpress.XtraEditors.CheckEdit chMehsulAlisi;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinCount;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
    }
}