﻿using Microsoft.EntityFrameworkCore;
using NguyenThiTrucQuynh_buoi4.Models;

namespace NguyenThiTrucQuynh_buoi4.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            // return await _context.Products.ToListAsync();
            return await _context.Products
            .Include(p => p.Category) // Include thông tin về category
            .ToListAsync();
        }


        //Lấy Sản Phẩm theo Id và trả về một sản phẩm có Id tương ứng.
        public async Task<Product> GetByIdAsync(int id)         
        {
            // return await _context.Products.FindAsync(id);
            // lấy thông tin kèm theo category
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);           //
        }


        // Thêm một sản phẩm mới vào bảng Products
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();          //Lưu thay đổi vào cơ sở dữ liệu
        }


        //Cập nhật thông tin sản phẩm 
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }


        //Tìm sản phẩm theo Id nếu tìm thấy xóa sản phẩm khỏi cơ sở dữ liệu
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
