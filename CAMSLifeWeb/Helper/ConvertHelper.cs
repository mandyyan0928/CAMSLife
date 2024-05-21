using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaliphWeb.Helper
{
    public class ConvertHelper
    {
        public static int ConvertInt(string str, int defaultVal = 0)
        {
            int a = 0;
            if (int.TryParse(str, out a))
                return a;
            else
                return defaultVal;
        }

        public static decimal ConvertDecimal(string str, decimal defaultVal = 0)
        {
            decimal a = 0;
            if (decimal.TryParse(str, out a))
                return a;
            else
                return defaultVal;
        }
    }
}