using CaliphWeb.Core.Helper;
using CaliphWeb.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CaliphWeb.Services.Helper
{
    public class One2OneApiHelper : IOne2OneApiHelper
    {
        private readonly IRestHelper _restHelper;

        public One2OneApiHelper(IRestHelper restHelper)
        {
            this._restHelper = restHelper;
        }

        public async Task<TReturn> PostAsync<T, TReturn>(T req, string post, TReturn instanceFactory) where T : class
        {
            var url = ConfigurationManager.AppSettings["One2OneUrl"] + post;

            _restHelper.Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync());

            try
            {
                //var response = await _restHelper.PostAsync(url, req);

                //var returnData = JsonConvert.DeserializeObject<TReturn>(response);
                //Console.WriteLine(response);
                //return returnData;

                return instanceFactory;

            }
            catch (Exception ex)
            { }
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
                return token;

            }
            finally
            {

            }
        }
    }
}