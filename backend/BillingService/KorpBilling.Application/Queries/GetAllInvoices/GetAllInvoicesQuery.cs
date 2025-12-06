using KorpBilling.Application.ViewModels;
using MediatR;

namespace KorpBilling.Application.Queries.GetAllInvoices
{
    public class GetAllInvoicesQuery : IRequest<IEnumerable<InvoiceViewModel>>
    {
    }
}
