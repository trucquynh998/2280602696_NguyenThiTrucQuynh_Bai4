using Microsoft.EntityFrameworkCore;
using NguyenThiTrucQuynh_buoi4.Models;

namespace NguyenThiTrucQuynh_buoi4.Repositories
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        //Nhận ApplicationDbContext qua dependency injection, giúp repository thao tác với cơ sở dữ liệu.
        public EFCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Lấy danh sách tất cả các mục 
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        //Lấy danh mục theo ID
        //Tìm 1 danh mục có Id tương ứng và sử dụng FirstOrDefaultAsync() để lấy danh mục đầu tiên khớp với id.
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(p => p.Id == id);
        }

        //Thêm danh mục vào bảng và lưu thay đổi
        public async Task AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        //Cập nhật danh mục hiện có 
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }


        //Tìm danh mục theo Id nếu thấy thì gọi Remove(cate) để xóa và lưu thay đổi
        public async Task DeleteAsync(int id)
        {
            var cate = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(cate);
            await _context.SaveChangesAsync();
        }
    }
}
