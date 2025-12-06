namespace KorpBilling.Application.ViewModels
{
    public class InvoiceItemViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string FormattedUnitPrice => $"R$ {UnitPrice:N2}";
        public string FormattedTotalPrice => $"R$ {TotalPrice:N2}";
    }
}
