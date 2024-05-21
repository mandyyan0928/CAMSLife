using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace CaliphWeb.Core.Helper
{
    public interface IRestHelper
    {
        AuthenticationHeaderValue Authorization { get; set; }
        Task<string> GetAsync(string requestUri);
        Task<string> PostAsync<T>(string requestUri, T request, bool log =true) where T : class;

        Task<string> PostAsync(string requestUri);
        Task<string> PostAsync(string requestUri, string content);
    }
}