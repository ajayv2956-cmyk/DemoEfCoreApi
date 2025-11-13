using DemoEfCoreApi.Data;
using DemoEfCoreApi.Models;
using DemoEfCoreApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DemoEfCoreApi.Services
{
    public class ProductService : IProductService
    {
        private readonly DemoEfDbContext _context;

        public ProductService(DemoEfDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
