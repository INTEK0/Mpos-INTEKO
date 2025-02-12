using WindowsFormsApp2.Helpers;

namespace WindowsFormsApp2.Forms
{
    partial class fCustomers<TParent>
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.bPrint = new DevExpress.XtraEditors.SimpleButton();
            this.bDelete = new DevExpress.XtraEditors.SimpleButton();
            this.bEdit = new DevExpress.XtraEditors.SimpleButton();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gridControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelControl1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1053, 586);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Location = new System.Drawing.Point(4, 54);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1045, 528);
            this.gridControl1.TabIndex = 3;
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
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.ReadOnly = true;
            this.gridView1.OptionsEditForm.PopupEditFormWidth = 1067;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.bPrint);
            this.panelControl1.Controls.Add(this.bDelete);
            this.panelControl1.Controls.Add(this.bEdit);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(1, 1);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(1);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1051, 48);
            this.panelControl1.TabIndex = 4;
            // 
            // bPrint
            // 
            this.bPrint.AllowFocus = false;
            this.bPrint.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bPrint.Appearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Bold);
            this.bPrint.Appearance.Options.UseBackColor = true;
            this.bPrint.Appearance.Options.UseFont = true;
            this.bPrint.Dock = System.Windows.Forms.DockStyle.Right;
            this.bPrint.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.bPrint.Location = new System.Drawing.Point(935, 2);
            this.bPrint.LookAndFeel.SkinName = "WXI";
            this.bPrint.LookAndFeel.UseDefaultLookAndFeel = false;
            this.bPrint.Name = "bPrint";
            this.bPrint.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bPrint.Size = new System.Drawing.Size(114, 44);
            this.bPrint.TabIndex = 2;
            this.bPrint.TabStop = false;
            this.bPrint.Text = "Çap et";
            this.bPrint.Click += new System.EventHandler(this.bPrint_Click);
            // 
            // bDelete
            // 
            this.bDelete.AllowFocus = false;
            this.bDelete.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.bDelete.Appearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Bold);
            this.bDelete.Appearance.Options.UseBackColor = true;
            this.bDelete.Appearance.Options.UseFont = true;
            this.bDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.bDelete.Location = new System.Drawing.Point(183, 4);
            this.bDelete.LookAndFeel.SkinName = "WXI";
            this.bDelete.LookAndFeel.UseDefaultLookAndFeel = false;
            this.bDelete.Name = "bDelete";
            this.bDelete.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bDelete.Size = new System.Drawing.Size(166, 42);
            this.bDelete.TabIndex = 2;
            this.bDelete.TabStop = false;
            this.bDelete.Text = "Sil";
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // bEdit
            // 
            this.bEdit.AllowFocus = false;
            this.bEdit.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
            this.bEdit.Appearance.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Bold);
            this.bEdit.Appearance.Options.UseBackColor = true;
            this.bEdit.Appearance.Options.UseFont = true;
            this.bEdit.Location = new System.Drawing.Point(11, 4);
            this.bEdit.LookAndFeel.SkinName = "WXI";
            this.bEdit.LookAndFeel.UseDefaultLookAndFeel = false;
            this.bEdit.Name = "bEdit";
            this.bEdit.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bEdit.Size = new System.Drawing.Size(166, 42);
            this.bEdit.TabIndex = 2;
            this.bEdit.TabStop = false;
            this.bEdit.Text = "Düzəliş et";
            this.bEdit.Click += new System.EventHandler(this.bEdit_Click);
            // 
            // fCustomers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1053, 586);
            this.Controls.Add(this.tableLayoutPanel1);
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.MinimumSize = new System.Drawing.Size(1055, 620);
            this.Name = "fCustomers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MÜŞTƏRİLƏR";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fCustomers_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton bEdit;
        private DevExpress.XtraEditors.SimpleButton bPrint;
        private DevExpress.XtraEditors.SimpleButton bDelete;
    }
}