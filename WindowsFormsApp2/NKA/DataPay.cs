using DevExpress.Map.Kml.Model;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.Messages;
using static WindowsFormsApp2.NKA.NBA;

namespace WindowsFormsApp2.NKA
{
    public static class DataPay
    {
        public static string Login(string ipAddress)
        {
            ipAddress = $"{ipAddress}/api/account/login";

            RootObject root = new RootObject();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(root, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            RestClient rest = new RestClient();
            RestRequest request = new RestRequest(ipAddress, Method.Post);
            request.AddHeader("Content-Type", "application/json;charset=utf-8");
            request.AddStringBody(json, DataFormat.Json);
            RestResponse response = rest.Execute(request);

            DataPayResponse weatherForecast = System.Text.Json.JsonSerializer.Deserialize<DataPayResponse>(response.Content);

            if (weatherForecast.message != "Successful operation")
            {
                ReadyMessages.ERROR_SERVER_CONNECTION_MESSAGE(weatherForecast.message);
                return null;
            }

            string token = weatherForecast.accessToken;
            return token;
        }

        public static void OpenShift(string ip, string keys)
        {
            string url = $"{ip}/api/shift/open";
            var client = new RestClient();
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Authorization", keys);
            request.AddParameter("text/plain", string.Empty, ParameterType.RequestBody);
            RestResponse response = client.Execute(request);
            var data = response.Content.ToString();

            DataPayResponse weatherForecast = System.Text.Json.JsonSerializer.Deserialize<DataPayResponse>(data);

            if (weatherForecast.message == "success")
            {
                ReadyMessages.SUCCESS_OPEN_SHIFT_MESSAGE();
                FormHelpers.Log(CommonData.SUCCESS_OPEN_SHIFT);
            }
            else
            {
                ReadyMessages.ERROR_OPENSHIFT_MESSAGE(weatherForecast.message);
                FormHelpers.Log($"{ErrorMessages.ERROR_OPENSHIFT} Xəta mesajı: {weatherForecast.message}");
            }
        }

        public static void CloseShift(string ip, string keys)
        {
            string url = $"{ip}/api/shift/close";

            var client = new RestClient();

            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", keys);

            RestResponse response = client.Execute(request);
            var data = response.Content.ToString();
            FormHelpers.Log("GÜNSONU (Z) HESABATI ÇIXARILDI");
        }

        public static void X_Report(string ip, string keys)
        {
            string url = $"{ip}/api/report/xreport";

            var client = new RestClient();

            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Authorization", keys);

            RestResponse response = client.Execute(request);
            var data = response.Content.ToString();
            FormHelpers.Log("GÜNLÜK (X) HESABATI UĞURLA ÇIXARILDI");
        }

        public static void sales(string ip, string keys, string datasa)
        {
            string url = $"{ip}/api/sale/create";

            var client = new RestClient();

            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Authorization", keys);
            var body = datasa;
            request.AddParameter("text/plain", body, ParameterType.RequestBody);

            RestResponse response = client.Execute(request);
            var data = response.Content.ToString();
        }




        #region [..Request Classes..]
        private class RootObject
        {
            public string userName { get; set; } = "Menecer";
            public string password { get; set; } = "12348765";
        }

        #endregion [..Request Classes..]


        #region [..Response Classes..]
        private class DataPayResponse
        {
            public string status { get; set; }
            public double code { get; set; }
            public string message { get; set; }
            public string documentid { get; set; }
            public string accessToken { get; set; }
            public string long_id { get; set; }
            public int document_number { get; set; }
            public string short_id { get; set; }
            public int documentNumber { get; set; }
            public string documentId { get; set; }
            public string shortDocumentId { get; set; }
        }
        #endregion [..Response Classes..]
    }
}
