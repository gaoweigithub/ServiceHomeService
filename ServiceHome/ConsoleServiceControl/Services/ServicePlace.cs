using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceHome.Model.Common;

namespace ServiceHome.Services
{
    /// <summary>
    /// 获取地址
    /// </summary>
    public class GetServicePlaces : BaseService<GetServicePlaceListRequest, GetServicePlaceListResponse>
    {
        public override GetServicePlaceListResponse Excute(GetServicePlaceListRequest request)
        {
            GetServicePlaceListResponse response = null;

            response = new GetServicePlaceListResponse();
            response.UserID = request.UserID;
            response.ServicePlaces = new DAL.ServicePlace().GetServicePlaces(request.UserID);
            response.ResponseStatus = new Model.Common.ResponseStatus { isSuccess = true };
            return response;
        }
    }
    public class AddServicePlace : BaseService<AddServicePlaceRequest, AddServicePlaceResponse>
    {
        /// <summary>
        /// 增加服务地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override AddServicePlaceResponse Excute(AddServicePlaceRequest request)
        {
            AddServicePlaceResponse response = null;

            if (!(request.SERVICEPLACE == null
                || string.IsNullOrWhiteSpace(request.SERVICEPLACE.ADDR_TITLE)
                || string.IsNullOrWhiteSpace(request.SERVICEPLACE.PLACE_DETAIL)
                || string.IsNullOrWhiteSpace(request.SERVICEPLACE.PHONE)))
            {
                request.SERVICEPLACE.CREATE_TIME = DateTime.Now;
                request.SERVICEPLACE.LAST_TIME = DateTime.Now;
                request.SERVICEPLACE.USERID = request.requestHeader.UserID;
                int i = new DAL.ServicePlace().AddServicePlace(request.SERVICEPLACE);
                response = new AddServicePlaceResponse
                {
                    ResponseStatus = new Model.Common.ResponseStatus
                    {
                        isSuccess = i > 0 ? true : false
                    }
                };
            }

            return response;
        }
    }
    public class DeleteServicePlace : BaseService<DeleteServicePlaceRequest, DeleteServicePlaceResponse>
    {
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override DeleteServicePlaceResponse Excute(DeleteServicePlaceRequest request)
        {
            DeleteServicePlaceResponse response = null;
            bool result = new DAL.ServicePlace().DeleteServicePlace(request.ServicePlaceID);
            response = new DeleteServicePlaceResponse
            {
                ResponseStatus = new Model.Common.ResponseStatus { isSuccess = result }
            };
            return response;
        }
    }

    [Route("/GetServicePlaces", "POST")]
    public class GetServicePlaceListRequest : RequestBase
    {
        public int UserID { get; set; }
    }
    public class GetServicePlaceListResponse : ResponseBase
    {
        public int UserID { get; set; }
        public List<ServiceHomeDB.SERVICEPLACES> ServicePlaces { get; set; }
    }

    [Route("/DeleteServicePlace", "POST")]
    public class DeleteServicePlaceRequest : RequestBase
    {
        public int ServicePlaceID { get; set; }
    }
    public class DeleteServicePlaceResponse : ResponseBase
    {
    }
    [Route("/AddServicePlace", "POST")]
    public class AddServicePlaceRequest : RequestBase
    {
        public int UserID { get; set; }
        public ServiceHomeDB.SERVICEPLACES SERVICEPLACE { get; set; }
    }
    public class AddServicePlaceResponse : ResponseBase
    {
    }
}