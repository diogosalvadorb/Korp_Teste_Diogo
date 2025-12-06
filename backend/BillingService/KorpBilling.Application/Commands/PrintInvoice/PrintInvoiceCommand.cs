using MediatR;

namespace KorpBilling.Application.Commands.PrintInvoice
{
    public class PrintInvoiceCommand : IRequest<Unit>
    {
        public int InvoiceId { get; set; }
    }
}
