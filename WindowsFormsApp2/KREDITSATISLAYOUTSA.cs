using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using WindowsFormsApp2.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.NKA;
using WindowsFormsApp2.Validations;
using static WindowsFormsApp2.Helpers.Enums;

namespace WindowsFormsApp2
{
    public partial class KREDITSATISLAYOUTSA : DevExpress.XtraEditors.XtraForm
    {
        public static string radio = "";
        public static string mal_alisi_details_id = "0";
        public static string edv = "";
        public static string anbargalig = "0";
        private string customerId;
        private string zaminId;

        private DataTable dt;
        private SqlDataAdapter da;

        public KREDITSATISLAYOUTSA()
        {
            InitializeComponent();
        }

        private void KREDITSATISLAYOUTSA_Load(object sender, EventArgs e)
        {
            InitLookUpEdit_();
            DateTime dateTime = DateTime.UtcNow.Date;
            dateEdit1.Text = dateTime.ToShortDateString();
            tProccessNo.Text = DbProsedures.GET_CreditSaleProccessNo();
            lCashier.Text = DbProsedures.GetUser().NameSurname;
            listView1.Visible = false;
            get_ip_model();
        }

        public void techizatci_axtar(string techizatci_adi, string Mehsul_ad, string satis_giymeti,
            int mal_Details, string anbar_galig, string _edv_)
        {
            tSupplier.Text = techizatci_adi;
            tProductName.Text = Mehsul_ad;
            tSalePrice.Text = satis_giymeti;
            mal_alisi_details_id = mal_Details.ToString();
            anbargalig = anbar_galig;
            edv = _edv_;

            evdkontrol(_edv_);
        }

        public void MUSTERI(string ID, string MUSTERI_AD)
        {
            customerId = ID;
            tCustomerName.Text = MUSTERI_AD;
        }

        public void ZAMIN(string ID, string ZAMIN_AD)
        {
            zaminId = ID;
            tZamin.Text = ZAMIN_AD;
        }

        private void evdkontrol(string edvs)
        {
            string query = $"SELECT  [EDV_ID] FROM  [VERGI_DERECESI] WHERE [EDV] = N'{edvs}'";
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                object result = cmd.ExecuteScalar();
                label9.Text = result != null ? result.ToString() : "";
            }
        }

        private void getmebleg(string paramValue, string paramValue1, string paramValue2, string paramValue3)
        {
            string queryString = " exec yekun_mebleg_calc @migdar =@pricePoint,@alis_giymet =@pricePoint1,@endirim_faiz =@pricePoint2,@endirim_azn =@pricePoint3";
            SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlCommand command = new SqlCommand(queryString, connection);

            command.Parameters.AddWithValue("@pricePoint", paramValue);
            command.Parameters.AddWithValue("@pricePoint1", paramValue1);
            command.Parameters.AddWithValue("@pricePoint2", paramValue2);
            command.Parameters.AddWithValue("@pricePoint3", paramValue3);
            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                tDiscountTotalAmount.Text = dr["endirim_meblegi"].ToString();
                tYekunMebleg.Text = dr["yekun_mebleg"].ToString();
            }
            connection.Close();
        }

        private void textEdit7_TextChanged_1(object sender, EventArgs e)
        {
            tDiscountAmount.Text = "0.00";
            getmebleg(tQuantity.Text, tSalePrice.Text, tDiscountPercent.Text, tDiscountAmount.Text);
        }

        private void textEdit13_TextChanged_1(object sender, EventArgs e)
        {
            getmebleg(tQuantity.Text, tSalePrice.Text, tDiscountPercent.Text, tDiscountAmount.Text);
        }

        private void textEdit8_TextChanged_1(object sender, EventArgs e)
        {
            getmebleg(tQuantity.Text, tSalePrice.Text, tDiscountPercent.Text, tDiscountAmount.Text);
        }

        private void textEdit6_TextChanged_1(object sender, EventArgs e)
        {
            getmebleg(tQuantity.Text, tSalePrice.Text, tDiscountPercent.Text, tDiscountAmount.Text);
        }

        private void clear()
        {
            tProductName.Text = "";
            tYekunMebleg.Text = "";
            tSupplier.Text = "";
            tZamin.Text = "";
            tCustomerName.Text = "";
            tSalePrice.Text = "";
            tDiscountPercent.Text = "";
            tDiscountAmount.Text = "";
            tDiscountTotalAmount.Text = "";
            textEdit17.Text = "";
            textEdit1.Text = "";
            tIllikFaiz.Text = "";
            tKomissiyaMeblegi.Text = "0";
            tIlkinOdenis.Text = "";
            tAyliqOdenis.Text = "";
            tTotal.Text = "";
            textEdit24.Text = "";
            textEdit25.Text = "";
            tIlkinOdenisdenSonraQaliq.Text = "";
            tMuddetAy.Text = "";
            tIllikFaiz.Text = "";
            tAyliqOdenis.Text = "";
            tTotal.Text = "";
            textEdit1.Text = "";
            textEdit11.Text = "";
            tContractNo.Text = "";
            tProccessNo.Text = "";
            memoEdit1.Text = "";
            listView1.Columns.Clear();
            listView1.Items.Clear();

            tProccessNo.Text = DbProsedures.GET_CreditSaleProccessNo();
        }

        private async void CreditSaleMain(DatabaseClasses.CreditMain data, string longFiscalId, string shortFiscalId, string receiptNo)
        {
            data.LonfFiskalId = longFiscalId;
            data.ShortFiskalId = shortFiscalId;
            data.ReceiptNo = receiptNo;
            int id = await DbProsedures.Insert_CreditMain(data);

            DbProsedures.InsertCustomerDebt(CustomerDebtType.CreditSale,
                DateTime.Now,
                Convert.ToInt32(data.CustomerId),
                Convert.ToDecimal(data.OdenilenMebleg));

            await CreditMonthAdd(id, longFiscalId);

            FormHelpers.Log($"{tProductName.Text} məhsulu {tContractNo.Text} müqavilə nömrəsinə əsasən kredit satışı ilə satıldı.");
            //krediprint();
            RestartForm();
        }

        private void RestartForm()
        {
            this.Hide();
            XtraForm newForm = (XtraForm)Activator.CreateInstance(this.GetType());
            newForm.StartPosition = this.StartPosition;
            newForm.Location = this.Location;
            newForm.Show();
            this.Close();
        }

        private async Task CreditMonthAdd(int creditMainId, string fiscalId)
        {
            int month = Convert.ToInt32(tMuddetAy.Text);
            for (int i = 1; i <= month; i++)
            {
                int j = 30 * i;
                DatabaseClasses.CreditSaleMonth item = new DatabaseClasses.CreditSaleMonth()
                {
                    CreditSaleId = creditMainId,
                    Month = i,
                    PaymentDay = DateTime.Today.AddDays(j),
                    Amount = Convert.ToDecimal(tAyliqOdenis.Text),
                    CreditSaleFiscalId = fiscalId,
                };

                await DbProsedures.Insert_CreditMonth(item);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            MUSTERIAXTARKREDIT M = new MUSTERIAXTARKREDIT(this);
            M.ShowDialog();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            TECHIZATCIKREDIT T = new TECHIZATCIKREDIT(this);
            T.ShowDialog();
        }

        private List<Account> datasource_;

        private void InitLookUpEdit_()
        {
            datasource_ = new List<Account>();
            datasource_.Add(new Account("MƏRKƏZ OBYEKT") { ID = 1002 });
            lookUpEdit1.Properties.DataSource = datasource_;
            lookUpEdit1.Properties.DisplayMember = "MƏRKƏZ OBYEKT";
            lookUpEdit1.Properties.ValueMember = "4";
        }

        private void KREDITSATISLAYOUTSA_Shown(object sender, EventArgs e)
        {
            if (datasource_.Count == 1)
                lookUpEdit1.EditValue = datasource_[0].ID;
        }

        private void textEdit4_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tYekunMebleg.Text))
            {
                getmebleg_(tYekunMebleg.Text.ToString(), edv);
            }
        }

        public void getmebleg_(string paramValue, string paramValue1)
        {
            string queryString = " exec mehsul_alisi_edv @yekun_mebleg_=@pricePoint,@vergi_derece =@pricePoint1";
            SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlCommand command = new SqlCommand(queryString, connection);

            command.Parameters.AddWithValue("@pricePoint", paramValue);
            command.Parameters.AddWithValue("@pricePoint1", paramValue1);

            connection.Open();
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                textEdit1.Text = dr["vergisiz"].ToString();
                textEdit11.Text = dr["vergi"].ToString();
            }
            connection.Close();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            ZAMINKREDIT M = new ZAMINKREDIT(this);
            M.ShowDialog();
        }

        private void textEdit20_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double komisya = Math.Round(((Convert.ToDouble(tYekunMebleg.Text) - Convert.ToDouble(tIlkinOdenis.Text)) * (Convert.ToDouble(tKomissiyaMeblegi.Text) / 100)), 2);
                double faizh = Math.Round(((Convert.ToDouble(tIlkinOdenisdenSonraQaliq.Text) / 100) * (Convert.ToDouble(tIllikFaiz.Text))) * Convert.ToInt32(tMuddetAy.Text), 2);
                textEdit17.Text = Math.Round(faizh, 2).ToString();
                double kreditmebl = Convert.ToDouble(tIlkinOdenisdenSonraQaliq.Text);
                double tota = Math.Round(faizh, 2) + kreditmebl;
                double aylikfaizsiz = kreditmebl / Convert.ToInt32(tMuddetAy.Text);

                double yilfaiz = Math.Round((Convert.ToDouble(tYekunMebleg.Text) + komisya - Convert.ToDouble(tIlkinOdenis.Text)) * (Convert.ToDouble(tIllikFaiz.Text) / 100) / 12, 2);
                double krediteverilenmeblag = Convert.ToDouble(tYekunMebleg.Text) - Convert.ToDouble(tIlkinOdenis.Text) + komisya + (yilfaiz * Convert.ToInt32(tMuddetAy.Text));

                tAyliqOdenis.Text = (Math.Round((tota / Convert.ToInt32(tMuddetAy.Text)), 2)).ToString();
                tTotal.Text = Math.Round(tota, 2).ToString();
                double edvderece = Math.Round(Convert.ToDouble(textEdit11.Text) / Convert.ToDouble(textEdit1.Text), 2);

                double yekuna = tota;

                textEdit1.Text = tota.ToString();
                textEdit11.Text = (tota * 0.18).ToString();
                listView1.Visible = true;
                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.View = View.Details;

                //ListView'e ızgaralı görünüm kazandırır. (Excel tablosu gibi.)
                listView1.GridLines = true;
                listView1.Columns.Add("NO", 50);
                listView1.Columns.Add("QRAFİK ÜZRƏ ÖDƏNİŞ TARİXİ", 100);
                listView1.Columns.Add("ƏSAS MƏBLƏĞDƏN QALIQ", 100);
                //listView1.Columns.Add("AYLIQ ƏSAS MƏBLƏĞ", 100);
                //listView1.Columns.Add("AYLIQ FAİZ", 100);
                listView1.Columns.Add("AYLIQ CƏM ÖDƏNİŞ", 100);
                listView1.Columns.Add("QALIQ MƏBLƏĞ", 100);
                double aylikfaiztut = faizh / Convert.ToInt32(tMuddetAy.Text);
                int countay = Convert.ToInt32(tMuddetAy.Text);
                DateTime bugun = DateTime.Today;
                for (int counti = 1; counti <= countay; counti++)
                {
                    int j = 30 * counti;
                    double hesap1 = kreditmebl - ((counti - 1) * Math.Round(aylikfaizsiz, 2));

                    double hesap2 = Convert.ToDouble(tTotal.Text) - (Convert.ToDouble(tAyliqOdenis.Text) * counti);

                    listView1.Items.Add(counti.ToString());
                    listView1.Items[counti - 1].SubItems.Add(DateTime.Today.AddMonths(counti).ToString("dd.MM.yyyy"));
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(hesap1, 2).ToString());
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(aylikfaizsiz, 2).ToString());
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(aylikfaiztut, 2).ToString());
                    listView1.Items[counti - 1].SubItems.Add(tAyliqOdenis.Text);
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(hesap2, 2).ToString());
                }
                tProccessNo.Text = DbProsedures.GET_CreditSaleProccessNo();
                listView1.Visible = true;
            }
            catch
            {
            }
        }

        private void textEdit21_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (tIlkinOdenis.Text == "")
                {
                    tIlkinOdenis.Text = "0";

                    tIlkinOdenisdenSonraQaliq.Text = textEdit25.Text;
                }
                else if (Convert.ToDouble(tIlkinOdenis.Text) < 1)
                {
                    tIlkinOdenis.Text = "0";
                    double netsa = Convert.ToDouble(textEdit25.Text);
                    double ilksa = Convert.ToDouble(tIlkinOdenis.Text);

                    double totala = netsa;
                    tIlkinOdenisdenSonraQaliq.Text = totala.ToString();
                }
                else
                {
                    if (Convert.ToDecimal(tIlkinOdenis.Text) >= Convert.ToDecimal(tYekunMebleg.Text))
                    {
                        tIlkinOdenis.Text = "0";
                        double netsa = Convert.ToDouble(textEdit25.Text);
                        double ilksa = Convert.ToDouble(tIlkinOdenis.Text);

                        double totala = netsa - ilksa;
                        tIlkinOdenisdenSonraQaliq.Text = totala.ToString();

                    }
                    else
                    {
                        double netsa = Convert.ToDouble(textEdit25.Text);
                        double ilksa = Convert.ToDouble(tIlkinOdenis.Text);
                        double totala = netsa - ilksa;
                        tIlkinOdenisdenSonraQaliq.Text = totala.ToString();
                    }
                }
            }
            catch
            {
                XtraMessageBox.Show("Xəta!\n" + e);
            }
        }

        private void get_ip_model()
        {
            var data = FormHelpers.GetIpModel();

            lModel.Text = data.Model;
            lIpAdress.Text = data.Ip;
            lMerchantId.Text = data.MerchantId;
            lBankName.Text = data.BankName;
        }

        private string get_vahid()
        {
            string query = $"SELECT [VAHID] FROM [MAL_ALISI_DETAILS] where [MAL_ALISI_DETAILS_ID]={mal_alisi_details_id}";

            using (SqlConnection conn = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        return dr["VAHID"].ToString();
                    else
                        return null;
                }
            }
        }

        private void textEdit19_EditValueChanged(object sender, EventArgs e)
        {
            double kommisyah = Math.Round(Convert.ToDouble(tYekunMebleg.Text) * Convert.ToDouble(tKomissiyaMeblegi.Text) / 100, 2);
            textEdit24.Text = kommisyah.ToString();
            double totalkom = kommisyah + Math.Round(Convert.ToDouble(tYekunMebleg.Text), 2);
            textEdit25.Text = totalkom.ToString();

            double netsa = Convert.ToDouble(textEdit25.Text);
            double ilksa = Convert.ToDouble(tIlkinOdenis.Text);

            double totala = netsa - ilksa;
            tIlkinOdenisdenSonraQaliq.Text = totala.ToString();
        }

        private void Hesapla_Click(object sender, EventArgs e)
        {
            try
            {
                double kommisyah = Math.Round(Convert.ToDouble(tYekunMebleg.Text) * Convert.ToDouble(tKomissiyaMeblegi.Text) / 100, 2);
                textEdit24.Text = kommisyah.ToString();
                double totalkom = kommisyah + Math.Round(Convert.ToDouble(tYekunMebleg.Text), 2);
                textEdit25.Text = totalkom.ToString();

                double netsa = Convert.ToDouble(textEdit25.Text);
                double ilksa = Convert.ToDouble(tIlkinOdenis.Text);

                double totala = netsa - ilksa;
                tIlkinOdenisdenSonraQaliq.Text = totala.ToString();

                if (tIlkinOdenis.Text == "")
                {
                    tIlkinOdenis.Text = "0";

                    tIlkinOdenisdenSonraQaliq.Text = textEdit25.Text;
                }
                else if (Convert.ToDouble(tIlkinOdenis.Text) < 1)
                {
                    tIlkinOdenis.Text = "0";
                    double netsa2 = Convert.ToDouble(textEdit25.Text);
                    double ilksa2 = Convert.ToDouble(tIlkinOdenis.Text);

                    double totala2 = netsa2;
                    tIlkinOdenisdenSonraQaliq.Text = totala.ToString();
                }
                else
                {
                    if (Convert.ToDecimal(tIlkinOdenis.Text) >= Convert.ToDecimal(tYekunMebleg.Text))
                    {
                        tIlkinOdenis.Text = "0";
                        double netsa2 = Convert.ToDouble(textEdit25.Text);
                        double ilksa2 = Convert.ToDouble(tIlkinOdenis.Text);
                        double totala2 = netsa2 - ilksa2;
                        tIlkinOdenisdenSonraQaliq.Text = totala2.ToString();
                    }
                    else
                    {
                        double netsa2 = Convert.ToDouble(textEdit25.Text);
                        double ilksa2 = Convert.ToDouble(tIlkinOdenis.Text);
                        double totala2 = netsa2 - ilksa2;
                        tIlkinOdenisdenSonraQaliq.Text = totala2.ToString();
                    }
                }

                double komisya = Math.Round(((Convert.ToDouble(tYekunMebleg.Text) - Convert.ToDouble(tIlkinOdenis.Text)) * (Convert.ToDouble(tKomissiyaMeblegi.Text) / 100)), 2);
                double faizh = Math.Round(((Convert.ToDouble(tIlkinOdenisdenSonraQaliq.Text) / 100) * (Convert.ToDouble(tIllikFaiz.Text))) * Convert.ToInt32(tMuddetAy.Text), 2);
                textEdit17.Text = Math.Round(faizh, 2).ToString();
                double kreditmebl = Convert.ToDouble(tIlkinOdenisdenSonraQaliq.Text);
                double tota = Math.Round(faizh, 2) + kreditmebl;
                double aylikfaizsiz = kreditmebl / Convert.ToInt32(tMuddetAy.Text);

                double yilfaiz = Math.Round(((Convert.ToDouble(tYekunMebleg.Text) + komisya - Convert.ToDouble(tIlkinOdenis.Text)) * (Convert.ToDouble(tIllikFaiz.Text) / 100)) / 1, 2);
                double krediteverilenmeblag = Convert.ToDouble(tYekunMebleg.Text) - Convert.ToDouble(tIlkinOdenis.Text) + komisya + (yilfaiz * Convert.ToInt32(tMuddetAy.Text));

                tAyliqOdenis.Text = (Math.Round((tota / Convert.ToInt32(tMuddetAy.Text)), 2)).ToString();
                tTotal.Text = Math.Round(tota, 2).ToString();
                double edvderece = Math.Round(Convert.ToDouble(textEdit11.Text) / Convert.ToDouble(textEdit1.Text), 2);

                double yekuna = tota;

                textEdit1.Text = tota.ToString();
                textEdit11.Text = (tota * 0.18).ToString();
                listView1.Visible = true;
                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.View = View.Details;

                //ListView'e ızgaralı görünüm kazandırır. (Excel tablosu gibi.)
                listView1.GridLines = true;
                listView1.Columns.Add("NO", 50);
                listView1.Columns.Add("QRAFİK ÜZRƏ ÖDƏNİŞ TARİXİ", 100);
                listView1.Columns.Add("ƏSAS MƏBLƏĞDƏN QALIQ", 100);
                //listView1.Columns.Add("AYLIQ ƏSAS MƏBLƏĞ", 100);
                //listView1.Columns.Add("AYLIQ FAİZ", 100);
                listView1.Columns.Add("AYLIQ CƏM ÖDƏNİŞ", 100);
                listView1.Columns.Add("QALIQ MƏBLƏĞ", 100);
                double aylikfaiztut = faizh / Convert.ToInt32(tMuddetAy.Text);
                int countay = Convert.ToInt32(tMuddetAy.Text);
                DateTime bugun = DateTime.Today;
                for (int counti = 1; counti <= countay; counti++)
                {
                    int j = 30 * counti;
                    double hesap1 = kreditmebl - ((counti - 1) * Math.Round(aylikfaizsiz, 2));
                    double hesap2 = Convert.ToDouble(tTotal.Text) - (Convert.ToDouble(tAyliqOdenis.Text) * counti);

                    listView1.Items.Add(counti.ToString());
                    listView1.Items[counti - 1].SubItems.Add(DateTime.Today.AddMonths(counti).ToString("dd.MM.yyyy"));
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(hesap1, 2).ToString());
                    listView1.Items[counti - 1].SubItems.Add(tAyliqOdenis.Text);
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(hesap2, 2).ToString());
                }

                tProccessNo.Text = DbProsedures.GET_CreditSaleProccessNo();
                listView1.Visible = true;
            }
            catch
            {
            }
        }

        private void textEdit4_EditValueChanged(object sender, EventArgs e)
        {
            double kommisyah = Math.Round(Convert.ToDouble(tYekunMebleg.Text) * Convert.ToDouble(tKomissiyaMeblegi.Text) / 100, 2);
            textEdit24.Text = kommisyah.ToString();
            double totalkom = kommisyah + Math.Round(Convert.ToDouble(tYekunMebleg.Text), 2);
            textEdit25.Text = totalkom.ToString();

            double netsa = Convert.ToDouble(textEdit25.Text);
            double ilksa = Convert.ToDouble(tIlkinOdenis.Text);

            double totala = netsa - ilksa;
            tIlkinOdenisdenSonraQaliq.Text = totala.ToString();
        }

        private void textEdit26_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double komisya = Math.Round(((Convert.ToDouble(tYekunMebleg.Text) - Convert.ToDouble(tIlkinOdenis.Text)) * (Convert.ToDouble(tKomissiyaMeblegi.Text) / 100)), 2);
                double faizh = Math.Round(((Convert.ToDouble(tIlkinOdenisdenSonraQaliq.Text) / 100) * (Convert.ToDouble(tIllikFaiz.Text))) * Convert.ToInt32(tMuddetAy.Text), 2);
                textEdit17.Text = Math.Round(faizh, 2).ToString();
                double kreditmebl = Convert.ToDouble(tIlkinOdenisdenSonraQaliq.Text);
                double tota = Math.Round(faizh, 2) + kreditmebl;
                double aylikfaizsiz = kreditmebl / Convert.ToInt32(tMuddetAy.Text);

                double yilfaiz = Math.Round(((Convert.ToDouble(tYekunMebleg.Text) + komisya - Convert.ToDouble(tIlkinOdenis.Text)) * (Convert.ToDouble(tIllikFaiz.Text) / 100)) / 12, 2);
                double krediteverilenmeblag = Convert.ToDouble(tYekunMebleg.Text) - Convert.ToDouble(tIlkinOdenis.Text) + komisya + (yilfaiz * Convert.ToInt32(tMuddetAy.Text));

                tAyliqOdenis.Text = (Math.Round((tota / Convert.ToInt32(tMuddetAy.Text)), 2)).ToString();
                tTotal.Text = Math.Round(tota, 2).ToString();
                double edvderece = Math.Round(Convert.ToDouble(textEdit11.Text) / Convert.ToDouble(textEdit1.Text), 2);

                double yekuna = tota;

                textEdit1.Text = tota.ToString();
                textEdit11.Text = (tota * 0.18).ToString();
                listView1.Visible = true;
                listView1.Columns.Clear();
                listView1.Items.Clear();
                listView1.View = View.Details;

                //ListView'e ızgaralı görünüm kazandırır. (Excel tablosu gibi.)
                listView1.GridLines = true;
                listView1.Columns.Add("NO", 50);
                listView1.Columns.Add("QRAFİK ÜZRƏ ÖDƏNİŞ TARİXİ", 100);
                listView1.Columns.Add("ƏSAS MƏBLƏĞDƏN QALIQ", 100);
                listView1.Columns.Add("AYLIQ CƏM ÖDƏNİŞ", 100);
                listView1.Columns.Add("QALIQ MƏBLƏĞ", 100);
                double aylikfaiztut = faizh / Convert.ToInt32(tMuddetAy.Text);
                int countay = Convert.ToInt32(tMuddetAy.Text);
                DateTime bugun = DateTime.Today;
                for (int counti = 1; counti <= countay; counti++)
                {
                    int j = 30 * counti;
                    double hesap1 = kreditmebl - ((counti - 1) * Math.Round(aylikfaizsiz, 2));

                    double hesap2 = Convert.ToDouble(tTotal.Text) - (Convert.ToDouble(tAyliqOdenis.Text) * counti);


                    listView1.Items.Add(counti.ToString());
                    listView1.Items[counti - 1].SubItems.Add(DateTime.Today.AddMonths(counti).ToString("dd.MM.yyyy"));
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(hesap1, 2).ToString());
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(aylikfaizsiz, 2).ToString());
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(aylikfaiztut, 2).ToString());
                    listView1.Items[counti - 1].SubItems.Add(tAyliqOdenis.Text);
                    listView1.Items[counti - 1].SubItems.Add(Math.Round(hesap2, 2).ToString());



                }

                tProccessNo.Text = DbProsedures.GET_CreditSaleProccessNo();
                listView1.Visible = true;
            }
            catch
            {

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tMuddetAy.EditValue = cmbMonth.SelectedItem.ToString();
        }

        public void krediprint()
        {
            DialogResult pdr = printDialog1.ShowDialog();
            if (pdr == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            double totalmeblag = Convert.ToDouble(textEdit1.Text) + Convert.ToDouble(tIlkinOdenis.Text);
            Font myFont = new Font("Calibri", 18);
            Font myFont2 = new Font("Calibri", 12);
            Font myFont3 = new Font("Calibri", 10);
            SolidBrush sbrush = new SolidBrush(Color.Black);
            Pen myPen = new Pen(Color.Black);

            //Bu kısımda sipariş formu yazısını ve çizgileri yazdırıyorum
            e.Graphics.DrawString("Tarix:" + DateTime.Now.ToString("dd/MM/yyyy"), myFont3, sbrush, 600, 80);

            e.Graphics.DrawLine(myPen, 50, 100, 750, 100);
            e.Graphics.DrawLine(myPen, 50, 160, 750, 160);

            e.Graphics.DrawString("KREDİT SATIŞI", myFont, sbrush, 270, 120);
            e.Graphics.DrawString("KASSİR:  " + lCashier.Text, myFont3, sbrush, 50, 180);
            e.Graphics.DrawString("Müqavilə №:  " + tContractNo.Text, myFont3, sbrush, 50, 200);
            e.Graphics.DrawString("MÜŞTƏRİ ADI:  " + tCustomerName.Text, myFont3, sbrush, 250, 200);
            e.Graphics.DrawString("MƏHSUL ADI:  " + tProductName.Text, myFont3, sbrush, 50, 220);
            e.Graphics.DrawString("KREDİT MÜDDƏTİ(AY):  " + tMuddetAy.Text, myFont3, sbrush, 50, 240);
            e.Graphics.DrawString("İLKİN ÖDƏNİŞ:  " + Convert.ToDouble(tIlkinOdenis.Text).ToString(), myFont3, sbrush, 50, 260);
            e.Graphics.DrawString("KREDİT MƏBLƏĞ:  " + textEdit1.Text, myFont3, sbrush, 50, 280);
            e.Graphics.DrawString("TOPLAM MƏBLƏĞ:  " + totalmeblag.ToString(), myFont3, sbrush, 50, 300);

            e.Graphics.DrawLine(myPen, 50, 320, 750, 320);


            myFont = new Font("Calibri", 8, FontStyle.Bold);
            e.Graphics.DrawString("NO", myFont, sbrush, 50, 328);
            e.Graphics.DrawString("QRAFİK ÜZRƏ ÖDƏNİŞ TARİXİ", myFont, sbrush, 80, 328);
            e.Graphics.DrawString("ƏSAS MƏBLƏĞDƏN QALIQ", myFont, sbrush, 250, 328);
            e.Graphics.DrawString("AYLIQ CƏM ÖDƏNİŞ", myFont, sbrush, 610, 328);

            e.Graphics.DrawLine(myPen, 50, 348, 750, 348);

            int y = 360;

            StringFormat myStringFormat = new StringFormat();
            myStringFormat.Alignment = StringAlignment.Far;

            decimal gTotal = 0;

            foreach (ListViewItem lvi in listView1.Items)
            {
                e.Graphics.DrawString(lvi.SubItems[0].Text, myFont, sbrush, 50, y);
                e.Graphics.DrawString(lvi.SubItems[1].Text, myFont, sbrush, 80, y);
                e.Graphics.DrawString(lvi.SubItems[2].Text, myFont, sbrush, 250, y);
                e.Graphics.DrawString(lvi.SubItems[3].Text, myFont, sbrush, 400, y);
                e.Graphics.DrawString(lvi.SubItems[4].Text, myFont, sbrush, 530, y);
                e.Graphics.DrawString(lvi.SubItems[5].Text, myFont, sbrush, 610, y);
                y += 20;

            }
            e.Graphics.DrawLine(myPen, 50, y, 750, y);

        }

        private void bPay_Click(object sender, EventArgs e)
        {
            decimal payment = Convert.ToDecimal(tIlkinOdenis.Text);
            if (payment > 0)
            {
                fPay pay = new fPay(payment);
                if (pay.ShowDialog() is DialogResult.OK)
                {
                    Payment(pay.Result.Total, pay.Result.Cash, pay.Result.Card, pay.Result.IncomingSum);
                }
            }
            else
            {
                Payment(0, 0, 0, 0);
            }

        }

        private void Payment(decimal Total, decimal Cash, decimal Card, decimal IncomingSum)
        {
            string uuid = Guid.NewGuid().ToString();
            string vahid = get_vahid();

            var data = Validation();
            if (data is null) return;

            try
            {
                decimal quantity = Convert.ToDecimal(tQuantity.Text);

                int vatType = Convert.ToInt32(label9.Text);
                int quantityType = Convert.ToInt32(vahid);

                decimal creditPayment = Convert.ToDecimal(tTotal.Text);

                DTOs.CreditSaleDto creditDto = new DTOs.CreditSaleDto()
                {
                    Url = lIpAdress.Text,
                    MerchantId = lMerchantId.Text,
                    CustomerName = tCustomerName.Text,
                    Cashier = lCashier.Text,
                    DocumentUUID = uuid,
                    CreditContract = tContractNo.Text.Trim(),
                    CashPayment = Cash,
                    CardPayment = Card,
                    IncomingSum = IncomingSum,
                    creditPayment = creditPayment,
                    Note = memoEdit1.Text.Trim(),
                    item = new DTOs.CreditSaleDto.Item()
                    {
                        ProductName = tProductName.Text,
                        ProductCode = mal_alisi_details_id,
                        Quantity = quantity,
                        QuantityType = quantityType,
                        SalePrice = Convert.ToDecimal(tSalePrice.Text),
                        VatType = vatType
                    }
                };


                data.PaymentType = (short)(
                    (creditDto.CashPayment > 0 ? 1 : 0) +
                    (creditDto.CardPayment > 0 ? 2 : 0));

                switch (lModel.Text)
                {
                    case "1":
                        var SunmiResult = Sunmi.CreditSale(creditDto);

                        if (SunmiResult.Item1 == true)
                        {
                            CreditSaleMain(data, SunmiResult.Item2, SunmiResult.Item3, SunmiResult.Item4);
                        }
                        break;
                    case "2":
                        var AzSmartResult = AzSmart.CreditSale(creditDto);

                        if (AzSmartResult.Item1)
                        {
                            CreditSaleMain(data, AzSmartResult.Item2, AzSmartResult.Item3, "");
                        }
                        break;
                    case "3":
                        var OmnitechResult = Omnitech.CreditSale(creditDto);

                        if (OmnitechResult.Item1)
                            CreditSaleMain(data, OmnitechResult.Item2, OmnitechResult.Item3, OmnitechResult.Item4);
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("XƏTA \n\n" + ex.Message);
            }
        }

        private DatabaseClasses.CreditMain Validation()
        {
            DatabaseClasses.CreditMain credit = new DatabaseClasses.CreditMain();
            credit.ProcessNo = tProccessNo.Text;
            credit.ContractNo = tContractNo.Text.Trim();
            credit.OdenilenMebleg = Decimal.Parse(tTotal.Text);
            credit.CustomerName = tCustomerName.Text.Trim();
            credit.CustomerId = string.IsNullOrWhiteSpace(customerId) ? 0 : Convert.ToInt32(customerId);
            credit.ZaminName = string.IsNullOrWhiteSpace(tZamin.Text) ? "YOXDUR" : tZamin.Text.Trim();
            credit.ZaminId = string.IsNullOrWhiteSpace(zaminId) ? 1 : Convert.ToInt32(zaminId);
            credit.SupplierName = tSupplier.Text;
            credit.ProductId = string.IsNullOrWhiteSpace(mal_alisi_details_id) ? 0 : Convert.ToInt32(mal_alisi_details_id);
            credit.ProductName = tProductName.Text;
            credit.Quantity = Decimal.Parse(tQuantity.Text);
            credit.SalePrice = Decimal.Parse(tSalePrice.Text);
            credit.DiscountPercent = Decimal.Parse(tDiscountPercent.Text);
            credit.DiscountAmount = Decimal.Parse(tDiscountAmount.Text);
            credit.Taksit = string.IsNullOrWhiteSpace(cmbMonth.Text) ? 0 : Convert.ToInt32(cmbMonth.Text);
            credit.Total = Decimal.Parse(tTotal.Text);
            credit.IlkinOdenis = Decimal.Parse(tIlkinOdenis.Text);
            credit.Comment = memoEdit1.Text.Trim();
            credit.MonthAmount = Decimal.Parse(tAyliqOdenis.Text);


            var validator = new CreditValidation();
            var validateResult = validator.Validate(credit);

            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                {
                    FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                    return null;
                }
            }

            return credit;
        }
    }
}