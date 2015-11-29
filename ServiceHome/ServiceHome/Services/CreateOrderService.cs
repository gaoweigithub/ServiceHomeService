using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceHome.Model.Common;

namespace ServiceHome.Services
{
    public class CreateOrderService : BaseService<CreateOrderRequest, CreateOrderResponse>
    {
        public override CreateOrderResponse Excute(CreateOrderRequest request)
        {
            throw new NotImplementedException();
        }
    }
    [Route("/CreateOrder", "POST")]
    public class CreateOrderRequest : RequestBase
    {
        public ServiceHomeDB.ORDERS OrderDetail { get; set; }
    }
    public class CreateOrderResponse : ResponseBase
    {

    }
}