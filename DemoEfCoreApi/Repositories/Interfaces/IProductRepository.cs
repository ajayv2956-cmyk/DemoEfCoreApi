using DemoEfCoreApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoEfCoreApi.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
