namespace KorpBilling.Application.ViewModels
{
    public class CreateInvoiceItemViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
    }
}
