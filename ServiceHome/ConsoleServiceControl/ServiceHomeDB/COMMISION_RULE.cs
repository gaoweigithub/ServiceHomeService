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
    
    public partial class COMMISION_RULE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COMMISION_RULE()
        {
            this.COMMISION_RULE_ITEM = new HashSet<COMMISION_RULE_ITEM>();
        }
    
        public int RULEID { get; set; }
        public string RULE_NAME { get; set; }
        public Nullable<System.DateTime> CT { get; set; }
        public Nullable<int> CU_ID { get; set; }
        public Nullable<bool> ISOPEN { get; set; }
        public Nullable<decimal> DEFAULT_RATE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMISION_RULE_ITEM> COMMISION_RULE_ITEM { get; set; }
    }
}
