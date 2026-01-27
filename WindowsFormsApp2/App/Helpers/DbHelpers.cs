using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WindowsFormsApp2.App.Dtos;

namespace WindowsFormsApp2.App.Helpers
{
    public class DbHelpers
    {
        public static InvoiceProductDto GetInvoiceData()
        {
            var result = new InvoiceProductDto();
            result.rows = new List<InvoiceProductDto.Row>();

            using (var conn = new SqlConnection(WindowsFormsApp2.Helpers.DB.DbHelpers.CurrentConnectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM MAHSUL_ALIS_HESABATI", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (result.voen == null)
                            result.voen = reader["VOEN"].ToString();

                        var tarix = ((DateTime)reader["TARİX"]).Date;

                        result.rows.Add(new InvoiceProductDto.Row
                        {
                            tarix = tarix.ToString("yyyy-MM-dd"),
                            istifadeciAdi = reader["İSTİFADƏÇİ ADI"].ToString(),
                            fakturaNo = reader["FAKTURA №"].ToString(),
                            techizatciAdi = reader["TƏCHİZATÇININ ADI"].ToString(),
                            mehsulAdi = reader["MƏHSULUN ADI"].ToString(),
                            mehsulKodu = reader["MƏHSULUN KODU"].ToString(),
                            miqdari = Convert.ToDouble(reader["MİQDARI"]),
                            alisQiymeti = Convert.ToDouble(reader["ALIŞ QİYMƏTİ"]),
                            endirimFaiz = Convert.ToInt32(reader["ENDİRİM FAİZ"]),
                            endirimAzn = Convert.ToDouble(reader["ENDİRİM AZN"]),
                            endirimMeblegi = Convert.ToDouble(reader["ENDİRİM MƏBLƏĞİ"]),
                            odenilecekMebleg = Convert.ToDouble(reader["ÖDƏNİLƏCƏK MƏBLƏĞ"])
                        });
                    }
                }
            }

            return result;
        }

        public static SaleDetailsDto GetSaleDetailsData()
        {
            var result = new SaleDetailsDto();
            result.rows = new List<SaleDetailsDto.Row>();

            using (var conn = new SqlConnection(WindowsFormsApp2.Helpers.DB.DbHelpers.CurrentConnectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM IZAHLI_SATIS", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (result.voen == null)
                            result.voen = reader["VOEN"].ToString();

                        result.rows.Add(new SaleDetailsDto.Row
                        {
                            tarix = Convert.ToDateTime(reader["TARİX"].ToString()),
                            istifadeci = reader["İSTİFADƏÇİ"].ToString(),
                            techizatci = reader["TƏCHİZATÇI"].ToString(),
                            category = reader["CATEGORY"].ToString(),
                            mehsulAdi = reader["MƏHSUL ADI"].ToString(),
                            customerName = reader["CUSTOMER_NAME"].ToString(),
                            doctorName = reader["DOCTOR_NAME"].ToString(),
                            miqdari = Convert.ToDouble(reader["MİQDARI"]),
                            vahidi = reader["VAHİDİ"].ToString(),
                            satisQiymeti = Convert.ToDouble(reader["SATIŞ QİYMƏTİ"]),
                            endirimAzn = Convert.ToDouble(reader["ENDİRİM AZN"]),
                            odenisNovu = reader["ÖDƏNİŞ NÖVÜ"].ToString(),
                            cemOdenis = Convert.ToDouble(reader["CƏM ÖDƏNİŞ"]),
                            vergiFaiz = Convert.ToInt32(reader["VERGİ %"]),
                            processNo = reader["ProccessNo"].ToString(),
                            qebz = reader["QƏBZ"].ToString(),
                        });
                    }
                }
            }

            return result;
        }

        public static PaymentTypesDto PaymentTypesData()
        {
            var result = new PaymentTypesDto();
            result.items = new List<PaymentTypesDto.Row>();

            using (var conn = new SqlConnection(WindowsFormsApp2.Helpers.DB.DbHelpers.CurrentConnectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM ODENISGUNLUK", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (result.voen == null)
                            result.voen = reader["VOEN"].ToString();

                        var tarix = ((DateTime)reader["TARİX"]).Date;

                        result.items.Add(new PaymentTypesDto.Row
                        {
                            TARİX = tarix.ToString("yyyy-MM-dd"),
                            NAĞD = Convert.ToDouble(reader["NAĞD"]),
                            KART = Convert.ToDouble(reader["KART"]),
                            BANK = Convert.ToDouble(reader["BANK"]),
                            NİSYƏ = Convert.ToDouble(reader["NİSYƏ"]),
                        });
                    }
                }
            }

            return result;
        }
    }
}
