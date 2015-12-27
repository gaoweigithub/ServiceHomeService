using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceHome
{
    /// <summary>
    /// 固定值类
    /// </summary>
    public class ConstString
    {
        /// <summary>
        /// 计算城市业务缓存key
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns></returns>
        public static string GetCityServiceKey(int cityID)
        {
            //cityservice
            return string.Format("CS_{0}", cityID);
        }
        /// <summary>
        /// 获取已开通城市列表key
        /// </summary>
        /// <returns></returns>
        public static string GetOpenedCityListKey
        {
            get { return "OCL"; }
        }
    }
}