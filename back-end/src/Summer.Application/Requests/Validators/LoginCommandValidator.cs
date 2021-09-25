using FluentValidation;
using Summer.Application.Requests.Commands;

namespace Summer.Application.Requests.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
        }
    }
}