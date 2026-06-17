using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Reflection.Emit;

namespace Infrastructure
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options)
            : base (options) { 
        }

        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Order>orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }

        public DbSet<Cart> carts {get; set; }
        public DbSet<CartItem> cartItems { get; set; }  
        public DbSet<Discount> discounts { get; set; }

        public DbSet<Payment> payments { get; set; }

        public DbSet<ProductImage> productImages { get; set; }

        public DbSet<ProductVariant> productVariants { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<WishList> wishLists { get; set; }

        public DbSet<WishListItem> wishListItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasMany(c=>c.Product)
                .WithOne(p=>p.Category)
                .HasForeignKey(p=>p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductImages)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId);
            modelBuilder.Entity<Product>()
                .HasMany(p=>p.ProductVariants)
                .WithOne(v=>v.Product)
                .HasForeignKey(v=>v.ProductId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId);

            modelBuilder.Entity<User>()
                .HasOne(u=>u.Cart)
                .WithOne(c=>c.User)
                .HasForeignKey<Cart>(c=>c.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(c => c.User)
                .HasForeignKey(o => o.UserId);
            modelBuilder.Entity<Category>()
                .HasOne(c=>c.ParentCategory)
                .WithMany(c=>c.SubCategories)
                .HasForeignKey(c=>c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Cart>()
                .HasIndex(c=>c.UserId)
                .IsUnique();

            modelBuilder.Entity<ProductVariant>()
                .HasIndex(v=>new {v.ProductId,v.Size,v.Color})
                .IsUnique();

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.ProductVariant)
                .WithMany()
                .HasForeignKey(ci => ci.ProductVariantId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.ProductVariant)
                .WithMany()
                .HasForeignKey(oi => oi.ProductVariantId);

            modelBuilder.Entity<Order>()
                .Property(x => x.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(x=>x.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ProductVariant>()
                .Property (x => x.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CartItem>()
                .Property(x=>x.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Discount>()
                .Property(x => x.Percentage)
                .HasPrecision(5, 2);
            modelBuilder.Entity<WishList>()
             .HasMany(w => w.WishListItems)
             .WithOne(wi => wi.WishList)
             .HasForeignKey(wi => wi.WishListId);

            modelBuilder.Entity<WishListItem>()
                .HasOne(wi => wi.ProductVariant)
                .WithMany()
                .HasForeignKey(wi => wi.ProductvariantId);
        }

    }
}
