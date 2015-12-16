using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using CommonTool;
using System.Data.Entity;
using System.Data.SqlClient;
namespace ServiceHome.DAL
{
    /// <summary>
    /// 获取基础服务
    /// </summary>
    public class GetServices
    {
        /// <summary>
        /// 根据城市id获取该城市服务列表
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns></returns>
        public List<Services.showService> GetServiceList(int cityID)
        {
            //cityid不做判断是否开通  前台默认绑定开通
            //从缓存获取，获取不到则请求数据库，然后更新缓存
            //List<ServiceHomeDB.SERVICE> listService = new List<ServiceHomeDB.SERVICE>();
            List<Services.showService> listService = null;

            string key = ConstString.GetCityServiceKey(cityID);
            if (MemoryCacheHelper.IsExist(key) && MemoryCacheHelper.GetValue(key) != null)
            {
                //缓存
                listService = MemoryCacheHelper.GetValue(key) as List<Services.showService>;
            }
            else
            {
                listService = new List<Services.showService>();
                //数据库
                using (ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities())
                {
                    string sql = @"SELECT S.* FROM CITY C INNER JOIN CITY_SERVICE CS ON C.CITYID=CS.CITYID 
                                    INNER JOIN SERVICE S ON CS.SERVICE_ID=S.SERVICE_ID
                                    WHERE C.CITYID=@CITYID AND C.ISOPEN=1";
                    List<ServiceHomeDB.SERVICE> sl = db.SERVICE.SqlQuery(sql, new SqlParameter("@CITYID", cityID)).ToList();


                    foreach (var item in sl)
                    {
                        if (item.ISLEAF.HasValue && !item.ISLEAF.Value)
                        {
                            Services.showService parentService = new Services.showService
                            {
                                SERVICE_ID = item.SERVICE_ID.ToString(),
                                SERVICE_CODE = item.SERVICE_CODE,
                                SERVICE_NAME = item.SERVICE_NAME,
                                PICURL = item.PICURL,
                                SERVICE_DESC = item.SERVICE_ITEMS,
                                SERVICE_ITEMS = new List<Services.showService>()
                            };
                            foreach (var ch in sl)
                            {
                                if (ch.PARENT_SERVICE_ID.HasValue && ch.PARENT_SERVICE_ID.Value == item.SERVICE_ID)
                                {
                                    parentService.SERVICE_ITEMS.Add(new Services.showService
                                    {
                                        SERVICE_ID = ch.SERVICE_ID.ToString(),
                                        SERVICE_CODE = ch.SERVICE_CODE,
                                        SERVICE_NAME = ch.SERVICE_NAME,
                                        PICURL = ch.PICURL,
                                        SERVICE_DESC = ch.SERVICE_ITEMS
                                    });
                                }
                            }
                            listService.Add(parentService);
                        }
                    }

                    //更新缓存
                    //半小时过期
                    MemoryCacheHelper.Set(key, listService, DateTimeOffset.Now.AddHours(0.5));
                }
            }

            return listService;

        }
    }
}