namespace WindowsFormsApp2.Forms
{
    partial class fPrepaymentPay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fPrepaymentPay));
            this.navigationFrame1 = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.pageHome = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.bCash = new DevExpress.XtraEditors.SimpleButton();
            this.bCard = new DevExpress.XtraEditors.SimpleButton();
            this.bCashCard = new DevExpress.XtraEditors.SimpleButton();
            this.pageCash = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.tCash_Paid = new DevExpress.XtraEditors.TextEdit();
            this.bCash_Enter = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.tCash_Balance = new DevExpress.XtraEditors.TextEdit();
            this.tCash_Total = new DevExpress.XtraEditors.TextEdit();
            this.pageCashCard = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.tCashCard_Enter = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.tCashCard_Balance = new DevExpress.XtraEditors.TextEdit();
            this.tCashCard_Total = new DevExpress.XtraEditors.TextEdit();
            this.tCashCard_Card = new DevExpress.XtraEditors.TextEdit();
            this.tCashCard_Cash = new DevExpress.XtraEditors.TextEdit();
            this.lCashCard_Message = new DevExpress.XtraEditors.LabelControl();
            this.lCash_Message = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame1)).BeginInit();
            this.navigationFrame1.SuspendLayout();
            this.pageHome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.pageCash.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tCash_Paid.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCash_Balance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCash_Total.Properties)).BeginInit();
            this.pageCashCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tCashCard_Balance.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCashCard_Total.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCashCard_Card.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCashCard_Cash.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // navigationFrame1
            // 
            this.navigationFrame1.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.False;
            this.navigationFrame1.Controls.Add(this.pageHome);
            this.navigationFrame1.Controls.Add(this.pageCash);
            this.navigationFrame1.Controls.Add(this.pageCashCard);
            this.navigationFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationFrame1.Location = new System.Drawing.Point(0, 0);
            this.navigationFrame1.Name = "navigationFrame1";
            this.navigationFrame1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.pageHome,
            this.pageCash,
            this.pageCashCard});
            this.navigationFrame1.SelectedPage = this.pageHome;
            this.navigationFrame1.Size = new System.Drawing.Size(458, 136);
            this.navigationFrame1.TabIndex = 0;
            this.navigationFrame1.Text = "navigationFrame1";
            // 
            // pageHome
            // 
            this.pageHome.Controls.Add(this.tablePanel1);
            this.pageHome.Name = "pageHome";
            this.pageHome.Size = new System.Drawing.Size(458, 136);
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 100F)});
            this.tablePanel1.Controls.Add(this.bCash);
            this.tablePanel1.Controls.Add(this.bCard);
            this.tablePanel1.Controls.Add(this.bCashCard);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(458, 136);
            this.tablePanel1.TabIndex = 0;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // bCash
            // 
            this.bCash.AllowFocus = false;
            this.bCash.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bCash.Appearance.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold);
            this.bCash.Appearance.Options.UseBackColor = true;
            this.bCash.Appearance.Options.UseFont = true;
            this.tablePanel1.SetColumn(this.bCash, 0);
            this.bCash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bCash.Location = new System.Drawing.Point(4, 4);
            this.bCash.Name = "bCash";
            this.tablePanel1.SetRow(this.bCash, 0);
            this.bCash.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bCash.Size = new System.Drawing.Size(146, 128);
            this.bCash.TabIndex = 4;
            this.bCash.Text = "NAĞD";
            this.bCash.Click += new System.EventHandler(this.bCash_Click);
            // 
            // bCard
            // 
            this.bCard.AllowFocus = false;
            this.bCard.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Question;
            this.bCard.Appearance.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold);
            this.bCard.Appearance.Options.UseBackColor = true;
            this.bCard.Appearance.Options.UseFont = true;
            this.tablePanel1.SetColumn(this.bCard, 1);
            this.bCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bCard.Location = new System.Drawing.Point(156, 4);
            this.bCard.Name = "bCard";
            this.tablePanel1.SetRow(this.bCard, 0);
            this.bCard.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bCard.Size = new System.Drawing.Size(146, 128);
            this.bCard.TabIndex = 4;
            this.bCard.Text = "KART";
            this.bCard.Click += new System.EventHandler(this.bCard_Click);
            // 
            // bCashCard
            // 
            this.bCashCard.AllowFocus = false;
            this.bCashCard.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Warning;
            this.bCashCard.Appearance.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold);
            this.bCashCard.Appearance.Options.UseBackColor = true;
            this.bCashCard.Appearance.Options.UseFont = true;
            this.tablePanel1.SetColumn(this.bCashCard, 2);
            this.bCashCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bCashCard.Location = new System.Drawing.Point(308, 4);
            this.bCashCard.Name = "bCashCard";
            this.tablePanel1.SetRow(this.bCashCard, 0);
            this.bCashCard.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.bCashCard.Size = new System.Drawing.Size(146, 128);
            this.bCashCard.TabIndex = 4;
            this.bCashCard.Text = "NAĞD - KART";
            this.bCashCard.Click += new System.EventHandler(this.bCashCard_Click);
            // 
            // pageCash
            // 
            this.pageCash.Controls.Add(this.lCash_Message);
            this.pageCash.Controls.Add(this.tCash_Paid);
            this.pageCash.Controls.Add(this.bCash_Enter);
            this.pageCash.Controls.Add(this.labelControl3);
            this.pageCash.Controls.Add(this.labelControl2);
            this.pageCash.Controls.Add(this.labelControl1);
            this.pageCash.Controls.Add(this.tCash_Balance);
            this.pageCash.Controls.Add(this.tCash_Total);
            this.pageCash.Name = "pageCash";
            this.pageCash.Size = new System.Drawing.Size(458, 136);
            // 
            // tCash_Paid
            // 
            this.tCash_Paid.Location = new System.Drawing.Point(137, 74);
            this.tCash_Paid.Name = "tCash_Paid";
            this.tCash_Paid.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F);
            this.tCash_Paid.Properties.Appearance.Options.UseFont = true;
            this.tCash_Paid.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.tCash_Paid.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.tCash_Paid.Properties.MaskSettings.Set("mask", "f");
            this.tCash_Paid.Properties.NullText = "0";
            this.tCash_Paid.Properties.UseMaskAsDisplayFormat = true;
            this.tCash_Paid.Size = new System.Drawing.Size(176, 54);
            this.tCash_Paid.TabIndex = 25;
            this.tCash_Paid.EditValueChanged += new System.EventHandler(this.tCash_Paid_EditValueChanged);
            // 
            // bCash_Enter
            // 
            this.bCash_Enter.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.bCash_Enter.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bCash_Enter.Appearance.Options.UseBackColor = true;
            this.bCash_Enter.Appearance.Options.UseFont = true;
            this.bCash_Enter.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("bCash_Enter.ImageOptions.SvgImage")));
            this.bCash_Enter.Location = new System.Drawing.Point(320, 13);
            this.bCash_Enter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bCash_Enter.Name = "bCash_Enter";
            this.bCash_Enter.Size = new System.Drawing.Size(135, 178);
            this.bCash_Enter.TabIndex = 24;
            this.bCash_Enter.Text = "ÖDƏNİŞ ET";
            this.bCash_Enter.Click += new System.EventHandler(this.bCash_Enter_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(13, 156);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 26);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "QALIQ";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(13, 86);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(103, 26);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "ÖDƏNİLƏN";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(13, 24);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(84, 26);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "MƏBLƏĞ";
            // 
            // tCash_Balance
            // 
            this.tCash_Balance.Location = new System.Drawing.Point(137, 137);
            this.tCash_Balance.Margin = new System.Windows.Forms.Padding(4);
            this.tCash_Balance.Name = "tCash_Balance";
            this.tCash_Balance.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F);
            this.tCash_Balance.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.tCash_Balance.Properties.Appearance.Options.UseFont = true;
            this.tCash_Balance.Properties.Appearance.Options.UseForeColor = true;
            this.tCash_Balance.Properties.NullText = "0";
            this.tCash_Balance.Properties.ReadOnly = true;
            this.tCash_Balance.Size = new System.Drawing.Size(176, 54);
            this.tCash_Balance.TabIndex = 5;
            // 
            // tCash_Total
            // 
            this.tCash_Total.Location = new System.Drawing.Point(137, 13);
            this.tCash_Total.Margin = new System.Windows.Forms.Padding(4);
            this.tCash_Total.Name = "tCash_Total";
            this.tCash_Total.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F);
            this.tCash_Total.Properties.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            this.tCash_Total.Properties.Appearance.Options.UseFont = true;
            this.tCash_Total.Properties.Appearance.Options.UseForeColor = true;
            this.tCash_Total.Properties.NullText = "0";
            this.tCash_Total.Properties.ReadOnly = true;
            this.tCash_Total.Size = new System.Drawing.Size(176, 54);
            this.tCash_Total.TabIndex = 5;
            // 
            // pageCashCard
            // 
            this.pageCashCard.Controls.Add(this.tCashCard_Cash);
            this.pageCashCard.Controls.Add(this.tCashCard_Card);
            this.pageCashCard.Controls.Add(this.tCashCard_Enter);
            this.pageCashCard.Controls.Add(this.labelControl4);
            this.pageCashCard.Controls.Add(this.lCashCard_Message);
            this.pageCashCard.Controls.Add(this.labelControl7);
            this.pageCashCard.Controls.Add(this.labelControl5);
            this.pageCashCard.Controls.Add(this.labelControl6);
            this.pageCashCard.Controls.Add(this.tCashCard_Balance);
            this.pageCashCard.Controls.Add(this.tCashCard_Total);
            this.pageCashCard.Name = "pageCashCard";
            this.pageCashCard.Size = new System.Drawing.Size(458, 136);
            // 
            // tCashCard_Enter
            // 
            this.tCashCard_Enter.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.tCashCard_Enter.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tCashCard_Enter.Appearance.Options.UseBackColor = true;
            this.tCashCard_Enter.Appearance.Options.UseFont = true;
            this.tCashCard_Enter.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("tCashCard_Enter.ImageOptions.SvgImage")));
            this.tCashCard_Enter.Location = new System.Drawing.Point(315, 13);
            this.tCashCard_Enter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tCashCard_Enter.Name = "tCashCard_Enter";
            this.tCashCard_Enter.Size = new System.Drawing.Size(135, 236);
            this.tCashCard_Enter.TabIndex = 31;
            this.tCashCard_Enter.Text = "ÖDƏNİŞ ET";
            this.tCashCard_Enter.Click += new System.EventHandler(this.tCashCard_Enter_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(13, 209);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 26);
            this.labelControl4.TabIndex = 28;
            this.labelControl4.Text = "QALIQ";
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl7.Appearance.Options.UseFont = true;
            this.labelControl7.Location = new System.Drawing.Point(13, 89);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(52, 26);
            this.labelControl7.TabIndex = 29;
            this.labelControl7.Text = "KART";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(13, 148);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(56, 26);
            this.labelControl5.TabIndex = 29;
            this.labelControl5.Text = "NAĞD";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Nunito", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(13, 24);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(84, 26);
            this.labelControl6.TabIndex = 30;
            this.labelControl6.Text = "MƏBLƏĞ";
            // 
            // tCashCard_Balance
            // 
            this.tCashCard_Balance.Location = new System.Drawing.Point(132, 195);
            this.tCashCard_Balance.Margin = new System.Windows.Forms.Padding(4);
            this.tCashCard_Balance.Name = "tCashCard_Balance";
            this.tCashCard_Balance.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F);
            this.tCashCard_Balance.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.tCashCard_Balance.Properties.Appearance.Options.UseFont = true;
            this.tCashCard_Balance.Properties.Appearance.Options.UseForeColor = true;
            this.tCashCard_Balance.Properties.NullText = "0";
            this.tCashCard_Balance.Properties.ReadOnly = true;
            this.tCashCard_Balance.Size = new System.Drawing.Size(176, 54);
            this.tCashCard_Balance.TabIndex = 25;
            // 
            // tCashCard_Total
            // 
            this.tCashCard_Total.Location = new System.Drawing.Point(132, 13);
            this.tCashCard_Total.Margin = new System.Windows.Forms.Padding(4);
            this.tCashCard_Total.Name = "tCashCard_Total";
            this.tCashCard_Total.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F);
            this.tCashCard_Total.Properties.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            this.tCashCard_Total.Properties.Appearance.Options.UseFont = true;
            this.tCashCard_Total.Properties.Appearance.Options.UseForeColor = true;
            this.tCashCard_Total.Properties.NullText = "0";
            this.tCashCard_Total.Properties.ReadOnly = true;
            this.tCashCard_Total.Size = new System.Drawing.Size(176, 54);
            this.tCashCard_Total.TabIndex = 27;
            // 
            // tCashCard_Card
            // 
            this.tCashCard_Card.Location = new System.Drawing.Point(132, 74);
            this.tCashCard_Card.Name = "tCashCard_Card";
            this.tCashCard_Card.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F);
            this.tCashCard_Card.Properties.Appearance.Options.UseFont = true;
            this.tCashCard_Card.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.tCashCard_Card.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.tCashCard_Card.Properties.MaskSettings.Set("mask", "f");
            this.tCashCard_Card.Properties.NullText = "0";
            this.tCashCard_Card.Properties.UseMaskAsDisplayFormat = true;
            this.tCashCard_Card.Size = new System.Drawing.Size(176, 54);
            this.tCashCard_Card.TabIndex = 32;
            this.tCashCard_Card.EditValueChanged += new System.EventHandler(this.tCashCard_Card_EditValueChanged);
            // 
            // tCashCard_Cash
            // 
            this.tCashCard_Cash.Location = new System.Drawing.Point(132, 134);
            this.tCashCard_Cash.Name = "tCashCard_Cash";
            this.tCashCard_Cash.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 24F);
            this.tCashCard_Cash.Properties.Appearance.Options.UseFont = true;
            this.tCashCard_Cash.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            this.tCashCard_Cash.Properties.MaskSettings.Set("MaskManagerSignature", "allowNull=False");
            this.tCashCard_Cash.Properties.MaskSettings.Set("mask", "f");
            this.tCashCard_Cash.Properties.NullText = "0";
            this.tCashCard_Cash.Properties.UseMaskAsDisplayFormat = true;
            this.tCashCard_Cash.Size = new System.Drawing.Size(176, 54);
            this.tCashCard_Cash.TabIndex = 33;
            this.tCashCard_Cash.EditValueChanged += new System.EventHandler(this.tCashCard_Cash_EditValueChanged);
            // 
            // lCashCard_Message
            // 
            this.lCashCard_Message.Appearance.Font = new System.Drawing.Font("Nunito", 8.5F, System.Drawing.FontStyle.Bold);
            this.lCashCard_Message.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.lCashCard_Message.Appearance.Options.UseFont = true;
            this.lCashCard_Message.Appearance.Options.UseForeColor = true;
            this.lCashCard_Message.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lCashCard_Message.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lCashCard_Message.Location = new System.Drawing.Point(0, 115);
            this.lCashCard_Message.Margin = new System.Windows.Forms.Padding(1);
            this.lCashCard_Message.Name = "lCashCard_Message";
            this.lCashCard_Message.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lCashCard_Message.Size = new System.Drawing.Size(458, 21);
            this.lCashCard_Message.TabIndex = 29;
            this.lCashCard_Message.Text = "NAĞD ÖDƏNİŞ MƏBLƏĞİ AZ DAXİL EDİLMİŞDİR. MİNİMUM NAĞD ÖDƏNİŞ : ";
            this.lCashCard_Message.Visible = false;
            // 
            // lCash_Message
            // 
            this.lCash_Message.Appearance.Font = new System.Drawing.Font("Nunito", 8.5F, System.Drawing.FontStyle.Bold);
            this.lCash_Message.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.lCash_Message.Appearance.Options.UseFont = true;
            this.lCash_Message.Appearance.Options.UseForeColor = true;
            this.lCash_Message.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lCash_Message.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lCash_Message.Location = new System.Drawing.Point(0, 115);
            this.lCash_Message.Margin = new System.Windows.Forms.Padding(1);
            this.lCash_Message.Name = "lCash_Message";
            this.lCash_Message.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lCash_Message.Size = new System.Drawing.Size(458, 21);
            this.lCash_Message.TabIndex = 30;
            this.lCash_Message.Text = "NAĞD ÖDƏNİŞ MƏBLƏĞİ AZ DAXİL EDİLMİŞDİR. MİNİMUM ÖDƏNİŞ : ";
            this.lCash_Message.Visible = false;
            // 
            // fPrepaymentPay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(458, 136);
            this.Controls.Add(this.navigationFrame1);
            this.IconOptions.Image = global::WindowsFormsApp2.Properties.Resources.Mpos_png1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(460, 310);
            this.MinimumSize = new System.Drawing.Size(460, 170);
            this.Name = "fPrepaymentPay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ödəniş növü";
            this.Load += new System.EventHandler(this.fPrepaymentPay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame1)).EndInit();
            this.navigationFrame1.ResumeLayout(false);
            this.pageHome.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.pageCash.ResumeLayout(false);
            this.pageCash.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tCash_Paid.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCash_Balance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCash_Total.Properties)).EndInit();
            this.pageCashCard.ResumeLayout(false);
            this.pageCashCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tCashCard_Balance.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCashCard_Total.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCashCard_Card.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCashCard_Cash.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Navigation.NavigationFrame navigationFrame1;
        private DevExpress.XtraBars.Navigation.NavigationPage pageHome;
        private DevExpress.XtraBars.Navigation.NavigationPage pageCash;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.SimpleButton bCash;
        private DevExpress.XtraEditors.SimpleButton bCard;
        private DevExpress.XtraEditors.SimpleButton bCashCard;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit tCash_Total;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit tCash_Balance;
        private DevExpress.XtraEditors.SimpleButton bCash_Enter;
        private DevExpress.XtraBars.Navigation.NavigationPage pageCashCard;
        private DevExpress.XtraEditors.SimpleButton tCashCard_Enter;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit tCashCard_Balance;
        private DevExpress.XtraEditors.TextEdit tCashCard_Total;
        private DevExpress.XtraEditors.TextEdit tCash_Paid;
        private DevExpress.XtraEditors.TextEdit tCashCard_Card;
        private DevExpress.XtraEditors.TextEdit tCashCard_Cash;
        private DevExpress.XtraEditors.LabelControl lCashCard_Message;
        private DevExpress.XtraEditors.LabelControl lCash_Message;
    }
}