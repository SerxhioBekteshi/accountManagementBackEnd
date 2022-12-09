using Entities.DTO;
using FluentValidation;

namespace AccountManagement.Validation
{
    public class EmployeeValidation : AbstractValidator<EmployeeForCreationAndUpdateDto>
    {
        public EmployeeValidation()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 64).WithMessage("Name cannot be null and with size between 1 to 64");
            RuleFor(x => x.Age).NotNull().NotEmpty().WithMessage("Age cannot be null");
            RuleFor(x => x.Position).NotNull().NotEmpty().WithMessage("Position cannot be null");

        }
    }
}
