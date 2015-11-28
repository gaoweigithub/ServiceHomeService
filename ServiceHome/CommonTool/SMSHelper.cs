using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
namespace CommonTool
{
    /// <summary>
    /// 信息发送类
    /// </summary>
    public class SMSHelper
    {
        /// <summary>
        ///发送注册验证码 
        /// </summary>
        /// <param name="phoneNO"></param>
        /// <param name="randonCode"></param>
        /// <param name="product"></param>
        /// <param name="url"></param>
        /// <param name="appkey"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static bool SendRegisterCheckCode(string phoneNO, string randonCode)
        {
            return SendRegisterCheckCode(phoneNO, randonCode, CommonAppSetting.SMS_PRODUCTNAME, CommonAppSetting.SMS_PRO_SERVERURL, CommonAppSetting.SMS_AppKey, CommonAppSetting.SMS_AppSecret);
        }
        /// <summary>
        ///发送注册验证码  后期写到config中
        /// </summary>
        /// <param name="phoneNO"></param>
        /// <param name="randonCode"></param>
        /// <param name="product"></param>
        /// <param name="url"></param>
        /// <param name="appkey"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static bool SendRegisterCheckCode(string phoneNO, string randonCode, string product = "小牛到家", string url = "http://gw.api.taobao.com/router/rest", string appkey = "23274384", string secretKey = "9cddf0e0c922065933e6a5acc1c7a68e")
        {
            ITopClient client = new DefaultTopClient(url, appkey, secretKey);
            AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
            req.SmsType = "normal";
            req.SmsFreeSignName = "注册验证";
            req.SmsParam = "{'code':'" + randonCode + "','product':'" + product + "'}";
            req.RecNum = phoneNO;
            //注册验证码模板
            req.SmsTemplateCode = "SMS_2650250";
            AlibabaAliqinFcSmsNumSendResponse response = client.Execute(req);
            return !response.IsError;
        }

    }
}
