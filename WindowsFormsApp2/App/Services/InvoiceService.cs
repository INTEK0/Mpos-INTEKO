using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WindowsFormsApp2.App.Dtos;
using WindowsFormsApp2.Helpers.Messages;

namespace WindowsFormsApp2.App.Services
{
    public class InvoiceService
    {
        private readonly HttpClient _client;
        private readonly string _apiHost;

        public InvoiceService(string apiHost)
        {
            _apiHost = apiHost;
            _client = new HttpClient();
        }

        public async Task<bool> SendInvoicesAsync(InvoiceProductDto invoices)
        {
            try
            {
                var json = JsonConvert.SerializeObject(invoices);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(_apiHost, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE(ex.Message);
                return false;
            }
        }
    }
}
