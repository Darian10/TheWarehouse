using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using TheWarehouse.Data;

namespace TheWarehouse.Repo
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public ApplicationDbContext()
        {

        }
       
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleList> SalesList { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Announcement> Announcement { get; set; }
        public DbSet<AnnouncementSaleList> AnnouncementSaleList { get; set; }
        public DbSet<Auction> Auction { get; set; }

    }
}
