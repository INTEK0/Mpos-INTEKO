using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

                        result.rows.Add(new InvoiceProductDto.Row
                        {
                            tarix = reader["TARİX"].ToString(),
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
    }
}
