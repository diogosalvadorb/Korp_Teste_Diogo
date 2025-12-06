using KorpInventory.Application.ViewModel;
using KorpInventory.Core.Repository;
using MediatR;

namespace KorpInventory.Application.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductViewModel>
    {
        private readonly IProductRepository _repository;
        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductViewModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null)
            {
                throw new InvalidOperationException($"Produto com ID {request.Id} não encontrado.");
            }

            if (await _repository.CodeExistsAsync(request.Code, request.Id))
            {
                throw new InvalidOperationException($"Produto com código '{request.Code}' já existe.");
            }

            product.Code = request.Code;
            product.Description = request.Description;
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;

            await _repository.UpdateAsync(product);

            return new ProductViewModel
            {
                Id = product.Id,
                Code = product.Code,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };
        }
    }
}
