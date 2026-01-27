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
            return await _apiService.SendAsync(data, ApiOperation.Odenis);
        }

        public async Task<ApiResult> SendInvoicesAsync()
        {
            var data = DbHelpers.GetInvoiceData();
            return await _apiService.SendAsync(data, ApiOperation.Invoice);
        }

        public async Task<ApiResult> SendSalesAsync()
        {
            var data = DbHelpers.GetSaleDetailsData();
            return await _apiService.SendAsync(data, ApiOperation.Stock);
        }
    }
}
