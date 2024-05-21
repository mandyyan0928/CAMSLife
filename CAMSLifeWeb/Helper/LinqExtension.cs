using System;
using System.Linq;

namespace CaliphWeb.Helper
{
    public static class LinqExtension {

        public static bool In<T>(this T theObject, params T[] collection)
        {

            return collection.Contains(theObject);
        }
    }

   
}