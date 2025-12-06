using MediatR;

namespace KorpInventory.Application.Commands.UpdateStock
{
    public class UpdateStockCommand : IRequest<Unit>
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
