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
        private string BaseUrl { get { return ConfigurationManager.AppSettings["baseUrl"]; } }
        private string ApiKey { get { return ConfigurationManager.AppSettings["apiKey"]; } }

        public JObject GetOrdersWithStatus(string[] statuses)
        {
            var uri = BuildUri(RequestType.orders, statuses);
            return JObject.Parse(Get(uri));
            
        }

        public JObject GetProductWithEan(string[] eans)
        {
            var uri = BuildUri(RequestType.products, eans);
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

            return result;
        }
        

        private Uri BuildUri(RequestType type, string[] filters)
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
                        query += $"eanList={filter}&";
                    }
                    break;
            }

            query += $"apikey={ApiKey}";

            var requestUri = Path.Combine(BaseUrl, type.ToString(), "?" + query);

            return new Uri(requestUri);
        }

    }
}
