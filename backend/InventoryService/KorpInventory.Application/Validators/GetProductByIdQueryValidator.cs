using FluentValidation;
using KorpInventory.Application.Queries.GetProductById;

namespace KorpInventory.Application.Validators
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ID do produto inválido.");
        }
    }
}
