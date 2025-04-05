using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NguyenThiTrucQuynh_buoi4.Models
{
    //Này là cái lớp dùng để thao tác đến cơ sở dữ liệu
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public
ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
base(options)
        { 
        }
        //Khai báo 3 cái bảng trong cơ sở dữ liệu
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
