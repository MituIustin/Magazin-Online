using Magazin_Online.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Magazin_Online.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order > Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
           .HasOne(a => a.Basket)
           .WithOne(b => b.User)
           .HasForeignKey<Basket>(b => b.UserId);

            modelBuilder.Entity<Request>()
            .HasOne<ApplicationUser>(a => a.User)
            .WithMany(c => c.Requests)
            .HasForeignKey(a => a.UserId);
        }
    }
}