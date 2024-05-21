using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Core
{
    public class ResponseData<T>
    {
        public T Data { get; set; }
        public int ItemCount { get; set; }
        public string StatusCode { get; set; }
        public string StatusMsg { get; set; }

        public bool IsSuccess => StatusCode == "0000";
    }
}