using FluentValidation;
using Summer.Application.Requests.Commands;

namespace Summer.Application.Requests.Validators
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("用户名不能为空").MinimumLength(5)
                .WithMessage("用户名长度至少5位");
            RuleFor(x => x.Password).NotEmpty().WithMessage("密码不能为空").MinimumLength(5)
                .WithMessage("密码长度至少6位");
        }
    }
}