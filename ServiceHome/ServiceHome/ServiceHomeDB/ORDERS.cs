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
    
    public partial class ORDERS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORDERS()
        {
            this.ORDER_ITEM = new HashSet<ORDER_ITEM>();
        }
    
        public int ORDERID { get; set; }
        public Nullable<int> STAFFID { get; set; }
        public Nullable<int> USERID { get; set; }
        public Nullable<System.DateTime> CREATE_TIME { get; set; }
        public Nullable<System.DateTime> BEGINTIME { get; set; }
        public Nullable<System.DateTime> ENDTIME { get; set; }
        public string STATUS { get; set; }
        public Nullable<bool> ISREJECT { get; set; }
        public string REJECT_TYPE { get; set; }
        public string REJECT_DETAIL { get; set; }
        public string STARRATE { get; set; }
        public byte[] USERCOMMENT { get; set; }
        public string REMARK { get; set; }
        public string PAY_TYPE { get; set; }
        public Nullable<bool> ISCASHPAYED { get; set; }
        public Nullable<decimal> ALLCOUNT { get; set; }
        public Nullable<decimal> ORDERCOST { get; set; }
        public Nullable<decimal> ORDERINCOME { get; set; }
        public string SERVICE_ADDRESS { get; set; }
        public string SERVICE_LOCATION { get; set; }
        public Nullable<decimal> LAT { get; set; }
        public Nullable<decimal> LNG { get; set; }
        public Nullable<System.DateTime> ACTUAL_START_TIME { get; set; }
        public Nullable<System.DateTime> ACTUAL_END_TIME { get; set; }
        public Nullable<bool> ISGIVENTIP { get; set; }
        public Nullable<decimal> AMOUNT_TIP { get; set; }
        public string CITYID { get; set; }
        public string SPECIAL_REQUEST { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDER_ITEM> ORDER_ITEM { get; set; }
        public virtual STAFF STAFF { get; set; }
        public virtual USERS USERS { get; set; }
    }
}
