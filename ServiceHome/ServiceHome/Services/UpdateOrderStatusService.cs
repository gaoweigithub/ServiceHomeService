using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceHome.Model.Common;

namespace ServiceHome.Services
{
    public class UpdateOrderService : BaseService<UpdateOrderOrderRequest, UpdateOrderOrderResponse>
    {
        public override UpdateOrderOrderResponse Excute(UpdateOrderOrderRequest request)
        {
            throw new NotImplementedException();
        }
    }
    [Route("/UpdateOrder", "POST")]
    public class UpdateOrderOrderRequest : RequestBase
    {
        public int OrderID { get; set; }
    }
    public class UpdateOrderOrderResponse : ResponseBase
    {

    }
}