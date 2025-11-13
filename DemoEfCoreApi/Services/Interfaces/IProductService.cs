using DemoEfCoreApi.Models;

namespace DemoEfCoreApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
