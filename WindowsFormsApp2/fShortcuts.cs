using System;
using System.Data;

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
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Keys", typeof(string));
            dataTable.Columns.Add("Comment", typeof(string));

            dataTable.Rows.Add("CTRL + N", "Satış ekranını təmizləyir.");
            dataTable.Rows.Add("F1", "Barkod qutusuna fokuslanır");
            dataTable.Rows.Add("F2", "Növbəni aç");
            dataTable.Rows.Add("F3", "Növbəni bağla");
            dataTable.Rows.Add("F5", "Nağd satış");
            dataTable.Rows.Add("F6", "Kart satış");
            dataTable.Rows.Add("F7", "Nağd & Kart satış");
            dataTable.Rows.Add("F8", "Təkrar qəbz");
            dataTable.Rows.Add("F9", "Qaytarma");
            dataTable.Rows.Add("F12", "Məhsul alışı");

            gridControl1.DataSource = dataTable;

            gridView1.OptionsBehavior.Editable = false;

            gridView1.Columns["Keys"].Caption = "Qısa yol";
            gridView1.Columns["Comment"].Caption = "Qeyd";
        }
    }
}