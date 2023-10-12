using FluentValidation;
using zs.bcs.BobsCatSalesServices.Application.SalesAssociate.Queries;

namespace zs.bcs.BobsCatSalesServices.Application.SalesAssociate.Validators
{
    public class GetSalesAssociateByEmailValidators : AbstractValidator<GetSalesAssociateByEmailQuery>
    {
        public GetSalesAssociateByEmailValidators()
        {
            // Validation configured to short-circuit on more abstract concerns.
            // Check for email would also cover not empty; but if value was empty, an invalid email
            // error would be sent back when we want the specific issue noted, which is that the value is empty.
            RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress();
        }
    }
}
