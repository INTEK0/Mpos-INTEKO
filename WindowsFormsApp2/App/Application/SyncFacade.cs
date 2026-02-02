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

        public async Task<ApiResult> SendPaymentsAsync()
        {
            var data = DbHelpers.PaymentTypesData();
            if (data.items.Count > 0)
                return await _apiService.SendAsync(data, ApiOperation.PaymentTypes);
            else
                return ApiResult.Fail(null);
        }

        public async Task<ApiResult> SendInvoicesAsync()
        {
            var data = DbHelpers.InvoiceData();
            if (data.items.Count > 0)
                return await _apiService.SendAsync(data, ApiOperation.ProductInvoice);
            else
                return ApiResult.Fail(null);
        }

        public async Task<ApiResult> SendSalesAsync()
        {
            var data = DbHelpers.SaleDetailsData();
            if (data.items.Count > 0)
                return await _apiService.SendAsync(data, ApiOperation.SaleDetail);
            else
                return ApiResult.Fail(null);
        }

        public async Task<ApiResult> SendStockAsync()
        {
            var data = DbHelpers.StockData();
            if (data.items.Count > 0)
                return await _apiService.SendAsync(data, ApiOperation.WarehouseStock);
            else
                return ApiResult.Fail(null);
        }

        public async Task<ApiResult> SendSaleRefundAsync()
        {
            var data = DbHelpers.SaleRefund();
            if (data.items.Count > 0)
                return await _apiService.SendAsync(data, ApiOperation.SaleRefund);
            else
                return ApiResult.Fail(null);
        }

        public async Task<ApiResult> SendProfitAsync()
        {
            var data = DbHelpers.ProfitData();
            if (data.items.Count > 0)
                return await _apiService.SendAsync(data, ApiOperation.Profit);
            else
                return ApiResult.Fail(null);
        }
    }
}
