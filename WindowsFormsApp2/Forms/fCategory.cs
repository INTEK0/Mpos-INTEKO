using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
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

namespace WindowsFormsApp2.Forms
{
    public partial class fCategory<TParent> : BaseForm where TParent : BaseForm
    {
        private TParent parentForm;
        public fCategory(TParent _parent)
        {
            InitializeComponent();
            parentForm = _parent;
        }

        private void fCategory_Load(object sender, EventArgs e)
        {
            GetDataLoad();
        }

        private void GetDataLoad()
        {
            string query = "select KATEGORIYA_ID,KATEGORIYA as  N'KATEQORİYA' from KATEGORIYA";
            var data = DbProsedures.ConvertToDataTable(query);
            gridControl1.DataSource = data;
            //gridView1.Columns[0].Visible = false;
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
        }

        private async void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button is MouseButtons.Left && e.Clicks is 2)
            {
                GridView view = sender as GridView;
                if (view != null && view.FocusedRowHandle >= 0)
                {
                    int Id = Convert.ToInt32(gridView1.GetFocusedRowCellValue("KATEGORIYA_ID").ToString());
                    DatabaseClasses.Categories categories = await GetCategoryByIdAsync(Id);
                    if (parentForm.Name == "fAddProduct" && categories != null)
                    {
                        var method = parentForm.GetType().GetMethod("ReceiveData");
                        if (method != null)
                        {
                            method.MakeGenericMethod(categories.GetType()).Invoke(parentForm, new object[] { categories });
                            this.Close();
                        }
                    }
                }
            }
        }

        private async Task<DatabaseClasses.Categories> GetCategoryByIdAsync(int id)
        {
            DatabaseClasses.Categories categories = null;
            string query = "select KATEGORIYA_ID,KATEGORIYA as  N'KATEQORİYA' from KATEGORIYA WHERE KATEGORIYA_ID = @categoryId";
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon))
            {
                await connection.OpenAsync();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@categoryId", id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            categories = new DatabaseClasses.Categories
                            {
                                KATEGORIYA_ID = reader.GetInt32(0),
                                KATEGORIYA = reader.GetString(1),
                            };
                        }
                    }
                }
            }
            return categories;
        }

        private async void bCategoryEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gridView1.GetFocusedRow() is null)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ErrorMessages.NOT_SELECTION);
            }
            tablePanel1.Rows[0].Visible = true;
            int categoryId = Convert.ToInt32(gridView1.GetFocusedRowCellValue("KATEGORIYA_ID").ToString());
            DatabaseClasses.Categories categories = await GetCategoryByIdAsync(categoryId);
            tCategoryName.Text = categories.KATEGORIYA;
        }

        private void bSave_Click(object sender, EventArgs e)
        {

        }
    }
}