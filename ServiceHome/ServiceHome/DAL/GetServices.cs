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
        public List<ServiceHomeDB.SERVICE> GetServiceList(int cityID)
        {
            //cityid不做判断是否开通  前台默认绑定开通
            //从缓存获取，获取不到则请求数据库，然后更新缓存
            List<ServiceHomeDB.SERVICE> listService = new List<ServiceHomeDB.SERVICE>();
            string key = ConstString.GetCityServiceKey(cityID);
            if (MemoryCacheHelper.IsExist(key))
            {
                //缓存
                listService = MemoryCacheHelper.GetValue(key) as List<ServiceHomeDB.SERVICE>;
            }
            else
            {
                //数据库
                using (ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities())
                {
                    string sql = @"SELECT S.* FROM CITY C INNER JOIN CITY_SERVICE CS ON C.CITYID=CS.CITYID 
                                    INNER JOIN SERVICE S ON CS.SERVICE_ID=S.SERVICE_ID
                                    WHERE C.CITYID=@CITYID AND C.ISOPEN=1";
                    List<ServiceHomeDB.SERVICE> tt = db.SERVICE.SqlQuery(sql, new SqlParameter("@CITYID", cityID)).ToList();
                    if (tt != null)
                    {
                        tt.ForEach(item =>
                        {
                            listService.Add(new ServiceHomeDB.SERVICE
                            {
                                CITY_SERVICE = item.CITY_SERVICE,
                                CREATE_TIME = item.CREATE_TIME,
                                ISLEAF = item.ISLEAF,
                                PARENT_SERVICE_ID = item.PARENT_SERVICE_ID,
                                SERVICE_NAME = item.SERVICE_NAME,
                                PICURL = item.PICURL,
                                SERVICE_CODE = item.SERVICE_CODE,
                                SERVICE_ID = item.SERVICE_ID,
                                SERVICE_ITEMS = item.SERVICE_ITEMS,
                                URL = item.URL
                            });
                        });
                    }

                    //更新缓存
                    MemoryCacheHelper.Set(key, listService);
                }
            }

            return listService;

        }
    }
}