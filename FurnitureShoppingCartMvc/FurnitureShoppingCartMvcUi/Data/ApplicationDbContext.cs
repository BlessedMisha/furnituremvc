using FurnitureShoppingCartMvcUi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShoppingCartMvcUi.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Colection> Colections { get; set; }
        public DbSet<Furniture> Furnitures { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configure ItemPrice and TotalPrice properties of Order entity
            builder.Entity<Order>()
                .Property(o => o.ItemPrice)
                .HasColumnType("decimal(18,2)");

            builder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");

            // Configure Price property of CatalogItem entity
            builder.Entity<CatalogItem>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(builder);
        }
    }
}
