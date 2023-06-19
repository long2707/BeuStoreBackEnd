using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BeuStoreApi.Entities
{
    public class MyDbContext:IdentityDbContext<User>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<Products> products { get; set; }
        public DbSet<Categories> categories { get; set; } 
       // public DbSet<ProductCategories> productsCategories { get; set; }
        public DbSet<Gallerles> gallerles { get; set; }
        public DbSet<Tags> tags { get; set; }
        //public DbSet<Attrbutes> attributes { get; set; }
        //public DbSet<AttrbuteValue> attributeValue { get; set; }
        //public DbSet<ProductAttribute> productAttributes { get; set; }
       // public DbSet<ProductAttributeValues> productAttributeValues { get; set; }
        //public DbSet<variant_options> variant_Options { get; set; }
        //public DbSet<Variants> variants { get; set; }
        //public DbSet<Variant_Values> variant_Values { get; set; }
        // public DbSet<Order_Status> order_Statuses { get; set; }
        //public DbSet<Orders> orders { get; set; }
        //public DbSet<OrderItems> orderItems { get; set; }   
        //public DbSet<Customers> customers { get; set; }
        //public DbSet<Carts> carts { get; set; }
        //public DbSet<Cart_Items> cart_items { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Categories>()
                .HasMany(c => c.Children)
                .WithOne(c => c.Parent)
                .HasForeignKey(c => c.parent_id);

            //builder.Entity<Products>
            //(entity => entity.HasCheckConstraint("check_price", "compare_price > sale_price OR compare_price = 0")
            //.Property(x=>x.compare_price)
            //.HasDefaultValue(0));

            //builder.Entity<variant_options>
            //(entity => entity.HasCheckConstraint("check_price", "compare_price > sale_price OR compare_price = 0")
            //.Property(x => x.compare_price)
            //.HasDefaultValue(0));

            //builder.Entity<Products>
            //(entity => entity.HasCheckConstraint("check_type", "product_type IN ('simple', 'variable')"));


        }
    }
}
