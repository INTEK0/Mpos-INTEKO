using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using static DevExpress.Xpo.DB.DataStoreLongrunnersWatch;

namespace WindowsFormsApp2
{
    public partial class nagd_kart : DevExpress.XtraEditors.XtraForm
    {
        private readonly POS_LAYOUT_NEW frm1;
        private decimal h { get; }

        decimal _total = default,
                _cash = default,
                _card = default,
                _odenilenNagd = default,
                _qaliq = default;

        public nagd_kart(decimal a, POS_LAYOUT_NEW frm)
        {
            InitializeComponent();
            h = a;
            frm1 = frm;
        }

        private void ClinicModule()
        {
            bool control = Convert.ToBoolean(Registry.CurrentUser.OpenSubKey("Mpos").GetValue("ClinicModule").ToString());
            if (control)
            {
                chClinicModul.Visible = true;
                this.MaximumSize = new System.Drawing.Size(430, 380);
                this.MinimumSize = new System.Drawing.Size(430, 380);
                this.Size = new System.Drawing.Size(430, 380);
            }
            else
            {
                chClinicModul.Visible = false;
                this.MaximumSize = new System.Drawing.Size(430, 350);
                this.MinimumSize = new System.Drawing.Size(430, 350);
                this.Size = new System.Drawing.Size(430, 350);
            }
        }

        private void nagd_kart_Load(object sender, EventArgs e)
        {
            ClinicModule();
            tTotal.Text = h.ToString();
            tTotal.Enabled = false;
        }

        private void textEdit4_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(tCash.Text))
            //{
            //    //textEdit4.Text = "0";


            //}
            //else
            //{
            //    getmebleg(tTotal.Text, tCash.Text, tCard.Text);
            //}
        }

        private void Calc()
        {
            decimal total = Convert.ToDecimal(tTotal.EditValue);
            decimal cash = Convert.ToDecimal(tCash.EditValue);
            decimal card = Convert.ToDecimal(tCard.EditValue);
            decimal odenilenNagd = Convert.ToDecimal(tOdenilenNagd.EditValue);
            decimal qaliq = Convert.ToDecimal(tQaliq.EditValue);



            cash = (card - total) * -1;
            if (odenilenNagd >= cash)
            {
                qaliq = odenilenNagd - cash;
            }
            else
            {
                qaliq = 0;
                //odenilenNagd = cash;
            }

            tCash.EditValue = cash;
            tCard.EditValue = card;
            tOdenilenNagd.EditValue = odenilenNagd;
            tQaliq.EditValue = qaliq;


            _total = total;
            _cash = cash;
            _card = card;
            _odenilenNagd = odenilenNagd;
            _qaliq = qaliq;
        }

        private void textEdit4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(tCash.Text))
            {
                // textEdit4.Text = "0,0";

            }
            else
            {
                getmebleg(tTotal.Text, tCash.Text, tCard.Text);
            }
        }




        public void getmebleg(string paramValue, string paramValue1, string paramValue2)
        {
            //string queryString = " exec  [dbo].[yekun_mebleg_nagd_kat]  @param1 =@pricePoint, @param2=@pricePoint1 ";
            //SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            //SqlCommand cmd = new SqlCommand();
            //SqlCommand command = new SqlCommand(queryString, connection);

            //command.Parameters.AddWithValue("@pricePoint", paramValue);
            //command.Parameters.AddWithValue("@pricePoint1", paramValue1);

            //connection.Open();
            //SqlDataReader dr = command.ExecuteReader();
            //while (dr.Read())
            //{

            //    tQaliq.Text = dr["galig"].ToString();
            //    tCard.Text = dr["kart"].ToString();

            //}
            //connection.Close();
        }

        private void textEdit4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //your function  
                //MessageBox.Show("enter");

                if (string.IsNullOrEmpty(tCash.Text) || string.IsNullOrEmpty(tCard.Text))
                {

                }
                else
                {
                    //frm1.gelen_data_negd_pos( Convert.ToDecimal(0.00), Convert.ToDecimal(textEdit4.Text));


                    string ka = tCard.Text.ToString();
                    string ne = tCash.Text.ToString();
                    string Ka_de = ka.Replace('.', ',');
                    string ne_de = ne.Replace('.', ',');

                    decimal n_ = Convert.ToDecimal(ne_de);
                    decimal k_ = Convert.ToDecimal(Ka_de);


                    string _um = tTotal.Text;
                    string _um_old = _um.Replace('.', ',');


                    if ((n_ + k_) >= Convert.ToDecimal(tTotal.Text))
                    {
                        //  MessageBox.Show(n_ + "/" + k_);
                        //yeri deyismemisden evvel   frm1.gelen_data_negd_pos(n_, k_,Convert.ToDecimal(_um_old));
                        frm1.gelen_data_negd_pos(n_, k_, n_);
                        this.Close();
                    }
                    else
                    {
                        XtraMessageBox.Show("ÖDƏNİLƏN MƏBLƏĞ AZDIR");
                    }

                }

            }

            if (e.KeyCode == Keys.Escape)
            {

                this.Close();
            }
        }

        private void bEnter_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tOdenilenNagd.Text))
            {
                decimal cash = Decimal.Parse(tOdenilenNagd.Text);
                if (cash > 0)
                {
                    Sales();
                }
                else
                {
                    FormHelpers.Alert("Nağd məbləğ daxil edilmədi", Enums.MessageType.Error);
                    return;
                }
            }
        }

        void Sales()
        {
            Cursor.Current = Cursors.WaitCursor;

            string ka = tCard.Text;
            string ne = tCash.Text;
            string Ka_de = ka.Replace('.', ',');
            string ne_de = ne.Replace('.', ',');

            decimal cash = Convert.ToDecimal(ne_de);
            decimal card = Convert.ToDecimal(Ka_de);


            string _um = tTotal.Text;
            string _um_old = _um.Replace('.', ',');
            decimal total = Convert.ToDecimal(_um_old);

            if ((cash + card) >= Convert.ToDecimal(tTotal.Text))
            {
                frm1.gelen_data_negd_pos(cash, card, total, _odenilenNagd, _qaliq, chClinicModul.Checked);
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("ÖDƏNİLƏN MƏBLƏĞ AZDIR");
            }
            Cursor.Current = Cursors.Default;

        }

        private void tCash_EditValueChanged(object sender, EventArgs e)
        {
            Calc();

        }

        private void tOdenilenNagd_EditValueChanged(object sender, EventArgs e)
        {
            Calc();
        }

        private void tCard_EditValueChanged(object sender, EventArgs e)
        {
            Calc();
        }
    }
}