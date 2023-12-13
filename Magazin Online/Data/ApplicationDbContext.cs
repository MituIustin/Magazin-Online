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

            // 1 USER    detine    1 BASKET

            modelBuilder.Entity<ApplicationUser>()
               .HasOne<Basket>(a => a.Basket)
               .WithOne(b => b.User)
               .HasForeignKey<Basket>(b => b.UserId);

            // 1 REQUEST    detine    1 PRODUS

            modelBuilder.Entity<Request>()
               .HasOne<Product>(a => a.Product)
               .WithOne(b => b.Request)
               .HasForeignKey<Request>(b => b.RequestId);

            // 1 BASKET    detine    1 ORDER

            modelBuilder.Entity<Basket>()
               .HasOne<Order>(a => a.Order)
               .WithOne(b => b.Basket)
               .HasForeignKey<Basket>(b => b.BasketId);

            // 1 PRODUCT    detine    M REVIEW

            modelBuilder.Entity<Review>()
               .HasOne<Product>(a => a.Product)
               .WithMany(b => b.Reviews)
               .HasForeignKey(b => b.ProductId);

            // 1 PRODUCT    detine    M COMMENT

            modelBuilder.Entity<Review>()
              .HasOne<Product>(a => a.Product)
              .WithMany(b => b.Reviews)
              .HasForeignKey(b => b.ProductId);

            // 1 USER      realizeaza    M REQUEST

            modelBuilder.Entity<Request>()
               .HasOne<ApplicationUser>(a => a.User)
               .WithMany(b => b.Requests)
               .HasForeignKey(b => b.UserId);


            // 1 USER      scrie    M COMMENT

            modelBuilder.Entity<Comment>()
               .HasOne<ApplicationUser>(a => a.User)
               .WithMany(b => b.Comments)
               .HasForeignKey(b => b.UserId);

            // 1 USER      posteaza    M REVIEW

            modelBuilder.Entity<Review>()
               .HasOne<ApplicationUser>(a => a.User)
               .WithMany(b => b.Reviews)
               .HasForeignKey(b => b.UserId);

            // 1 USER      posteaza    M PRODUCT

            modelBuilder.Entity<Product>()
               .HasOne<ApplicationUser>(a => a.User)
               .WithMany(b => b.Products)
               .HasForeignKey(b => b.UserId);

            // 1 USER      crud        M CATEGORY

            modelBuilder.Entity<Category>()
              .HasOne<ApplicationUser>(a => a.User)
              .WithMany(b => b.Categories)
              .HasForeignKey(b => b.UserId);

            // 1 BASKET    contine    M PRODUCT

            modelBuilder.Entity<Product>()
               .HasOne<Basket>(a => a.Basket)
               .WithMany(b => b.Products)
               .HasForeignKey(b => b.BasketId);

            // 1 CATEGORY   contine    M PRODUCT

            modelBuilder.Entity<Product>()
               .HasOne<Category>(a => a.Category)
               .WithMany(b => b.Products)
               .HasForeignKey(b => b.CategoryId);

        }
    }
}