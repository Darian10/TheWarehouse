using System;
using System.Collections.Generic;
using System.Text;

namespace TheWarehouse.Data
{
    public partial class SaleList:BaseEntity
    {
        public SaleList()
        {
                
        }
        public Nullable<int> SaleId { get; set; }
        public Nullable<int> AnnouncementId { get; set; }
        public Nullable<int> Cant { get; set; }
        public Nullable<double> Total { get; set; }

        public virtual Announcement Announcement { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
