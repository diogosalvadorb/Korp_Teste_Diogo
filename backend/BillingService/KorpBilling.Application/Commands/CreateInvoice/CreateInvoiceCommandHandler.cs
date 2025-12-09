using KorpBilling.Application.ViewModels;
using KorpBilling.Core.Entities;
using KorpBilling.Core.Entities.enums;
using KorpBilling.Core.Repository;
using MediatR;

namespace KorpBilling.Application.Commands.CreateInvoice
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, InvoiceViewModel>
    {
        private readonly IInvoiceRepository _repository;
        public CreateInvoiceCommandHandler(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<InvoiceViewModel> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            if (request.Items == null || !request.Items.Any())
            {
                throw new InvalidOperationException("A nota fiscal deve conter ao menos um item.");
            }

            var invoiceNumber = await _repository.GetNextInvoiceNumberAsync();

            var invoice = new Invoice
            {
                Number = invoiceNumber,
                Status = InvoiceStatus.Open,
                CreatedAt = DateTime.UtcNow,
                Items = request.Items.Select(item => new InvoiceItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Code = item.Code,
                    Description = item.Description,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.Quantity * item.UnitPrice
                }).ToList()
            };

            var created = await _repository.AddAsync(invoice);

            return new InvoiceViewModel
            {
                Id = created.Id,
                Number = created.Number,
                Status = created.Status,
                CreatedAt = created.CreatedAt,
                Items = created.Items.Select(i => new InvoiceItemViewModel
                {
                    Id = i.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Code = i.Code,
                    Description = i.Description,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList(),
                TotalAmount = created.TotalAmount
            };
        }
    }
}
