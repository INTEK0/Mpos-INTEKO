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
            return await _apiService.SendAsync(data, ApiOperation.PaymentTypes);
        }

        public async Task<ApiResult> SendInvoicesAsync()
        {
            var data = DbHelpers.InvoiceData();
            return await _apiService.SendAsync(data, ApiOperation.ProductInvoice);
        }

        public async Task<ApiResult> SendSalesAsync()
        {
            var data = DbHelpers.SaleDetailsData();
            return await _apiService.SendAsync(data, ApiOperation.SaleDetail);
        }

        public async Task<ApiResult> SendStockAsync()
        {
            var data = DbHelpers.StockData();
            return await _apiService.SendAsync(data, ApiOperation.WarehouseStock);
        }

        public async Task<ApiResult> SendSaleRefundAsync()
        {
            var data = DbHelpers.SaleRefund();
            return await _apiService.SendAsync(data, ApiOperation.SaleRefund);
        }

        public async Task<ApiResult> SendProfitAsync()
        {
            var data = DbHelpers.ProfitData();
            return await _apiService.SendAsync(data, ApiOperation.Profit);
        }
    }
}
