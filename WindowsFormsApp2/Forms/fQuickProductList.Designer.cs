namespace WindowsFormsApp2.Forms
{
    partial class fQuickProductList
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
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bDeleteRow = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panelControl9 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl10 = new DevExpress.XtraEditors.PanelControl();
            this.bAdd = new DevExpress.XtraEditors.SimpleButton();
            this.bCreate = new DevExpress.XtraEditors.SimpleButton();
            this.bRefresh = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDeleteRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl9)).BeginInit();
            this.panelControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl10)).BeginInit();
            this.panelControl10.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.AutoSize = true;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(5);
            this.gridControl1.Location = new System.Drawing.Point(0, 37);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.bDeleteRow});
            this.gridControl1.Size = new System.Drawing.Size(1188, 727);
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
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn13,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn6,
            this.gridColumn8,
            this.gridColumn5,
            this.gridColumn4});
            this.gridView1.DetailHeight = 431;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.GroupCount = 1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsEditForm.PopupEditFormWidth = 1067;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.PaintStyleName = "Skin";
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumn13, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "Təchizatçı";
            this.gridColumn13.FieldName = "SupplierName";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 1;
            this.gridColumn13.Width = 101;
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
            this.gridColumn2.VisibleIndex = 1;
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
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 117;
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
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 97;
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
            this.gridColumn8.VisibleIndex = 5;
            this.gridColumn8.Width = 91;
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
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 74;
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
            this.gridColumn4.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.MiddleLeft;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 6;
            this.gridColumn4.Width = 97;
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
            // 
            // panelControl9
            // 
            this.panelControl9.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl9.Controls.Add(this.gridControl1);
            this.panelControl9.Controls.Add(this.panelControl10);
            this.panelControl9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl9.Location = new System.Drawing.Point(0, 0);
            this.panelControl9.LookAndFeel.SkinName = "WXI";
            this.panelControl9.LookAndFeel.UseDefaultLookAndFeel = false;
            this.panelControl9.Name = "panelControl9";
            this.panelControl9.Size = new System.Drawing.Size(1188, 764);
            this.panelControl9.TabIndex = 7;
            // 
            // panelControl10
            // 
            this.panelControl10.Controls.Add(this.bRefresh);
            this.panelControl10.Controls.Add(this.bCreate);
            this.panelControl10.Controls.Add(this.bAdd);
            this.panelControl10.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl10.Location = new System.Drawing.Point(0, 0);
            this.panelControl10.Name = "panelControl10";
            this.panelControl10.Size = new System.Drawing.Size(1188, 37);
            this.panelControl10.TabIndex = 1;
            // 
            // bAdd
            // 
            this.bAdd.AllowFocus = false;
            this.bAdd.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bAdd.Appearance.Options.UseBackColor = true;
            this.bAdd.Location = new System.Drawing.Point(11, 4);
            this.bAdd.Margin = new System.Windows.Forms.Padding(2);
            this.bAdd.Name = "bAdd";
            this.bAdd.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bAdd.Size = new System.Drawing.Size(79, 28);
            this.bAdd.TabIndex = 5;
            this.bAdd.TabStop = false;
            this.bAdd.Text = "Əlavə et";
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bCreate
            // 
            this.bCreate.AllowFocus = false;
            this.bCreate.Location = new System.Drawing.Point(94, 4);
            this.bCreate.Margin = new System.Windows.Forms.Padding(2);
            this.bCreate.Name = "bCreate";
            this.bCreate.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bCreate.Size = new System.Drawing.Size(137, 28);
            this.bCreate.TabIndex = 6;
            this.bCreate.TabStop = false;
            this.bCreate.Text = "Yeni məhsul yarat";
            this.bCreate.Click += new System.EventHandler(this.bCreate_Click);
            // 
            // bRefresh
            // 
            this.bRefresh.AllowFocus = false;
            this.bRefresh.Location = new System.Drawing.Point(235, 4);
            this.bRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.bRefresh.Name = "bRefresh";
            this.bRefresh.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bRefresh.Size = new System.Drawing.Size(82, 28);
            this.bRefresh.TabIndex = 6;
            this.bRefresh.TabStop = false;
            this.bRefresh.Text = "Yenilə";
            this.bRefresh.Click += new System.EventHandler(this.bRefresh_Click);
            // 
            // fQuickProductList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1188, 764);
            this.Controls.Add(this.panelControl9);
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.Name = "fQuickProductList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MƏHSULLAR";
            this.Load += new System.EventHandler(this.fQuickProductList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bDeleteRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl9)).EndInit();
            this.panelControl9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl10)).EndInit();
            this.panelControl10.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit bDeleteRow;
        private DevExpress.XtraEditors.PanelControl panelControl9;
        private DevExpress.XtraEditors.PanelControl panelControl10;
        private DevExpress.XtraEditors.SimpleButton bAdd;
        private DevExpress.XtraEditors.SimpleButton bCreate;
        private DevExpress.XtraEditors.SimpleButton bRefresh;
    }
}