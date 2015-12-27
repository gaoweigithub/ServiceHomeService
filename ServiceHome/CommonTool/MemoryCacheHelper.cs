using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
namespace CommonTool
{
    public class MemoryCacheHelper
    {
        public static bool IsExist(string key)
        {
            return MemoryCache.Default.Contains(key);
        }
        public static object GetValue(string key)
        {
            return MemoryCache.Default[key];
        }
        public static void Set(string key, object value)
        {
            //默认一小时
            MemoryCache.Default.Set(key, value, DateTimeOffset.Now.AddHours(1));
        }
        public static void Set(string key, object value, DateTimeOffset outDate)
        {
            MemoryCache.Default.Set(key, value, outDate);
        }
    }
}