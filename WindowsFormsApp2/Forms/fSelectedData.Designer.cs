namespace WindowsFormsApp2.Forms
{
    partial class fSelectedData<TParent>
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode3 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode4 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridDoctor = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridZamin = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridCustomers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.bRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.bAdd = new DevExpress.XtraEditors.SimpleButton();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridDoctor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridZamin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridDoctor
            // 
            this.gridDoctor.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn8});
            this.gridDoctor.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridDoctor.GridControl = this.gridControl1;
            this.gridDoctor.Name = "gridDoctor";
            this.gridDoctor.OptionsBehavior.Editable = false;
            this.gridDoctor.DoubleClick += new System.EventHandler(this.gridDoctor_DoubleClick);
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "AD SOYAD";
            this.gridColumn5.FieldName = "AD SOYAD";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "VƏZİFƏSİ";
            this.gridColumn8.FieldName = "VƏZİFƏSİ";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            gridLevelNode3.LevelTemplate = this.gridDoctor;
            gridLevelNode3.RelationName = "Level1";
            gridLevelNode4.LevelTemplate = this.gridZamin;
            gridLevelNode4.RelationName = "Level2";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode3,
            gridLevelNode4});
            this.gridControl1.Location = new System.Drawing.Point(3, 71);
            this.gridControl1.MainView = this.gridCustomers;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(959, 680);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridZamin,
            this.gridCustomers,
            this.gridDoctor});
            // 
            // gridZamin
            // 
            this.gridZamin.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16});
            this.gridZamin.GridControl = this.gridControl1;
            this.gridZamin.Name = "gridZamin";
            this.gridZamin.DoubleClick += new System.EventHandler(this.gridZamin_DoubleClick);
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Id";
            this.gridColumn9.FieldName = "ID";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "AD SOYAD";
            this.gridColumn10.FieldName = "NameSurname";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            // 
            // gridCustomers
            // 
            this.gridCustomers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn4});
            this.gridCustomers.DetailHeight = 431;
            this.gridCustomers.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridCustomers.GridControl = this.gridControl1;
            this.gridCustomers.Name = "gridCustomers";
            this.gridCustomers.OptionsBehavior.Editable = false;
            this.gridCustomers.OptionsEditForm.PopupEditFormWidth = 1067;
            this.gridCustomers.OptionsView.EnableAppearanceEvenRow = true;
            this.gridCustomers.OptionsView.EnableAppearanceOddRow = true;
            this.gridCustomers.OptionsView.ShowIndicator = false;
            this.gridCustomers.DoubleClick += new System.EventHandler(this.gridCustomers_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Id";
            this.gridColumn1.FieldName = "MUSTERILER_ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "F/Ş - H/Ş ADI";
            this.gridColumn2.FieldName = "F/Ş - H/Ş ADI";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 174;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "AD SOYAD ATA ADI";
            this.gridColumn3.FieldName = "AD SOYAD ATA ADI";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 195;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "TƏVƏLLÜD";
            this.gridColumn6.FieldName = "DOĞUM TARİXİ";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 195;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "FİN KOD";
            this.gridColumn7.FieldName = "FİNKOD";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 195;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "ƏLAQƏ NÖMRƏSİ";
            this.gridColumn4.FieldName = "MOBİL TEL";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 198;
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.bRefresh);
            this.panelControl1.Controls.Add(this.bAdd);
            this.panelControl1.Location = new System.Drawing.Point(3, 3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(959, 61);
            this.panelControl1.TabIndex = 2;
            // 
            // bRefresh
            // 
            this.bRefresh.AllowFocus = false;
            this.bRefresh.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.bRefresh.Appearance.Font = new System.Drawing.Font("Nunito", 11F, System.Drawing.FontStyle.Bold);
            this.bRefresh.Appearance.Options.UseBackColor = true;
            this.bRefresh.Appearance.Options.UseFont = true;
            this.bRefresh.Location = new System.Drawing.Point(212, 8);
            this.bRefresh.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.bRefresh.Name = "bRefresh";
            this.bRefresh.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bRefresh.Size = new System.Drawing.Size(194, 42);
            this.bRefresh.TabIndex = 2;
            this.bRefresh.TabStop = false;
            this.bRefresh.Text = "YENİLƏ";
            this.bRefresh.Visible = false;
            this.bRefresh.Click += new System.EventHandler(this.bRefresh_Click);
            // 
            // bAdd
            // 
            this.bAdd.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bAdd.Appearance.Font = new System.Drawing.Font("Nunito", 11F, System.Drawing.FontStyle.Bold);
            this.bAdd.Appearance.Options.UseBackColor = true;
            this.bAdd.Appearance.Options.UseFont = true;
            this.bAdd.Location = new System.Drawing.Point(10, 8);
            this.bAdd.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(194, 42);
            this.bAdd.TabIndex = 3;
            this.bAdd.TabStop = false;
            this.bAdd.Text = "YENİ MÜŞTƏRİ";
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "Ad";
            this.gridColumn11.FieldName = "Name";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "Soyad";
            this.gridColumn12.FieldName = "Surname";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "gridColumn13";
            this.gridColumn13.FieldName = "FatherName";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.OptionsColumn.AllowEdit = false;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "DOĞUM TARİXİ";
            this.gridColumn14.FieldName = "DateBirth";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 1;
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "FİN KOD";
            this.gridColumn15.FieldName = "FinCode";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 2;
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "ƏLAQƏ NÖMRƏSİ";
            this.gridColumn16.FieldName = "MobPhone";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 3;
            // 
            // fSelectedData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(965, 755);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.gridControl1);
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.Name = "fSelectedData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.fSelectedData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridDoctor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridZamin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridCustomers;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton bRefresh;
        private DevExpress.XtraEditors.SimpleButton bAdd;
        private DevExpress.XtraGrid.Views.Grid.GridView gridDoctor;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Views.Grid.GridView gridZamin;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
    }
}