using FluentValidation;
using InventoryService.ViewModels;

namespace InventoryService.Validators
{
    public class ProductCreateViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductCreateViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Stock).GreaterThan(0);
            RuleFor(x => x.Category).NotEmpty().MaximumLength(100);
        }
    }

    public class ProductUpdateViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductUpdateViewModelValidator()
        {
            RuleFor(x => x.Name).MaximumLength(100);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Category).MaximumLength(100);
        }
    }
}
