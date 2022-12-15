using System;
using System.Collections.Generic;
using System.Text;

namespace TheWarehouse.Data
{
    public class AnnouncementSaleList:BaseEntity
    {
        public AnnouncementSaleList()
        {
            this.SaleList = new HashSet<SaleList>();
        }
        public string ApplicationUserId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Cant { get; set; }
        public string Foto { get; set; }

        public DateTime VigencyDate { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<SaleList> SaleList { get; set; }
    }
}
