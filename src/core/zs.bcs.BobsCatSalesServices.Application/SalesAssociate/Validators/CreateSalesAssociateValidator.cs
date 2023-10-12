using FluentValidation;
using zs.bcs.BobsCatSalesServices.Application.SalesAssociate.Commands;
using zs.bcs.BobsCatSalesServices.Domain.Enumerations;

namespace zs.bcs.BobsCatSalesServices.Application.SalesAssociate.Validators
{
    /// <summary>
    /// Validates commands to create a new sales associate.
    /// </summary>
    public class CreateSalesAssociateValidator : AbstractValidator<CreateSalesAssociateCommand>
    {
        public CreateSalesAssociateValidator()
        {
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.PhoneType).NotEqual(PhoneType.Default).When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
            RuleFor(x => x.PhoneCountryCode).NotEmpty().When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
            RuleFor(x => x.PhoneAreaCode).NotEmpty().When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
            RuleFor(x => x.EmailType).NotEqual(EmailType.Default);

            // Validation configured to short-circuit on more abstract concerns.
            // Check for email would also cover not empty; but if value was empty, an invalid email
            // error would be sent back when we want the specific issue noted, which is that the value is empty.
            RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress();
        }
    }
}
