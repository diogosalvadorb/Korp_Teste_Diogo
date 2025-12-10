using KorpInventory.Core.Entities;
using KorpInventory.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace KorpInventory.Infrastructure.Persistence.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _context;
        public ProductRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.SingleOrDefaultAsync(p => p.Id == id); 
        }

        public async Task<Product?> GetByCodeAsync(string code)
        {
            return await _context.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Code == code);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CodeExistsAsync(string code, int? excludeId = null)
        {
            return await _context.Products
                .AnyAsync(p => p.Code == code && (excludeId == null || p.Id != excludeId));
        }
    }
}
