using KorpInventory.Application.ViewModel;
using KorpInventory.Core.Entities;
using KorpInventory.Core.Repository;
using MediatR;

namespace KorpInventory.Application.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductViewModel>
    {
        private readonly IProductRepository _repository;
        public CreateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductViewModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.CodeExistsAsync(request.Code))
            {
                throw new InvalidOperationException($"Produto com código '{request.Code}' já existe.");
            }

            var product = new Product
            {
                Code = request.Code,
                Description = request.Description,
                Price = request.Price,
                StockQuantity = request.StockQuantity
            };

            var created = await _repository.AddAsync(product);

            return new ProductViewModel
            {
                Id = created.Id,
                Code = created.Code,
                Description = created.Description,
                Price = created.Price,
                StockQuantity = created.StockQuantity
            };
        }
    }
}
