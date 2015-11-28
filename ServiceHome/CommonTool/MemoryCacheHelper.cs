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
        public static Tuple<bool, object> GetValueIfExist(string key)
        {
            return new Tuple<bool, object>(IsExist(key), GetValue(key));
        }
        public static void Set(string key, object value)
        {
            MemoryCache.Default.Set(key, value, new CacheItemPolicy());
        }
        public static void Set(string key, object value, DateTimeOffset outDate)
        {
            MemoryCache.Default.Set(key, value, outDate);
        }
    }
}