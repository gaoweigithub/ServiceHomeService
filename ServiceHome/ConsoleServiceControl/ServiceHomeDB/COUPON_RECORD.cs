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
    
    public partial class COUPON_RECORD
    {
        public int RECORD_ID { get; set; }
        public Nullable<int> COUPON_ID { get; set; }
        public Nullable<int> USERID { get; set; }
        public string PHONE { get; set; }
        public System.DateTime DOWNLOAD_TIME { get; set; }
        public bool ISUSED { get; set; }
        public Nullable<System.DateTime> USETIME { get; set; }
        public Nullable<short> CHANNEL { get; set; }
        public System.DateTime START_TIME { get; set; }
        public System.DateTime END_TIME { get; set; }
    
        public virtual COUPON_INFO COUPON_INFO { get; set; }
        public virtual USERS USERS { get; set; }
    }
}