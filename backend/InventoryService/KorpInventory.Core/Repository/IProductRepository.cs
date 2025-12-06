using KorpInventory.Core.Entities;

namespace KorpInventory.Core.Repository
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<Product?> GetByCodeAsync(string code);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<bool> CodeExistsAsync(string code, int? excludeId = null);
    }
}
