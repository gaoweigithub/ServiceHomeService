using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ServiceStack;
using ServiceHome.Model;
using ServiceHome.ServiceHomeDB;
using ServiceHome.Model.Common;
namespace ServiceHome
{
    [Route("/Ping")]
    [Route("/Ping/{ClientName}/{ClientID}")]
    public class PingRequest : RequestBase
    {
        public string ClientID { get; set; }
        public string ClientName { get; set; }
    }
}