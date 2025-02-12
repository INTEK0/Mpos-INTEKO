using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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

namespace WindowsFormsApp2
{
    public partial class SearchTechizatci_LAYOUT : DevExpress.XtraEditors.XtraForm
    {
        public SearchTechizatci_LAYOUT()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = "output.xlsx";
                gridControl1.ExportToXlsx(path);
                // Open the created XLSX file with the default application. 
                Process.Start(path);
            }
            catch
            {
                MessageBox.Show("Fayl aciqdir");
            }
        }
        public void getall()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);


                string queryString = @"SELECT 
[TECHIZATCI_ID],
[TECHIZATCI_NOMRE] AS N'TƏCHİZATÇI NÖMRƏ',
[START_DATE] AS N'QEYDİYYAT TARİXİ',
[MUGAVİLE_NOM] AS N'MÜQAVİLƏ NÖMRƏ' ,
[SIRKET_ADI] AS N'TƏCHİZATÇI ADI',
[TECHIZATCI_VOEN] AS N'VÖEN',
[UNVAN] AS N'ÜNVAN',
[ELAGE_NOMRE] AS N'TELEFON' ,
[ELEKTRON_POCT] AS N'E-POÇT',
[ILKIN_BORC] AS N'İLKİN BORC',
[SAHIBKAR_TECHIZATCI] AS N'BORC TƏYİNATI',
[HESAB_AD] AS N'HESAB NÖMRƏSİ' ,
[BANK_ADI] AS N'BANK ADI' ,
[BANK_VOEN] AS N'BANK VÖEN' ,
[KOD] AS N'KOD',
[SWIFT] AS N'SWIFT' ,
[DESCRIPTION] AS N'QEYD' 
FROM[COMPANY].[TECHIZATCI] where IsDeleted = 0";


                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@pricePoint", paramValue);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
            }
            catch (Exception)
            {
               
            }

        }
        techizatci_odenis t = new techizatci_odenis();

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {

                if (Convert.ToInt32(labelControl4.Text.ToString()) > 0)
                {
                    int id = Convert.ToInt32(labelControl4.Text.ToString());
                    string unvan = textEdit1.Text.ToString();
                    string mugavile_nomr = textEdit2.Text.ToString();
                    string tel1 = textEdit3.Text.ToString();
                    string tel2 = textEdit4.Text.ToString();

                    int a = t.update_techizatci(id, unvan, mugavile_nomr, tel1, tel2);
                }
            }
            catch (Exception ec)
            {
                XtraMessageBox.Show(ec.Message.ToString());
            }

            getall();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {


                UPDATE_techizatci(Convert.ToInt32(dr[0].ToString()), dr[1].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(),
                dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString(), dr[11].ToString(), dr[12].ToString(), dr[13].ToString(),
                dr[14].ToString(), dr[15].ToString(), dr[16].ToString(), dr[17].ToString(), dr[18].ToString(), dr[19].ToString());
                getall();


            }
        }


        public void UPDATE_techizatci(int TECHIZATCI_ID, string TECHIZATCI_NOMRE, string MUGAVİLE_NOM, string SIRKET_ADI, string UNVAN, string ELAGE_NOMRE, string ELAGE_NOMRE2, string ELEKTRON_POCT,
            string ILKIN_BORC, string SAHIBKAR_TECHIZATCI, string TECHIZATCI_VOEN, string HESAB_AD, string BANK_ADI, string BANK_VOEN, string KOD,
            string MH, string SWIFT, string MESUL_SEXS, string DESCRIPTION)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand("search_techizatci_update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param;

            param = cmd.Parameters.Add("@TECHIZATCI_ID", SqlDbType.Int);
            param.Value = TECHIZATCI_ID;
            param = cmd.Parameters.Add("@TECHIZATCI_NOMRE", SqlDbType.NVarChar);
            param.Value = TECHIZATCI_NOMRE;
            param = cmd.Parameters.Add("@MUGAVİLE_NOM", SqlDbType.NVarChar);
            param.Value = MUGAVİLE_NOM;
            param = cmd.Parameters.Add("@SIRKET_ADI", SqlDbType.NVarChar);
            param.Value = SIRKET_ADI;
            param = cmd.Parameters.Add("@UNVAN", SqlDbType.NVarChar);
            param.Value = UNVAN;
            param = cmd.Parameters.Add("@ELAGE_NOMRE", SqlDbType.NVarChar);
            param.Value = ELAGE_NOMRE;
            param = cmd.Parameters.Add("@ELAGE_NOMRE2", SqlDbType.NVarChar);
            param.Value = ELAGE_NOMRE2;
            //param = cmd.Parameters.Add("@ELAGE_NOMRE3", SqlDbType.NVarChar);
            //param.Value = ELAGE_NOMRE3;
            param = cmd.Parameters.Add("@ELEKTRON_POCT", SqlDbType.NVarChar);
            param.Value = ELEKTRON_POCT;
            param = cmd.Parameters.Add("@ILKIN_BORC", SqlDbType.NVarChar);
            param.Value = ILKIN_BORC;
            param = cmd.Parameters.Add("@SAHIBKAR_TECHIZATCI", SqlDbType.NVarChar);
            param.Value = SAHIBKAR_TECHIZATCI;
            param = cmd.Parameters.Add("@TECHIZATCI_VOEN", SqlDbType.NVarChar);
            param.Value = TECHIZATCI_VOEN;
            param = cmd.Parameters.Add("@HESAB_AD", SqlDbType.NVarChar);
            param.Value = HESAB_AD;
            param = cmd.Parameters.Add("@BANK_ADI", SqlDbType.NVarChar);
            param.Value = BANK_ADI;
            param = cmd.Parameters.Add("@BANK_VOEN", SqlDbType.NVarChar);
            param.Value = BANK_VOEN;
            param = cmd.Parameters.Add("@KOD", SqlDbType.NVarChar);
            param.Value = KOD;
            param = cmd.Parameters.Add("@MH", SqlDbType.NVarChar);
            param.Value = MH;
            param = cmd.Parameters.Add("@SWIFT", SqlDbType.NVarChar);
            param.Value = SWIFT;
            param = cmd.Parameters.Add("@MESUL_SEXS", SqlDbType.NVarChar);
            param.Value = MESUL_SEXS;
            param = cmd.Parameters.Add("@DESCRIPTION", SqlDbType.NVarChar);
            param.Value = DESCRIPTION;


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {


                int paramValue = Convert.ToInt32(dr[0]);
                labelControl4.Text = dr[0].ToString();
                textEdit2.Text = dr[3].ToString();
                textEdit1.Text = dr[5].ToString();
                textEdit3.Text = dr[6].ToString();
                textEdit4.Text = dr[7].ToString();
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                {
                  

                    try
                    {
                       
                    }
                    catch (Exception ex)
                    {
                       
                    }

                }

            }
        }

        private void SearchTechizatci_LAYOUT_Load(object sender, EventArgs e)
        {
            textEdit1.TabIndex = 1;
            textEdit2.TabIndex = 2;
            textEdit3.TabIndex = 3;
            textEdit4.TabIndex = 4;


            //labelControl1.Text = "0";
            getall();
            gridControl1.TabStop = false;
          
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}