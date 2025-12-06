using KorpBilling.Core.Entities;

namespace KorpBilling.Core.Repository
{
    public interface IInvoiceRepository
    {
        Task<Invoice?> GetByIdAsync(int id);
        Task<Invoice?> GetByIdWithItemsAsync(int id);
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<Invoice> AddAsync(Invoice invoice);
        Task UpdateAsync(Invoice invoice);
        Task<int> GetNextInvoiceNumberAsync();
    }
}
