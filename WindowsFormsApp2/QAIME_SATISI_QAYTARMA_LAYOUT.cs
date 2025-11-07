using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using DevExpress.XtraEditors;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class QAIME_SATISI_QAYTARMA_LAYOUT : XtraForm
    {
        public QAIME_SATISI_QAYTARMA_LAYOUT()
        {
            InitializeComponent();
            GridPanelText(gridView1);
        }

        private void QAIME_SATISI_QAYTARMA_LAYOUT_Load(object sender, EventArgs e)
        {
            dateTarix.Properties.MaxDate = DateTime.Today;
            dateTarix.Text = DateTime.Now.ToShortDateString();
            tProccessNo.Text = DbProsedures.GET_GaimeRefundProccessNo();
        }

        CRUD_GAIME_SATISI CG = new CRUD_GAIME_SATISI();

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            gridView1.UpdateCurrentRow();


            int[] selectedRows = gridView1.GetSelectedRows();
            if (selectedRows.Length > 0)
            {
                foreach (int item in selectedRows)
                {
                    var row = gridView1.GetRow(item) as Product;

                    if (row.ReturnProduct <= 0)
                    {
                        FormHelpers.Alert("Seçili olan sətirdə qaytarılacaq miqdar daxil edilmədi", Enums.MessageType.Warning);
                        return;
                    }

                    if (row.Id > 0)
                    {
                        CG.insert_gaime_satis_gaytarma_proc_(
                            row.Id,
                            row.ReturnProduct,
                            tProccessNo.Text,
                            Convert.ToDateTime(dateTarix.Text),
                            tComment.Text);

                        FormHelpers.Log($"{row.ProccessNo} nömrəli satış qaiməsində {row.ReturnProduct} ədəd {row.ProductName} məhsulu geri qaytarıldı");

                    }
                }

                FormHelpers.Alert("Məhsul uğurla geri qaytarıldı", Enums.MessageType.Success);

                Clear();
            }
            else
            {
                FormHelpers.Alert("Məhsul seçimi edilmədi", Enums.MessageType.Warning);
            }

        }

        private void Clear()
        {
            gridControl1.DataSource = null;
            gridView1.RefreshData();
            tTotalAmount.Clear();
            tCustomerName.Text = "";
            tContractNo.Text = "";
            tProccessNo.Text = DbProsedures.GET_GaimeRefundProccessNo();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.OpenForm<gaytarilan_siyahi>();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            musteri_gaytar MG = new musteri_gaytar(this);
            MG.Show();
        }
        public void data_(string musteri_adi_, string _emeliyyat_nomre_, string gaime_main_id_, string gaime_nomre_)
        {
            GetallData_id_(_emeliyyat_nomre_);
            tCustomerName.Text = musteri_adi_;
            tContractNo.Text = gaime_nomre_;
        }

        private void GetallData_id_(string id_)
        {
            Clear();

            string queryString = $@"SELECT gd.GAIME_SATISI_DETAILS_ID AS Id,
       gm.EMELIIYYAT_NOMRE AS N'ProccessNo',
	   gm.TARIX AS N'Date',
       ct.SIRKET_ADI AS N'SupplierName',
       md.MEHSUL_ADI AS N'ProductName',
       CAST(gd.SATIS_GIYMETI AS decimal(18,3)) as 'SalePrice',
       gd.MIGDARI AS N'Quantity',
       0 AS N'ReturnProduct',
	   FORMAT(CAST(gd.YEKUN_MEBLEG AS decimal(18,5)), 'N2', 'az-AZ') as TotalAmount
from GAIME_SATISI_MAIN gm
inner
 join GAIME_SATISI_DETAILS gd on gm.GAIME_SATISI_MAIN_ID = gd.GAIME_SATISI_MAIN_ID
left join (
             (select gaime_satis_details_id,
                     sum(isnull(migdar, 0.00)) migdar
              from gaime_satis_gaytarma group  by gaime_satis_details_id)) gdg on gdg.gaime_satis_details_id=gd.GAIME_SATISI_DETAILS_ID
inner join MAL_ALISI_DETAILS md on md.MAL_ALISI_DETAILS_ID = gd.MAL_DETAILS_ID
inner join MAL_ALISI_MAIN mm on mm.MAL_ALISI_MAIN_ID = md.MAL_ALISI_MAIN_ID
inner join COMPANY.TECHIZATCI ct on ct.TECHIZATCI_ID = mm.TECHIZATCI_ID
WHERE GM.EMELIIYYAT_NOMRE = '{id_}'
  and gd.MIGDARI -isnull(gdg.migdar, 0.00)>0";

            var data = DbProsedures.ConvertToDataTable(queryString);

            BindingList<Product> _Products = new BindingList<Product>();

            foreach (DataRow item in data.Rows)
            {
                Product product = new Product();
                product.Id = Convert.ToInt32(item["Id"].ToString());
                product.ProccessNo = item["ProccessNo"].ToString();
                product.SupplierName = item["SupplierName"].ToString();
                product.Date = Convert.ToDateTime(item["Date"].ToString());
                product.ProductName = item["ProductName"].ToString();
                product.SalePrice = Convert.ToDecimal(item["SalePrice"].ToString());
                product.Quantity = item["Quantity"].ToString().Trim();
                product.TotalAmount = Convert.ToDecimal(item["TotalAmount"].ToString());
                product.ReturnProduct = 0;
                _Products.Add(product);
            }

            gridControl1.DataSource = _Products;
            tTotalAmount.Text = _Products.Sum(x => x.TotalAmount).ToString("C2");
        }

        private class Product
        {
            public int Id { get; set; }
            public string ProccessNo { get; set; }
            public DateTime? Date { get; set; }
            public string SupplierName { get; set; }
            public string ProductName { get; set; }
            public decimal SalePrice { get; set; }
            public string Quantity { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal ReturnProduct { get; set; }
        }
    }
}