using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fTereziler : DevExpress.XtraEditors.XtraForm
    {
        KASSA_IP_CRUD KIC = new KASSA_IP_CRUD();

        public fTereziler()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void fTereziler_Load(object sender, EventArgs e)
        {
            TereziDataLoad();
            TereziFirmaLoad();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tIpAddress.Text) || string.IsNullOrWhiteSpace(lookTerezi.EditValue.ToString()))
            {
                FormHelpers.Alert("Ip adresi və ya Tərəzi seçimi edilmədi", Enums.MessageType.Warning);
                return;
            }
            else
            {
                int A = KIC.Insert_IPT(Convert.ToInt32(lookTerezi.EditValue.ToString()), tIpAddress.Text);
                FormHelpers.Log($"{tIpAddress.Text} ip adresli {lookTerezi.Text} tərəzi əlavə edildi");
            }
            TereziDataLoad();
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);

                int B = Convert.ToInt32(row[0].ToString());
                if (B > 0)
                {
                    KIC.DELETE_IPT(B);
                    FormHelpers.Log($"{row[2]} ip adresli {row[1]} tərəzi silindi");
                }
            }
            TereziDataLoad();
        }

        private async void bPing_Click(object sender, EventArgs e)
        {
            string ip = string.Empty;
            int[] selectedRows = gridView1.GetSelectedRows();
            foreach (var item in selectedRows)
            {
                var row = gridView1.GetDataRow(item);
                ip = row[2].ToString();
                break;
            }
            await PingHostAsync(ip);
        }

        private static async Task PingHostAsync(string host)
        {
            Cursor.Current = Cursors.WaitCursor;
            using (Ping ping = new Ping())
            {
                try
                {
                    PingReply reply = await ping.SendPingAsync(host);
                    if (reply.Status == IPStatus.Success)
                    {
                        MessageBox.Show($"Ping to {host} successful: {reply.RoundtripTime} ms");
                    }
                    else
                    {
                        MessageBox.Show($"Ping to {host} failed: {reply.Status}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error pinging {host}: {ex.Message}");
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void TereziDataLoad()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                string queryString = " select TERAZI_IP_ID, kf.TERAZI_FIRMALAR AS N'TƏRƏZİ MODELİNİN ADI', ki.IP_ADRESS AS N'TƏRƏZİNİN İP ÜNVANI' from TERAZI_IP ki inner join TERAZI_FIRMALAR kf    on ki.TERAZI_FIRMA_IP = kf.TERAZI_FIRMALAR_ID";

                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
                //gridView1.OptionsSelection.MultiSelect = false;
                //gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DATALOAD_MESSAGE(e.Message);
            }
        }

        private void TereziFirmaLoad()
        {
            string strQuery = "SELECT TERAZI_FIRMALAR_ID ,TERAZI_FIRMALAR AS N'FİRMALAR' FROM TERAZI_FIRMALAR";
            var data = DbProsedures.ConvertToDataTable(strQuery);

            lookTerezi.Properties.DisplayMember = "FİRMALAR";
            lookTerezi.Properties.ValueMember = "TERAZI_FIRMALAR_ID";
            lookTerezi.Properties.DataSource = data;
            lookTerezi.Properties.NullText = "--Seçin--";
            lookTerezi.Properties.PopulateColumns();
            lookTerezi.Properties.Columns[0].Visible = false;
            //}
        }
    }
}