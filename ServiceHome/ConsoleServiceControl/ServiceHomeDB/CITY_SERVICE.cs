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
    
    public partial class CITY_SERVICE
    {
        public decimal CITY_SERVICE_ID { get; set; }
        public Nullable<int> SERVICE_ID { get; set; }
        public Nullable<int> CITYID { get; set; }
        public Nullable<System.DateTime> CREATETIME { get; set; }
    
        public virtual CITY CITY { get; set; }
        public virtual SERVICE SERVICE { get; set; }
    }
}