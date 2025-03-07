using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.Validations;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses.ProductDetail;
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
            GridLocalizer.Active = new MyGridLocalizer();
        }

        KASSA_IP_CRUD KIC = new KASSA_IP_CRUD();

        private void fKassalar_Load(object sender, EventArgs e)
        {
            firma_main();
            magaza_main();
            GetallData();
        }

        private void BankDataLoad()
        {
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
            lookBank.EditValue = _bankType;
        }

        private void magaza_main()
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

        public void GetallData()
        {
            try
            {
                SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
                string queryString = " select KASSA_IP_ID, kf.KASSA_FIRMALAR AS N'KASSA MODELİNİN ADI'," +
                    "ki.IP_ADRESS AS N'KASSANIN İP ADRESİ',isnull(ki.merchant_id,'') N'MERCHANT İD',isnull(u.AD,'') AS N'İSTİFADƏÇİ ADI'" +
                           " from KASSA_IP ki inner join KASSA_FIRMALAR kf " +
                           " on ki.KASSA_FIRMA_IP = kf.KASSA_FIRMALAR_ID " +
                          " left join userParol u on u.id = ki.KASSIR_ID ";

                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
                gridView1.Columns[0].Visible = false;
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DATALOAD_MESSAGE(e.Message);
            }
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            foreach (int i in gridView1.GetSelectedRows())
            {
                DataRow row = gridView1.GetDataRow(i);

                int B = Convert.ToInt32(row[0].ToString());
                if (B > 0)
                {
                    int x = KIC.DELETE_IP(B);
                    FormHelpers.Log($"{row[2]} ip adresli {row[1]} kassası silindi");

                }
                GetallData();
            }
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            //Terminal terminal = new Terminal
            //{
            //    ModelId = lookKassa.EditValue == null ? 0 : Convert.ToInt32(lookKassa.EditValue.ToString()),
            //    IpAddress = tIpAddress.Text.Trim(),
            //    MerchantIdKey = tMerchantId.Text.Trim(),
            //    BankName = lookBank.Text,
            //    UserId = Convert.ToInt32(lookUser.EditValue.ToString())
            //};


            //var validator = new TerminalValidation();
            //var validateResult = validator.Validate(terminal);

            //if (!validateResult.IsValid)
            //{
            //    foreach (var error in validateResult.Errors)
            //    {
            //        FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
            //        return;
            //    }
            //}

            //DbProsedures.TerminalAdd(terminal);

            //todo Terminal kodlarını əlavə et

            //DAXIL ET 
            if (string.IsNullOrEmpty(tIpAddress.Text.ToString()) || string.IsNullOrEmpty(lookKassa.EditValue.ToString())
                || string.IsNullOrEmpty(lookUser.EditValue.ToString()))
            {

            }
            else
            {
                int A = KIC.Insert_IP(Convert.ToInt32(lookKassa.EditValue.ToString()), tIpAddress.Text,
               Convert.ToInt32(lookUser.EditValue.ToString()), tMerchantId.Text);
                FormHelpers.Log($"{tIpAddress.Text} ip adresli {lookKassa.Text} kassa əlavə edildi");
            }
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
            else if (lookKassa.Text == "NBA")
            {
                BankDataLoad();    
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