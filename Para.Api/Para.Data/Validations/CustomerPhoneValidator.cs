using FluentValidation;
using Para.Data.Domain;

namespace Para.Data.Validations
{
    public class CustomerPhoneValidator : AbstractValidator<CustomerPhone>
    {
        public CustomerPhoneValidator()
        {
            RuleFor(x => x.InsertDate).NotEmpty();
            RuleFor(x => x.IsActive).NotEmpty();
            RuleFor(x => x.InsertUser).NotEmpty().MaximumLength(50);

            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.CountyCode).NotEmpty().MaximumLength(3);
            RuleFor(x => x.Phone).NotEmpty().MaximumLength(10);
            RuleFor(x => x.IsDefault).NotEmpty();
        }
    }
}