using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheWarehouse.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public double MoneyCard { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }
    }
}
