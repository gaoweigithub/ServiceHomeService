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
    
    public partial class AUTHORS
    {
        public int AUTHOR_ID { get; set; }
        public Nullable<int> MASTER_ID { get; set; }
        public Nullable<int> MODULE_ID { get; set; }
        public Nullable<System.DateTime> CT { get; set; }
        public Nullable<int> CU_ID { get; set; }
        public Nullable<bool> R_ADD { get; set; }
        public Nullable<bool> R_DELETE { get; set; }
        public Nullable<bool> R_UPDATE { get; set; }
        public Nullable<bool> R_QUERY { get; set; }
    
        public virtual MASTERS MASTERS { get; set; }
        public virtual MODULES MODULES { get; set; }
    }
}
