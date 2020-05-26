using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ConsoleApp
{
    public class ApiService
    {
        private string BaseUrl { get { return "https://api-dev.channelengine.net/api/v2/"; } }

        public JObject GetOrdersWithStatus(string[] statuses, string apiKey)
        {
            var uri = BuildUri(RequestType.orders, statuses, apiKey);
            return JObject.Parse(Get(uri));            
        }

        public JObject GetProductWithMerchantProductNo(string[] merchantProductNoList, string apiKey)
        {
            var uri = BuildUri(RequestType.products, merchantProductNoList, apiKey);
            return JObject.Parse(Get(uri));
        }

        private string Get(Uri uri)
        {
            var result = string.Empty;

            try
            {
                var request = WebRequest.CreateHttp(uri);
                request.Method = "GET";

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var responseStream = response.GetResponseStream())
                        {
                            using (var streamReader = new StreamReader(responseStream))
                            {
                                result = streamReader.ReadToEnd();
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Request failed");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now} - Exception occurred: {e.Message}");
            }

            Console.WriteLine($"{DateTime.Now} - Succeeded getting data from {uri}");
            return result;
        }
        

        private Uri BuildUri(RequestType type, string[] filters, string apiKey)
        {
            string query = string.Empty;

            switch (type)
            {
                case RequestType.orders:
                    foreach(var filter in filters)
                    {
                        query += $"statuses={filter}&";
                    }
                    break;
                case RequestType.products:
                    foreach (var filter in filters)
                    {
                        query += $"merchantProductNoList={filter}&";
                    }
                    break;
            }

            query += $"apikey={apiKey}";

            var requestUri = Path.Combine(BaseUrl, type.ToString(), "?" + query);

            return new Uri(requestUri);
        }

    }
}
