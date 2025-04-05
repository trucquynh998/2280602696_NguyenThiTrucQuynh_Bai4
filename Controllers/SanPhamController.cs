﻿using Microsoft.AspNetCore.Mvc;
using NguyenThiTrucQuynh_buoi4.Repositories;

namespace NguyenThiTrucQuynh_buoi4.Controllers
{
    public class SanPhamController : Controller
    {
        //Mục đích controller này để hiển thị layout sản phẩm cho thật đẹp
        //Và user bấm add to cart
        private readonly IProductRepository _productRepository;

        public SanPhamController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }
    }
}
