using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceHome.DAL
{
    public class AddOperationLog
    {
        public static void Info(string title, string content)
        {
            InsertLog(LogType.Info, title, content, "1000", "");
        }
        public static void Warn(string title, string content)
        {
            InsertLog(LogType.Warn, title, content, "1000", "");
        }
        public static void Error(string title, string content)
        {
            InsertLog(LogType.Error, title, content, "1000", "");
        }
        private static void InsertLog(LogType logType, string logTitle, string content, string Appid, string Ip)
        {
            try
            {
                ServiceHomeDB.housekeepingEntities db = new ServiceHomeDB.housekeepingEntities();

                db.OPERATION_LOG.Add(new ServiceHomeDB.OPERATION_LOG { APPID = Appid, CONTENT = content, CT = DateTime.Now, IP = Ip, TYPE = logType.ToString(), LOGTITLE = logTitle });
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }

        }
        public enum LogType
        {
            Info = 1,
            Warn = 2,
            Error = 3
        }
    }
}