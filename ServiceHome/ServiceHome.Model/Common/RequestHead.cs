﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceHome.Model.Common
{
    public class RequestHead
    {
        public string Version { get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }
        public string UserPwd { get; set; }
        public string AccessCode { get; set; }
        //调用的接口名称
        public string interfaceName { get; set; }
    }
}
