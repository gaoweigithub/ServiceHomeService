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
    
    public partial class STAFF_LEVEL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public STAFF_LEVEL()
        {
            this.STAFF = new HashSet<STAFF>();
        }
    
        public int LEVELID { get; set; }
        public string LEVELNAME { get; set; }
        public string 等级说明 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STAFF> STAFF { get; set; }
    }
}