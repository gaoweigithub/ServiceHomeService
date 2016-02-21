using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonTool;
using ServiceHome.ServiceHomeDB;
namespace ServiceHome.DAL
{
    public class CityDao
    {
        /// <summary>
        /// 获取已开通的城市列表
        /// </summary>
        /// <param name="isOpened">是否已开通</param>
        /// <param name="provinceID">省份id</param>
        /// <returns></returns>
        public List<CITY> GetOpenedCityList(bool isOpened = true, int provinceID = -1)
        {
            try
            {
                using (var db = new housekeepingEntities())
                {
                    var cities = from c in db.CITY
                                where c.ISOPEN == true && provinceID == -1 ? true : c.PROVINCEID == provinceID
                                orderby c.CITYID ascending
                                select c;
                    if(cities!=null)
                    {
                        return cities.ToList<CITY>();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.AddOperationLog.Error("CreateOrder", ex.Message);
                return null;
            }
        }
    }
}