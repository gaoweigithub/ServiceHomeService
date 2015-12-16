using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceHome.Model.Common;


namespace ServiceHome.Services
{
    /// <summary>
    /// 根据城市id获取城市服务列表
    /// </summary>
    public class GetCityService : BaseService<GetCityServiceRequest, GetCityServiceResponse>
    {
        public override GetCityServiceResponse Excute(GetCityServiceRequest request)
        {
            GetCityServiceResponse response = new GetCityServiceResponse();
            response.ServiceList = new DAL.GetServices().GetServiceList(request.cityID);

            response.ResponseStatus = new Model.Common.ResponseStatus { isSuccess = true };
            return response;
        }
    }
    [Route("/GetCityService")]
    public class GetCityServiceRequest : RequestBase
    {
        /// <summary>
        /// 城市id
        /// </summary>        
        public int cityID { get; set; }
    }

    public class GetCityServiceResponse : ResponseBase
    {
        //服务列标
        //public List<ServiceHomeDB.SERVICE> ServiceList { get; set; }
        public List<showService> ServiceList { get; set; }

    }
    public class showService
    {
        public string SERVICE_ID { get; set; }
        public string PARENT_SERVICE_ID { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SERVICE_NAME { get; set; }
        public string PICURL { get; set; }
        public string SERVICE_DESC { get; set; }
        public List<showService> SERVICE_ITEMS { get; set; }
    }
}