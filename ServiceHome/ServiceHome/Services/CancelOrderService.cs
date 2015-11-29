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
            throw new NotImplementedException();
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