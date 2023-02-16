using FluentValidation;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;

namespace P3AddNewFunctionalityDotNetCore.Models.Validations
{
    public class ProductViewModelValidator : AbstractValidator<ProductViewModel>
    {
        public ProductViewModelValidator()
        { 
            //Name
            RuleFor(x => x.Name).NotEmpty().WithMessage("MissingName");

            //Price
            RuleFor(x => x.Price).NotEmpty().WithMessage("MissingPrice");
            Transform(from:  x => x.Price, to: value => double.TryParse(value, out double val) ? (double?) val : null).NotNull().WithMessage("PriceNotANumber");
            Transform(from: x => x.Price, to: value => double.TryParse(value, out double val) ? (double?)val : null).GreaterThan(0).WithMessage("PriceNotGreaterThanZero");

            //Stock
            RuleFor(x => x.Stock).NotEmpty().WithMessage("MissingQuantity");
            Transform(from: x => x.Stock, to: value => int.TryParse(value, out int val) ? (int?)val : null).NotNull().WithMessage("QuantityNotAnInteger");
            Transform(from: x => x.Stock, to: value => int.TryParse(value, out int val) ? (int?)val : null).GreaterThan(0).WithMessage("QuantityNotGreaterThanZero");
        }
    }
}
