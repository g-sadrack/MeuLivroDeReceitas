using FluentValidation;
using MyRecipeBook.Communication.Request;
using MyRecipeBook.Exception;

namespace MyRecipeBook.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
            RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage(ResourceMessagesException.EMAIL_NOT_VALID);
            RuleFor(user => user.Password).NotEmpty().MinimumLength(8).MaximumLength(20).WithMessage(ResourceMessagesException.PASSWORD_EMPTY); ;
        }
    }
}
