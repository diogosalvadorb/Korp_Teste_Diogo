using KorpBilling.Core.Entities.enums;
using KorpBilling.Core.Repository;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;



namespace KorpBilling.Application.Commands.PrintInvoice
{
    public class PrintInvoiceCommandHandler : IRequestHandler<PrintInvoiceCommand, Unit>
    {
        private readonly IInvoiceRepository _repository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public PrintInvoiceCommandHandler(IInvoiceRepository repository, HttpClient httpClient, IConfiguration configuration)
        {
            _repository = repository;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<Unit> Handle(PrintInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _repository.GetByIdWithItemsAsync(request.InvoiceId);

            if (invoice.Status == InvoiceStatus.Closed)
            {
                return Unit.Value;
            }

            if (invoice.Status != InvoiceStatus.Open)
            {
                throw new InvalidOperationException("Apenas notas fiscais abertas podem ser impressas.");
            }

            var inventoryServiceUrl = _configuration["Services:InventoryService"] ?? "http://localhost:5100";

            foreach (var item in invoice.Items)
            {
                try
                {
                    var updateStockRequest = new
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    };

                    var response = await _httpClient.PostAsJsonAsync(
                        $"{inventoryServiceUrl}/api/products/update-stock",
                        updateStockRequest,
                        cancellationToken);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                        throw new HttpRequestException(errorContent);
                    }
                }
                catch (HttpRequestException ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }

            invoice.Status = InvoiceStatus.Closed;
            await _repository.UpdateAsync(invoice);

            return Unit.Value;
        }
    }
}