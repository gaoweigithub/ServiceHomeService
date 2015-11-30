using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceHome.Model.Common;

namespace ServiceHome.Services
{
    public class CancelOrderService : BaseService<CancelOrderRequest, CancelOrderResponse>
    {
        public override CancelOrderResponse Excute(CancelOrderRequest request)
        {
            //ping++取消,退款
            //to do ...
            return null;
        }
    }
    [Route("/CancelOrder", "POST")]
    public class CancelOrderRequest : RequestBase
    {
        public int OrderID { get; set; }
    }
    public class CancelOrderResponse : ResponseBase
    {

    }
}