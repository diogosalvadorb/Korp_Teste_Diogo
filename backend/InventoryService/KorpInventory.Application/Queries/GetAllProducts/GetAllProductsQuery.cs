using KorpInventory.Application.ViewModel;
using MediatR;

namespace KorpInventory.Application.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductViewModel>>
    {
    }
}
