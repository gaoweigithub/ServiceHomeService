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
    
    public partial class SERVICE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SERVICE()
        {
            this.CITY_SERVICE = new HashSet<CITY_SERVICE>();
            this.SERVICE1 = new HashSet<SERVICE>();
        }
    
        public int SERVICE_ID { get; set; }
        public Nullable<int> PARENT_SERVICE_ID { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SERVICE_NAME { get; set; }
        public Nullable<System.DateTime> CREATE_TIME { get; set; }
        public string URL { get; set; }
        public Nullable<bool> ISLEAF { get; set; }
        public string PICURL { get; set; }
        public string SERVICE_ITEMS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CITY_SERVICE> CITY_SERVICE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SERVICE> SERVICE1 { get; set; }
        public virtual SERVICE SERVICE2 { get; set; }
    }
}
