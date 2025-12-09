using KorpBilling.Application.ViewModels;
using MediatR;

namespace KorpBilling.Application.Commands.CreateInvoice
{
    public class CreateInvoiceCommand : IRequest<InvoiceViewModel>
    {
        public List<InvoiceItemCommand> Items { get; set; } = new();
    }

    public class InvoiceItemCommand
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
    }
}

