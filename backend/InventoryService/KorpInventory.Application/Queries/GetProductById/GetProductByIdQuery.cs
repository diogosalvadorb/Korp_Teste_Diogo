using KorpInventory.Application.ViewModel;
using MediatR;

namespace KorpInventory.Application.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductViewModel?>
    {
        public int Id { get; set; }
    }
}
