using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonTool
{
    /// <summary>
    /// 配置获取
    /// </summary>
    public class CommonAppSetting
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetAppSetting(string name)
        {
            return System.Configuration.ConfigurationManager.AppSettings[name];
        }
        /// <summary>
        /// 短信服务器地址
        /// </summary>
        public static string SMS_PRO_SERVERURL
        {
            get { return GetAppSetting("SMS_PRO_SERVERURL"); }
        }
        /// <summary>
        /// 接入key
        /// </summary>
        public static string SMS_AppKey
        {
            get { return GetAppSetting("SMS_AppKey"); }
        }
        /// <summary>
        /// 接入secrete
        /// </summary>
        public static string SMS_AppSecret
        {
            get { return GetAppSetting("SMS_AppSecret"); }
        }
        /// <summary>
        /// 短信通知里得公司名
        /// </summary>
        public static string SMS_PRODUCTNAME
        {
            get { return GetAppSetting("SMS_ProductName"); }
        }
    }
}
