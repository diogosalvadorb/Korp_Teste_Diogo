namespace KorpBilling.Application.ViewModels
{
    public class CreateInvoiceItemViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
