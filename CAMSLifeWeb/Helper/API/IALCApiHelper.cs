using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CaliphWeb.Services.Helper
{
    public interface IALCApiHelper
    {
        Task<TReturn> GetDataAsync<T, TReturn>(T req, string post, TReturn instanceFactory) where T : class;
    }
}