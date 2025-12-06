using KorpInventory.Core.Repository;
using MediatR;

namespace KorpInventory.Application.Commands.UpdateStock
{
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand, Unit>
    {
        private readonly IProductRepository _repository;
        public UpdateStockCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.ProductId);
            if (product == null)
            {
                throw new InvalidOperationException($"Produto com ID {request.ProductId} não encontrado.");
            }

            var newStock = product.StockQuantity - request.Quantity;
            if (newStock < 0)
            {
                throw new InvalidOperationException($"Estoque insuficiente. Disponível: {product.StockQuantity}, Solicitado: {request.Quantity}");
            }

            product.StockQuantity = newStock;
            await _repository.UpdateAsync(product);

            return Unit.Value;
        }
    }
}
