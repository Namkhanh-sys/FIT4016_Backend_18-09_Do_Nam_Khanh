using Microsoft.EntityFrameworkCore;
using OrderManagement.Models;

namespace OrderManagement.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Cấu hình bảng products (Ánh xạ chính xác tên cột SQL)
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                
                entity.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(255);
                entity.HasIndex(e => e.Name).IsUnique(); // Ràng buộc duy nhất cho tên sản phẩm
                
                entity.Property(e => e.Sku).HasColumnName("sku").IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.Sku).IsUnique(); // Ràng buộc duy nhất cho mã SKU
                
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity").IsRequired();
                entity.Property(e => e.Category).HasColumnName("category").IsRequired().HasMaxLength(100);
                
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            });

            // 2. Cấu hình bảng orders
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ProductId).HasColumnName("product_id").IsRequired();
                
                entity.Property(e => e.OrderNumber).HasColumnName("order_number").IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.OrderNumber).IsUnique(); // Ràng buộc duy nhất cho mã đơn hàng
                
                entity.Property(e => e.CustomerName).HasColumnName("customer_name").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Quantity).HasColumnName("quantity").IsRequired();
                
                entity.Property(e => e.CustomerEmail).HasColumnName("customer_email").IsRequired().HasMaxLength(150);
                entity.HasIndex(e => e.CustomerEmail).IsUnique(); // Ràng buộc duy nhất cho email khách hàng
                
                entity.Property(e => e.OrderDate).HasColumnName("order_date").IsRequired();
                entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
                
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

                // Cấu hình quan hệ 1 - n: 1 Product có nhiều Orders
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Restrict); // Dùng Restrict để tránh lỗi xóa vòng trong SQL Server
            });

            // 

            // 3. Tạo dữ liệu mẫu (Seed Data)
            // Seed 15 Products
            var products = new List<Product>();
            for (int i = 1; i <= 15; i++)
            {
                products.Add(new Product 
                { 
                    Id = i, 
                    Name = $"High-end Product {i}", 
                    Sku = $"SKU-{2026}{i:D3}", 
                    Description = $"This is the official description for product item number {i}.",
                    Price = 100.50m + (i * 15),
                    StockQuantity = 100 + i,
                    Category = (i % 3 == 0) ? "Electronics" : (i % 3 == 1) ? "Furniture" : "Accessories",
                    CreatedAt = DateTime.Parse("2026-01-01"),
                    UpdatedAt = DateTime.Parse("2026-01-01")
                });
            }
            modelBuilder.Entity<Product>().HasData(products);

            // Seed 30 Orders
            var orders = new List<Order>();
            for (int i = 1; i <= 30; i++)
            {
                orders.Add(new Order
                {
                    Id = i,
                    ProductId = (i % 15) == 0 ? 15 : (i % 15), 
                    OrderNumber = $"ORD-20260117-{1000 + i}", 
                    CustomerName = $"Customer User {i}",
                    Quantity = (i % 4) + 1,
                    CustomerEmail = $"user.client{i}@ordermail.com",
                    OrderDate = DateTime.Parse("2026-01-17"),
                    // Seed một vài đơn đã giao (có DeliveryDate) để test hiển thị Status
                    DeliveryDate = (i % 3 == 0) ? DateTime.Parse("2026-01-18") : null,
                    CreatedAt = DateTime.Parse("2026-01-17"),
                    UpdatedAt = DateTime.Parse("2026-01-17")
                });
            }
            modelBuilder.Entity<Order>().HasData(orders);
        }
    }
}