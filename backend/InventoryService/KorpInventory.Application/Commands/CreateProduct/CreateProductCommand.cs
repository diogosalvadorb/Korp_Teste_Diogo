using KorpInventory.Application.ViewModel;
using MediatR;

namespace KorpInventory.Application.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductViewModel>
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
