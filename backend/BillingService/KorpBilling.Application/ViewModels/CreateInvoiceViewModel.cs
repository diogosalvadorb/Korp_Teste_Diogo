namespace KorpBilling.Application.ViewModels
{
    public class CreateInvoiceViewModel
    {
        public List<CreateInvoiceItemViewModel> Items { get; set; } = new();
    }
}
