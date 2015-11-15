using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceHome.Model.Common
{
    public class ResponseStatus
    {
        public bool isSuccess { get; set; }
        public List<string> ErrorList { get; set; }
    }
}
