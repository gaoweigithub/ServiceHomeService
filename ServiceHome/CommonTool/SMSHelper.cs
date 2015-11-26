using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;
namespace CommonTool
{
    public class SMSHelper
    {
        public static bool SendCheckCode(string phoneNO, string randonCode, string product = "小牛到家", string url = "http://gw.api.taobao.com/router/rest", string appkey = "23274384", string secretKey = "9cddf0e0c922065933e6a5acc1c7a68e")
        {
            ITopClient client = new DefaultTopClient(url, appkey, secretKey);
            AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
            req.SmsType = "normal";
            req.SmsFreeSignName = "注册验证";
            req.SmsParam = "{'code':'" + randonCode + "','product':'" + product + "'}";
            req.RecNum = phoneNO;
            req.SmsTemplateCode = "SMS_2650250";
            AlibabaAliqinFcSmsNumSendResponse response = client.Execute(req);
            return !response.IsError;
        }
    }
}
