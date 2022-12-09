using Entities.DTO;
using FluentValidation;

namespace AccountManagement.Validation
{
    public class CategoryValidation : AbstractValidator<CategoryForCreationAndUpdateDto>
    {
        public CategoryValidation()
        {
            RuleFor(x => x.Code).NotNull().NotEmpty().Length(1, 31).WithMessage("Code cannot be null and with size between 1 to 32");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("PhoneNumber cannot be null");
        }
    }
}
