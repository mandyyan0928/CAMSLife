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
    public class SLMAPIHelper : IALCApiHelper
    {
        private readonly IRestHelper _restHelper;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public SLMAPIHelper(IRestHelper restHelper)
        {
            this._restHelper = restHelper;
            log4net.Config.XmlConfigurator.Configure();
        }

        public async Task<TReturn> GetDataAsync<T, TReturn>(T req, string endpoint, TReturn instanceFactory) where T : class
        {
            var url = ConfigurationManager.AppSettings["SLMUrl"] + endpoint;

            _restHelper.Authorization = new AuthenticationHeaderValue("x-api-key", ConfigurationManager.AppSettings["SLMToken"]);

            try
            {
                var queryString = BuildQueryString(req);

                var response = await _restHelper.GetAsync(url + queryString);
                var returnData = JsonConvert.DeserializeObject<TReturn>(response);

                return returnData;


            }
            catch (Exception ex)
            {
                log.Error($"Error in SLM get data : {ex.Message}");
            }
            return instanceFactory;
        }

        private string BuildQueryString(object body)
        {
            if (body == null) {
                return "";
            }

            string queryString = "?";
            var properties = body.GetType().GetProperties();
            foreach (var property in properties)
            {
                var value = property.GetValue(body);

                if (value == null) continue;

                //// Check if the property is of type DateTime and format it
                if (value is DateTime dateValue)
                {
                    value = dateValue.ToString("yyyy-MM-dd");
                }

                queryString += $"{property.Name}={value}&";
            }


            if(queryString.EndsWith("&"))
            {
                queryString = queryString.Remove(queryString.Length - 1);
            }

            return queryString;
        }



    }
}