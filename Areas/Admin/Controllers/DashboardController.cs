using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenThiTrucQuynh_buoi4.Models;

namespace NguyenThiTrucQuynh_buoi4.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            // Thống kê tổng số khách hàng (khác nhau)
            var totalCustomers = await _context.Orders.Select(
                o => o.UserId).Distinct().CountAsync();

            // Tổng số lượng đơn hàng
            var totalOrders = await _context.Orders.CountAsync();

            // Tổng doanh số
            var totalRevenue = await _context.Orders.SumAsync(
                o => o.TotalPrice);

            //Đưa dữ liệu qua view thông qua biến ViewBag
            ViewBag.TotalCustomers = totalCustomers;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalRevenue = totalRevenue;

            return View();
        }
        //Trả về data để vẽ chart
        [HttpGet]
        public async Task<JsonResult> GetChartData()
        {
            var today = DateTime.UtcNow;
            var last30Days = today.AddDays(-30); //lấy trong vòng 30 ngày

            var ordersData = await _context.Orders
                .Where(o => o.OrderDate >= last30Days && o.OrderDate <= today)
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new
                {
                    Date = g.Key, // Để nguyên dạng DateTime
                    Orders = g.Count(),
                    Revenue = g.Sum(o => o.TotalPrice)
                })
                .OrderBy(g => g.Date)
                .ToListAsync();

            // Chuyển đổi DateTime thành chuỗi sau khi lấy từ database
            var result = ordersData.Select(d => new
            {
                Date = d.Date.ToString("yyyy-MM-dd"),
                Orders = d.Orders,
                Revenue = d.Revenue
            }).ToList();

            return Json(result);
        }
    }

}
