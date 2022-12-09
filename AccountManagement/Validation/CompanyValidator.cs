using Entities.DTO;
using FluentValidation;

namespace AccountManagement.Validation
{
    public class CompanyValidator : AbstractValidator<CompanyForCreationAndUpdateDto>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.Address).NotNull().NotEmpty().Length(1, 64).WithMessage("Address cannot be null and with size between 1 to 64");
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name cannot be null");
            RuleFor(x => x.Country).NotNull().NotEmpty().WithMessage("Country cannot be null");

        }
    }
}
