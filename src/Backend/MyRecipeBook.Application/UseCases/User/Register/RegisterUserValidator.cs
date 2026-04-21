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
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_NOT_VALID);
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage(ResourceMessagesException.PASSWORD_EMPTY)
                .MinimumLength(8).WithMessage(ResourceMessagesException.PASSWORD_MINIMUM_LENGHT)
                .MaximumLength(20).WithMessage(ResourceMessagesException.PASSWORD_MAXIMUM_LENGHT);
        }
    }
}
