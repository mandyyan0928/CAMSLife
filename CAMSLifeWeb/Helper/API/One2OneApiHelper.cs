using CaliphWeb.Core.Helper;
using CaliphWeb.ViewModel;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace CaliphWeb.Services.Helper
{
    public class One2OneApiHelper : IALCApiHelper
    {
        private readonly IRestHelper _restHelper;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public One2OneApiHelper(IRestHelper restHelper)
        {
            this._restHelper = restHelper;
            log4net.Config.XmlConfigurator.Configure();
        }

        public async Task<TReturn> GetDataAsync<T, TReturn>(T req, string post, TReturn instanceFactory) where T : class
        {
            var url = ConfigurationManager.AppSettings["One2OneUrl"] + post;

            _restHelper.Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync());

            try
            {
                var response = await _restHelper.PostAsync(url, req);

                var returnData = JsonConvert.DeserializeObject<TReturn>(response);
                return returnData;
            }
            catch (Exception ex)
            {
                log.Error($"Error in SLM get data : {ex.Message}");
            }
            return default(TReturn);
        }

        private async Task<string> GetTokenAsync()
        {

            var token = "";
            try
            {
                var url = ConfigurationManager.AppSettings["One2OneUrl"] + "/edfwebapi/Token";
                var content = "grant_type=password&username=" + ConfigurationManager.AppSettings["One2OneUsername"] + "&password=" + ConfigurationManager.AppSettings["One2OnePassword"];
                var result = await _restHelper.PostAsync(url, content);
                var jObject = JObject.Parse(result);

                if (jObject.GetValue("access_token")!=null)
                    token = jObject.GetValue("access_token").ToString();
                return token;

            }
            catch (Exception ex)
            {
                log.Error($"Error in SLM get data : {ex.Message}");
                return token;

            }
        }
    }
}