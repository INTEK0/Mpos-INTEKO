using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WindowsFormsApp2.App.Dtos;
using WindowsFormsApp2.Helpers.DB;

namespace WindowsFormsApp2.App
{
    public class DbOperation
    {
        public static void SaleSend(PosSaleDto data)
        {
            string query = @"
        INSERT INTO CloudPosSale
        (
            Voen,
            posSaleId,
            ReceiptNo,
            ShortFiscalId,
            SaleDate,
            UserId,
            ProccessNo,
            Cash,
            [Card],
            TotalAmount,
            BankRrn,
            BankTransactionId,
            CustomerName,
            DoctorName
        )
        VALUES
        (
            @Voen,
            @posSaleId,
            @ReceiptNo,
            @ShortFiscalId,
            @SaleDate,
            @UserId,
            @ProccessNo,
            @Cash,
            @Card,
            @TotalAmount,
            @BankRrn,
            @BankTransactionId,
            @CustomerName,
            @DoctorName
        );";

            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add("@Voen", SqlDbType.NVarChar, 15).Value = data.Voen;
                cmd.Parameters.Add("@posSaleId", SqlDbType.Int).Value = data.PosSaleId;
                cmd.Parameters.Add("@ReceiptNo", SqlDbType.NVarChar, 50).Value = data.ReceiptNo;
                cmd.Parameters.Add("@ShortFiscalId", SqlDbType.NVarChar, 20).Value = data.ShortFiscalId;
                cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime).Value = data.SaleDate;
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = data.UserId;
                cmd.Parameters.Add("@ProccessNo", SqlDbType.NVarChar, 20).Value = data.ProccessNo;

                cmd.Parameters.Add("@Cash", SqlDbType.Decimal).Value = data.Cash;
                cmd.Parameters.Add("@Card", SqlDbType.Decimal).Value = data.Card;
                cmd.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = data.TotalAmount;

                cmd.Parameters.Add("@BankRrn", SqlDbType.NVarChar, 20).Value = (object)data.BankRRN ?? DBNull.Value;
                cmd.Parameters.Add("@BankTransactionId", SqlDbType.NVarChar, 20).Value = (object)data.BankTransactionId ?? DBNull.Value;
                cmd.Parameters.Add("@CustomerName", SqlDbType.NVarChar,100).Value = (object)data.CustomerName ?? DBNull.Value;
                cmd.Parameters.Add("@DoctorName", SqlDbType.NVarChar, 100).Value = (object)data.DoctorName ?? DBNull.Value;

                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    SaleUpdate(data.PosSaleId);
                }
            }
        }

        public static void SaleUpdate(int posSaleId)
        {
            string query = @"UPDATE pos_satis_check_main SET IsSendServer = @IsSend, IsSendServerDate = @ServerDate WHERE pos_satis_check_main_id = @Id";

            using (SqlConnection con = new SqlConnection(DbHelpers.CurrentConnectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.Add("@IsSend", SqlDbType.Bit).Value = true;
                cmd.Parameters.Add("@ServerDate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = posSaleId;

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static void GetSaleData(List<PosSaleDto> dataList)
        {

        }
    }
}
