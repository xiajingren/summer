using FluentValidation;
using Microsoft.Extensions.Options;
using Summer.Domain.Options;

namespace Summer.Application.Apis.Users.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator(IOptions<UserOptions> userOptions)
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("用户名不能为空")
                .MinimumLength(userOptions.Value.UserNameRequiredLength)
                .WithMessage($"用户名长度至少{userOptions.Value.UserNameRequiredLength}位");
        }
    }
}