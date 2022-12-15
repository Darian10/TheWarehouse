using System;
using System.Collections.Generic;
using System.Text;

namespace TheWarehouse.Data
{
    public partial class Sale:BaseEntity
    {
        
            public Sale()
            {
                this.SaleList = new HashSet<SaleList>();
            }

            
            public Nullable<System.DateTime> SaleDay { get; set; }
            public Nullable<double> Subtotal { get; set; }
            public Nullable<double> Tax { get; set; }
            public Nullable<double> Total { get; set; }

            public virtual ICollection<SaleList> SaleList { get; set; }
        
    }
}
