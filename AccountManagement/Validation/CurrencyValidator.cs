using Entities.DTO;
using FluentValidation;

namespace AccountManagement.Validation
{
    public class CurrencyValidator : AbstractValidator<CurrencyForCreationAndUpdateDto>
    {
        public CurrencyValidator()
        {
            RuleFor(x => x.Code).NotNull().NotEmpty().Length(1, 31).WithMessage("Code cannot be null and with size between 1 to 32");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description cannot be null");
            RuleFor(x => x.ExchangeRate).NotNull().NotEmpty().WithMessage("Exchange Rate cannot be 0");
        }
    }
}
