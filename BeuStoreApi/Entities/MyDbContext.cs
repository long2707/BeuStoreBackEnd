using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BeuStoreApi.Entities
{
    public class MyDbContext:IdentityDbContext<Staff>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
         public DbSet<Products> products { get; set; }
        public DbSet<Categories> categories { get; set; } 
        public DbSet<Gallerles> gallerles { get; set; }
        public DbSet<Tags> tags { get; set; }
        public DbSet<Attrbutes> attributes { get; set; }
        public DbSet<AttrbuteValue> attributesValue { get; set; }
        public DbSet<Variants> variants { get; set; }
        public DbSet<VariantValues> variantValues { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Categories>()
                .HasMany(c => c.Children)
                .WithOne(c => c.Parent)
                .HasForeignKey(c => c.parent_id);

        }
    }
}
