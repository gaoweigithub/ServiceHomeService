//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceHome.ServiceHomeDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class OPERATE_QUEUE
    {
        public int QUEUE_ID { get; set; }
        public Nullable<int> ORDER_ID { get; set; }
        public string TYPE { get; set; }
        public string RESULT { get; set; }
        public Nullable<System.DateTime> CREATE_TIME { get; set; }
        public Nullable<System.DateTime> UPDATE_TIME { get; set; }
    }
}
