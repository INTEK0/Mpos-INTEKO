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

namespace WindowsFormsApp2
{
    public partial class fShortcuts : DevExpress.XtraEditors.XtraForm
    {
        public fShortcuts()
        {
            InitializeComponent();
        }

        private void fShortcuts_Load(object sender, EventArgs e)
        {
            // DataTable oluştur
            DataTable dataTable = new DataTable();

            // Sütunları ekle
            dataTable.Columns.Add("Keys", typeof(string));
            dataTable.Columns.Add("Comment", typeof(string));

            // Verileri ekle
            dataTable.Rows.Add("CTRL + N", "Satış ekranını təmizləyir.");
            dataTable.Rows.Add("F1", "Barkod qutusuna fokuslanır");
            dataTable.Rows.Add("F2", "Növbəni aç");
            dataTable.Rows.Add("F3", "Növbəni bağla");
            dataTable.Rows.Add("F5", "Nağd satış");
            dataTable.Rows.Add("F6", "Kart satış");
            dataTable.Rows.Add("F7", "Nağd & Kart satış");
            dataTable.Rows.Add("F8", "Təkrar qəbz");
            dataTable.Rows.Add("F9", "Qaytarma");

            // GridControl'e veri kaynağını bağla
            gridControl1.DataSource = dataTable;

            // GridView özelleştirmeleri
            gridView1.OptionsBehavior.Editable = false; // Düzenlemeyi devre dışı bırak

            // İsteğe bağlı olarak sütunları özelleştir
            gridView1.Columns["Keys"].Caption = "Qısa yol"; // Sütun başlığını değiştir
            gridView1.Columns["Comment"].Caption = "Qeyd"; // Sütun başlığını değiştir
        }
    }
}