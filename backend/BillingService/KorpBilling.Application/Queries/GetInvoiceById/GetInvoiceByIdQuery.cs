using KorpBilling.Application.ViewModels;
using MediatR;

namespace KorpBilling.Application.Queries.GetInvoiceById
{
    public class GetInvoiceByIdQuery : IRequest<InvoiceViewModel?>
    {
        public int Id { get; set; }
    }
}
