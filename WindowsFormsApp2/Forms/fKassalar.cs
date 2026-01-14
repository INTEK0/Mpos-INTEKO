using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.CacheData;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.Validations;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fKassalar : DevExpress.XtraEditors.XtraForm
    {
        private BankType _bankType = BankType.NONE;
        public fKassalar()
        {
            InitializeComponent();
            GridPanelText(gridView1);
        }

        private void fKassalar_Load(object sender, EventArgs e)
        {
            firma_main();
            UserDataLoad();
            GetallData();

        }

        private void BankDataLoad()
        {
            lookBank.Visible = true;
            var data = Enum.GetValues(typeof(BankType))
                       .Cast<BankType>()
                       .Select(x => new
                       {
                           Value = GetEnumDescription(x)
                       })
                       .ToList();


            lookBank.Properties.DataSource = data;
            lookBank.Properties.DisplayMember = "Value";
            lookBank.Properties.ForceInitialize();
            lookBank.EditValue = null;
            lookBank.EditValue = _bankType;
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

        private void firma_main()
        {
            string strQuery = "SELECT KASSA_FIRMALAR_ID ,KASSA_FIRMALAR AS N'FİRMALAR' FROM KASSA_FIRMALAR";
            var data = DbProsedures.ConvertToDataTable(strQuery);

            lookKassa.Properties.DisplayMember = "FİRMALAR";
            lookKassa.Properties.ValueMember = "KASSA_FIRMALAR_ID";
            lookKassa.Properties.DataSource = data;
            lookKassa.Properties.NullText = "--Seçin--";
            lookKassa.Properties.PopulateColumns();
            lookKassa.Properties.Columns[0].Visible = false;
        }

        private void GetallData()
        {
            try
            {
                string queryString = @"SELECT KASSA_IP_ID, 
kf.KASSA_FIRMALAR AS N'KASSA MODELİNİN ADI',
ki.IP_ADRESS AS N'KASSANIN İP ADRESİ',
isnull(ki.merchant_id,'') N'MERCHANT İD',
isnull(u.AD,'') AS N'İSTİFADƏÇİ ADI',
isnull(ki.Bank,'') AS N'BANK ADI'
FROM KASSA_IP ki inner join KASSA_FIRMALAR kf 
ON ki.KASSA_FIRMA_IP = kf.KASSA_FIRMALAR_ID 
LEFT JOIN userParol u ON u.id = ki.KASSIR_ID";
                var data = DbProsedures.ConvertToDataTable(queryString);
                gridControl1.DataSource = data;
                gridView1.Columns["KASSA_IP_ID"].Visible = false;
                gridView1.RefreshData();
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DATALOAD_MESSAGE(e.Message);
            }
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            if (!UserCacheService.User.UserRole.TerminalDelete)
            {
                FormHelpers.Alert("Sizin icazəniz yoxdur", MessageType.Error);
                return;
            }
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);

                int B = Convert.ToInt32(row[0].ToString());
                if (B > 0)
                {
                    DbProsedures.TerminalRemove(B);
                    FormHelpers.Log($"{row[2]} ip adresli {row[1]} kassası silindi");

                }
                GetallData();
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            Terminal terminal = new Terminal
            {
                ModelId = lookKassa.EditValue == null ? 0 : Convert.ToInt32(lookKassa.EditValue.ToString()),
                IpAddress = tIpAddress.Text.Trim(),
                MerchantIdKey = tMerchantId.Text.Trim(),
                BankName = lookBank.Text,
                UserId = lookUser.EditValue == null ? 0 : Convert.ToInt32(lookUser.EditValue.ToString())
            };


            var validator = new TerminalValidation();
            var validateResult = validator.Validate(terminal);

            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                {
                    FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                    return;
                }
            }

           int result = DbProsedures.TerminalAdd(terminal);
            if (result == 0)
            {
                FormHelpers.Alert($"Kassa daha öncə əlavə edilib", MessageType.Info);
                return;
            }
            else
            {
                FormHelpers.Alert($"{tIpAddress.Text} ip adresli {lookKassa.Text} kassası sistemə əlavə edildi", MessageType.Success);
                FormHelpers.Log($"{tIpAddress.Text} ip adresli {lookKassa.Text} kassası sistemə əlavə edildi");
            }

            UserCacheService.RefreshTerminal();
            GetallData();
        }

        private void lookKassa_TextChanged(object sender, EventArgs e)
        {
            if (lookKassa.Text == "AzSMART")
            {
                labelControl1.Visible = true;
                tMerchantId.Visible = true;
                tMerchantId.Text = "";
                groupControl1.Height = 170;
                groupControl2.Location = new Point(0, 176);
                gridControl1.Location = new Point(5, 234);
            }
            else if (lookKassa.Text is "NBA" || lookKassa.Text is "CASPOS" || lookKassa.Text is "SUNMI")
            {
                BankDataLoad();
                labelControl1.Visible = false;
                tMerchantId.Visible = false;
                tMerchantId.Text = "";
                groupControl1.Height = 130;
                groupControl2.Location = new Point(0, 134);
                gridControl1.Location = new Point(5, 192);
            }
            else
            {
                labelControl1.Visible = false;
                tMerchantId.Visible = false;
                tMerchantId.Text = "";
                groupControl1.Height = 130;
                groupControl2.Location = new Point(0, 134);
                gridControl1.Location = new Point(5, 192);
            }
        }

        private void bPing_Click(object sender, EventArgs e)
        {
            string ip = string.Empty;
            int[] selectedRows = gridView1.GetSelectedRows();
            foreach (var item in selectedRows)
            {
                var row = gridView1.GetDataRow(item);
                ip = row[2].ToString();
                break;
            }
            FormHelpers.PingHostAsync(ip);
        }

        private void gridView1_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            //if (gridView1.IsRowSelected(e.RowHandle))
            //{
            //    // Eğer satır zaten seçili ise, seçimi kaldır
            //    gridView1.UnselectRow(e.RowHandle);
            //}
            //else
            //{
            //    // Diğer tüm satırların seçimlerini kaldır
            //    foreach (var rowHandle in gridView1.GetSelectedRows())
            //    {
            //        if (rowHandle != e.RowHandle) // Tıklanan satırı hariç tut
            //        {
            //            gridView1.UnselectRow(rowHandle);
            //        }
            //    }

            //    // Tıklanan satırı seç
            //    gridView1.SelectRow(e.RowHandle);
            //}
        }
    }
}