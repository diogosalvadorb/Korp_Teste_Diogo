using FluentValidation;
using KorpInventory.Application.Commands.CreateProduct;

namespace KorpInventory.Application.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("O código do produto é obrigatório.")
                .MinimumLength(7).WithMessage("O código deve ter no mínimo 7 caracteres.")
                .MaximumLength(50).WithMessage("O código deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A descrição do produto é obrigatória.")
                .MinimumLength(3).WithMessage("A descrição deve ter no mínimo 3 caracteres.")
                .MaximumLength(200).WithMessage("A descrição deve ter no máximo 200 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("O preço deve ser maior que zero.")
                .LessThan(1000000).WithMessage("O preço deve ser menor que 1.000.000.");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade em estoque não pode ser negativa.");
        }
    }
}
