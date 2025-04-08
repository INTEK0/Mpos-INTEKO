using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Validations;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fTereziler : DevExpress.XtraEditors.XtraForm
    {
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
            UserDataLoad();
        }

        private void UserDataLoad()
        {
            string strQuery = "SELECT id,AD as N'KASSİR' FROM userParol where IsDeleted = 0";
            var data = DbProsedures.ConvertToDataTable(strQuery);

            lookUser.Properties.DisplayMember = "KASSİR";
            lookUser.Properties.ValueMember = "id";
            lookUser.Properties.DataSource = data;
            lookUser.Properties.NullText = "--Seçin--";
            lookUser.Properties.PopulateColumns();
            lookUser.Properties.Columns[0].Visible = false;
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            DatabaseClasses.Terezi terezi = new DatabaseClasses.Terezi
            {
                IpAddress = tIpAddress.Text,
                FilePath = tFilePath.Text,
                ModelId = lookTerezi.EditValue == null ? 0 : Convert.ToInt32(lookTerezi.EditValue.ToString()),
                UserId = lookUser.EditValue == null ? 0 : Convert.ToInt32(lookUser.EditValue.ToString())
            };

            var validator = new TereziValidation();
            var validateResult = validator.Validate(terezi);

            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                {
                    FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                    return;
                }
            }

            if (DbProsedures.TereziAdd(terezi))
            {
                Alert($"{tIpAddress.Text} ip adresli {lookTerezi.Text} tərəzi əlavə edildi", Enums.MessageType.Success);
                FormHelpers.Log($"{tIpAddress.Text} ip adresli {lookTerezi.Text} tərəzi əlavə edildi");
            }

            TereziDataLoad();
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);

                int B = Convert.ToInt32(row["TERAZI_IP_ID"].ToString());
                if (B > 0)
                {
                    DbProsedures.TereziRemove(B);
                    Alert($"{row[2]} ip adresli {row[1]} tərəzi silindi", Enums.MessageType.Success);
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
            string queryString = @"SELECT TERAZI_IP_ID, 
kf.TERAZI_FIRMALAR AS N'ModelName',
ki.IP_ADRESS AS N'IpAddress' 
FROM TERAZI_IP ki 
INNER JOIN TERAZI_FIRMALAR kf ON ki.TERAZI_FIRMA_IP = kf.TERAZI_FIRMALAR_ID";

            var data = DbProsedures.ConvertToDataTable(queryString);

            gridControl1.DataSource = data;
            gridView1.Columns["TERAZI_IP_ID"].Visible = false;
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
        }

        private void tFilePath_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Title = "İNTEKO - MPOS";
                if (openFile.ShowDialog() is DialogResult.OK)
                {
                    tFilePath.Text = openFile.FileName;
                }
            }
        }
    }
}