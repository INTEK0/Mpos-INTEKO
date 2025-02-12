using DevExpress.DashboardCommon;
using DevExpress.LookAndFeel;
using DevExpress.Utils.Colors;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraGrid.Views.Grid;
using FluentValidation;
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
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.Validations;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using static WindowsFormsApp2.Helpers.Enums;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fAddProduct : BaseForm
    {
        private string PaymentTypeRadioButton { get; set; }
        private int productID { get; set; }
        private int categoryID { get; set; }
        private DataTable _currentDataTable = new DataTable();

        public fAddProduct()
        {
            InitializeComponent();
            dateTarix.DateTime = DateTime.Now;
        }

        private enum ProductOperation
        {
            Add,
            Update,
            Delete
        }

        private void fAddProduct_Load(object sender, EventArgs e)
        {
            SupplierDataLoad();
            UnitDataLoad();
            CurrencyDataLoad();
            TaxDataLoad();
            WarehouseDataLoad();
            tProccessNo.Text = DbProsedures.GET_ProductProcessNo();
        }

        private void SupplierDataLoad()
        {
            string query = "select TECHIZATCI_ID,SIRKET_ADI AS N'ŞİRKƏT ADI' from COMPANY.TECHIZATCI WHERE IsDeleted = 0";
            var data = DbProsedures.ConvertToDataTable(query);
            lookSupplier.Properties.DisplayMember = "ŞİRKƏT ADI";
            lookSupplier.Properties.ValueMember = "TECHIZATCI_ID";
            lookSupplier.Properties.DataSource = data;
            lookSupplier.Properties.PopulateColumns();
            lookSupplier.Properties.Columns[0].Visible = false;
        }

        private void UnitDataLoad()
        {
            string query = "select VAHIDLER_ID, VAHIDLER_NAME as N'VAHİDLƏR' from VAHIDLER";
            var data = DbProsedures.ConvertToDataTable(query);
            lookUnit.Properties.DisplayMember = "VAHİDLƏR";
            lookUnit.Properties.ValueMember = "VAHIDLER_ID";
            lookUnit.Properties.DataSource = data;
            lookUnit.Properties.PopulateColumns();
            lookUnit.Properties.Columns[0].Visible = false;
        }

        private void CurrencyDataLoad()
        {
            string query = "select VALYUTALAR_ID,VALYUTALAR from VALYUTALAR";
            var data = DbProsedures.ConvertToDataTable(query);
            lookCurrency.Properties.DisplayMember = "VALYUTALAR";
            lookCurrency.Properties.ValueMember = "VALYUTALAR_ID";
            lookCurrency.Properties.DataSource = data;
            lookCurrency.Properties.PopulateColumns();
            lookCurrency.Properties.Columns[0].Visible = false;
            lookCurrency.EditValue = 1;
        }

        private void TaxDataLoad()
        {
            string query = "select EDV_ID,EDV as N'VERGİ DƏRƏCƏSİ' from VERGI_DERECESI";
            var data = DbProsedures.ConvertToDataTable(query);
            lookTaxType.Properties.DisplayMember = "VERGİ DƏRƏCƏSİ";
            lookTaxType.Properties.ValueMember = "EDV_ID";
            lookTaxType.Properties.DataSource = data;
            lookTaxType.Properties.PopulateColumns();
            lookTaxType.Properties.Columns[0].Visible = false;
        }

        private void WarehouseDataLoad()
        {
            string query = "select WAREHOUSE_ID,WAREHOUSE_NAME AS N'ANBAR ADI' from COMPANY.WAREHOUSE";
            var data = DbProsedures.ConvertToDataTable(query);
            lookWarehouse.Properties.DisplayMember = "ANBAR ADI";
            lookWarehouse.Properties.ValueMember = "WAREHOUSE_ID";
            lookWarehouse.Properties.DataSource = data;
            lookWarehouse.Properties.PopulateColumns();
            lookWarehouse.Properties.Columns["WAREHOUSE_ID"].Visible = false;
            lookWarehouse.EditValue = 4;
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            var selectedPaymentType = panelControl3.Controls.OfType<CheckEdit>().FirstOrDefault(x => x.Checked);

            ProductsDetail productsDetail = new ProductsDetail
            {
                ProductMainId = 0,
                CategoryName = tCategoryName.Text,
                Barocde = tBarcode.Text,
                ProductName = tProductName.Text,
                ProductCode = tProductCode.Text,
                WarehouseName = lookWarehouse.Text,
                Quantity = Convert.ToDecimal(tQuantity.Text),
                UnitName = lookUnit.Text,
                CurrencyName = lookCurrency.Text,
                TaxName = lookTaxType.Text,
                PurchasePrice = Convert.ToDecimal(tPurchasePrice.Text),
                SalePrice = Convert.ToDecimal(tSalePrice.Text),
                DiscountPercent = tDiscountPercentage.Text,
                DiscountAZN = tDiscountAmount.Text,
                DiscountAmount = tDiscountTotal.Text,
                TotalAmount = tTotalAmount.Text,
                IstehsalTarixi = dateİstehsal.Text,
                BitisTarixi = dateBitis.Text,
                XeberdarEt = spinEdit1.Text
            };

            var validator = new ProductValidation();
            var validateResult = validator.Validate(productsDetail);

            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                {
                    FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
                    return;
                }
            }


            int IsExists = DbProsedures.Exists_ProductCode(tProductCode.Text, Convert.ToInt32(lookSupplier.EditValue));

            if (IsExists > 1)
            {
                int addMainProduct = DbProsedures.InsertProductMain(new ProductsMain
                {
                    FakturaNo = tFakturaNo.Text,
                    SupplierName = lookSupplier.Text,
                    Date = dateTarix.DateTime,
                    PaymentType = selectedPaymentType.Text,
                    ProccessNo = tProccessNo.Text,
                    Status = "MƏHSUL ALIŞI"
                });

                if (addMainProduct > 0)
                {
                    productsDetail.ProductMainId = addMainProduct;
                    int? IsSuccess = DbProsedures.InsertProductDetails(productsDetail);
                    if (IsSuccess > 0)
                    {
                        Clear();
                        GetAllData(tProccessNo.Text, ProductOperation.Add);
                        YeniBorcHesabla();
                        
                    }
                }
            }
            else
            {
                FormHelpers.Alert("MƏHSUL KODU BAŞKA BİR TƏCHİZATÇIDA MÖVCUDDUR", MessageType.Warning);
                return;
            }
            lookSupplier.Enabled = false;
        }

        private void YeniBorcHesabla()
        {
            string query = @"
            SELECT 
            cast(sum(isnull(d.ALIS_GIYMETI,0.00)*isnull(d.MIGDARI,0.00)) as decimal(9,2)) as yeni_borc
            from MAL_ALISI_MAIN m inner join MAL_ALISI_DETAILS d on m.MAL_ALISI_MAIN_ID = d.MAL_ALISI_MAIN_ID 
            AND m.TECHIZATCI_ID=@pricePoint1 
            where m.EMELIYYAT_NOMRE = @pricePoint";

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@pricePoint", tProccessNo.Text);
                    cmd.Parameters.AddWithValue("@pricePoint1", (int)lookSupplier.EditValue);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            tDebtNew.Text = dr["yeni_borc"].ToString();
                        }
                    }
                }
            }
        }

        private void QaliqBorcHesabla(int supplierId)
        {
            string query = @"
            SELECT Y.BORC - X.GAYTARMA_MEBLEG AS BORC FROM( select 1 AS ID, cast(sum(isnull(BORC, 0.00)) as decimal(9, 2)) as BORC
            FROM (SELECT f.MAL_ALISI_MAIN_ID, f.[FAKTURA NÖMRƏ],f.TARIX, f.QİYMƏT - isnull(t.odenis, 0.00) BORC,0 AS 'ÖDƏNİŞ'
            FROM dbo.fn_TECHIZATCI_BORC(@pricePoint) f 
            left join(select  MAL_ALISI_MAIN_ID, sum(ODENIS) odenis FROM TECHIZATCI_ODENIS
            group by MAL_ALISI_MAIN_ID)t  on f.MAL_ALISI_MAIN_ID = t.MAL_ALISI_MAIN_ID)o )Y
            LEFT JOIN(SELECT 1 AS ID, ISNULL(CAST(SUM(MD.ALIS_GIYMETI * D.MIGDARI) AS decimal(9, 2)), 0.00)
            AS GAYTARMA_MEBLEG FROM MAL_GEYTARMA_MAIN M
            INNER JOIN  MAL_GEYTARMA_DETAILS D ON
            M.MAL_GEYTARMA_MAIN_ID = D.MAL_GEYTARMA_MAIN_ID
            INNER JOIN MAL_ALISI_DETAILS MD ON MD.MAL_ALISI_DETAILS_ID = D.MAL_ALISI_DETAILS_ID
            INNER JOIN MAL_ALISI_MAIN MM ON MM.MAL_ALISI_MAIN_ID = MD.MAL_ALISI_MAIN_ID
            WHERE MM.TECHIZATCI_ID = @pricePoint)X ON X.ID = Y.ID";

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@pricePoint", supplierId);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            tDebtBalance.Text = dr["BORC"].ToString();
                        }
                    }
                }
            }
        }

        private void TotalBorcHesabla()
        {
            decimal qaliqBorc = Convert.ToDecimal(tDebtBalance.Text);
            decimal yeniBorc = Convert.ToDecimal(tDebtNew.Text);
            decimal yekunBorc = qaliqBorc + yeniBorc;
            if (yekunBorc > 0)
            {
                tDebtTotal.EditValue = yekunBorc;
            }
        }

        private void bAlinanMallar_Click(object sender, EventArgs e)
        {
            FormHelpers.OpenForm<fReceivedProducts>();
        }

        public override void ReceiveData<T>(T data)
        {
            if (data != null)
            {
                if (data is Categories categories)
                {
                    tCategoryName.Text = categories.KATEGORIYA;
                    categoryID = categories.KATEGORIYA_ID;
                }
                else if (data is Products product)
                {
                    productID = product.Id;
                    tProductName.Text = product.ProductName;
                    tBarcode.Text = product.Barocde;
                    tProductCode.Text = product.ProductCode;
                    tPurchasePrice.EditValue = product.PurchasePrice;
                    tSalePrice.EditValue = product.SalePrice;
                    lookUnit.EditValue = product.UnitId;
                    lookTaxType.EditValue = product.TaxId;
                }
            }
        }

        private void tCategoryName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(lookSupplier.Text))
            {
                fCategory<fAddProduct> selectCategory = new fCategory<fAddProduct>(this);
                selectCategory.ShowDialog();
            }
            else
            {
                FormHelpers.Alert("Təchizatçı seçimi edilmədi", MessageType.Warning);
            }

        }

        private void tCategoryName_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tCategoryName.Text))
            {
                int count = DbProsedures.Exists_Category(tCategoryName.Text);
                if (count is -1)
                {
                    DialogResult dialogResult = XtraMessageBox.Show("YENİ KATEGORİYA YARADILSIN ?", nameof(HeaderMessage.Bildiriş), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult is DialogResult.Yes)
                    {
                        int categorySuccess = DbProsedures.Insert_Category(tCategoryName.Text);
                        if (categorySuccess > 0)
                        {
                            categoryID = categorySuccess;
                            FormHelpers.Alert("Yeni kateqoriya uğurla yaradıldı", Enums.MessageType.Success);
                        }
                    }
                    else
                    {
                        tCategoryName.Text = null;
                    }
                }
            }
        }

        private void tProductName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(lookSupplier.Text))
            {
                if (!string.IsNullOrWhiteSpace(tCategoryName.Text))
                {
                    int supplierID = Convert.ToInt32(lookSupplier.EditValue);
                    fSearchProduct<fAddProduct> searchProduct = new fSearchProduct<fAddProduct>(supplierID, categoryID, this);
                    searchProduct.ShowDialog();
                }
                else
                {
                    Alert("Kateqoriya seçimi edilmədi", Enums.MessageType.Warning);
                }
            }
            else
            {
                Alert("Təchizatçı seçimi edilmədi", Enums.MessageType.Warning);
            }
        }

        private void tBarcode_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            tBarcode.Text = CreateBarcode();
        }

        private string CreateBarcode()
        {
            Random rastgele = new Random();
            string ida = "";
            SqlConnection conn = new SqlConnection(Properties.Settings.Default.SqlCon);
            SqlCommand cmd = new SqlCommand();
            conn.Open();
            string query = "SELECT MAX([MAL_ALISI_DETAILS_ID])+1 AS id  FROM  [MAL_ALISI_DETAILS]";

            cmd.Connection = conn;
            cmd.CommandText = query;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string id = dr["id"].ToString();
                ida = id;
            }


            string a1 = "994";
            string a2 = "";
            string a3 = "";
            string barkoda1 = "";
            if (ida.Length == 1)
            {
                int sayi = rastgele.Next(1000, 9999);
                a2 = sayi.ToString();
            }
            else if (ida.Length == 2)
            {
                int sayi2 = rastgele.Next(100, 999);
                a2 = sayi2.ToString();
            }
            else if (ida.Length == 3)
            {
                int sayi3a = rastgele.Next(10, 999);
                a2 = sayi3a.ToString();
            }
            else if (ida.Length == 4)
            {
                int sayi4 = rastgele.Next(0, 9);
                a2 = sayi4.ToString();
            }

            int sayi3 = rastgele.Next(1000, 9999);
            a3 = sayi3.ToString();

            barkoda1 = a1 + ida + a2 + a3;

            string code = barkoda1;
            var reversed = code.Reverse().ToArray();
            var sum =
               (from i in Enumerable.Range(0, reversed.Count())
                let digit = (int)char.GetNumericValue(reversed[i])
                select digit * (i % 2 == 0 ? 3 : 1)).Sum();
            var kontrol1 = (10 - sum % 10) % 10;

            return barkoda1 + kontrol1.ToString();
        }

        private void Clear()
        {
            tCategoryName.Text = null;
            tBarcode.Text = null;
            tProductName.Text = null;
            tProductCode.Text = null;
            tComment.Text = null;
            tQuantity.EditValue = "";
            tSalePrice.EditValue = "";
            tPurchasePrice.EditValue = "";
            tDiscountPercentage.EditValue = "";
            tDiscountAmount.EditValue = "";
            dateİstehsal.Text = null;
            dateBitis.Text = null;
            spinEdit1.EditValue = 0;
        }

        private void GetAllData(string proccessNo, ProductOperation operation)
        {
            if (operation is ProductOperation.Add)
            {
                using (SqlConnection connection = new SqlConnection(DbHelpers.DbConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.fn_MAL_ALISI_LOAD(@pricepoint)", connection))
                    {
                        cmd.Parameters.AddWithValue("@pricepoint", proccessNo);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);

                                // Əgər _currentDataTable boş deyilsə, sadəcə yeni sətirləri əlavə et
                                if (_currentDataTable.Rows.Count > 0)
                                {
                                    foreach (DataRow row in dt.Rows)
                                    {
                                        string malDetailsId = row["MAL_ALISI_DETAILS_ID"].ToString();

                                        // Əgər bu sətir varsa _currentDataTable'a əlavə etmə
                                        if (!_currentDataTable.AsEnumerable()
                                                             .Any(r => r["MAL_ALISI_DETAILS_ID"].ToString() == malDetailsId))
                                        {
                                            _currentDataTable.ImportRow(row);
                                        }
                                    }
                                }
                                else
                                {
                                    // Əgər _currentDataTable tamamilə boşdursa, yeni DataTable olaraq təyin et
                                    _currentDataTable = dt;

                                    // Manual sətirləri işarətləmək üçün IsManual sütununu yarat
                                    if (!_currentDataTable.Columns.Contains("IsManual"))
                                    {
                                        _currentDataTable.Columns.Add("IsManual", typeof(bool));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (operation is ProductOperation.Delete)
            {
                //_currentDataTable əgər null vəya sətir sayısı sıfırdırsa yeni sütunları yaratsın
                if (_currentDataTable == null || _currentDataTable.Rows.Count == 0)
                {
                    _currentDataTable.Columns.Add("MAL_ALISI_DETAILS_ID", typeof(int));
                    _currentDataTable.Columns.Add("TƏCHİZATÇI ADI", typeof(string));
                    _currentDataTable.Columns.Add("MƏHSUL ADI", typeof(string));
                    _currentDataTable.Columns.Add("MƏHSUL KODU", typeof(string));
                    _currentDataTable.Columns.Add("MİQDARI", typeof(decimal));
                    _currentDataTable.Columns.Add("ALIŞ QİYMƏTİ", typeof(decimal));
                    _currentDataTable.Columns.Add("MƏNFƏƏT", typeof(decimal));
                    _currentDataTable.Columns.Add("SATIŞ QİYMƏTİ", typeof(decimal));
                    _currentDataTable.Columns.Add("YEKUN MƏBLƏĞ", typeof(decimal));
                    _currentDataTable.Columns.Add("STATUS", typeof(string));
                    _currentDataTable.Columns.Add("IsManual", typeof(bool));
                }


                DataRow row = _currentDataTable.NewRow();
                row["MAL_ALISI_DETAILS_ID"] = productID;
                row["TƏCHİZATÇI ADI"] = lookSupplier.Text.Trim();
                row["MƏHSUL ADI"] = tProductName.Text.Trim();
                row["MƏHSUL KODU"] = tProductCode.Text.Trim();
                row["MİQDARI"] = 0;
                row["ALIŞ QİYMƏTİ"] = tPurchasePrice.Text;
                row["MƏNFƏƏT"] = 0;
                row["SATIŞ QİYMƏTİ"] = tSalePrice.Text;
                row["YEKUN MƏBLƏĞ"] = 0;
                row["STATUS"] = "SİLİNDİ";
                row["IsManual"] = true;

                _currentDataTable.Rows.Add(row);

                Log($"{tProductName.Text} məhsulu anbardan silindi");
                Clear();
            }

            gridControl1.DataSource = _currentDataTable;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns["IsManual"].Visible = false;
        }

        private void bSendWarehouse_Click(object sender, EventArgs e)
        {
            tProccessNo.Text = DbProsedures.GET_ProductProcessNo();
            lookSupplier.Enabled = true;
            Clear();
            lookSupplier.Text = null;
            gridControl1.DataSource = null;
            _currentDataTable.Clear();
        }

        private void tProductCode_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tBarcode.Text))
            {
                tProductCode.Text = CreateBarcode();
            }
            else
            {
                tProductCode.Text = tBarcode.Text;
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void TotalCalc(string quantity, string purchasePrice, string discountPercent, string discountAzn)
        {
            string query = @"exec yekun_mebleg_calc 
            @migdar =@pricePoint,
            @alis_giymet =@pricePoint1,
            @endirim_faiz =@pricePoint2,
            @endirim_azn =@pricePoint3";
            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@pricePoint", quantity);
                        cmd.Parameters.AddWithValue("@pricePoint1", purchasePrice);
                        cmd.Parameters.AddWithValue("@pricePoint2", discountPercent);
                        cmd.Parameters.AddWithValue("@pricePoint3", discountAzn);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                tDiscountTotal.Text = dr["endirim_meblegi"].ToString();
                                tTotalAmount.Text = dr["yekun_mebleg"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }

        }

        private void tDiscountAmount_TextChanged(object sender, EventArgs e)
        {
            TotalCalc(tQuantity.Text, tPurchasePrice.Text, tDiscountPercentage.Text, tDiscountAmount.Text);
        }

        private void tDiscountPercentage_TextChanged(object sender, EventArgs e)
        {
            TotalCalc(tQuantity.Text, tPurchasePrice.Text, tDiscountPercentage.Text, tDiscountAmount.Text);
        }

        private void tPurchasePrice_TextChanged_1(object sender, EventArgs e)
        {
            TotalCalc(tQuantity.Text, tPurchasePrice.Text, tDiscountPercentage.Text, tDiscountAmount.Text);
        }

        private void tQuantity_TextChanged(object sender, EventArgs e)
        {
            TotalCalc(tQuantity.Text, tPurchasePrice.Text, tDiscountPercentage.Text, tDiscountAmount.Text);
        }

        private void lookSupplier_TextChanged(object sender, EventArgs e)
        {
            if (lookSupplier.EditValue != null)
            {
                YeniBorcHesabla();
                QaliqBorcHesabla(Convert.ToInt32(lookSupplier.EditValue));
                TotalBorcHesabla();
            }
        }

        private void bEdit_Click(object sender, EventArgs e)
        {
            //Müvəqqəti olaraq deaktiv edilmiştir
            return;
            YeniBorcHesabla();
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show($"{tProductName.Text} məhsulunu silmək istədiyinizə əminsiniz ?", nameof(HeaderMessage.Xəbərdarlıq), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int response = DbProsedures.DeleteProduct(new ProductsDetail
                {
                    SupplierName = lookSupplier.Text,
                    Barocde = tBarcode.Text.Trim(),
                    ProductId = productID
                });

                if (response == 1)
                {
                    GetAllData(tProccessNo.Text, ProductOperation.Delete);
                }
            }
        }

        private void fAddProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason is CloseReason.UserClosing)
            {
                DialogResult result = XtraMessageBox.Show("SƏHİFƏDƏN ÇIXMAQ İSTƏDİYİNİZƏ ƏMİNSİNİZ ?", nameof(HeaderMessage.Xəbərdarlıq), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result is DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void bNewSupplier_Click(object sender, EventArgs e)
        {
            fAddSupplier fAddSupplier = new fAddSupplier();
            if (fAddSupplier.ShowDialog() is DialogResult.OK)
            {
                SupplierDataLoad();
            }
        }
    }
}