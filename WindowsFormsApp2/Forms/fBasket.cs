using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp2.Helpers.Enums;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers;
using DevExpress.DashboardWin.Design;
using System.Data.SqlClient;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace WindowsFormsApp2.Forms
{
    public partial class fBasket : DevExpress.XtraEditors.XtraForm
    {
        public string BasketName;
        public fBasket(string _basketName)
        {
            InitializeComponent();
            BasketName = _basketName;
            DataLoad();
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (DbProsedures.ExportPosBasketData(this.Text))
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void DataLoad()
        {
            gridControl1.DataSource = DbProsedures.GET_BasketDataLoad(BasketName);
            gridView1.GroupPanelText = $"Məhsul sayı: {gridView1.RowCount}";

            decimal total = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                //var miqdar = Decimal.Parse(gridView1.GetRowCellValue(i,"MİQDAR").ToString());
                //var salePrice = Decimal.Parse(gridView1.GetRowCellValue(i,"SATIŞ QİYMƏTİ").ToString());

                //total += miqdar * salePrice;

                total += Decimal.Parse(gridView1.GetRowCellValue(i, "ÜMUMİ MƏBLƏĞ").ToString());
            }
            lTotal.Text = total.ToString("C2");
        }
    }
}