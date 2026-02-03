using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WindowsFormsApp2.App.Helpers;
using static WindowsFormsApp2.App.Dtos.ApiResponseDto;
using static WindowsFormsApp2.App.Helpers.Enums;

namespace WindowsFormsApp2.App.Services
{
    public class ApiService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "http://31.210.36.169:5050";
        public ApiService()
        {
            _client = new HttpClient();
        }

        public async Task<ApiResult> SendAsync<T>(T data, ApiOperation operation)
        {
            var endpoint = ApiEndpointResolver.Resolve(operation);
            var url = $"{_baseUrl}/api/{endpoint}";

            var json = JsonConvert.SerializeObject(data);

            Serilog.Log.Information(
                "POST {Url} | Operation={Operation} | PayloadLength={Length}",
                url,
                operation,
                json.Length
            );

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            if (!Licence.Helpers.FormHelpers.HasInternetConnection())
            {
                Serilog.Log.Error(
                    "API Error | Operation={Operation} | Error={Error}",
                    operation,
                    "No internet connection"
                );

                return ApiResult.Fail("No internet connection");
            }

            var response = await _client.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var success = JsonConvert.DeserializeObject<ApiSuccessResponse>(responseBody);

                Serilog.Log.Information(
                    "API Success | Operation={Operation} | Inserted={Inserted}",
                    operation,
                    success.inserted
                );

                return ApiResult.Success(success.inserted);
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ApiErrorResponse>(responseBody);

                Serilog.Log.Error(
                    "API Error | Operation={Operation} | Error={Error} | RequestJson={requestJson}",
                    operation,
                    error.error,
                    json
                );

                return ApiResult.Fail(error.error);
            }
        }
    }
}
