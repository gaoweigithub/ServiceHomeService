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
    [Route("/AddUser", "POST")]
    [Route("/AddUser/{requestHeader}/{User}","POST")]
    public class AddUserRequest : RequestBase
    {
        public USERS User { get; set; }
    }
}