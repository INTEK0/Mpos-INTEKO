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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Services;

namespace WindowsFormsApp2.Forms
{
    public partial class fProductMovementReport : DevExpress.XtraEditors.XtraForm
    {
        RepositoryItemTextEdit repositoryF3;
        RepositoryItemTextEdit repositoryF0;

        public fProductMovementReport()
        {
            InitializeComponent();
        }

        private void fProductMovementReport_Load(object sender, EventArgs e)
        {
            GridRepoAdd();
            dateEdit1.DateTime = DatetimeService.FirstDayOfCurrentMonth;
            dateEdit2.DateTime = DatetimeService.CurrentDateTime;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string query = "select * from VW_ProductMovementReport order by OperationDate asc";
            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                using (DataTable dt = new DataTable())
                {
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                    string InQuantityfieldName = "InputQuantity";
                    string OutQuantityfieldName = "OutputQuantity";

                    gridView1.Columns[InQuantityfieldName].Summary.Clear();
                    gridView1.Columns[OutQuantityfieldName].Summary.Clear();
                    GridColumnSummaryItem inQuantity = new GridColumnSummaryItem
                    {
                        FieldName = InQuantityfieldName,
                        SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        DisplayFormat = "{0:N2}"
                    };
                    GridColumnSummaryItem outQuantity = new GridColumnSummaryItem
                    {
                        FieldName = OutQuantityfieldName,
                        SummaryType = DevExpress.Data.SummaryItemType.Sum,
                        DisplayFormat = "{0:N2}"
                    };
                    gridView1.Columns[InQuantityfieldName].Summary.Add(inQuantity);
                    gridView1.Columns[OutQuantityfieldName].Summary.Add(outQuantity);
                }
            }
        }

        private void GridRepoAdd()
        {
            repositoryF3 = new RepositoryItemTextEdit();
            repositoryF3.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repositoryF3.Mask.EditMask = "F3";
            repositoryF3.Mask.UseMaskAsDisplayFormat = true;

            repositoryF0 = new RepositoryItemTextEdit();
            repositoryF0.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            repositoryF0.Mask.EditMask = "F0";
            repositoryF0.Mask.UseMaskAsDisplayFormat = true;

            gridControl1.RepositoryItems.Add(repositoryF3);
            gridControl1.RepositoryItems.Add(repositoryF0);
        }

        private void gridView1_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            var view = sender as GridView;

            if (e.Column.FieldName is "InputQuantity")
            {
                var unitName = view.GetRowCellValue(e.RowHandle, "UnitName")?.ToString();

                if (unitName.ToLower() is "ədəd")
                    e.RepositoryItem = repositoryF0;
                else
                    e.RepositoryItem = repositoryF3;
            }

            if (e.Column.FieldName is "OutputQuantity")
            {
                var unitName = view.GetRowCellValue(e.RowHandle, "UnitName")?.ToString();

                if (unitName.ToLower() is "ədəd")
                    e.RepositoryItem = repositoryF0;
                else
                    e.RepositoryItem = repositoryF3;
            }
        }
    }
}