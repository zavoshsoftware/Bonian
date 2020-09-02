using System;
using System.Collections.Generic;
using System.Data.Entity;
namespace Models
{
   public class DatabaseContext:DbContext
    {
        static DatabaseContext()
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<ZarinpallAuthority> ZarinpallAuthorities { get; set; }
        public DbSet<Text> Texts { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ActivationCode> ActivationCodes { get; set; }
        public DbSet<TextType> TextType { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageStatus> MessageStatuses { get; set; }
        public DbSet<UserProductsLike> UserProductsLikes { get; set; }
    }
}
