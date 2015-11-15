using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
namespace ServiceHome
{
    public class HomeServices : BaseService
    {
        public object Any(PingRequest request)
        {
            return new PingResponse()
            {
                ResponseStatus = new Model.Common.ResponseStatus
                {
                    isSuccess = true,
                    ErrorList = new List<string> { "" }
                },
                Result = string.Format("Welcome {0} ,your ClientID is {1}", request.ClientName, request.ClientID)
            };
        }
    }
}