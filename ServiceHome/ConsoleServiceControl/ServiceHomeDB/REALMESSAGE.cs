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
    
    public partial class REALMESSAGE
    {
        public int MESSAGE_ID { get; set; }
        public string CONTENT { get; set; }
        public string URL { get; set; }
        public Nullable<int> ORDER_ID { get; set; }
        public Nullable<bool> IS_READ { get; set; }
        public Nullable<bool> IS_PUSHED { get; set; }
        public Nullable<System.DateTime> CREATE_TIME { get; set; }
        public Nullable<System.DateTime> UPDATE_TIME { get; set; }
    }
}