using FluentValidation;
using Para.Data.Domain;

namespace Para.Data.Validations
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.InsertDate).NotEmpty();
            RuleFor(x => x.IsActive).NotEmpty();
            RuleFor(x => x.InsertUser).NotEmpty().MaximumLength(50);

            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.IdentityNumber).NotEmpty().MaximumLength(11);
            RuleFor(x => x.Email).NotEmpty().MaximumLength(100).EmailAddress();
            RuleFor(x => x.CustomerNumber).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();

            RuleForEach(x => x.CustomerAddresses).SetValidator(new CustomerAddressValidator());
            RuleForEach(x => x.CustomerPhones).SetValidator(new CustomerPhoneValidator());
            RuleFor(x => x.CustomerDetail).SetValidator(new CustomerDetailValidator());
        }
    }
}