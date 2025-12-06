using KorpBilling.Core.Entities.enums;

namespace KorpBilling.Application.ViewModels
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public InvoiceStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FormattedDate => CreatedAt.ToString("dd/MM/yyyy HH:mm");
        public List<InvoiceItemViewModel> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public string FormattedTotal => $"R$ {TotalAmount:N2}";
        public bool CanPrint => Status == InvoiceStatus.Open;
    }
}
