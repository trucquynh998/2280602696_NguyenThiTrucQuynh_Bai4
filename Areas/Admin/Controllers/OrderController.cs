using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenThiTrucQuynh_buoi4.Models;

namespace NguyenThiTrucQuynh_buoi4.Areas.Admin.Controllers
{
    //chú ý phân vùng cho controller
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context) { 
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.ApplicationUser)
                .ToListAsync();
            return View(orders);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var order = await _context.Orders.Include(o => o.ApplicationUser).Include(o => o.OrderDetails).ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}
