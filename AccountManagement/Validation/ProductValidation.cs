using Entities.DTO;
using FluentValidation;

namespace AccountManagement.Validation
{
    public class ProductValidation : AbstractValidator<ProductForCreationAndUpdateDto>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().Length(1, 31).WithMessage("Name cannot be null and with size between 1 to 32");
            RuleFor(x => x.ShortDescription).NotNull().Length(1, 64).WithMessage("Short Description cannot be larger than size 64");
            RuleFor(x => x.LongDescription).NotNull().Length(1, 256).WithMessage("Long Description cannot be larger thane size 256");
        }
    }
}
