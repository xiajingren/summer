using FluentValidation;
using Microsoft.Extensions.Options;
using Summer.Domain.Options;

namespace Summer.Application.Apis.User.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator(IOptions<UserOptions> userOptions)
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("用户名不能为空")
                .MinimumLength(userOptions.Value.UserNameRequiredLength)
                .WithMessage($"用户名长度至少{userOptions.Value.UserNameRequiredLength}位");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("密码不能为空")
                .MinimumLength(userOptions.Value.PasswordRequiredLength)
                .WithMessage($"密码长度至少{userOptions.Value.PasswordRequiredLength}位");

            if (userOptions.Value.PasswordRequireDigit)
            {
                RuleFor(x => x.Password)
                    .Matches("[0-9]")
                    .WithMessage($"密码必须包含数字");
            }

            if (userOptions.Value.PasswordRequireLowercase)
            {
                RuleFor(x => x.Password)
                    .Matches("[a-z]")
                    .WithMessage($"密码必须包含小写字母");
            }

            if (userOptions.Value.PasswordRequireUppercase)
            {
                RuleFor(x => x.Password)
                    .Matches("[A-Z]")
                    .WithMessage($"密码必须包含大写字母");
            }
        }
    }
}