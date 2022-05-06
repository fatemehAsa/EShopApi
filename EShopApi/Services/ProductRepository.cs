using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShopApi.Interfaces;
using EShopApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EShopApi.Services
{
    public class ProductRepository : IProductRepository
    {
        private EShopApi_DBContext _context;

        public ProductRepository(EShopApi_DBContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public async Task<Product> Find(int id)
        {
            return await _context.Products.SingleOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> IsExistProduct(int id)
        {
            return await _context.Products.AnyAsync(p => p.ProductId == id);
        }
    }
}
