namespace WindowsFormsApp2.Forms
{
    partial class fAddDoctor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fAddDoctor));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.bDoctorList = new DevExpress.XtraEditors.SimpleButton();
            this.bClear = new DevExpress.XtraEditors.SimpleButton();
            this.bAdd = new DevExpress.XtraEditors.SimpleButton();
            this.tProccessNo = new DevExpress.XtraEditors.ButtonEdit();
            this.lDoctorID = new DevExpress.XtraEditors.LabelControl();
            this.tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            this.tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.dateBirth = new DevExpress.XtraEditors.DateEdit();
            this.lookGender = new DevExpress.XtraEditors.LookUpEdit();
            this.tNameSurname = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl16 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl23 = new DevExpress.XtraEditors.LabelControl();
            this.tEmail = new DevExpress.XtraEditors.TextEdit();
            this.tMobPhone = new DevExpress.XtraEditors.TextEdit();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.tPosition = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tProccessNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).BeginInit();
            this.tabPane1.SuspendLayout();
            this.tabNavigationPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateBirth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBirth.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookGender.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tNameSurname.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tMobPhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tPosition.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 38.1F)});
            this.tablePanel1.Controls.Add(this.panelControl1);
            this.tablePanel1.Controls.Add(this.tabPane1);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 51F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(948, 421);
            this.tablePanel1.TabIndex = 4;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // panelControl1
            // 
            this.tablePanel1.SetColumn(this.panelControl1, 0);
            this.panelControl1.Controls.Add(this.bDoctorList);
            this.panelControl1.Controls.Add(this.bClear);
            this.panelControl1.Controls.Add(this.bAdd);
            this.panelControl1.Controls.Add(this.tProccessNo);
            this.panelControl1.Controls.Add(this.lDoctorID);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(1);
            this.panelControl1.Name = "panelControl1";
            this.tablePanel1.SetRow(this.panelControl1, 0);
            this.panelControl1.Size = new System.Drawing.Size(944, 49);
            this.panelControl1.TabIndex = 0;
            // 
            // bDoctorList
            // 
            this.bDoctorList.AllowFocus = false;
            this.bDoctorList.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Primary;
            this.bDoctorList.Appearance.Font = new System.Drawing.Font("Nunito", 11F, System.Drawing.FontStyle.Bold);
            this.bDoctorList.Appearance.Options.UseBackColor = true;
            this.bDoctorList.Appearance.Options.UseFont = true;
            this.bDoctorList.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bDoctorList.ImageOptions.SvgImage")));
            this.bDoctorList.Location = new System.Drawing.Point(410, 4);
            this.bDoctorList.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.bDoctorList.Name = "bDoctorList";
            this.bDoctorList.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bDoctorList.Size = new System.Drawing.Size(198, 42);
            this.bDoctorList.TabIndex = 3;
            this.bDoctorList.TabStop = false;
            this.bDoctorList.Text = "HƏKİM SİYAHISI";
            this.bDoctorList.Click += new System.EventHandler(this.bDoctorList_Click);
            // 
            // bClear
            // 
            this.bClear.AllowFocus = false;
            this.bClear.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.bClear.Appearance.Font = new System.Drawing.Font("Nunito", 11F, System.Drawing.FontStyle.Bold);
            this.bClear.Appearance.Options.UseBackColor = true;
            this.bClear.Appearance.Options.UseFont = true;
            this.bClear.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bClear.ImageOptions.SvgImage")));
            this.bClear.Location = new System.Drawing.Point(208, 4);
            this.bClear.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.bClear.Name = "bClear";
            this.bClear.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bClear.Size = new System.Drawing.Size(194, 42);
            this.bClear.TabIndex = 1;
            this.bClear.TabStop = false;
            this.bClear.Text = "TƏMİZLƏ";
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // bAdd
            // 
            this.bAdd.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bAdd.Appearance.Font = new System.Drawing.Font("Nunito", 11F, System.Drawing.FontStyle.Bold);
            this.bAdd.Appearance.Options.UseBackColor = true;
            this.bAdd.Appearance.Options.UseFont = true;
            this.bAdd.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bAdd.ImageOptions.SvgImage")));
            this.bAdd.Location = new System.Drawing.Point(6, 4);
            this.bAdd.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(194, 42);
            this.bAdd.TabIndex = 1;
            this.bAdd.TabStop = false;
            this.bAdd.Text = "DAXİL ET";
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // tProccessNo
            // 
            this.tProccessNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tProccessNo.EditValue = "";
            this.tProccessNo.Location = new System.Drawing.Point(632, 9);
            this.tProccessNo.Margin = new System.Windows.Forms.Padding(4);
            this.tProccessNo.Name = "tProccessNo";
            this.tProccessNo.Properties.AllowFocused = false;
            this.tProccessNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tProccessNo.Properties.Appearance.Options.UseFont = true;
            this.tProccessNo.Properties.Appearance.Options.UseTextOptions = true;
            this.tProccessNo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tProccessNo.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tProccessNo.Properties.AppearanceReadOnly.Options.UseFont = true;
            this.tProccessNo.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.tProccessNo.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.tProccessNo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "HƏKİM NÖMRƏSİ", -1, true, true, true, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.tProccessNo.Properties.ReadOnly = true;
            this.tProccessNo.Size = new System.Drawing.Size(302, 34);
            this.tProccessNo.TabIndex = 0;
            this.tProccessNo.TabStop = false;
            // 
            // lDoctorID
            // 
            this.lDoctorID.Appearance.Font = new System.Drawing.Font("Nunito", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lDoctorID.Appearance.Options.UseFont = true;
            this.lDoctorID.Location = new System.Drawing.Point(626, 16);
            this.lDoctorID.Margin = new System.Windows.Forms.Padding(4);
            this.lDoctorID.Name = "lDoctorID";
            this.lDoctorID.Size = new System.Drawing.Size(54, 18);
            this.lDoctorID.TabIndex = 6;
            this.lDoctorID.Text = "DoctorID";
            this.lDoctorID.Visible = false;
            // 
            // tabPane1
            // 
            this.tabPane1.Appearance.BackColor = System.Drawing.Color.White;
            this.tabPane1.Appearance.Options.UseBackColor = true;
            this.tabPane1.AppearanceButton.Hovered.Font = new System.Drawing.Font("Nunito", 12F);
            this.tabPane1.AppearanceButton.Hovered.Options.UseFont = true;
            this.tabPane1.AppearanceButton.Normal.Font = new System.Drawing.Font("Nunito", 12F);
            this.tabPane1.AppearanceButton.Normal.Options.UseFont = true;
            this.tabPane1.AppearanceButton.Pressed.Font = new System.Drawing.Font("Nunito", 12F, System.Drawing.FontStyle.Bold);
            this.tabPane1.AppearanceButton.Pressed.Options.UseFont = true;
            this.tablePanel1.SetColumn(this.tabPane1, 0);
            this.tabPane1.Controls.Add(this.tabNavigationPage1);
            this.tabPane1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPane1.Location = new System.Drawing.Point(4, 55);
            this.tabPane1.Name = "tabPane1";
            this.tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.tabNavigationPage1});
            this.tabPane1.RegularSize = new System.Drawing.Size(940, 362);
            this.tablePanel1.SetRow(this.tabPane1, 1);
            this.tabPane1.SelectedPage = this.tabNavigationPage1;
            this.tabPane1.Size = new System.Drawing.Size(940, 362);
            this.tabPane1.TabIndex = 2;
            this.tabPane1.Text = "tabPane1";
            // 
            // tabNavigationPage1
            // 
            this.tabNavigationPage1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.tabNavigationPage1.Appearance.Options.UseBackColor = true;
            this.tabNavigationPage1.Caption = "Həkim məlumatları";
            this.tabNavigationPage1.Controls.Add(this.groupControl2);
            this.tabNavigationPage1.Controls.Add(this.groupControl1);
            this.tabNavigationPage1.Font = new System.Drawing.Font("Nunito", 10F, System.Drawing.FontStyle.Bold);
            this.tabNavigationPage1.Name = "tabNavigationPage1";
            this.tabNavigationPage1.Properties.AppearanceCaption.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold);
            this.tabNavigationPage1.Properties.AppearanceCaption.Options.UseFont = true;
            this.tabNavigationPage1.Size = new System.Drawing.Size(940, 315);
            // 
            // groupControl2
            // 
            this.groupControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Nunito", 10F, System.Drawing.FontStyle.Bold);
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.Controls.Add(this.labelControl11);
            this.groupControl2.Controls.Add(this.dateBirth);
            this.groupControl2.Controls.Add(this.lookGender);
            this.groupControl2.Controls.Add(this.tPosition);
            this.groupControl2.Controls.Add(this.tNameSurname);
            this.groupControl2.Controls.Add(this.labelControl1);
            this.groupControl2.Controls.Add(this.labelControl2);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Controls.Add(this.labelControl16);
            this.groupControl2.Location = new System.Drawing.Point(2, 3);
            this.groupControl2.LookAndFeel.SkinName = "WXI";
            this.groupControl2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.ShowCaption = false;
            this.groupControl2.Size = new System.Drawing.Size(935, 179);
            this.groupControl2.TabIndex = 19;
            this.groupControl2.Text = "MÜŞTƏRİ MƏLUMATLARI";
            // 
            // labelControl11
            // 
            this.labelControl11.Appearance.Font = new System.Drawing.Font("Nunito", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl11.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl11.Appearance.Options.UseFont = true;
            this.labelControl11.Appearance.Options.UseForeColor = true;
            this.labelControl11.Location = new System.Drawing.Point(145, 15);
            this.labelControl11.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(6, 18);
            this.labelControl11.TabIndex = 35;
            this.labelControl11.Text = "*";
            // 
            // dateBirth
            // 
            this.dateBirth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateBirth.EditValue = null;
            this.dateBirth.Location = new System.Drawing.Point(159, 91);
            this.dateBirth.Margin = new System.Windows.Forms.Padding(4);
            this.dateBirth.Name = "dateBirth";
            this.dateBirth.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dateBirth.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateBirth.Properties.Appearance.Options.UseFont = true;
            this.dateBirth.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBirth.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateBirth.Size = new System.Drawing.Size(769, 34);
            this.dateBirth.TabIndex = 3;
            // 
            // lookGender
            // 
            this.lookGender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lookGender.Location = new System.Drawing.Point(159, 133);
            this.lookGender.Margin = new System.Windows.Forms.Padding(4);
            this.lookGender.Name = "lookGender";
            this.lookGender.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lookGender.Properties.Appearance.Options.UseFont = true;
            this.lookGender.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookGender.Properties.DropDownRows = 2;
            this.lookGender.Properties.NullText = "";
            this.lookGender.Properties.NullValuePrompt = "--SEÇİN--";
            this.lookGender.Properties.ShowFooter = false;
            this.lookGender.Size = new System.Drawing.Size(769, 34);
            this.lookGender.TabIndex = 10;
            // 
            // tNameSurname
            // 
            this.tNameSurname.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tNameSurname.Location = new System.Drawing.Point(159, 7);
            this.tNameSurname.Margin = new System.Windows.Forms.Padding(4);
            this.tNameSurname.Name = "tNameSurname";
            this.tNameSurname.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tNameSurname.Properties.Appearance.Options.UseFont = true;
            this.tNameSurname.Size = new System.Drawing.Size(769, 34);
            this.tNameSurname.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Nunito", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(6, 15);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(70, 18);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "AD SOYAD";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Nunito", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(6, 99);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(99, 18);
            this.labelControl3.TabIndex = 11;
            this.labelControl3.Text = "DOĞUM TARİXİ";
            // 
            // labelControl16
            // 
            this.labelControl16.Appearance.Font = new System.Drawing.Font("Nunito", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl16.Appearance.Options.UseFont = true;
            this.labelControl16.Location = new System.Drawing.Point(6, 141);
            this.labelControl16.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl16.Name = "labelControl16";
            this.labelControl16.Size = new System.Drawing.Size(73, 18);
            this.labelControl16.TabIndex = 15;
            this.labelControl16.Text = "CİNSİYYƏTİ";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Nunito", 10F, System.Drawing.FontStyle.Bold);
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.Controls.Add(this.labelControl23);
            this.groupControl1.Controls.Add(this.tEmail);
            this.groupControl1.Controls.Add(this.tMobPhone);
            this.groupControl1.Controls.Add(this.labelControl22);
            this.groupControl1.Location = new System.Drawing.Point(4, 188);
            this.groupControl1.LookAndFeel.SkinName = "WXI";
            this.groupControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(936, 122);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "ƏLAQƏ MƏLUMATLARI";
            // 
            // labelControl23
            // 
            this.labelControl23.Appearance.Font = new System.Drawing.Font("Nunito", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl23.Appearance.Options.UseFont = true;
            this.labelControl23.Location = new System.Drawing.Point(6, 83);
            this.labelControl23.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl23.Name = "labelControl23";
            this.labelControl23.Size = new System.Drawing.Size(42, 18);
            this.labelControl23.TabIndex = 19;
            this.labelControl23.Text = "MOBİL";
            // 
            // tEmail
            // 
            this.tEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tEmail.Location = new System.Drawing.Point(157, 33);
            this.tEmail.Margin = new System.Windows.Forms.Padding(4);
            this.tEmail.Name = "tEmail";
            this.tEmail.Properties.AllowFocused = false;
            this.tEmail.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tEmail.Properties.Appearance.Options.UseFont = true;
            this.tEmail.Size = new System.Drawing.Size(772, 34);
            this.tEmail.TabIndex = 0;
            // 
            // tMobPhone
            // 
            this.tMobPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tMobPhone.Location = new System.Drawing.Point(157, 75);
            this.tMobPhone.Margin = new System.Windows.Forms.Padding(4);
            this.tMobPhone.Name = "tMobPhone";
            this.tMobPhone.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tMobPhone.Properties.Appearance.Options.UseFont = true;
            this.tMobPhone.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.RegExpMaskManager));
            this.tMobPhone.Properties.MaskSettings.Set("MaskManagerSignature", "isOptimistic=False");
            this.tMobPhone.Properties.MaskSettings.Set("mask", "(\\d\\d\\d) \\d\\d\\d-\\d\\d\\d\\d");
            this.tMobPhone.Size = new System.Drawing.Size(772, 34);
            this.tMobPhone.TabIndex = 1;
            // 
            // labelControl22
            // 
            this.labelControl22.Appearance.Font = new System.Drawing.Font("Nunito", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl22.Appearance.Options.UseFont = true;
            this.labelControl22.Location = new System.Drawing.Point(6, 41);
            this.labelControl22.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(50, 18);
            this.labelControl22.TabIndex = 19;
            this.labelControl22.Text = "E-POÇT";
            // 
            // tPosition
            // 
            this.tPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tPosition.Location = new System.Drawing.Point(159, 49);
            this.tPosition.Margin = new System.Windows.Forms.Padding(4);
            this.tPosition.Name = "tPosition";
            this.tPosition.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tPosition.Properties.Appearance.Options.UseFont = true;
            this.tPosition.Size = new System.Drawing.Size(769, 34);
            this.tPosition.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Nunito", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(6, 57);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 18);
            this.labelControl1.TabIndex = 9;
            this.labelControl1.Text = "VƏZİFƏSİ";
            // 
            // fAddDoctor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(948, 421);
            this.Controls.Add(this.tablePanel1);
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.MinimumSize = new System.Drawing.Size(950, 455);
            this.Name = "fAddDoctor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HƏKİM";
            this.Load += new System.EventHandler(this.fAddDoctor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tProccessNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabPane1)).EndInit();
            this.tabPane1.ResumeLayout(false);
            this.tabNavigationPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateBirth.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateBirth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookGender.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tNameSurname.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tMobPhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tPosition.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton bDoctorList;
        private DevExpress.XtraEditors.SimpleButton bClear;
        private DevExpress.XtraEditors.SimpleButton bAdd;
        private DevExpress.XtraEditors.ButtonEdit tProccessNo;
        private DevExpress.XtraEditors.LabelControl lDoctorID;
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.LabelControl labelControl23;
        private DevExpress.XtraEditors.TextEdit tEmail;
        private DevExpress.XtraEditors.TextEdit tMobPhone;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.DateEdit dateBirth;
        private DevExpress.XtraEditors.LookUpEdit lookGender;
        private DevExpress.XtraEditors.TextEdit tNameSurname;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl16;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.TextEdit tPosition;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}