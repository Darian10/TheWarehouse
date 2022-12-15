using System;
using System.ComponentModel.DataAnnotations;

namespace TheWarehouse.Data
{
    public class Auction : BaseEntity
    {
        public string ApplicationUserId { get; set; }
        public string ProductName { get; set; }
        public string Winner { get; set; }
        public int InitialPrice { get; set; }
        public int ActualPrice { get; set; }
        public DateTime Date { get; set; }
        public string Picture { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
