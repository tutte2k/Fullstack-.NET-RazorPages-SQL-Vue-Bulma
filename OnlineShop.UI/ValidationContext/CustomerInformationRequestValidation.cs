using FluentValidation;
using OnlineShop.Application.Cart;

namespace OnlineShop.UI.ValidationContext
{
    public class CustomerInformationRequestValidation : AbstractValidator<AddCustomerInformation.Request>
    {
        public CustomerInformationRequestValidation()
        {
            RuleFor(x => x.FirstName).NotNull();
            RuleFor(x => x.LastName).NotNull();
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.PhoneNumber).NotNull().MinimumLength(7);
            RuleFor(x => x.Address1).NotNull();
            RuleFor(x => x.City).NotNull();
            RuleFor(x => x.PostCode).NotNull();
        }
    }
}
