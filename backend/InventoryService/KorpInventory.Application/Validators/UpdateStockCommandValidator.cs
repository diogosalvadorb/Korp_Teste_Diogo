using FluentValidation;
using KorpInventory.Application.Commands.UpdateStock;

namespace KorpInventory.Application.Validators
{
    public class UpdateStockCommandValidator : AbstractValidator<UpdateStockCommand>
    {
        public UpdateStockCommandValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage("ID do produto inválido.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");
        }
    }
}
