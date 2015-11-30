using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceHome.Model.Common;

namespace ServiceHome.Services
{
    /// <summary>
    /// 订单支付接口
    /// </summary>
    public class PayOrderService : BaseService<PayOrderRequest, PayOrderResponse>
    {
        public override PayOrderResponse Excute(PayOrderRequest request)
        {
            //ping++支付

            return null;
        }
    }
    [Route("PayOrder", "POST")]
    public class PayOrderRequest : RequestBase
    {
        public int OrderID { get; set; }
        /// <summary>
        /// 请求类别
        /// </summary>
        public RequestType RequestType { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public PayType PayType { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }
    }
    public class PayOrderResponse : ResponseBase
    {
    }
    /// <summary>
    /// 请求类别
    /// </summary>
    public enum RequestType
    {
        /// <summary>
        /// 创建付款码
        /// </summary>
        CreatePayCode = 1,
        /// <summary>
        /// 确认支付
        /// </summary>
        ConfirmPay = 2
    }
    public enum PayType
    {
        /// <summary>
        /// 微信
        /// </summary>
        WeiXin = 1,
        /// <summary>
        /// 支付宝
        /// </summary>
        Alipay = 2
    }

}