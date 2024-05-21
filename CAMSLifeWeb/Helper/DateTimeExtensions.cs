using System;
using System.Collections.Generic;

namespace CaliphWeb.Helper
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static List<int> Years(int from = 2020, int to = 2030)
        {
            List<int> year = new List<int>();
            for (int a = from; a <= to; a++)
                year.Add(a);
            return year;
        }

        public static List<int> QuarterMonths()
        {
            return new List<int>() { 3, 6, 9, 12 };
        }

        
    }

   
}