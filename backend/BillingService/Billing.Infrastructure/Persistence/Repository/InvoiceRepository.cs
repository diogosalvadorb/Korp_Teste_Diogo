using KorpBilling.Core.Entities;
using KorpBilling.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace KorpBilling.Infrastructure.Persistence.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly BillingDbContext _context;
        public InvoiceRepository(BillingDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            return await _context.Invoices.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Invoice?> GetByIdWithItemsAsync(int id)
        {
            return await _context.Invoices
                .AsNoTracking()
                .Include(i => i.Items)
                .SingleOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices
                .AsNoTracking()
                .Include(i => i.Items)
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetNextInvoiceNumberAsync()
        {
            var lastInvoice = await _context.Invoices
                .AsNoTracking()
                .OrderByDescending(i => i.Number)
                .FirstOrDefaultAsync();

            return lastInvoice?.Number + 1 ?? 1;
        }
    }
}
