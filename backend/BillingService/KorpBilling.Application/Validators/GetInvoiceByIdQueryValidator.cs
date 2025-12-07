using FluentValidation;
using KorpBilling.Application.Queries.GetInvoiceById;

namespace KorpBilling.Application.Validators
{
    internal class GetInvoiceByIdQueryValidator : AbstractValidator<GetInvoiceByIdQuery>
    {
        public GetInvoiceByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("ID da nota fiscal inválido.");
        }
    }
}

