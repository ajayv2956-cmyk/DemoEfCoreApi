using DemoEfCoreApi.Data;
using DemoEfCoreApi.Models;
using DemoEfCoreApi.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoEfCoreApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DemoEfDbContext _context;

        public ProductRepository(DemoEfDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            // using store procedure
            var products =  _context.Products
            .FromSqlRaw("EXECUTE dbo.GetAllProducts")
            .AsEnumerable()
            .ToList();
            return products;

            //return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            string sql = "EXECUTE dbo.GetProductById @Id";

            var product =  _context.Products
                .FromSqlRaw(sql, new SqlParameter("@Id", id))
                .AsEnumerable()
                .FirstOrDefault();
            return product;
            //return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await SaveChangesAsync();
            return product;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
