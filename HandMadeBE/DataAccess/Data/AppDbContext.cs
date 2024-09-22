using DataAccess.Entites;
using DataAccess.Enum;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    public virtual DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category() { Id = 1, Name = "Nến Thơm" },
            new Category() { Id = 2, Name = "Khung Ảnh" },
            new Category() { Id = 3, Name = "Giấy Sáp Ong" },
            new Category() { Id = 4, Name = "Sách Decor" }
        );

        modelBuilder.Entity<Customer>().HasData(
            new Customer() { Id = 1, FullName = "John Administration", Email = "admin@gmail.com", Status = UserStatus.Actived,
                BirthDate = DateTime.Now, PassWord = "12345" ,TokenCode = 0000}
        );
    }
}