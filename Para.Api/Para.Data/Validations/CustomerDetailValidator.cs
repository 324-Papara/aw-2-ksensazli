using FluentValidation;
using Para.Data.Domain;

namespace Para.Data.Validations
{
    public class CustomerDetailValidator : AbstractValidator<CustomerDetail>
    {
        public CustomerDetailValidator()
        {
            RuleFor(x => x.InsertDate).NotEmpty();
            RuleFor(x => x.IsActive).NotEmpty();
            RuleFor(x => x.InsertUser).NotEmpty().MaximumLength(50);

            RuleFor(x => x.FatherName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.MotherName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.MontlyIncome).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Occupation).NotEmpty().MaximumLength(50);
            RuleFor(x => x.EducationStatus).NotEmpty().MaximumLength(50);
        }
    }
}