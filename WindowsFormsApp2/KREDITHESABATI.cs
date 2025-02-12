﻿using DevExpress.XtraGrid.Localization;
using System;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    public partial class KREDITHESABATI : DevExpress.XtraEditors.XtraForm
    {
        public KREDITHESABATI()
        {
            InitializeComponent();
            GridPanelText(gridView1);
            GridLocalizer.Active = new MyGridLocalizer();
        }

        private void KREDITHESABATI_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            dateEdit1.Text = dateTime.ToShortDateString();
            dateEdit2.Text = dateTime.ToShortDateString();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                GetallData_id_(Convert.ToDateTime(dateEdit1.Text), Convert.ToDateTime(dateEdit2.Text));
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
            }
        }

        public void GetallData_id_(DateTime D1_, DateTime D2_)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.SqlCon);
            string queryString = "EXEC dbo.KREDIT_HESABAT1  @d1 = @pricepoint  ,@d2=@pricepoint1 ";

            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@pricepoint", D1_);
            command.Parameters.AddWithValue("@pricepoint1", D2_);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormHelpers.ExcelExport(gridControl1, "Kredit satış hesabatı");
        }
    }
}