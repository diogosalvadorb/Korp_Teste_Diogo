using KorpInventory.Application.ViewModel;
using MediatR;

namespace KorpInventory.Application.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ProductViewModel>
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
