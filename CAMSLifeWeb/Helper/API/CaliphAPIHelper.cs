using CaliphWeb.Core.Helper;
using CaliphWeb.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace CaliphWeb.Services.Helper
{
    public class CaliphAPIHelper : ICaliphAPIHelper
    {
        private readonly IRestHelper _restHelper;

        public CaliphAPIHelper(IRestHelper restHelper)
        {
            this._restHelper = restHelper;
        }

        public async Task<TReturn> PostAsync<T, TReturn>(T req, string post) where T : class
        {
            var url = ConfigurationManager.AppSettings["CaliphUrl"] + post;

            _restHelper.Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync());

            try
            {
                 var response = await _restHelper.PostAsync(url, req);
                var returnData = JsonConvert.DeserializeObject<TReturn>(response);
                return returnData;

            }
            catch (Exception ex)
            { }
            return default(TReturn);
        }

        public async Task<TReturn> PostAsync<TReturn>(string post) where TReturn : class
        {
            var url = ConfigurationManager.AppSettings["CaliphUrl"] + post;
            _restHelper.Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync());
            var response = await _restHelper.PostAsync(url);
            var returnData = JsonConvert.DeserializeObject<TReturn>(response);
            return returnData;
        }

        public async Task<TReturn> PostAsyncPayLater<T, TReturn>(T req, string post) where T : class
        {
            var url = ConfigurationManager.AppSettings["PayLaterUrl"] + post;

            try
            {
                var response = await _restHelper.PostAsync(url, req);
                var returnData = JsonConvert.DeserializeObject<TReturn>(response);
                return returnData;

            }
            catch (Exception ex)
            { }
            return default(TReturn);
        }

        private async Task<string> GetTokenAsync()
        {
            var url = ConfigurationManager.AppSettings["CaliphUrl"] + "/api/GetToken";
            var tokenreq = new TokenRequest { Username = ConfigurationManager.AppSettings["CaliphUsername"], Password = ConfigurationManager.AppSettings["CaliphPassword"] };
            var token = await _restHelper.PostAsync(url, tokenreq,false);
            return JsonConvert.DeserializeObject<string>(token);
        }
    }
}