using FluentValidation;
using Summer.Application.Requests;

namespace Summer.Application.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(command => command.UserName).NotEmpty().WithMessage("用户名不能为空");
            RuleFor(command => command.Password).NotEmpty().WithMessage("密码不能为空");
        }
    }
}