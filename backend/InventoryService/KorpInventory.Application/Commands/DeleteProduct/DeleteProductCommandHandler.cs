using KorpInventory.Core.Repository;
using MediatR;

namespace KorpInventory.Application.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _repository;
        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.GetByIdAsync(request.Id);
            if (exists == null)
            {
                throw new InvalidOperationException($"Produto com ID {request.Id} não encontrado.");
            }

            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
