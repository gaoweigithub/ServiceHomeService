using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceHome.DAL;
using ServiceHome.Model.Common;
using CommonTool;

namespace ServiceHome.Services
{
    public class GetOpenedCity : BaseService<GetOpenedCityRequest, GetOpenedCityResponse>
    {
        public override GetOpenedCityResponse Excute(GetOpenedCityRequest request)
        {
            var cacheCityList = MemoryCacheHelper.GetValue(ConstString.GetOpenedCityListKey);
            if (cacheCityList != null)
            {
                return cacheCityList as GetOpenedCityResponse;
            }
            else
            {
                CityDao dal = new CityDao();
                List<ServiceHomeDB.CITY> cityList = dal.GetOpenedCityList();

                if (cityList != null)
                {
                    GetOpenedCityResponse response = new GetOpenedCityResponse
                    {
                        DATA = new List<OpenedCity>(),
                        ResponseStatus = new Model.Common.ResponseStatus()
                    };
                    cityList.ForEach(item =>
                    {
                        response.DATA.Add(new OpenedCity
                        {
                            CITYID = item.CITYID.ToString(),
                            CITYNAME = item.CITYNAME
                        });
                    });
                    response.ResponseStatus = new Model.Common.ResponseStatus { isSuccess = true };

                    //更新缓存  一天缓存
                    MemoryCacheHelper.Set(ConstString.GetOpenedCityListKey, response, DateTimeOffset.Now.AddDays(1));

                    return response;
                }

                return new GetOpenedCityResponse { ResponseStatus = new Model.Common.ResponseStatus { isSuccess = false } };
            }
        }
    }

    [Route("/GetOpenedCity")]
    public class GetOpenedCityRequest : RequestBase
    {
    }

    public class GetOpenedCityResponse : ResponseBase
    {
        public List<OpenedCity> DATA
        {
            get; set;
        }
    }
    public class OpenedCity
    {
        public string CITYID { get; set; }
        public string CITYNAME { get; set; }
    }
}