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
    
    public partial class MODULES
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MODULES()
        {
            this.AUTHORS = new HashSet<AUTHORS>();
        }
    
        public int MODULE_ID { get; set; }
        public string MODULE_NAME { get; set; }
        public string MODULE_URL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AUTHORS> AUTHORS { get; set; }
    }
}
