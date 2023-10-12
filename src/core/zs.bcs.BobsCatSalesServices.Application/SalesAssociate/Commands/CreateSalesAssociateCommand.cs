using MediatR;
using zs.bcs.BobsCatSalesServices.Domain.Enumerations;

namespace zs.bcs.BobsCatSalesServices.Application.SalesAssociate.Commands
{
    /// <summary>
    /// A command to create a new sales associate. 
    /// </summary>
    public class CreateSalesAssociateCommand : IRequest<string>
    {
        /// <summary>
        /// The password the user would like to use.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The middle initial of the user.
        /// </summary>
        public string MiddleInitial { get; set; }

        /// <summary>
        /// The prefix of the user's name.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// The suffix of the user's name.
        /// </summary>
        public string Suffix { get; set; }

        /// <summary>
        /// The type of the phone.
        /// </summary>
        public PhoneType PhoneType { get; set; }

        /// <summary>
        /// The country code of the phone number.
        /// </summary>
        public string PhoneCountryCode { get; set; }

        /// <summary>
        /// The area code of the phone number.
        /// </summary>
        public string PhoneAreaCode { get; set; }

        /// <summary>
        /// The phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The type of the email.
        /// </summary>
        public EmailType EmailType { get; set; }

        /// <summary>
        /// The email address.
        /// </summary>
        public string EmailAddress { get; set; }
    }
}
