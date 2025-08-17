using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DevExpress.XtraGrid.Localization;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class techizatci_odenisleri_hesabar : DevExpress.XtraEditors.XtraForm
    {
        public techizatci_odenisleri_hesabar()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void techizatci_odenisleri_hesabar_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;

            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit1.Text = dateTime.ToShortDateString();
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ExcelExport(gridControl1, "Təchizatçı ödənişləri hesabatı");
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dateEdit1.Text))
            {
                Alert("TARİX ARALIĞI SEÇİLMƏYİB", Helpers.Enums.MessageType.Warning);
            }
            else
            {
                LOAD(Convert.ToDateTime(dateEdit1.Text));
            }
        }

        private void LOAD(DateTime endDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
                {
                    string queryString = "select * from dbo.TECHIZATCI_borclu_all_HESABAT (@endDate) ";
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@endDate", endDate);
                        using (SqlDataAdapter da = new SqlDataAdapter(command))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                da.Fill(dt);
                                gridControl1.DataSource = dt;
                                gridView1.Columns[0].Visible = false;
                                gridView1.Columns["SB_ID"].Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(e.Message);
            }
        }

        private async Task TotalSupplierDebt()
        {
            using (SqlConnection connection = new SqlConnection(DbHelpers.CurrentConnectionString))
            {
                await connection.OpenAsync();
                string query = "sp_GetAllSupplierDebt";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var totalAmountParam = new SqlParameter("@TOTAL_DEBT", SqlDbType.Decimal)
                    {
                        Precision = 18,
                        Scale = 4,
                        Direction = ParameterDirection.Output
                    };
                    var mainAmountParam = new SqlParameter("@MAIN_DEBT", SqlDbType.Decimal)
                    {
                        Precision = 18,
                        Scale = 4,
                        Direction = ParameterDirection.Output
                    };
                    var taxAmountParam = new SqlParameter("@TAX_DEBT", SqlDbType.Decimal)
                    {
                        Precision = 18,
                        Scale = 4,
                        Direction = ParameterDirection.Output
                    };

                    cmd.Parameters.Add(totalAmountParam);
                    cmd.Parameters.Add(mainAmountParam);
                    cmd.Parameters.Add(taxAmountParam);

                    await cmd.ExecuteNonQueryAsync();


                    decimal totalAmount = (decimal)totalAmountParam.Value;
                    decimal mainAmount = (decimal)mainAmountParam.Value;
                    decimal taxAmount = (decimal)taxAmountParam.Value;

                    tTotalAmount.Text = totalAmount.ToString("N2");
                    tMainDebt.Text = mainAmount.ToString("N2");
                    tTaxDebt.Text = taxAmount.ToString("N2");
                }
            }
        }

        private async void techizatci_odenisleri_hesabar_Activated(object sender, EventArgs e)
        {
            await TotalSupplierDebt();
        }
    }
}