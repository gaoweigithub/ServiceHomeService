using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceHome.Model.Common
{
    public abstract class ResponseBase
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}
