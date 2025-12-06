using KorpInventory.Application.ViewModel;
using KorpInventory.Core.Repository;
using MediatR;

namespace KorpInventory.Application.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductViewModel>>
    {
        private readonly IProductRepository _repository;
        public GetAllProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductViewModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();

            return products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Code = p.Code,
                Description = p.Description,
                Price = p.Price,
                StockQuantity = p.StockQuantity
            });
        }
    }
}
