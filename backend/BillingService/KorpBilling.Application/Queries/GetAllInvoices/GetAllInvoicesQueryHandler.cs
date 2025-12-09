using KorpBilling.Application.ViewModels;
using KorpBilling.Core.Repository;
using MediatR;

namespace KorpBilling.Application.Queries.GetAllInvoices
{
    public class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, IEnumerable<InvoiceViewModel>>
    {
        private readonly IInvoiceRepository _repository;
        public GetAllInvoicesQueryHandler(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InvoiceViewModel>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _repository.GetAllAsync();
            return invoices.Select(i => new InvoiceViewModel
            {
                Id = i.Id,
                Number = i.Number,
                Status = i.Status,
                CreatedAt = i.CreatedAt,
                Items = i.Items.Select(item => new InvoiceItemViewModel
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Code = item.Code,
                    Description = item.Description,
                    TotalPrice = item.TotalPrice
                }).ToList(),
                TotalAmount = i.TotalAmount
            });
        }
    }
}
