using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WindowsFormsApp2.App.Dtos;

namespace WindowsFormsApp2.App.Helpers
{
    public class DbHelpers
    {
        public static StockDto StockData()
        {
            var result = new StockDto();
            result.items = new List<StockDto.Items>();

            using (var conn = new SqlConnection(WindowsFormsApp2.Helpers.DB.DbHelpers.CurrentConnectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM VW_WAREHOUSE_STOCK", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (result.voen == null)
                            result.voen = reader["VOEN"].ToString();


                        result.items.Add(new StockDto.Items
                        {
                            SupplierName = reader["SupplierName"].ToString(),
                            CategoryName = reader["CategoryName"].ToString(),
                            ProductName = reader["ProductName"].ToString(),
                            ProductCode = reader["ProductCode"].ToString(),
                            ProductBarcode = reader["ProductBarcode"].ToString(),
                            UnitName = reader["UnitName"].ToString(),
                            TaxName = reader["TaxName"].ToString(),
                            PurchasePrice = Convert.ToDecimal(reader["PurchasePrice"].ToString()),
                            SalePrice = Convert.ToDecimal(reader["SalePrice"].ToString()),
                            Quantity = Convert.ToDecimal(reader["StockQuantity"].ToString()),
                        });
                    }
                }
            }

            return result;
        }

        public static InvoiceProductDto InvoiceData()
        {
            var result = new InvoiceProductDto();
            result.items = new List<InvoiceProductDto.Items>();

            using (var conn = new SqlConnection(WindowsFormsApp2.Helpers.DB.DbHelpers.CurrentConnectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM VW_PRODUCT_INVOICES", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (result.voen == null)
                            result.voen = reader["VOEN"].ToString();

                        var tarix = ((DateTime)reader["TARİX"]).Date;

                        result.items.Add(new InvoiceProductDto.Items
                        {
                            Date = tarix.ToString("yyyy-MM-dd"),
                            Username = reader["Username"].ToString(),
                            InvoiceNo = reader["FAKTURA NÖMRƏSİ"].ToString(),
                            SupplierName = reader["TƏCHİZATÇI ADI"].ToString(),
                            CategoryName = reader["KATEQORİYA"].ToString(),
                            ProductName = reader["MƏHSUL ADI"].ToString(),
                            ProductCode = reader["MƏHSUL KODU"].ToString(),
                            Barcode = reader["BARCODE"].ToString(),
                            Quantity = Convert.ToDouble(reader["MİQDARI"]),
                            UnitName = reader["VAHİDİ"].ToString(),
                            PurchasePrice = Convert.ToDouble(reader["ALIŞ QİYMƏTİ"]),
                            SalePrice = Convert.ToDouble(reader["SATIŞ QİYMƏTİ"]),
                            DiscountPercantages = Convert.ToInt32(reader["ENDİRİM FAİZ"]),
                            DiscountAzn = Convert.ToDouble(reader["ENDİRİM AZN"]),
                            DiscountTotalAmount = Convert.ToDouble(reader["ENDİRİM MƏBLƏĞİ"]),
                            PayableAmount = Convert.ToDouble(reader["YEKUN MƏBLƏĞ"])
                        });
                    }
                }
            }

            return result;
        }

        public static SaleDetailsDto SaleDetailsData()
        {
            var result = new SaleDetailsDto();
            result.items = new List<SaleDetailsDto.Items>();

            using (var conn = new SqlConnection(WindowsFormsApp2.Helpers.DB.DbHelpers.CurrentConnectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM VW_SALE_DETAILS", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (result.voen is null)
                            result.voen = reader["VOEN"].ToString();

                        result.items.Add(new SaleDetailsDto.Items
                        {
                            Date = Convert.ToDateTime(reader["TARİX"].ToString()),
                            Username = reader["İSTİFADƏÇİ"].ToString(),
                            SupplierName = reader["TƏCHİZATÇI"].ToString(),
                            CategoryName = reader["CATEGORY"].ToString(),
                            ProductName = reader["MƏHSUL ADI"].ToString(),
                            ProductCode = reader["ProductCode"].ToString(),
                            Barcode = reader["Barcode"].ToString(),
                            CustomerName = reader["CUSTOMER_NAME"].ToString(),
                            DoctorName = reader["DOCTOR_NAME"].ToString(),
                            Quantity = Convert.ToDouble(reader["MİQDARI"]),
                            UnitName = reader["VAHİDİ"].ToString(),
                            SalePrice = Convert.ToDouble(reader["SATIŞ QİYMƏTİ"]),
                            DiscountAzn = Convert.ToDouble(reader["ENDİRİM AZN"]),
                            PaymentType = reader["ÖDƏNİŞ NÖVÜ"].ToString(),
                            TotalAmount = Convert.ToDouble(reader["CƏM ÖDƏNİŞ"]),
                            TaxPercantages = Convert.ToInt32(reader["VERGİ %"]),
                            ProccessNo = reader["ProccessNo"].ToString(),
                            ReceiptNo = reader["QƏBZ"].ToString(),
                        });
                    }
                }
            }

            return result;
        }

        public static PaymentTypesDto PaymentTypesData()
        {
            var result = new PaymentTypesDto();
            result.items = new List<PaymentTypesDto.Items>();

            using (var conn = new SqlConnection(WindowsFormsApp2.Helpers.DB.DbHelpers.CurrentConnectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM VW_DAILY_PAYMENTS", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (result.voen == null)
                            result.voen = reader["VOEN"].ToString();

                        var tarix = ((DateTime)reader["TARİX"]).Date;

                        result.items.Add(new PaymentTypesDto.Items
                        {
                            Date = tarix.ToString("yyyy-MM-dd"),
                            Cash = reader.GetDecimal(reader.GetOrdinal("NAĞD")),
                            Card = reader.GetDecimal(reader.GetOrdinal("KART")),
                            Bank = reader.GetDecimal(reader.GetOrdinal("BANK")),
                            Credit = reader.GetDecimal(reader.GetOrdinal("NİSYƏ"))
                        });
                    }
                }
            }

            return result;
        }

        public static ProfitReportDto ProfitData()
        {
            var result = new ProfitReportDto();
            result.items = new List<ProfitReportDto.Items>();

            using (var conn = new SqlConnection(WindowsFormsApp2.Helpers.DB.DbHelpers.CurrentConnectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM VW_PROFIT_REPORT", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (result.voen == null)
                            result.voen = reader["VOEN"].ToString();

                        var tarix = (DateTime)reader["Date"];

                        result.items.Add(new ProfitReportDto.Items
                        {
                            Date = tarix,
                            Username = reader["CashierName"].ToString(),
                            ProccessNo = reader["ProccessNo"].ToString(),
                            SupplierName = reader["SupplierName"].ToString(),
                            CategoryName = reader["CategoryName"].ToString(),
                            ProductName = reader["ProductName"].ToString(),
                            ProductCode = reader["ProductCode"].ToString(),
                            Barcode = reader["Barcode"].ToString(),
                            UnitName = reader["UnitName"].ToString(),
                            SaleQuantity = Convert.ToDecimal(reader["SaleQuantity"].ToString()),
                            PurchasePrice = Convert.ToDecimal(reader["PurchasePrice"].ToString()),
                            SalePrice = Convert.ToDecimal(reader["SalePrice"].ToString()),
                            TotalPurchaseAmount = Convert.ToDecimal(reader["TotalPurchaseAmount"].ToString()),
                            TotalSaleAmount = Convert.ToDecimal(reader["TotalSaleAmount"].ToString()),
                            ProfitAmount = Convert.ToDecimal(reader["ProfitAmount"].ToString()),
                        });
                    }
                }
            }

            return result;
        }

        public static SaleRefundsDto SaleRefund()
        {
            var result = new SaleRefundsDto();
            result.items = new List<SaleRefundsDto.Items>();

            using (var conn = new SqlConnection(WindowsFormsApp2.Helpers.DB.DbHelpers.CurrentConnectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM VW_SALE_REFUNDS", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (result.voen == null)
                            result.voen = reader["VOEN"].ToString();

                        var tarix = (DateTime)reader["TARİX"];

                        result.items.Add(new SaleRefundsDto.Items
                        {
                            Date = tarix,
                            SaleProccessNo = reader["SaleProccessNo"].ToString(),
                            Username = reader["Username"].ToString(),
                            Supplier = reader["Supplier"].ToString(),
                            CategoryName = reader["CategoryName"].ToString(),
                            ProductName = reader["ProductName"].ToString(),
                            ProductCode = reader["ProductCode"].ToString(),
                            Barcode = reader["Barcode"].ToString(),
                            Quantity = Convert.ToDecimal(reader["Quantity"].ToString()),
                            UnitName = reader["UnitName"].ToString(),
                            SalePrice = Convert.ToDecimal(reader["SalePrice"].ToString()),
                            DiscountAzn = Convert.ToDecimal(reader["DiscountAzn"].ToString()),
                            PaymentType = reader["PaymentType"].ToString(),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"].ToString()),
                            RefundQuantity = Convert.ToDecimal(reader["RefundQuantity"].ToString()),
                            RefundAmount = Convert.ToDecimal(reader["RefundAmount"].ToString()),
                            RefundProccessNo = reader["RefundProccessNo"].ToString(),
                        });
                    }
                }
            }

            return result;
        }
    }
}