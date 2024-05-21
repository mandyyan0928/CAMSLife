using CaliphWeb.Core;
using CaliphWeb.Core.Helper;
using CaliphWeb.ViewModel;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CaliphWeb.Helper
{
    public class HttpClientHelper : IRestHelper
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public AuthenticationHeaderValue Authorization { get; set; }
        public HttpClientHelper()
        {
           

            log4net.Config.XmlConfigurator.Configure();
        }

 
        public async Task<string> GetAsync(string requestUri)
        {
            string result = "Error";
            using (HttpClientHandler httpClientHandler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(httpClientHandler))
                {
                    try
                    {
 
                        HttpResponseMessage response = await httpClient.GetAsync(requestUri);
                        result = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == HttpStatusCode.OK)
                            LogRequestAndResponse(requestUri, null, result);
                        else
                            throw new Exception($" status :{response.StatusCode },  result :{result} ");

                    }
                    catch (Exception ex)
                    {
                        log.Error($"Error in GET request to {requestUri}: {ex.Message}");
                    }
                }
            }
            return result;
        }

       

        public async Task<string> PostAsync<T>(string requestUri, T request, bool shouldLog =true) where T : class
        {
            string result = "Error";
            using (HttpClientHandler httpClientHandler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(httpClientHandler))
                {
                    try
                    {
                        if (Authorization != null)
                            httpClient.DefaultRequestHeaders.Authorization = Authorization;

                        var content = new StringContent(JsonConvert.SerializeObject(request))
                        {
                            Headers =
                            {
                                ContentType = new MediaTypeHeaderValue("application/json")
                            }
                        };

                        HttpResponseMessage response = await httpClient.PostAsync(requestUri, content);
                        result = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == HttpStatusCode.OK)
                            LogRequestAndResponse(requestUri, null, result);
                        else
                            throw new Exception($" status :{response.StatusCode },  result :{result} ");
                    }
                    catch (Exception ex)
                    {
                        log.Error($"Error in POST request to {requestUri}: {ex.Message}");
                    }
                }
            }
            return result;
        }

        private void AddHeaders(HttpClient httpClient, Dictionary<string, string> additionalHeaders)
        {
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
           
            
            if (additionalHeaders == null)
                return;
          
            foreach (KeyValuePair<string, string> current in additionalHeaders)
            {
                httpClient.DefaultRequestHeaders.Add(current.Key, current.Value);
            }
        }

        public async Task<string> PostAsync(string requestUri)
        {
             string result = "Error";
            using (HttpClientHandler httpClientHandler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(httpClientHandler))
                {
                    try
                    {
                        if (Authorization != null)
                            httpClient.DefaultRequestHeaders.Authorization = Authorization;

                        HttpResponseMessage response = await httpClient.PostAsync(requestUri, new StringContent("")
                        {
                            Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("application/json")
                        }
                        });

                        result = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == HttpStatusCode.OK )
                            LogRequestAndResponse(requestUri, null, result);
                        else
                            throw new Exception($" status :{response.StatusCode },  result :{result} ");

                    }
                    catch (Exception ex)
                    {
                        log.Error($"Error in POST request to {requestUri}: {ex.Message}");
                    }
                }
            }
            return result;
        }

        public async Task<string> PostAsync(string requestUri, string content)
        {
            string result = "Error";
            using (HttpClientHandler httpClientHandler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(httpClientHandler))
                {
                    try
                    {
                        if (Authorization != null)
                            httpClient.DefaultRequestHeaders.Authorization = Authorization;




                        HttpResponseMessage response = await httpClient.PostAsync(requestUri, new StringContent(content)
                        {
                            Headers =
                        {
                            ContentType = new MediaTypeHeaderValue("application/json")
                        }
                        });

                        result = await response.Content.ReadAsStringAsync();
                        if (response.StatusCode == HttpStatusCode.OK)
                            LogRequestAndResponse(requestUri, null, result);
                        else
                            throw new Exception($" status :{response.StatusCode },  result :{result} ");


                    }
                    catch (Exception ex)
                    {
                        log.Error($"Error in POST request to {requestUri}: {ex.Message}");
                    }
                }
            }
            return result;
        }


        private void LogRequestAndResponse(string requestUri, object request, string response)
        {
            log.Info("============================================================================================");
            log.Info($"Request to {requestUri}:{Environment.NewLine}Request: {JsonConvert.SerializeObject(request)}{Environment.NewLine}Response: {response}");
            log.Info("============================================================================================");
        }
    }
}