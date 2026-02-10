using System.Threading.Tasks;
using WindowsFormsApp2.App.Helpers;
using WindowsFormsApp2.App.Services;
using static WindowsFormsApp2.App.Helpers.Enums;

namespace WindowsFormsApp2.App.Application
{
    public class SyncFacade
    {
        private readonly ApiService _apiService;

        public SyncFacade()
        {
            _apiService = new ApiService();
        }

        public async Task<ApiResult> SendPaymentsAsync(bool fullData = false)
        {
            var data = await DbHelpers.PaymentTypesData(fullData);
            if (data.items.Count <= 0)
            {
                Serilog.Log.Warning($"Operation={ApiOperation.PaymentTypes} | Message=No data found");
                return ApiResult.Fail("No data");
            }

            return await _apiService.SendAsync(data, ApiOperation.PaymentTypes);
        }

        public async Task<ApiResult> SendInvoicesAsync(bool fullData = false)
        {
            var data = await DbHelpers.InvoiceData(fullData);
            if (data.items.Count <= 0)
            {
                Serilog.Log.Warning($"Operation={ApiOperation.ProductInvoice} | Message=No data found");
                return ApiResult.Fail("No data");
            }

            return await _apiService.SendAsync(data, ApiOperation.ProductInvoice);
        }

        public async Task<ApiResult> SendSalesAsync(bool fullData = false)
        {
            var data = await DbHelpers.SaleDetailsData(fullData);
            if (data.items.Count <= 0)
            {
                Serilog.Log.Warning($"Operation={ApiOperation.SaleDetail} | Message=No data found");
                return ApiResult.Fail("No data");
            }

            return await _apiService.SendAsync(data, ApiOperation.SaleDetail);
        }

        public async Task<ApiResult> SendStockAsync()
        {
            var data = await DbHelpers.StockData();
            if (data.items.Count <= 0)
            {
                Serilog.Log.Warning($"Operation={ApiOperation.WarehouseStock} | Message=No data found");
                return ApiResult.Fail("No data");
            }

            return await _apiService.SendAsync(data, ApiOperation.WarehouseStock);
        }

        public async Task<ApiResult> SendSaleRefundAsync(bool fullData = false)
        {
            var data = await DbHelpers.SaleRefund(fullData);
            if (data?.items?.Count <= 0)
            {
                Serilog.Log.Warning($"Operation={ApiOperation.SaleRefund} | Message=No data found");
                return ApiResult.Fail("No data");
            }

            return await _apiService.SendAsync(data, ApiOperation.SaleRefund);
        }

        public async Task<ApiResult> SendProfitAsync(bool fullData = false)
        {
            var data = await DbHelpers.ProfitData(fullData);
            if (data.items.Count <= 0)
            {
                Serilog.Log.Warning($"Operation={ApiOperation.Profit} | Message=No data found");
                return ApiResult.Fail("No data");
            }

            return await _apiService.SendAsync(data, ApiOperation.Profit);
        }
    }
}
