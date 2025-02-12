using DevExpress.DashboardCommon;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using Microsoft.Win32;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using static WindowsFormsApp2.Helpers.Enums;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Reports;

namespace WindowsFormsApp2
{
    public partial class bank : DevExpress.XtraEditors.XtraForm
    {
        private readonly POS_LAYOUT_NEW frm1;

        // public string ConString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        public decimal g { get; set; }

        decimal _total = default,
              _odenilenNagd = default,
              _qaliq = default;


        public bank(decimal A, POS_LAYOUT_NEW frm)
        {
            InitializeComponent();
            g = A;
            frm1 = frm;
        }

        private void nagd_Load(object sender, EventArgs e)
        {
            // MessageBox.Show(g.ToString());
            tTotal.Text = g.ToString();
            tPaid.Text = g.ToString();
            tTotal.Enabled = false;
            ClinicModule();
        }

        private void ClinicModule()
        {
            //bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("ClinicModule").ToString());
            //if (control)
            //{
            //    chClinicModul.Visible = true;
            //    this.MaximumSize = new System.Drawing.Size(460, 270);
            //    this.MinimumSize = new System.Drawing.Size(460, 270);
            //    this.Size = new System.Drawing.Size(460, 270);
            //}
            //else
            //{
            //    chClinicModul.Visible = false;
            //    this.MaximumSize = new System.Drawing.Size(460, 230);
            //    this.MinimumSize = new System.Drawing.Size(460, 230);
            //    this.Size = new System.Drawing.Size(460, 230);
            //}
        }

        public void getmebleg(string paramValue, string paramValue1)
        {
            string queryString = " exec  yekun_mebleg_nagd  @param1 =@pricePoint, @param2=@pricePoint1";
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand();
            SqlCommand command = new SqlCommand(queryString, connection);

            command.Parameters.AddWithValue("@pricePoint", paramValue);
            command.Parameters.AddWithValue("@pricePoint1", paramValue1);

            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {

                tQaliq.Text = dr["galig"].ToString();

            }
            connection.Close();
        }

        private void textEdit4_TextChanged(object sender, EventArgs e)
        {
            Calc();
            //if (string.IsNullOrEmpty(tPaid.Text))
            //{
            //    // textEdit4.Text = "0.0";

            //}
            //else
            //{
            //    //getmebleg(tTotal.Text, tPaid.Text);
            //}
        }

        private void Calc()
        {
            decimal total = Convert.ToDecimal(tTotal.EditValue);
            decimal odenilenNagd = Convert.ToDecimal(tPaid.EditValue);
            decimal qaliq = Convert.ToDecimal(tQaliq.EditValue);



            if (odenilenNagd >= total)
            {
                qaliq = odenilenNagd - total;
            }
            else
            {
                qaliq = 0;
            }

            tPaid.EditValue = odenilenNagd;
            tQaliq.EditValue = qaliq;


            _total = total;
            _odenilenNagd = odenilenNagd;
            _qaliq = qaliq;
        }

        private void textEdit4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(tPaid.Text))
            {
                tQaliq.Text = "0";
            }
            else
            {
                getmebleg(tTotal.Text, tPaid.Text);
            }
        }

        private void nagd_FormClosing(object sender, FormClosingEventArgs e)
        {
            ///


        }

        private void textEdit4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(tPaid.Text))
                {
                    Sales();
                }
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void bEnter_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!string.IsNullOrEmpty(tPaid.Text))
            {
                Sales();
            }
            Cursor.Current = Cursors.Default;
            this.Close();
        }

        private void Sales()
        {
            string cash_ = tPaid.Text;
            string cash_old = cash_.Replace('.', ',');

            string _umumi_mebleg = tTotal.Text;
            string umumi_mebleb_old = _umumi_mebleg.Replace('.', ',');

            if (Convert.ToDecimal(cash_old) >= Convert.ToDecimal(tTotal.Text))
            {
                frm1.gelen_data_negd_pos(_total, Convert.ToDecimal(0.00), _total, _odenilenNagd, _qaliq, chClinicModul.Checked);
            }
            else
            {
                XtraMessageBox.Show("ÖDƏNİLƏN MƏBLƏĞ AZDIR");
            }
        }
    }
}