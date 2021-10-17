using FluentValidation;
using Summer.Application.Requests.Commands;

namespace Summer.Application.Requests.Validators
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(command => command.UserName).NotEmpty().WithMessage("用户名不能为空");
            RuleFor(command => command.Password).NotEmpty().WithMessage("密码不能为空");
        }
    }
}