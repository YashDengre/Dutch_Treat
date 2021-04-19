using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchContext : IdentityDbContext<StoreUser>//DbContext //Change DBcontext to IdentityDbContext for using the authorization with identity
    {
        private readonly IConfiguration _config;
        public DutchContext(IConfiguration config)
        {
            _config = config;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }     //optional - will not be useful for this projects

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:DutchContextDb"]);
        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            //modelbuilder.Entity<Product>().Property(p => p.Title).HasMaxLength(50); 
            //use this to specify diff prop about the model when you created here -use when migrations build them

            modelbuilder.Entity<Order>().HasData(new Order()
            {
                Id = 1,
                OrderDate = DateTime.UtcNow,
                OrderNumber = "12345"
            });


        }

    }
}
