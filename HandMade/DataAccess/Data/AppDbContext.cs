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
    public virtual DbSet<Seller> Sellers { get; set; }
    public virtual DbSet<Admin> Admins { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category() { Id = 1, Name = "Nến Thơm" },
            new Category() { Id = 2, Name = "Khung Ảnh" },
            new Category() { Id = 3, Name = "Giấy Sáp Ong" },
            new Category() { Id = 4, Name = "Sách Decor" },
            new Category() { Id = 5, Name = "Sao Giấy" }
        );

        modelBuilder.Entity<Role>().HasData(
            new Role() { Id = 1, RoleType = RoleType.Admin },
            new Role() { Id = 2, RoleType = RoleType.Seller },
            new Role() { Id = 3, RoleType = RoleType.Customer }
        );

        modelBuilder.Entity<User>().HasData(
            new User() { Id = 1, FullName = "John Administrator", Email = "admin@gmail.com", PassWord = "12345", RoleId = 1 },
            new User() { Id = 2, FullName = "Trương Tử Hoàng", Email = "seller1@gmail.com", PassWord = "12345", RoleId = 2 },
            new User() { Id = 3, FullName = "Tôn Cung Linh", Email = "seller2@gmail.com", PassWord = "12345", RoleId = 2 },
            new User() { Id = 4, FullName = "Đả Thần Tiên", Email = "seller3@gmail.com", PassWord = "12345", RoleId = 2 },
            new User() { Id = 5, FullName = "Ngô Đồng Thụ", Email = "long88ka@gmail.com", PassWord = "12345", RoleId = 3 }
        );

        modelBuilder.Entity<Seller>().HasData(
            new Seller() { Id = 1, UserId = 2 },
            new Seller() { Id = 2, UserId = 3 },
            new Seller() { Id = 3, UserId = 4 }
        );

        modelBuilder.Entity<Admin>().HasData(
            new Admin() { Id = 1, UserId = 1 }
        );
        modelBuilder.Entity<Customer>().HasData(
            new Customer() { Id = 1, UserId = 5,Status = UserStatus.Actived,Address = "6-16 Ấp 4, XTT, TP.HCM", PhoneNumber = "0397528860"}
        );
    }
}