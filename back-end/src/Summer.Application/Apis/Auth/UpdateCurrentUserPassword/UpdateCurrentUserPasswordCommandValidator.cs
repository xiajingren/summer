using FluentValidation;
using Microsoft.Extensions.Options;
using Summer.Domain.Options;

namespace Summer.Application.Apis.Auth.UpdateCurrentUserPassword
{
    public class UpdateCurrentUserPasswordCommandValidator : AbstractValidator<UpdateCurrentUserPasswordCommand>
    {
        public UpdateCurrentUserPasswordCommandValidator(IOptions<UserOptions> userOptions)
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("旧密码不能为空")
                .Must((x, password) => x.NewPassword == password)
                .WithMessage("新旧密码不能一致");
            
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("新密码不能为空")
                .MinimumLength(userOptions.Value.PasswordRequiredLength)
                .WithMessage($"新密码长度至少{userOptions.Value.PasswordRequiredLength}位");

            if (userOptions.Value.PasswordRequireDigit)
            {
                RuleFor(x => x.NewPassword)
                    .Matches("[0-9]")
                    .WithMessage($"新密码必须包含数字");
            }

            if (userOptions.Value.PasswordRequireLowercase)
            {
                RuleFor(x => x.NewPassword)
                    .Matches("[a-z]")
                    .WithMessage($"新密码必须包含小写字母");
            }

            if (userOptions.Value.PasswordRequireUppercase)
            {
                RuleFor(x => x.NewPassword)
                    .Matches("[A-Z]")
                    .WithMessage($"新密码必须包含大写字母");
            }
        }
    }
}