using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceHome.DAL
{
    public class Order
    {
        /// <summary>
        /// 保存订单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool CreateOrder(ServiceHomeDB.ORDERS model)
        {
            try
            {
                using (ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities())
                {
                    db.ORDERS.Add(model);
                    int i = db.SaveChanges();
                    return i > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                DAL.AddOperationLog.Error("CreateOrder", ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 数据库取消订单，更改状态
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public bool CancelOrder(int orderID)
        {
            try
            {
                using (ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities())
                {
                    string sql = "update ORDERS set STATUS='3' where ORDERID={0}";
                    int i = db.Database.ExecuteSqlCommand(string.Format(sql, orderID));
                    db.SaveChanges();
                    return i > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                DAL.AddOperationLog.Error("CancelOrder--db", ex.Message);
                return false;
            }
        }
    }
}