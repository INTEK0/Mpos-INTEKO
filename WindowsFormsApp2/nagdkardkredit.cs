using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class nagdkardkredit : DevExpress.XtraEditors.XtraForm
    {
        private readonly KREDITSATISLAYOUTSA frm1;
        public decimal h { get; set; }
        public nagdkardkredit(decimal a, KREDITSATISLAYOUTSA frm)
        {
            InitializeComponent();
            h = a;
            frm1 = frm;
        }

        private void nagdkardkredit_Load(object sender, EventArgs e)
        {
            textEdit1.Text = h.ToString();
            textEdit1.Enabled = false;
        }

        private void textEdit4_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textEdit4.Text))
            {
                //textEdit4.Text = "0";


            }
            else
            {

                getmebleg(textEdit1.Text, textEdit4.Text, textEdit3.Text);
            }
        }

        private void textEdit4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(textEdit4.Text))
            {
                // textEdit4.Text = "0,0";

            }
            else
            {
                getmebleg(textEdit1.Text, textEdit4.Text, textEdit3.Text);
            }
        }

        private void textEdit3_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textEdit3.Text))
            {
                textEdit3.Text = "0,0";

            }
            else
            {
                getmebleg(textEdit1.Text, textEdit4.Text, textEdit3.Text);
            }
        }

        private void textEdit3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(textEdit3.Text))
            {
                textEdit3.Text = "0";

            }
            else
            {
                getmebleg(textEdit1.Text, textEdit4.Text, textEdit3.Text);
            }

        }
        //public string ConString = "Data Source=.;Initial Catalog=NewInteko;Integrated Security=True";
        public void getmebleg(string paramValue, string paramValue1, string paramValue2)
        {


            string queryString = " exec  [dbo].[yekun_mebleg_nagd_kat]  @param1 =@pricePoint, @param2=@pricePoint1 ";
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand();
            SqlCommand command = new SqlCommand(queryString, connection);

            command.Parameters.AddWithValue("@pricePoint", paramValue);
            command.Parameters.AddWithValue("@pricePoint1", paramValue1);

            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {

                textEdit2.Text = dr["galig"].ToString();
                textEdit3.Text = dr["kart"].ToString();

            }
            connection.Close();
        }

        private void nagdkardkredit_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void textEdit4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //your function  
                //MessageBox.Show("enter");

                if (string.IsNullOrEmpty(textEdit4.Text) || string.IsNullOrEmpty(textEdit3.Text))
                {

                }
                else
                {
                    //frm1.gelen_data_negd_pos( Convert.ToDecimal(0.00), Convert.ToDecimal(textEdit4.Text));


                    string ka = textEdit3.Text.ToString();
                    string ne = textEdit4.Text.ToString();
                    string Ka_de = ka.Replace('.', ',');
                    string ne_de = ne.Replace('.', ',');

                    decimal n_ = Convert.ToDecimal(ne_de);
                    decimal k_ = Convert.ToDecimal(Ka_de);


                    string _um = textEdit1.Text;
                    string _um_old = _um.Replace('.', ',');
                    decimal total = Convert.ToDecimal(_um_old);


                    if ((n_ + k_) >= Convert.ToDecimal(textEdit1.Text))
                    {
                        //  MessageBox.Show(n_ + "/" + k_);
                        //yeri deyismemisden evvel   frm1.gelen_data_negd_pos(n_, k_,Convert.ToDecimal(_um_old));
                        frm1.gelen_data_negd_pos(n_, k_, total);
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

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}