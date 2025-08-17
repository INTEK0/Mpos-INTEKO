using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.Forms
{
    public partial class fUserRoleShow : DevExpress.XtraEditors.XtraForm
    {
        private readonly int _userId;
        List<RoleDisplayModel> roleList = new List<RoleDisplayModel>();
        public fUserRoleShow(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void fUserRoleShow_Load(object sender, EventArgs e)
        {
            UserRoleDataLoad();
        }

        private void UserRoleDataLoad()
        {
            tUsername.Text = DbProsedures.GetUser(_userId).Username;
            tNameSurname.Text = DbProsedures.GetUser(_userId).NameSurname;


            var roleMappings = new Dictionary<string, (string Group, string DisplayName)>
            {
                { "ProductAdd", ("Məhsul", "Məhsul Alışı") },
                { "RefundProduct", ("Məhsul", "Məhsul qaytarma") },
                { "ProductDelete", ("Məhsul", "Məhsul silmə") },
                { "ProductDiscount", ("Məhsul", "Məhsul endirimi") },
                { "ProductBarcodePrint", ("Məhsul", "Barkod çap") },
                { "ScalesProductDownload", ("Məhsul", "Tərəziyə məhsul yükləmə") },
                { "Suppliers", ("Təchizatçılar", "Təchizatçılar") },
                { "Customers", ("Müştərilər", "Müştərilər") },

                { "BankSale", ("Satış", "Qaimə satış") },
                { "Credit", ("Satış", "Kredit") },
                { "PosPrepayment", ("Satış", "Avans") },
                { "PosSale", ("Satış", "Pos satış") },
                { "PosRefund", ("Satış", "Pos satış qaytarma") },
                { "PosSalePriceEdit", ("Satış", "Pos satış qiymət dəyiştirmə") },
                { "PosSalePriceLimit", ("Satış", "Kassa satış limiti") },

                { "Report", ("Hesabatlar", "Hesabatlar") },
                { "Payments", ("Digər", "Ödənişlər") },
                { "TerminalDelete", ("Digər", "Terminal silmə") },
                { "Users", ("Digər", "İstifadəçilər") },
                { "Backups", ("Digər", "Backup") },
                { "Logs", ("Digər", "Arxivə nəzarət") },
                { "ScalesDelete", ("Digər", "Tərəzi silmə") },
            };

            using (SqlConnection conn = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                conn.Open();
                string query = $"SELECT * FROM UserRole WHERE UserId = {_userId}";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        foreach (var map in roleMappings)
                        {
                            string propName = map.Key;
                            var (groupName, displayName) = map.Value;

                            object value = reader[propName];
                            if (value == DBNull.Value) value = false;

                            roleList.Add(new RoleDisplayModel
                            {
                                GroupName = groupName,
                                Name = displayName,
                                Value = value
                            });
                        }
                    }
                }
            }

            gridControl1.DataSource = roleList;
        }

        public class RoleDisplayModel
        {
            public string GroupName { get; set; }
            public string Name { get; set; }
            public object Value { get; set; }
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colValue)
            {
                if (e.Value != null || !string.IsNullOrWhiteSpace(e.DisplayText))
                {
                    if (e.Value.GetType() == typeof(bool))
                    {
                        if ((bool)e.Value is true)
                        {
                            e.DisplayText = "Aktiv";
                        }
                        else
                        {
                            e.DisplayText = "Deaktiv";
                        }
                    }
                    else if (e.Value.GetType() == typeof(decimal))
                    {
                        if ((decimal?)e.Value != null)
                        {
                            e.DisplayText = e.Value.ToString();
                        }
                        else
                        {
                            e.DisplayText = "Yoxdur";
                        }
                    }

                }
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.Column == colValue && e.CellValue != null)
            {
                e.Appearance.FontStyleDelta = FontStyle.Bold;
                if (e.CellValue.GetType() == typeof(bool))
                {
                    if ((bool)e.CellValue == true)
                    {
                        e.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
                    }
                    else if ((bool)e.CellValue == false)
                    {
                        e.Appearance.ForeColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
                    }
                }
            }
        }
    }
}