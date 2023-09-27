using BeuStoreApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeuStoreApi.Entities
{
    public class MyDbContext : IdentityDbContext<User>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
             public DbSet<RefreshToken> refreshTokens { get; set; }
             public DbSet<Gallery> galleries { get; set; }
             public DbSet<Products> products { get;}
             public DbSet<Categories> categories { get; set; }
             public DbSet<Tags> tags { get; set; }

            public DbSet<Attrbutes> Attrbutes { get; set; }
            public DbSet<ProductAttribute> ProductAttribute { get; set; }
            public DbSet<AttrbuteValue> AttrbuteValues { get; set; }
            public DbSet<ProductAttributeValues> ProductAttributeValues { get; set; }

            public DbSet<Variants> Variants { get; set; }
            public DbSet<Variant_Values> VariantValues { get; set; }
            public DbSet<variant_options> variant_options { get; set; }

            public DbSet<Customers> Customers { get; set; }
            public DbSet<Orders> Orders { get; set; }
            public DbSet<Order_Status> OrderStatus { get; set; }
            public DbSet<OrderItems> OrderItems { get; set; }
            public DbSet<Carts> Carts { get; set; }
            public DbSet<Cart_Items> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (modelBuilder == null)
                throw new ArgumentNullException("modelBuilder");

            // for the other conventions, we do a metadata model loop
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // equivalent of modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                entityType.SetTableName(entityType.DisplayName());

                // equivalent of modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Categories>()
                    .HasMany(c => c.Children)
                    .WithOne(c => c.Parent)
                    .HasForeignKey(c => c.parent_id);
        }
    }
}
