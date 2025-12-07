using FluentValidation;
using KorpBilling.Application.Commands.PrintInvoice;

namespace KorpBilling.Application.Validators
{
    public class PrintInvoiceCommandValidator : AbstractValidator<PrintInvoiceCommand>
    {
        public PrintInvoiceCommandValidator()
        {
            RuleFor(x => x.InvoiceId)
                .GreaterThan(0).WithMessage("ID da nota fiscal inválido.");
        }
    }
}
