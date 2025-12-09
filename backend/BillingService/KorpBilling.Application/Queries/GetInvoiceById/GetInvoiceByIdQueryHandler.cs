using KorpBilling.Application.ViewModels;
using KorpBilling.Core.Repository;
using MediatR;

namespace KorpBilling.Application.Queries.GetInvoiceById
{
    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceViewModel?>
    {
        private readonly IInvoiceRepository _repository;
        public GetInvoiceByIdQueryHandler(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<InvoiceViewModel?> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _repository.GetByIdWithItemsAsync(request.Id);
            if (invoice == null) return null;

            return new InvoiceViewModel
            {
                Id = invoice.Id,
                Number = invoice.Number,
                Status = invoice.Status,
                CreatedAt = invoice.CreatedAt,
                Items = invoice.Items.Select(item => new InvoiceItemViewModel
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Code = item.Code,
                    Description = item.Description,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice
                }).ToList(),
                TotalAmount = invoice.TotalAmount
            };
        }
    }
}
