using Entities.DTO;
using FluentValidation;

namespace AccountManagement.Validation
{
    public class BankAccountValidation : AbstractValidator<BankAccountForCreationAndUpdateDto>
    {
        public BankAccountValidation()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 31).WithMessage("Name cannot be null and with size between 1 to 32");
            RuleFor(x => x.Code).NotNull().NotEmpty().Length(1, 16).WithMessage("Code cannot be larger than size 16");
        }
    }
}
