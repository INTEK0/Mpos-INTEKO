namespace WindowsFormsApp2.Forms
{
    partial class fQuickAddProduct
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.bAdd = new DevExpress.XtraEditors.SimpleButton();
            this.bSelectedProducts = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.dateTarix = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.tInvoiceNo = new DevExpress.XtraEditors.TextEdit();
            this.tBarcode = new DevExpress.XtraEditors.TextEdit();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bDeleteRow = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.bDelete = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTarix.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTarix.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tInvoiceNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBarcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDeleteRow)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 55F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.tablePanel1.Controls.Add(this.bAdd);
            this.tablePanel1.Controls.Add(this.bSelectedProducts);
            this.tablePanel1.Controls.Add(this.panelControl1);
            this.tablePanel1.Controls.Add(this.gridControl1);
            this.tablePanel1.Controls.Add(this.bDelete);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 120F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(1235, 745);
            this.tablePanel1.TabIndex = 0;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // bAdd
            // 
            this.bAdd.AllowFocus = false;
            this.bAdd.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bAdd.Appearance.Options.UseBackColor = true;
            this.bAdd.AutoSize = true;
            this.tablePanel1.SetColumn(this.bAdd, 0);
            this.bAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bAdd.Location = new System.Drawing.Point(3, 123);
            this.bAdd.Margin = new System.Windows.Forms.Padding(2);
            this.bAdd.Name = "bAdd";
            this.tablePanel1.SetRow(this.bAdd, 1);
            this.bAdd.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bAdd.Size = new System.Drawing.Size(88, 28);
            this.bAdd.TabIndex = 4;
            this.bAdd.TabStop = false;
            this.bAdd.Text = "Yadda saxla";
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bSelectedProducts
            // 
            this.bSelectedProducts.AllowFocus = false;
            this.bSelectedProducts.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.bSelectedProducts.Appearance.Options.UseBackColor = true;
            this.bSelectedProducts.AutoSize = true;
            this.tablePanel1.SetColumn(this.bSelectedProducts, 1);
            this.bSelectedProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bSelectedProducts.Location = new System.Drawing.Point(95, 123);
            this.bSelectedProducts.Margin = new System.Windows.Forms.Padding(2);
            this.bSelectedProducts.Name = "bSelectedProducts";
            this.tablePanel1.SetRow(this.bSelectedProducts, 1);
            this.bSelectedProducts.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bSelectedProducts.Size = new System.Drawing.Size(154, 28);
            this.bSelectedProducts.TabIndex = 4;
            this.bSelectedProducts.TabStop = false;
            this.bSelectedProducts.Text = "Toplu məhsul əlavə et";
            this.bSelectedProducts.Click += new System.EventHandler(this.bSelectedProducts_Click);
            // 
            // panelControl1
            // 
            this.tablePanel1.SetColumn(this.panelControl1, 0);
            this.tablePanel1.SetColumnSpan(this.panelControl1, 4);
            this.panelControl1.Controls.Add(this.dateTarix);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.tInvoiceNo);
            this.panelControl1.Controls.Add(this.tBarcode);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(4, 4);
            this.panelControl1.Name = "panelControl1";
            this.tablePanel1.SetRow(this.panelControl1, 0);
            this.panelControl1.Size = new System.Drawing.Size(1227, 114);
            this.panelControl1.TabIndex = 0;
            this.panelControl1.Click += new System.EventHandler(this.panelControl1_Click);
            // 
            // dateTarix
            // 
            this.dateTarix.EditValue = null;
            this.dateTarix.Location = new System.Drawing.Point(106, 5);
            this.dateTarix.Margin = new System.Windows.Forms.Padding(4);
            this.dateTarix.Name = "dateTarix";
            this.dateTarix.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTarix.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateTarix.Size = new System.Drawing.Size(246, 30);
            this.dateTarix.TabIndex = 0;
            this.dateTarix.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(8, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 16);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "TARİX";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(8, 84);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(44, 16);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Barkod";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(8, 48);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(80, 16);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "FAKTURA №";
            // 
            // tInvoiceNo
            // 
            this.tInvoiceNo.Location = new System.Drawing.Point(106, 41);
            this.tInvoiceNo.Name = "tInvoiceNo";
            this.tInvoiceNo.Size = new System.Drawing.Size(246, 30);
            this.tInvoiceNo.TabIndex = 1;
            this.tInvoiceNo.TabStop = false;
            // 
            // tBarcode
            // 
            this.tBarcode.EditValue = "";
            this.tBarcode.Location = new System.Drawing.Point(106, 77);
            this.tBarcode.Name = "tBarcode";
            this.tBarcode.Properties.AdvancedModeOptions.AllowSelectionAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.tBarcode.Properties.AdvancedModeOptions.SelectionColor = System.Drawing.Color.Transparent;
            this.tBarcode.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.tBarcode.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.tBarcode.Properties.Appearance.Options.UseBackColor = true;
            this.tBarcode.Properties.Appearance.Options.UseForeColor = true;
            this.tBarcode.Size = new System.Drawing.Size(246, 30);
            this.tBarcode.TabIndex = 2;
            this.tBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tBarcode_KeyDown);
            // 
            // gridControl1
            // 
            this.tablePanel1.SetColumn(this.gridControl1, 0);
            this.tablePanel1.SetColumnSpan(this.gridControl1, 4);
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.AutoSize = true;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5);
            this.gridControl1.Location = new System.Drawing.Point(4, 156);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.bDeleteRow});
            this.tablePanel1.SetRow(this.gridControl1, 2);
            this.gridControl1.Size = new System.Drawing.Size(1227, 585);
            this.gridControl1.TabIndex = 3;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.EvenRow.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gridView1.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridView1.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.OddRow.Options.UseBackColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn13,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn12,
            this.gridColumn8,
            this.gridColumn11,
            this.gridColumn9,
            this.gridColumn10});
            this.gridView1.DetailHeight = 431;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsEditForm.PopupEditFormWidth = 1067;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.PaintStyleName = "Skin";
            this.gridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView1_KeyDown);
            this.gridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gridView1_KeyPress);
            this.gridView1.InvalidValueException += new DevExpress.XtraEditors.Controls.InvalidValueExceptionEventHandler(this.gridView1_InvalidValueException);
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.gridColumn1.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn1.Caption = "#";
            this.gridColumn1.FieldName = "No";
            this.gridColumn1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.MiddleLeft;
            this.gridColumn1.MaxWidth = 50;
            this.gridColumn1.MinWidth = 50;
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilter = false;
            this.gridColumn1.OptionsFilter.AllowFilterModeChanging = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsFilter.AllowInHeaderSearch = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn1.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 50;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Təchizatçı";
            this.gridColumn13.FieldName = "SupplierName";
            this.gridColumn13.Name = "gridColumn13";
            // 
            // gridColumn2
            // 
            this.gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn2.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.gridColumn2.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn2.Caption = "Məhsul Adı";
            this.gridColumn2.FieldName = "ProductName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 217;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn3.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.gridColumn3.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn3.Caption = "Barkod";
            this.gridColumn3.FieldName = "Barcode";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 129;
            // 
            // gridColumn4
            // 
            this.gridColumn4.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn4.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn4.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.gridColumn4.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn4.Caption = "Miqdar";
            this.gridColumn4.DisplayFormat.FormatString = "N3";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn4.FieldName = "Amount";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 107;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn5.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn5.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.gridColumn5.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn5.Caption = "Vahidi";
            this.gridColumn5.FieldName = "UnitName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 83;
            // 
            // gridColumn6
            // 
            this.gridColumn6.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn6.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn6.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.gridColumn6.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn6.Caption = "Alış qiyməti";
            this.gridColumn6.DisplayFormat.FormatString = "N2";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "PurchasePrice";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 108;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn7.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn7.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.gridColumn7.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn7.Caption = "Ümumi alış məbləği";
            this.gridColumn7.DisplayFormat.FormatString = "N2";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "TotalPurchaseAmount";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 134;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn12.AppearanceCell.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.gridColumn12.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn12.Caption = "+%";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn12.FieldName = "Percent";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 8;
            this.gridColumn12.Width = 65;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn8.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn8.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.gridColumn8.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn8.Caption = "Satış qiyməti";
            this.gridColumn8.DisplayFormat.FormatString = "N2";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "SalePrice";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 9;
            this.gridColumn8.Width = 101;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.gridColumn11.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn11.Caption = "Ümumi satış məbləği";
            this.gridColumn11.DisplayFormat.FormatString = "N2";
            this.gridColumn11.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn11.FieldName = "TotalSaleAmount";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn9
            // 
            this.gridColumn9.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn9.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn9.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.gridColumn9.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn9.Caption = "Mənfəət";
            this.gridColumn9.DisplayFormat.FormatString = "N2";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn9.FieldName = "GainAmount";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 10;
            this.gridColumn9.Width = 76;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.Trimming = DevExpress.Utils.Trimming.EllipsisCharacter;
            this.gridColumn10.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn10.Caption = "SİL";
            this.gridColumn10.ColumnEdit = this.bDeleteRow;
            this.gridColumn10.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.MiddleLeft;
            this.gridColumn10.MaxWidth = 60;
            this.gridColumn10.MinWidth = 60;
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowMove = false;
            this.gridColumn10.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.OptionsColumn.ShowCaption = false;
            this.gridColumn10.OptionsFilter.AllowAutoFilter = false;
            this.gridColumn10.OptionsFilter.AllowFilter = false;
            this.gridColumn10.OptionsFilter.AllowFilterModeChanging = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.OptionsFilter.AllowInHeaderSearch = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.gridColumn10.OptionsFilter.ImmediateUpdatePopupDateFilterOnCheck = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.OptionsFilter.ImmediateUpdatePopupDateFilterOnDateChange = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.OptionsFilter.ImmediateUpdatePopupExcelFilter = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.OptionsFilter.ShowBlanksFilterItems = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn10.OptionsFilter.ShowEmptyDateFilter = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 11;
            this.gridColumn10.Width = 60;
            // 
            // bDeleteRow
            // 
            this.bDeleteRow.AllowFocused = false;
            this.bDeleteRow.AutoHeight = false;
            serializableAppearanceObject1.Font = new System.Drawing.Font("Nunito", 11F, System.Drawing.FontStyle.Bold);
            serializableAppearanceObject1.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            serializableAppearanceObject1.Options.UseFont = true;
            serializableAppearanceObject1.Options.UseForeColor = true;
            this.bDeleteRow.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "SİL", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.bDeleteRow.Name = "bDeleteRow";
            this.bDeleteRow.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.bDeleteRow.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.bDeleteRow_ButtonClick);
            // 
            // bDelete
            // 
            this.bDelete.AllowFocus = false;
            this.bDelete.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.bDelete.Appearance.Options.UseBackColor = true;
            this.bDelete.AutoSize = true;
            this.tablePanel1.SetColumn(this.bDelete, 2);
            this.bDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bDelete.Location = new System.Drawing.Point(253, 123);
            this.bDelete.Margin = new System.Windows.Forms.Padding(2);
            this.bDelete.Name = "bDelete";
            this.tablePanel1.SetRow(this.bDelete, 1);
            this.bDelete.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bDelete.Size = new System.Drawing.Size(130, 28);
            this.bDelete.TabIndex = 4;
            this.bDelete.TabStop = false;
            this.bDelete.Text = "Seçili məhsulları sil";
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // fQuickAddProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1235, 745);
            this.Controls.Add(this.tablePanel1);
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.Name = "fQuickAddProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MƏHSUL ALIŞI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fQuickAddProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.tablePanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTarix.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTarix.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tInvoiceNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tBarcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDeleteRow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit tInvoiceNo;
        private DevExpress.XtraEditors.DateEdit dateTarix;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit bDeleteRow;
        private DevExpress.XtraEditors.TextEdit tBarcode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraEditors.SimpleButton bAdd;
        private DevExpress.XtraEditors.SimpleButton bSelectedProducts;
        private DevExpress.XtraEditors.SimpleButton bDelete;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
    }
}