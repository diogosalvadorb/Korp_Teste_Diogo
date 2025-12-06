using KorpBilling.Core.Entities.enums;

namespace KorpBilling.Core.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public InvoiceStatus Status { get; set; } 
        public DateTime CreatedAt { get; set; }
        public List<InvoiceItem> Items { get; set; } = new();
        public decimal TotalAmount => Items.Sum(i => i.TotalPrice);
    }
}
