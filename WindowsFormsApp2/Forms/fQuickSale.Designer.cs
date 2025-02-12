namespace WindowsFormsApp2.Forms
{
    partial class fQuickSale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fQuickSale));
            this.gridControlProducts = new DevExpress.XtraGrid.GridControl();
            this.gridProducts = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControlSelected = new DevExpress.XtraGrid.GridControl();
            this.gridSelected = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.bAdd = new DevExpress.XtraEditors.SimpleButton();
            this.bDelete = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSelected)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlProducts
            // 
            this.gridControlProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlProducts.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(1);
            this.gridControlProducts.Location = new System.Drawing.Point(3, 2);
            this.gridControlProducts.MainView = this.gridProducts;
            this.gridControlProducts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridControlProducts.Name = "gridControlProducts";
            this.tableLayoutPanel1.SetRowSpan(this.gridControlProducts, 4);
            this.gridControlProducts.Size = new System.Drawing.Size(599, 721);
            this.gridControlProducts.TabIndex = 17;
            this.gridControlProducts.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridProducts});
            // 
            // gridProducts
            // 
            this.gridProducts.Appearance.EvenRow.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gridProducts.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridProducts.Appearance.HeaderPanel.Font = new System.Drawing.Font("Nunito", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridProducts.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridProducts.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gridProducts.Appearance.OddRow.Options.UseBackColor = true;
            this.gridProducts.Appearance.ViewCaption.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold);
            this.gridProducts.Appearance.ViewCaption.Options.UseFont = true;
            this.gridProducts.DetailHeight = 294;
            this.gridProducts.GridControl = this.gridControlProducts;
            this.gridProducts.Name = "gridProducts";
            this.gridProducts.OptionsBehavior.ReadOnly = true;
            this.gridProducts.OptionsEditForm.PopupEditFormWidth = 1067;
            this.gridProducts.OptionsNavigation.AutoFocusNewRow = true;
            this.gridProducts.OptionsSelection.MultiSelect = true;
            this.gridProducts.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridProducts.OptionsView.EnableAppearanceEvenRow = true;
            this.gridProducts.OptionsView.EnableAppearanceOddRow = true;
            this.gridProducts.OptionsView.ShowIndicator = false;
            this.gridProducts.OptionsView.ShowViewCaption = true;
            this.gridProducts.PaintStyleName = "Skin";
            this.gridProducts.ViewCaption = "Məhsullar";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.99413F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.011741F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.99413F));
            this.tableLayoutPanel1.Controls.Add(this.gridControlProducts, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridControlSelected, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.bAdd, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.bDelete, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.53709F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.934718F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.231454F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.59347F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1261, 725);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // gridControlSelected
            // 
            this.gridControlSelected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlSelected.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(1);
            this.gridControlSelected.Location = new System.Drawing.Point(658, 2);
            this.gridControlSelected.MainView = this.gridSelected;
            this.gridControlSelected.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridControlSelected.Name = "gridControlSelected";
            this.tableLayoutPanel1.SetRowSpan(this.gridControlSelected, 4);
            this.gridControlSelected.Size = new System.Drawing.Size(600, 721);
            this.gridControlSelected.TabIndex = 18;
            this.gridControlSelected.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridSelected});
            // 
            // gridSelected
            // 
            this.gridSelected.Appearance.EvenRow.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gridSelected.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridSelected.Appearance.HeaderPanel.Font = new System.Drawing.Font("Nunito", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridSelected.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridSelected.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gridSelected.Appearance.OddRow.Options.UseBackColor = true;
            this.gridSelected.Appearance.ViewCaption.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Bold);
            this.gridSelected.Appearance.ViewCaption.Options.UseFont = true;
            this.gridSelected.DetailHeight = 294;
            this.gridSelected.GridControl = this.gridControlSelected;
            this.gridSelected.Name = "gridSelected";
            this.gridSelected.OptionsBehavior.ReadOnly = true;
            this.gridSelected.OptionsEditForm.PopupEditFormWidth = 1067;
            this.gridSelected.OptionsNavigation.AutoFocusNewRow = true;
            this.gridSelected.OptionsSelection.MultiSelect = true;
            this.gridSelected.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridSelected.OptionsView.EnableAppearanceEvenRow = true;
            this.gridSelected.OptionsView.EnableAppearanceOddRow = true;
            this.gridSelected.OptionsView.ShowIndicator = false;
            this.gridSelected.OptionsView.ShowViewCaption = true;
            this.gridSelected.PaintStyleName = "Skin";
            this.gridSelected.ViewCaption = "Seçilən məhsullar";
            // 
            // bAdd
            // 
            this.bAdd.AllowFocus = false;
            this.bAdd.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bAdd.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.bAdd.Appearance.Options.UseBackColor = true;
            this.bAdd.Appearance.Options.UseFont = true;
            this.bAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bAdd.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.bAdd.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bAdd.ImageOptions.SvgImage")));
            this.bAdd.Location = new System.Drawing.Point(608, 274);
            this.bAdd.Name = "bAdd";
            this.bAdd.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bAdd.Size = new System.Drawing.Size(44, 36);
            this.bAdd.TabIndex = 39;
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bDelete
            // 
            this.bDelete.AllowFocus = false;
            this.bDelete.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.bDelete.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.bDelete.Appearance.Options.UseBackColor = true;
            this.bDelete.Appearance.Options.UseFont = true;
            this.bDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bDelete.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.bDelete.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bDelete.ImageOptions.SvgImage")));
            this.bDelete.Location = new System.Drawing.Point(608, 315);
            this.bDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bDelete.Name = "bDelete";
            this.bDelete.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bDelete.Size = new System.Drawing.Size(44, 41);
            this.bDelete.TabIndex = 39;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // fQuickSale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1261, 725);
            this.Controls.Add(this.tableLayoutPanel1);
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "fQuickSale";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İsti satışlar məhsul seçimi";
            this.Load += new System.EventHandler(this.fQuickSale_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProducts)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSelected)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlProducts;
        private DevExpress.XtraGrid.Views.Grid.GridView gridProducts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.GridControl gridControlSelected;
        private DevExpress.XtraGrid.Views.Grid.GridView gridSelected;
        private DevExpress.XtraEditors.SimpleButton bAdd;
        private DevExpress.XtraEditors.SimpleButton bDelete;
    }
}